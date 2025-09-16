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
                SqlCommand cmd = new SqlCommand("sp_ListarCategoriasPaginadas", Conexion.conectar);
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
            catch
            {
                lista = new List<L_Categoria>();
                totalRegistros = 0;
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public int Insertar(L_Categoria obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                SqlParameter paramId = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramMensaje);
                cmd.ExecuteNonQuery();
                idGenerado = Convert.ToInt32(paramId.Value);
                Mensaje = paramMensaje.Value.ToString();

            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return idGenerado;
        }

        public bool Editar(L_Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_EditarCategoria", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                SqlParameter paramRespuesta = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(paramRespuesta);
                cmd.Parameters.Add(paramMensaje);
                cmd.ExecuteNonQuery();
                respuesta = Convert.ToInt32(paramRespuesta.Value) == 1;
                Mensaje = paramMensaje.Value.ToString();

            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public bool Eliminar(int id_categoria, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_categoria", id_categoria);
                SqlParameter paramRespuesta = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(paramRespuesta);
                cmd.Parameters.Add(paramMensaje);
                cmd.ExecuteNonQuery();
                respuesta = Convert.ToInt32(paramRespuesta.Value) == 1;
                Mensaje = paramMensaje.Value.ToString();
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
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
            catch
            {
                lista = new List<L_Categoria>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}
