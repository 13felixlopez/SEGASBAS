using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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
            finally { Conexion.cerrar(); }
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

                    mensaje = paramMensaje.Value?.ToString() ?? "Sin mensaje";
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

        public string InsertarExt(L_Producto obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto_Ext", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(obj.Descripcion) ? (object)DBNull.Value : obj.Descripcion);
                    cmd.Parameters.AddWithValue("@id_unidad", obj.id_unidad == 0 ? (object)DBNull.Value : obj.id_unidad);
                    cmd.Parameters.AddWithValue("@id_marca", obj.id_marca == 0 ? (object)DBNull.Value : obj.id_marca);
                    cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria == 0 ? (object)DBNull.Value : obj.id_categoria);
                    cmd.Parameters.AddWithValue("@StockMinimo", obj.StockMinimo);
                    cmd.Parameters.AddWithValue("@CostoPromedio", obj.CostoPromedio);
                    cmd.Parameters.AddWithValue("@UltimoCostoFactura", obj.UltimoCostoFactura);

                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = paramMensaje.Value?.ToString() ?? "";
                }
            }
            catch (Exception ex) { throw new Exception("Error en la base de datos al insertar el producto (ext): " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
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

                    mensaje = paramMensaje.Value?.ToString() ?? "";
                }
            }
            catch (Exception ex) { throw new Exception("Error en la base de datos al editar el producto: " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
            return mensaje;
        }

        public string EditarExt(L_Producto obj)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_EditarProducto_Ext", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", obj.id_producto);
                    // Enviamos parámetros; si algo no aplica, enviamos DBNull.Value para no sobreescribir
                    cmd.Parameters.AddWithValue("@nombre", string.IsNullOrWhiteSpace(obj.nombre) ? (object)DBNull.Value : obj.nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", string.IsNullOrWhiteSpace(obj.Descripcion) ? (object)DBNull.Value : obj.Descripcion);
                    cmd.Parameters.AddWithValue("@id_unidad", obj.id_unidad == 0 ? (object)DBNull.Value : obj.id_unidad);
                    cmd.Parameters.AddWithValue("@id_marca", obj.id_marca == 0 ? (object)DBNull.Value : obj.id_marca);
                    cmd.Parameters.AddWithValue("@id_categoria", obj.id_categoria == 0 ? (object)DBNull.Value : obj.id_categoria);
                    cmd.Parameters.AddWithValue("@StockMinimo", obj.StockMinimo == 0 ? (object)DBNull.Value : obj.StockMinimo);
                    cmd.Parameters.AddWithValue("@CostoPromedio", obj.CostoPromedio == 0 ? (object)DBNull.Value : obj.CostoPromedio);
                    cmd.Parameters.AddWithValue("@UltimoCostoFactura", obj.UltimoCostoFactura == 0 ? (object)DBNull.Value : obj.UltimoCostoFactura);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado ? 1 : 0);

                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = paramMensaje.Value?.ToString() ?? "";
                }
            }
            catch (Exception ex) { throw new Exception("Error en la base de datos al editar el producto (ext): " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
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

                    mensaje = paramMensaje.Value?.ToString() ?? "";
                }
            }
            catch (Exception ex) { throw new Exception("Error en la base de datos al eliminar el producto: " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
            return mensaje;
        }

        public List<L_Producto> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            List<L_Producto> lista = new List<L_Producto>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ListarProductosPaginado", Conexion.conectar))
                {
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
                                nombre = dr["nombre"].ToString(),
                                Descripcion = dr["Descripcion"] == DBNull.Value ? "" : dr["Descripcion"].ToString(),
                                StockActual = dr["StockActual"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StockActual"]),
                                StockMinimo = dr["StockMinimo"] == DBNull.Value ? 0 : Convert.ToInt32(dr["StockMinimo"]),
                                CostoPromedio = dr["CostoPromedio"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["CostoPromedio"]),
                                UltimoCostoFactura = dr["UltimoCostoFactura"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["UltimoCostoFactura"]),
                                Estado = dr["Estado"] == DBNull.Value ? true : Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                    totalRegistros = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { throw new Exception("Error al obtener los productos paginados: " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public DataTable Buscar(string textoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductos", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return dt;
        }

        public DataTable BuscarAvanzado(string textoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductosAvanzado", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return dt;
        }
        public string ActualizarEstado(int id_producto, bool activar)
        {
            string mensaje = "OK";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarEstadoProducto", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    cmd.Parameters.AddWithValue("@estado", activar ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }
        public string ActualizarStock(int id_producto, int cantidad, string tipoMovimiento, string motivo, string usuario)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarStockMovimiento", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@tipoMovimiento", string.IsNullOrWhiteSpace(tipoMovimiento) ? (object)DBNull.Value : tipoMovimiento);
                    cmd.Parameters.AddWithValue("@motivo", string.IsNullOrWhiteSpace(motivo) ? (object)DBNull.Value : motivo);
                    cmd.Parameters.AddWithValue("@usuario", string.IsNullOrWhiteSpace(usuario) ? (object)DBNull.Value : usuario);

                    SqlParameter paramResultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramResultado);
                    SqlParameter paramMensaje = new SqlParameter("@mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramMensaje);

                    cmd.ExecuteNonQuery();

                    mensaje = paramMensaje.Value?.ToString() ?? "";
                }
            }
            catch (Exception ex) { throw new Exception("Error al actualizar stock: " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
            return mensaje;
        }

        public DataTable ObtenerMovimientos(int id_producto)
        {
            DataTable dt = new DataTable();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT FechaMovimiento, Cantidad, StockAnterior, StockNuevo, TipoMovimiento, Motivo, Usuario FROM StockMovimientos WHERE IdProducto = @id_producto ORDER BY FechaMovimiento DESC", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return dt;
        }
    }
}
