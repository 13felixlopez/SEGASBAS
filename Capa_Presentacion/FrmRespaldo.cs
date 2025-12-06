using Capa_Presentacion.Datos;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Capa_Presentacion
{
    public partial class FrmRespaldo : Form
    {
        private readonly string databaseName = "Sega#1";
        private readonly string carpetaBackup = @"C:\SQLBackups"; 
        public FrmRespaldo()
        {
            InitializeComponent();
            textBox1.Text = carpetaBackup;
            if (!Directory.Exists(carpetaBackup))
                Directory.CreateDirectory(carpetaBackup);
            progressBar1.Visible = false;
            label1.Text = "";


            this.MinimizeBox = false;

            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.ControlBox = true;

            this.StartPosition = FormStartPosition.CenterParent;
            textBox1.ReadOnly = true;
        }

        private void FrmRespaldo_Load(object sender, EventArgs e)
        {

        }

        private void Btexaminar_Click(object sender, EventArgs e)
        {
          
        }
        private string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
        private async void btbakups_Click(object sender, EventArgs e)
        {
            btbakups.Enabled = false;
            label1.Text = "Realizando respaldo...";
            progressBar1.Visible = true;

            try
            {
                // Asegurar carpeta
                if (!Directory.Exists(carpetaBackup))
                    Directory.CreateDirectory(carpetaBackup);

                // Crear nombre del archivo
                string fileName = $"{SanitizeFileName(databaseName)}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string fullPath = Path.Combine(carpetaBackup, fileName);

                // Ejecutar backup en un hilo secundario
                bool ok = await Task.Run(() => EjecutarBackup(fullPath));

                if (ok)
                {
                    label1.Text = "Respaldo completado ✔️";
                    MessageBox.Show($"Respaldo creado en:\n{fullPath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir carpeta
                    try
                    {
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = carpetaBackup,
                            UseShellExecute = true
                        });
                    }
                    catch { }
                }
                else
                {
                    label1.Text = "Error durante el respaldo ❌";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear backup:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label1.Text = "Error ❌";
            }
            finally
            {
                progressBar1.Visible = false;
                btbakups.Enabled = true;
            }
        }
        private bool EjecutarBackup(string destino)
        {
            try
            {
                string connStr = Conexion.conexion;

                string sql = $@"
                    BACKUP DATABASE [{databaseName}]
                    TO DISK = N'{destino.Replace("'", "''")}'
                    WITH INIT, NAME = N'Respaldo {databaseName}', STATS = 10;
                    ";

                using (SqlConnection cn = new SqlConnection(connStr))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en BACKUP:\n" + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
