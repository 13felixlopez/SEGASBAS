using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Marca
    {
        public List<L_Marca> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_Marca> lista = new List<L_Marca>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ListarMarcasPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Marca()
                        {
                            id_marca = Convert.ToInt32(dr["id_marca"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las marcas paginadas: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public string Insertar(L_Marca obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarMarca", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    // ¡AQUÍ ESTÁ LA CORRECCIÓN! Agregamos el parámetro @resultado que faltaba.
                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);
                    cmd.ExecuteNonQuery();
                    mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos al insertar la marca: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Editar(L_Marca obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarMarca", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_marca", obj.id_marca);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    // Y también aquí
                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);
                    cmd.ExecuteNonQuery();
                    mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos al editar la marca: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Eliminar(int id_marca)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarMarca", Conexion.conectar))
                {
                    // ¡AQUÍ ESTÁ LA CORRECCIÓN! Debes especificar que el tipo de comando es un Stored Procedure.
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_marca", id_marca);
                  
                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos al eliminar la marca: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }


        public List<L_Marca> Buscar(string textoBusqueda)
        {
            List<L_Marca> lista = new List<L_Marca>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarMarcas", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Marca()
                        {
                            id_marca = Convert.ToInt32(dr["id_marca"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar marcas: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}