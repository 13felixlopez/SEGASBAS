using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Capa_Presentacion.Logica;

namespace Capa_Presentacion.Datos
{
    public class D_Producto
    {
        public DataTable ObtenerProductos()
        {
            DataTable dt = new DataTable();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarProductos", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return dt;
        }

        public string Insertar(L_Producto obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                
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
                throw new Exception("Error en la base de datos al insertar el producto: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Editar(L_Producto obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarProducto", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", obj.id_producto);
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    
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
                throw new Exception("Error en la base de datos al editar el producto: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }

        public string Eliminar(int id_producto)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProducto", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
           
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
                throw new Exception("Error en la base de datos al eliminar el producto: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }
        public List<L_Producto> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_Producto> lista = new List<L_Producto>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ListarProductosPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paginaActual", paginaActual);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter outParam = new SqlParameter("@totalRegistros", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outParam);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Producto()
                        {
                            id_producto = Convert.ToInt32(dr["id_producto"]),
                            nombre = dr["nombre"].ToString()
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos paginados: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public DataTable Buscar(string textoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarProductos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
            return dt;
        }
    }
}