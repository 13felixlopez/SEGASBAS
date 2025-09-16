using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Datos
{
    public class D_Actividad
    {
        public List<KeyValuePair<int, string>> ObtenerActividades()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_actividad, descripcion FROM Actividad ORDER BY descripcion ASC", Conexion.conectar);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        lista.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(dr["id_actividad"]),
                            dr["descripcion"].ToString()
                        ));
                    }
                }

            }
            catch (Exception)
            {
                lista = new List<KeyValuePair<int, string>>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public string InsertarActividad(L_Actividad actividad)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_InsertarActividad", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                comando.ExecuteNonQuery();
                respuesta = "OK";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public List<L_Actividad> ObtenerActividadesConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Actividad> listaActividades = new List<L_Actividad>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ObtenerActividadesConPaginado", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@pagina", pagina);
                comando.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);

                SqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    totalRegistros = Convert.ToInt32(reader["TotalRegistros"]);
                }
                reader.NextResult();

                while (reader.Read())
                {
                    listaActividades.Add(new L_Actividad()
                    {
                        Id_actividad = Convert.ToInt32(reader["id_actividad"]),
                        Descripcion = reader["descripcion"].ToString()
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Obtener Actividades " + ex.Message, "Error al cargar datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return listaActividades;
        }

        public string ActualizarActividad(L_Actividad actividad)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ActualizarActividad", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_actividad", actividad.Id_actividad);
                comando.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                comando.ExecuteNonQuery();
                respuesta = "OK";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public string EliminarActividad(int idActividad)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_EliminarActividad", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_actividad", idActividad);

                comando.ExecuteNonQuery();
                respuesta = "OK";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public List<L_Actividad> BuscarActividades(string terminoBusqueda)
        {
            List<L_Actividad> listaActividades = new List<L_Actividad>();
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_BuscarActividades", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listaActividades.Add(new L_Actividad()
                    {
                        Id_actividad = Convert.ToInt32(reader["id_actividad"]),
                        Descripcion = reader["descripcion"].ToString()
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar Actividades " + ex.Message, "Error en Buscar Actividades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return listaActividades;
        }
    }
}