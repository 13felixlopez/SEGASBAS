using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Capa_Entidad; 

namespace Capa_Datos
{
    public class CD_Cargo
    {
        public List<Capa_Entidad.CE_Cargo> ObtenerCargos()
        {
            List<Capa_Entidad.CE_Cargo> lista = new List<Capa_Entidad.CE_Cargo>();
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id_cargo, Nombre FROM Cargo ORDER BY Nombre ASC", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Capa_Entidad.CE_Cargo()
                            {
                                Id_cargo = Convert.ToInt32(dr["Id_cargo"]),
                                Nombre = dr["Nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Capa_Entidad.CE_Cargo>();
            }
            return lista;
        }
        public string InsertarCargo(CE_Cargo cargo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_InsertarCargo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombre", cargo.Nombre);
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

        public List<CE_Cargo> ObtenerCargosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<CE_Cargo> listaCargos = new List<CE_Cargo>();
            totalRegistros = 0;
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ObtenerCargosConPaginado", conexion);
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
                        listaCargos.Add(new CE_Cargo()
                        {
                            Id_cargo = Convert.ToInt32(reader["id_cargo"]),
                            Nombre = reader["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaCargos;
        }

        public string ActualizarCargo(CE_Cargo cargo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ActualizarCargo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_cargo", cargo.Id_cargo);
                    comando.Parameters.AddWithValue("@nombre", cargo.Nombre);
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

        public string EliminarCargo(int idCargo)
        {
            string respuesta = "";
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_EliminarCargo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_cargo", idCargo);
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

        public List<CE_Cargo> BuscarCargos(string terminoBusqueda)
        {
            List<CE_Cargo> listaCargos = new List<CE_Cargo>();
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_BuscarCargos", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        listaCargos.Add(new CE_Cargo()
                        {
                            Id_cargo = Convert.ToInt32(reader["id_cargo"]),
                            Nombre = reader["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception) { }
            return listaCargos;
        }
    }
}