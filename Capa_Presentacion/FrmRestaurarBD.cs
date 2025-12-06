using Capa_Presentacion.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmRestaurarBD : Form
    {
        private readonly string carpetaBackup = @"C:\SQLBackups";
        private readonly string databaseName = "Sega#1";
        public FrmRestaurarBD()
        {
        
            InitializeComponent();

            this.MinimizeBox = false;

            this.MaximizeBox = false;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.ControlBox = true;

            this.StartPosition = FormStartPosition.CenterParent;
            cmbBackups.DropDownStyle = ComboBoxStyle.DropDownList;

            // Asegurar carpeta
            if (!Directory.Exists(carpetaBackup))
                Directory.CreateDirectory(carpetaBackup);

            cmbBackups.DropDownStyle = ComboBoxStyle.DropDownList;
            CargarBackups();
        }
        private void CargarBackups()
        {
            try
            {
                cmbBackups.Items.Clear();

                var archivos = Directory.GetFiles(carpetaBackup, "*.bak")
                    .OrderByDescending(f => File.GetCreationTime(f));

                foreach (var file in archivos)
                    cmbBackups.Items.Add(Path.GetFileName(file));

                if (cmbBackups.Items.Count > 0)
                    cmbBackups.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar backups: " + ex.Message);
            }
        }
        private void FrmRestaurarBD_Load(object sender, EventArgs e)
        {

        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarBackups();
        }

        private  async void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (cmbBackups.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un archivo .bak", "Atención");
                return;
            }

            string archivo = cmbBackups.SelectedItem.ToString();
            string ruta = Path.Combine(carpetaBackup, archivo);

            if (MessageBox.Show(
                $"¿Restaurar la base '{databaseName}' desde:\n\n{ruta}?\n\nEsto reemplazará todos los datos.",
                "Confirmar restauración",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            lblStatus.Text = "Restaurando base, por favor espere...";
            btnRestaurar.Enabled = false;

            bool ok = await System.Threading.Tasks.Task.Run(() => Restaurar(ruta));

            btnRestaurar.Enabled = true;
            lblStatus.Text = "";

            if (ok)
                MessageBox.Show("Base restaurada correctamente.", "Éxito");
            else
                MessageBox.Show("Ocurrió un error al restaurar.", "Error");
        }
        private bool Restaurar(string ruta)
        {
            try
            {
                var cs = new SqlConnectionStringBuilder(Conexion.conexion);
                cs.InitialCatalog = "master";

                using (var cn = new SqlConnection(cs.ToString()))
                {
                    cn.Open();

                    // Poner la BD en modo de un usuario
                    using (var cmd = new SqlCommand(
                        $"ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Restaurar
                    using (var cmd = new SqlCommand(
                        $@"RESTORE DATABASE [{databaseName}]
                           FROM DISK = '{ruta}'
                           WITH REPLACE, STATS = 10;", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }

                    // Devolver modo
                    using (var cmd = new SqlCommand(
                        $"ALTER DATABASE [{databaseName}] SET MULTI_USER;", cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
                return false;
            }
        }
    }
}
        
