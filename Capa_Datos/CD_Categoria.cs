using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

namespace Capa_Datos
{
    public class CD_Categoria
    {
        public List<CE_Categoria> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<CE_Categoria> lista = new List<CE_Categoria>();
            totalRegistros = 0;
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ListarCategoriasPaginadas", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                    cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                    SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outParam);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CE_Categoria()
                            {
                                id_categoria = Convert.ToInt32(dr["id_categoria"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                    totalRegistros = Convert.ToInt32(outParam.Value);
                }
            }
            catch
            {
                lista = new List<CE_Categoria>();
                totalRegistros = 0;
            }
            return lista;
        }

        public int Insertar(CE_Categoria obj, out string Mensaje)
        {
            int idGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    SqlParameter paramId = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramId);
                    cmd.Parameters.Add(paramMensaje);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    idGenerado = Convert.ToInt32(paramId.Value);
                    Mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idGenerado = 0;
                Mensaje = ex.Message;
            }
            return idGenerado;
        }

        public bool Editar(CE_Categoria obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarCategoria", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    SqlParameter paramRespuesta = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramRespuesta);
                    cmd.Parameters.Add(paramMensaje);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToInt32(paramRespuesta.Value) == 1;
                    Mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public bool Eliminar(int id_categoria, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);
                    SqlParameter paramRespuesta = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramRespuesta);
                    cmd.Parameters.Add(paramMensaje);
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToInt32(paramRespuesta.Value) == 1;
                    Mensaje = paramMensaje.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public List<CE_Categoria> Buscar(string textoBusqueda)
        {
            List<CE_Categoria> lista = new List<CE_Categoria>();
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_BuscarCategorias", oConexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CE_Categoria()
                            {
                                id_categoria = Convert.ToInt32(dr["id_categoria"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<CE_Categoria>();
            }
            return lista;
        }
    }
}