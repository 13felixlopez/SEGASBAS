using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Capa_Presentacion.Datos
{
    public class D_Cargo
    {
        public List<L_Cargo> ObtenerCargos()
        {
            List<L_Cargo> lista = new List<L_Cargo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT Id_cargo, Nombre FROM Cargo ORDER BY Nombre ASC", Conexion.conectar);
                cmd.CommandType = CommandType.Text;


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Cargo
                        {
                            Id_cargo = Convert.ToInt32(dr["Id_cargo"]),
                            Nombre = dr["Nombre"].ToString()
                        });

                    }
                }
            }
            catch (Exception)
            {
                lista = new List<L_Cargo>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public string InsertarCargo(L_Cargo cargo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_InsertarCargo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@nombre", cargo.Nombre);
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

        public List<L_Cargo> ObtenerCargosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Cargo> listaCargos = new List<L_Cargo>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ObtenerCargosConPaginado", Conexion.conectar);
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
                    listaCargos.Add(new L_Cargo()
                    {
                        Id_cargo = Convert.ToInt32(reader["id_cargo"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }

            }
            catch (Exception ex) { MessageBox.Show("Error al Obtener Cargos " + ex.Message, "Error en Obtener Cargos", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally
            {
                Conexion.cerrar();
            }
            return listaCargos;
        }

        public string ActualizarCargo(L_Cargo cargo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ActualizarCargo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_cargo", cargo.Id_cargo);
                comando.Parameters.AddWithValue("@nombre", cargo.Nombre);
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

        public string EliminarCargo(int idCargo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_EliminarCargo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_cargo", idCargo);

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

        public List<L_Cargo> BuscarCargos(string terminoBusqueda)
        {
            List<L_Cargo> listaCargos = new List<L_Cargo>();
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_BuscarCargos", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listaCargos.Add(new L_Cargo()
                    {
                        Id_cargo = Convert.ToInt32(reader["id_cargo"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al Buscar Cargos " + ex.Message, "Error en BuscarCrgos", MessageBoxButton.OK, MessageBoxImage.Error); }
            finally
            {
                Conexion.cerrar();
            }
            return listaCargos;
        }
    }
}
