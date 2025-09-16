using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class CD_Actividad
    {
        public List<KeyValuePair<int, string>> ObtenerActividades()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SELECT id_actividad, descripcion FROM Actividad ORDER BY descripcion ASC", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

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
            }
            catch (Exception)
            {
                lista = new List<KeyValuePair<int, string>>();
            }
            return lista;
        }
        public string InsertarActividad(CE_Actividad actividad)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_InsertarActividad", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    respuesta = "OK";
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }

        public List<CE_Actividad> ObtenerActividadesConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<CE_Actividad> listaActividades = new List<CE_Actividad>();
            totalRegistros = 0;
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ObtenerActividadesConPaginado", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@pagina", pagina);
                    comando.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();

                    if (reader.Read())
                    {
                        totalRegistros = Convert.ToInt32(reader["TotalRegistros"]);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        listaActividades.Add(new CE_Actividad()
                        {
                            Id_actividad = Convert.ToInt32(reader["id_actividad"]),
                            Descripcion = reader["descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaActividades;
        }

        public string ActualizarActividad(CE_Actividad actividad)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ActualizarActividad", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_actividad", actividad.Id_actividad);
                    comando.Parameters.AddWithValue("@descripcion", actividad.Descripcion);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    respuesta = "OK";
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }

        public string EliminarActividad(int idActividad)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_EliminarActividad", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_actividad", idActividad);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    respuesta = "OK";
                }
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            return respuesta;
        }

        public List<CE_Actividad> BuscarActividades(string terminoBusqueda)
        {
            List<CE_Actividad> listaActividades = new List<CE_Actividad>();
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_BuscarActividades", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        listaActividades.Add(new CE_Actividad()
                        {
                            Id_actividad = Convert.ToInt32(reader["id_actividad"]),
                            Descripcion = reader["descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaActividades;
        }
    }
}