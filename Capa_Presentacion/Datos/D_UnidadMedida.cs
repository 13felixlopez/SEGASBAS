using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_UnidadMedida
    {
        public List<L_UnidadMedida> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_UnidadMedida> lista = new List<L_UnidadMedida>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ListarUnidadesPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_UnidadMedida()
                        {
                            id_unidad = Convert.ToInt32(dr["id_unidad"]),
                            nombre = dr["nombre"].ToString(),
                            // Línea corregida para leer la abreviatura
                            abreviatura = dr["abreviatura"].ToString()
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las unidades paginadas: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public List<L_UnidadMedida> Buscar(string textoBusqueda)
        {
            List<L_UnidadMedida> lista = new List<L_UnidadMedida>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarUnidades", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_UnidadMedida()
                        {
                            id_unidad = Convert.ToInt32(dr["id_unidad"]),
                            nombre = dr["nombre"].ToString(),
                          
                            abreviatura = dr["abreviatura"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar unidades de medida: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public string Insertar(L_UnidadMedida obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarUnidadMedida", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                 
                    cmd.Parameters.AddWithValue("@abreviatura", obj.abreviatura);
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
                throw new Exception("Error en la base de datos al insertar la unidad de medida: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Editar(L_UnidadMedida obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarUnidadMedida", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_unidad", obj.id_unidad);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);

     
                    cmd.Parameters.AddWithValue("@abreviatura", obj.abreviatura);

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
                throw new Exception("Error en la base de datos al editar la unidad de medida: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Eliminar(int id_unidad)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarUnidadMedida", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_unidad", id_unidad);
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
                throw new Exception("Error en la base de datos al eliminar la unidad de medida: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

    }
}