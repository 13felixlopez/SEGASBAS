using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public static class ConfiguracionVisual
    {
        public static bool ModoOscuro { get; private set; } = false;

        private static string GetConnectionString()
        {
            return Capa_Presentacion.Datos.Conexion.conexion;
        }

        public static void CargarModoDesdeBD()
        {
            try
            {
                using (var con = new SqlConnection(GetConnectionString()))
                {
                    con.Open();
                    string sql = "SELECT TOP 1 ModoOscuro FROM ConfiguracionGeneral";

                    using (var cmd = new SqlCommand(sql, con))
                    {
                        object result = cmd.ExecuteScalar();
                        ModoOscuro = result != null && Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                ModoOscuro = false;
                MessageBox.Show("Advertencia: No se pudo cargar la configuración visual.\n" + ex.Message,
                    "Modo Oscuro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void GuardarModo(bool oscuro)
        {
            try
            {
                using (var con = new SqlConnection(GetConnectionString()))
                {
                    con.Open();

                    int count = 0;
                    using (var cmdCount = new SqlCommand("SELECT COUNT(*) FROM ConfiguracionGeneral", con))
                    {
                        count = Convert.ToInt32(cmdCount.ExecuteScalar());
                    }

                    if (count == 0)
                    {
                        using (var cmdInsert = new SqlCommand("INSERT INTO ConfiguracionGeneral (ModoOscuro) VALUES (@modo)", con))
                        {
                            cmdInsert.Parameters.Add("@modo", SqlDbType.Bit).Value = oscuro;
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var cmdUpdate = new SqlCommand("UPDATE ConfiguracionGeneral SET ModoOscuro = @modo", con))
                        {
                            cmdUpdate.Parameters.Add("@modo", SqlDbType.Bit).Value = oscuro;
                            cmdUpdate.ExecuteNonQuery();
                        }
                    }
                }

                ModoOscuro = oscuro;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el modo oscuro:\n" + ex.Message,
                    "Modo Oscuro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
