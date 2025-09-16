using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class CD_Ciclo
    {
        // Métodos para la ejecución de procedimientos almacenados
        public string InsertarCiclo(CE_Ciclo ciclo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_InsertarCiclo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@descripcion", ciclo.Descripcion);
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

        public List<CE_Ciclo> ObtenerCiclosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<CE_Ciclo> listaCiclos = new List<CE_Ciclo>();
            totalRegistros = 0;
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ObtenerCiclosConPaginado", conexion);
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
                        listaCiclos.Add(new CE_Ciclo()
                        {
                            Id_ciclo = Convert.ToInt32(reader["id_ciclo"]),
                            Descripcion = reader["descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaCiclos;
        }

        public string ActualizarCiclo(CE_Ciclo ciclo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ActualizarCiclo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_ciclo", ciclo.Id_ciclo);
                    comando.Parameters.AddWithValue("@descripcion", ciclo.Descripcion);
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

        public string EliminarCiclo(int idCiclo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_EliminarCiclo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_ciclo", idCiclo);
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

        public List<CE_Ciclo> BuscarCiclos(string terminoBusqueda)
        {
            List<CE_Ciclo> listaCiclos = new List<CE_Ciclo>();
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_BuscarCiclos", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        listaCiclos.Add(new CE_Ciclo()
                        {
                            Id_ciclo = Convert.ToInt32(reader["id_ciclo"]),
                            Descripcion = reader["descripcion"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaCiclos;
        }
    }
}