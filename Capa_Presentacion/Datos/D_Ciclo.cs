using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Capa_Presentacion.Datos
{
    public class D_Ciclo
    {
        // Métodos para la ejecución de procedimientos almacenados
        public string InsertarCiclo(L_Ciclo ciclo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_InsertarCiclo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@descripcion", ciclo.Descripcion);
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

        public List<L_Ciclo> ObtenerCiclosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Ciclo> listaCiclos = new List<L_Ciclo>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ObtenerCiclosConPaginado", Conexion.conectar);
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
                    listaCiclos.Add(new L_Ciclo()
                    {
                        Id_ciclo = Convert.ToInt32(reader["id_ciclo"]),
                        Descripcion = reader["descripcion"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los ciclos con paginado. " + ex.Message, "Error en ObtenerCiclos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return listaCiclos;
        }

        public string ActualizarCiclo(L_Ciclo ciclo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ActualizarCiclo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_ciclo", ciclo.Id_ciclo);
                comando.Parameters.AddWithValue("@descripcion", ciclo.Descripcion);
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

        public string EliminarCiclo(int idCiclo)
        {
            string respuesta = "";
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_EliminarCiclo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@id_ciclo", idCiclo);
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

        public List<L_Ciclo> BuscarCiclos(string terminoBusqueda)
        {
            List<L_Ciclo> listaCiclos = new List<L_Ciclo>();
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_BuscarCiclos", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    listaCiclos.Add(new L_Ciclo()
                    {
                        Id_ciclo = Convert.ToInt32(reader["id_ciclo"]),
                        Descripcion = reader["descripcion"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Buscar Ciclos " + ex.Message, "Error en BuscarCiclos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return listaCiclos;
        }
    }
}
