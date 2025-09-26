using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Proveedores
    {
    
        public List<L_Proveedor> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_Proveedor> lista = new List<L_Proveedor>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ListarProveedoresPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Proveedor()
                        {
                            id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                            numero_registro = dr["numero_registro"].ToString(),
                            razon_social = dr["razon_social"].ToString(),
                            numero_ruc = dr["numero_ruc"].ToString(),
                            correo_electronico = dr["correo_electronico"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            visitador = dr["visitador"].ToString(),
                            observacion = dr["observacion"].ToString(),
                            fecha_registro = Convert.ToDateTime(dr["fecha_registro"]),
                            es_producto = Convert.ToBoolean(dr["es_producto"]),
                            es_servicio = Convert.ToBoolean(dr["es_servicio"])
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los proveedores paginados: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

      
        public string Insertar(L_Proveedor obj)
        {
            string mensaje = "";
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_InsertarProveedor", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@numero_registro", obj.numero_registro);
                    cmd.Parameters.AddWithValue("@razon_social", obj.razon_social);
                    cmd.Parameters.AddWithValue("@numero_ruc", obj.numero_ruc);
                    cmd.Parameters.AddWithValue("@correo_electronico", obj.correo_electronico);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@visitador", obj.visitador);
                    cmd.Parameters.AddWithValue("@observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("@fecha_registro", obj.fecha_registro);
                    cmd.Parameters.AddWithValue("@es_producto", obj.es_producto);
                    cmd.Parameters.AddWithValue("@es_servicio", obj.es_servicio);

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
                throw new Exception("Error en la base de datos al insertar el proveedor: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Editar(L_Proveedor obj)
        {
            string mensaje = "";
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_EditarProveedor", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", obj.id_proveedor);
                    cmd.Parameters.AddWithValue("@numero_registro", obj.numero_registro);
                    cmd.Parameters.AddWithValue("@razon_social", obj.razon_social);
                    cmd.Parameters.AddWithValue("@numero_ruc", obj.numero_ruc);
                    cmd.Parameters.AddWithValue("@correo_electronico", obj.correo_electronico);
                    cmd.Parameters.AddWithValue("@telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("@visitador", obj.visitador);
                    cmd.Parameters.AddWithValue("@observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("@fecha_registro", obj.fecha_registro);
                    cmd.Parameters.AddWithValue("@es_producto", obj.es_producto);
                    cmd.Parameters.AddWithValue("@es_servicio", obj.es_servicio);

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
                throw new Exception("Error en la base de datos al editar el proveedor: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Eliminar(int id_proveedor)
        {
            string mensaje = "";
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProveedor", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", id_proveedor);

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
                throw new Exception("Error en la base de datos al eliminar el proveedor: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public List<L_Proveedor> Buscar(string textoBusqueda)
        {
            List<L_Proveedor> lista = new List<L_Proveedor>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarProveedores", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Proveedor()
                        {
                            id_proveedor = Convert.ToInt32(dr["id_proveedor"]),
                            numero_registro = dr["numero_registro"].ToString(),
                            razon_social = dr["razon_social"].ToString(),
                            numero_ruc = dr["numero_ruc"].ToString(),
                            correo_electronico = dr["correo_electronico"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            visitador = dr["visitador"].ToString(),
                            observacion = dr["observacion"].ToString(),
                            fecha_registro = Convert.ToDateTime(dr["fecha_registro"]),
                            es_producto = Convert.ToBoolean(dr["es_producto"]),
                            es_servicio = Convert.ToBoolean(dr["es_servicio"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar proveedores: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public string ObtenerUltimoNumeroRegistro()
        {
            string ultimoNumero = "0"; 
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerUltimoNumeroRegistro", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                object resultado = cmd.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    ultimoNumero = resultado.ToString();
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error al obtener el último número de registro: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return ultimoNumero;
        }

    }
}