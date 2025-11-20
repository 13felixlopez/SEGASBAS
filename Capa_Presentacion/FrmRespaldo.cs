using Capa_Presentacion.Datos;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Capa_Presentacion
{
    public partial class FrmRespaldo : Form
    {
        private readonly string databaseName = "Sega#1";
        private readonly string connectionStringName = "Conexión";
        public FrmRespaldo()
        {
            InitializeComponent();
            textBox1.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Backups");
            Directory.CreateDirectory(textBox1.Text);
            progressBar1.Visible = false;
            label1.Text = "";
        }

        private void FrmRespaldo_Load(object sender, EventArgs e)
        {

        }

        private void Btexaminar_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Seleccione la carpeta para guardar backups";
                dlg.SelectedPath = textBox1.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = dlg.SelectedPath;
                    Directory.CreateDirectory(textBox1.Text);
                }
            }
        }

        private async void btbakups_Click(object sender, EventArgs e)
        {
            btbakups.Enabled = false;
            Btexaminar.Enabled = false;
            label1.Text = "Realizando respaldo...";
            progressBar1.Visible = true;


            try
            {
                string folder = textBox1.Text;
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);


                string fileName = $"{databaseName}_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string fullPath = Path.Combine(folder, fileName);


                bool ok = await Task.Run(() => EjecutarBackup(fullPath));


                if (ok)
                {
                    label1.Text = "Respaldo completado ✔️";
                    MessageBox.Show($"Respaldo creado:\n{fullPath}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    label1.Text = "Error durante el respaldo ❌";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear backup: " + ex.Message);
                label1.Text = "Error ❌";
            }
            finally
            {
                progressBar1.Visible = false;
                btbakups.Enabled = true;
                Btexaminar.Enabled = true;
            }
        }
         private bool EjecutarBackup(string destino)
         {
            try
            {
                // Usar tu cadena fija
                string connStr = Conexion.conexion;

                string sql = $@"
            BACKUP DATABASE [Sega#1]
            TO DISK = N'{destino.Replace("'", "''")}'
            WITH INIT, NAME = N'Respaldo Sega', STATS = 10;
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
                MessageBox.Show("Error al ejecutar backup: " + ex.Message + "\n\n" + ex.ToString());
                return false;
            }

        }
    }
}
