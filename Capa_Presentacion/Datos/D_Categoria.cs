using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Categoria
    {
        public List<L_Categoria> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_Categoria> lista = new List<L_Categoria>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                // ¡AQUÍ ESTÁ LA CORRECCIÓN! El nombre del SP ahora coincide con el que me diste.
                SqlCommand cmd = new SqlCommand("sp_ListarCategorias", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Categoria()
                        {
                            id_categoria = Convert.ToInt32(dr["id_categoria"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las categorías paginadas: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public string Insertar(L_Categoria obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    // Agregamos el parámetro @resultado que faltaba
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
                throw new Exception("Error en la base de datos al insertar la categoría: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Editar(L_Categoria obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarCategoria", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    // Aquí agregamos los parámetros de salida que tu SP espera
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
                throw new Exception("Error en la base de datos al editar la categoría: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Eliminar(int id_categoria)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);
                    // Aquí agregamos los parámetros de salida que tu SP espera
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
                throw new Exception("Error en la base de datos al eliminar la categoría: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }
        public List<L_Categoria> Buscar(string textoBusqueda)
        {
            List<L_Categoria> lista = new List<L_Categoria>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarCategorias", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Categoria()
                        {
                            id_categoria = Convert.ToInt32(dr["id_categoria"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar categorías: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}