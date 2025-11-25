// D_Producto.cs
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Producto
    {
        // Helper: verificar si un SqlDataReader contiene una columna
        private bool TieneColumna(SqlDataReader reader, string nombreColumna)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(nombreColumna, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

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
                            var prod = new L_Producto()
                            {
                                id_producto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : 0,
                                nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : "",
                                Descripcion = TieneColumna(dr, "Descripcion") && dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                                StockActual = TieneColumna(dr, "StockActual") && dr["StockActual"] != DBNull.Value ? Convert.ToDecimal(dr["StockActual"]) : 0M,
                                StockMinimo = TieneColumna(dr, "StockMinimo") && dr["StockMinimo"] != DBNull.Value ? Convert.ToDecimal(dr["StockMinimo"]) : 0M,
                                CostoPromedio = TieneColumna(dr, "CostoPromedio") && dr["CostoPromedio"] != DBNull.Value ? Convert.ToDecimal(dr["CostoPromedio"]) : 0M,
                                UltimoCostoFactura = TieneColumna(dr, "UltimoCostoFactura") && dr["UltimoCostoFactura"] != DBNull.Value ? Convert.ToDecimal(dr["UltimoCostoFactura"]) : 0M,
                                Estado = TieneColumna(dr, "Estado") && dr["Estado"] != DBNull.Value ? Convert.ToBoolean(dr["Estado"]) : true,
                                id_unidad = TieneColumna(dr, "id_unidad") && dr["id_unidad"] != DBNull.Value ? Convert.ToInt32(dr["id_unidad"]) : 0,
                                id_marca = TieneColumna(dr, "id_marca") && dr["id_marca"] != DBNull.Value ? Convert.ToInt32(dr["id_marca"]) : 0,
                                id_categoria = TieneColumna(dr, "id_categoria") && dr["id_categoria"] != DBNull.Value ? Convert.ToInt32(dr["id_categoria"]) : 0
                            };

                            lista.Add(prod);
                        }
                    }

                    // Los parámetros de salida están disponibles después de cerrar/dispose del reader
                    totalRegistros = outParam.Value != DBNull.Value && outParam.Value != null ? Convert.ToInt32(outParam.Value) : 0;
                }
            }
            catch (Exception ex) { throw new Exception("Error al obtener los productos paginados: " + ex.Message, ex); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        // Búsqueda con opción para incluir inactivos (requiere que el SP acepte @incluirInactivos)
        public DataTable Buscar(string textoBusqueda, bool incluirInactivos = false)
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductos", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);
                    cmd.Parameters.AddWithValue("@incluirInactivos", incluirInactivos ? 1 : 0);
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

        public L_Producto ObtenerProductoPorId(int id)
        {
            L_Producto p = null;
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductoPorId", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            p = new L_Producto
                            {
                                id_producto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : 0,
                                nombre = dr["nombre"] != DBNull.Value ? dr["nombre"].ToString() : "",
                                Descripcion = TieneColumna(dr, "Descripcion") && dr["Descripcion"] != DBNull.Value ? dr["Descripcion"].ToString() : "",
                                StockActual = TieneColumna(dr, "StockActual") && dr["StockActual"] != DBNull.Value ? Convert.ToDecimal(dr["StockActual"]) : 0M,
                                StockMinimo = TieneColumna(dr, "StockMinimo") && dr["StockMinimo"] != DBNull.Value ? Convert.ToDecimal(dr["StockMinimo"]) : 10M,
                                CostoPromedio = TieneColumna(dr, "CostoPromedio") && dr["CostoPromedio"] != DBNull.Value ? Convert.ToDecimal(dr["CostoPromedio"]) : 0M,
                                UltimoCostoFactura = TieneColumna(dr, "UltimoCostoFactura") && dr["UltimoCostoFactura"] != DBNull.Value ? Convert.ToDecimal(dr["UltimoCostoFactura"]) : 0M,
                                Estado = TieneColumna(dr, "Estado") && dr["Estado"] != DBNull.Value ? Convert.ToBoolean(dr["Estado"]) : true,
                                id_unidad = TieneColumna(dr, "id_unidad") && dr["id_unidad"] != DBNull.Value ? Convert.ToInt32(dr["id_unidad"]) : 0,
                                id_marca = TieneColumna(dr, "id_marca") && dr["id_marca"] != DBNull.Value ? Convert.ToInt32(dr["id_marca"]) : 0,
                                id_categoria = TieneColumna(dr, "id_categoria") && dr["id_categoria"] != DBNull.Value ? Convert.ToInt32(dr["id_categoria"]) : 0,
                                // opcionales: nombres relacionados si el SP los retorna
                                NombreUnidad = TieneColumna(dr, "NombreUnidad") && dr["NombreUnidad"] != DBNull.Value ? dr["NombreUnidad"].ToString() : "",
                                NombreCategoria = TieneColumna(dr, "NombreCategoria") && dr["NombreCategoria"] != DBNull.Value ? dr["NombreCategoria"].ToString() : ""
                            };
                        }
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return p;
        }

        public string ActualizarEstado(int id_producto, bool estado)
        {
            string mensaje = "";
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarEstadoProducto", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    cmd.Parameters.AddWithValue("@estado", estado ? 1 : 0);
                    cmd.ExecuteNonQuery();
                    mensaje = "OK";
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            finally { Conexion.cerrar(); }
            return mensaje;
        }

        public string ActualizarStock(int id_producto, decimal cantidad, string tipoMovimiento, string motivo, string usuario,
                               int? idLoteDestino = null, int? idCiclo = null, string nombreLoteDestino = null, string descripcionCiclo = null)
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarStockMovimiento", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_producto", id_producto);

                    var pCantidad = new SqlParameter("@cantidad", SqlDbType.Decimal);
                    pCantidad.Precision = 18;
                    pCantidad.Scale = 4;
                    pCantidad.Value = cantidad;
                    cmd.Parameters.Add(pCantidad);

                    cmd.Parameters.AddWithValue("@tipoMovimiento", string.IsNullOrWhiteSpace(tipoMovimiento) ? (object)DBNull.Value : tipoMovimiento);
                    cmd.Parameters.AddWithValue("@motivo", string.IsNullOrWhiteSpace(motivo) ? (object)DBNull.Value : motivo);
                    cmd.Parameters.AddWithValue("@usuario", string.IsNullOrWhiteSpace(usuario) ? (object)DBNull.Value : usuario);

                    cmd.Parameters.AddWithValue("@IdLoteDestino", idLoteDestino.HasValue ? (object)idLoteDestino.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@IdCiclo", idCiclo.HasValue ? (object)idCiclo.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@NombreLoteDestino", string.IsNullOrWhiteSpace(nombreLoteDestino) ? (object)DBNull.Value : nombreLoteDestino);
                    cmd.Parameters.AddWithValue("@DescripcionCiclo", string.IsNullOrWhiteSpace(descripcionCiclo) ? (object)DBNull.Value : descripcionCiclo);

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
        public string SetStockDirecto(int id_producto, decimal nuevoStock, string usuario = null, string motivo = "Ajuste manual")
        {
            string mensaje = "";
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"
            BEGIN TRANSACTION;

            DECLARE @stockAnterior DECIMAL(18,4);
            SELECT @stockAnterior = ISNULL(StockActual, 0) FROM Producto WHERE id_producto = @id_producto;

            UPDATE Producto
            SET StockActual = @nuevoStock,
                Estado = CASE WHEN @nuevoStock <= 0 THEN 0 ELSE 1 END
            WHERE id_producto = @id_producto;

            INSERT INTO StockMovimientos (IdProducto, Cantidad, StockAnterior, StockNuevo, TipoMovimiento, Motivo, Usuario)
            VALUES (@id_producto, @cantidadMovimiento, @stockAnterior, @nuevoStock, 'Ajuste', @motivo, @usuario);

            COMMIT TRANSACTION;
        ", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);
                    cmd.Parameters.AddWithValue("@nuevoStock", nuevoStock);
                    // Cantidad en movimiento = nuevoStock - anterior (se registra con signo)
                    // para calcularlo en C# y pasarlo:
                    decimal cantidadMovimiento = 0;
                    // obtener stock anterior (consultar)
                    using (SqlCommand q = new SqlCommand("SELECT ISNULL(StockActual,0) FROM Producto WHERE id_producto = @id", Conexion.conectar))
                    {
                        q.Parameters.AddWithValue("@id", id_producto);
                        object o = q.ExecuteScalar();
                        decimal stockAnterior = o == DBNull.Value ? 0M : Convert.ToDecimal(o);
                        cantidadMovimiento = nuevoStock - stockAnterior;
                    }
                    cmd.Parameters.AddWithValue("@cantidadMovimiento", cantidadMovimiento);
                    cmd.Parameters.AddWithValue("@motivo", string.IsNullOrWhiteSpace(motivo) ? (object)DBNull.Value : motivo);
                    cmd.Parameters.AddWithValue("@usuario", string.IsNullOrWhiteSpace(usuario) ? (object)DBNull.Value : usuario);

                    cmd.ExecuteNonQuery();

                    mensaje = "Stock actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                // Rollback implícito si excepción; devolvemos mensaje
                mensaje = "Error al actualizar stock: " + ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return mensaje;
        }
    }

}
