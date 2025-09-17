using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Capa_Presentacion.Datos
{
    public class DUsuarios
    {
        public bool InsertarUsuario(LUsuarios parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("InsertarUsuario", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NombreA", parametros.NombreA);
                cmd.Parameters.AddWithValue("@Pass", parametros.Pass);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.Parameters.AddWithValue("@Correo", parametros.Correo);
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("@Roll", parametros.Roll);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void MostrarUsuarios(ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarUsuarios", Conexion.conectar);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void ObtenerIdUser(ref int IdUser, string login)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("ObtenerIdUser", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", login);
                IdUser = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { Conexion.cerrar(); }
        }
        public void VerificarUsuario(ref string Indicador, string db)
        {
            try
            {
                // Validar el nombre de la base de datos
                if (!Regex.IsMatch(db, @"^[\w]+$"))
                {
                    Indicador = "Nombre de base de datos inválido";
                    return;
                }

                Conexion.abrir();

                // Consulta segura usando QUOTENAME para escapar el nombre
                string query = $@"
            SELECT TOP 1 TABLE_NAME 
            FROM {EscapeDatabaseName(db)}.INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_TYPE = 'BASE TABLE'";

                SqlCommand cmd = new SqlCommand(query, Conexion.conectar);

                // Si devuelve algún resultado, hay al menos una tabla
                var resultado = cmd.ExecuteScalar();
                Conexion.cerrar();

                Indicador = (resultado != null)
                    ? "Correcto. Primera tabla: " + resultado.ToString()
                    : "No se encontraron tablas";
            }
            catch (Exception ex)
            {
                Indicador = "Error de conexión: " + ex.Message;
            }
        }

        // Función para escapar nombres de bases de datos de forma segura
        private string EscapeDatabaseName(string dbName)
        {
            // Usamos QUOTENAME para prevenir inyección SQL y manejar nombres especiales
            return $"[{dbName.Replace("]", "]]")}]";
        }
        public DataTable D_Usuarios(LUsuarios parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("Login", Conexion.conectar)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("@Pass", parametros.Pass);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                throw new Exception("Datos incorrectos");
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public bool EliminarUser(int IdUser)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("EliminarUser", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUser", IdUser);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un erro al eliminar el Ususario, " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public bool EditarUser(LUsuarios parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("EditarUser", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdUser", parametros.IdUser);
                cmd.Parameters.AddWithValue("@NombreApellidos", parametros.NombreA);
                cmd.Parameters.AddWithValue("@Correo", parametros.Correo);
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("@Pass", parametros.Pass);
                cmd.Parameters.AddWithValue("@Cargo", parametros.Roll);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void MostrarUsuarioLogueado(DataTable dt, int Iduser)
        {
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostrarUsuarioLogueado", Conexion.conectar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@IdUser", Iduser);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los datos del usuario " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public List<string> BuscarRoll(string filtro)
        {
            var cargos = new List<string>();

            try
            {
                Conexion.abrir();

                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Roll FROM Usuarios WHERE Roll LIKE @q + '%'", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@q", filtro);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                cargos.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar roll: " + ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }

            return cargos;
        }
    }
}
