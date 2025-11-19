using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_Presentacion.Datos
{
    public class DRecuperacion
    {
        public int ObtenerIdPorCorreo(string correo)
        {
            int id = 0;
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerIdUserPorCorreo", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Correo", correo);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        id = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener ID del usuario: " + ex.Message);
            }
            finally { Conexion.cerrar(); }

            return id;
        }

        public bool GuardarCodigo(int idUser, string codigo, DateTime fechaExpira)
        {
            try
            {
                Conexion.abrir();

                SqlCommand cmd = new SqlCommand("SP_GuardarCodigoRecuperacion", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdUser", idUser);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.Add("@FechaExpiracion", SqlDbType.DateTime).Value = fechaExpira;

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar código: " + ex.Message);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public bool VerificarCodigo(int idUser, string codigo)
        {
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("SP_VerificarCodigo", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar código: " + ex.Message);
                return false;
            }
            finally { Conexion.cerrar(); }
        }

        public bool MarcarCodigoUsado(int idUser, string codigo)
        {
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("SP_MarcarCodigoUsado", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al marcar código como usado: " + ex.Message);
                return false;
            }
            finally { Conexion.cerrar(); }
        }

        public bool CambiarContrasena(int idUser, string passEncriptada)
        {
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("SP_CambiarContrasena", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@NuevaPass", passEncriptada);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar contraseña: " + ex.Message);
                return false;
            }
            finally { Conexion.cerrar(); }
        }
    }
}
