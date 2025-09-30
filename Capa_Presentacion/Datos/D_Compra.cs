

using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{

    public class ItemCombo
    {
        public int Key { get; set; } 
        public string Value { get; set; } 
    }

    public class D_Compra
    {
    
        public bool InsertarCompra(L_Compra obj, DataTable dtDetalle, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_proveedor", obj.IDProveedor);
                cmd.Parameters.AddWithValue("@numero_factura", obj.NumeroFactura);
                cmd.Parameters.AddWithValue("@fecha_ingreso", obj.FechaIngreso.Date);
                cmd.Parameters.AddWithValue("@tipo_compra", obj.TipoCompra);

                cmd.Parameters.AddWithValue("@plazo_dias", obj.PlazoDias.HasValue ? obj.PlazoDias.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@vencimiento_factura", obj.VencimientoFactura.HasValue ? obj.VencimientoFactura.Value.Date : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@descripcion_encabezado", string.IsNullOrEmpty(obj.Descripcion) ? (object)DBNull.Value : obj.Descripcion);

                SqlParameter paramDetalle = new SqlParameter("@DetalleCompra", SqlDbType.Structured);
                paramDetalle.TypeName = "dbo.TipoDetalleCompra";
                paramDetalle.Value = dtDetalle;
                cmd.Parameters.Add(paramDetalle);

                SqlParameter paramResultado = cmd.Parameters.Add("@resultado", SqlDbType.Int);
                paramResultado.Direction = ParameterDirection.Output;
                SqlParameter paramMensaje = cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 500);
                paramMensaje.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                respuesta = Convert.ToBoolean(paramResultado.Value);
                Mensaje = paramMensaje.Value.ToString();
            }
            catch (Exception ex)
            {
                Mensaje = "Error en Capa de Datos al registrar la compra: " + ex.Message;
                respuesta = false;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public List<L_Compra> ObtenerComprasPaginadas(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Compra> lista = new List<L_Compra>();
            totalRegistros = 0;

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerComprasPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@paginaActual", pagina);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                cmd.Parameters.Add("TotalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Compra()
                        {
                            IDCompra = Convert.ToInt32(dr["id_compra"]),
                            NumeroFactura = dr["numero_factura"].ToString(),
                            FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                            Proveedor = dr["Proveedor"].ToString(),
                            Producto = dr["Producto"].ToString(),
                            UnidadMedida = dr["UnidadMedida"].ToString(),
                            Cantidad = Convert.ToDecimal(dr["cantidad"]),
                            PrecioUnitarioReal = Convert.ToDecimal(dr["PrecioUnitarioReal"]),
                            CostoPromedioActual = Convert.ToDecimal(dr["CostoPromedioActual"]),
                            TotalRealDetalle = Convert.ToDecimal(dr["TotalRealDetalle"])
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(cmd.Parameters["TotalRegistros"].Value);
            }
            catch (Exception)
            {
                lista = new List<L_Compra>();
                totalRegistros = 0;
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public List<L_Compra> BuscarCompras(string terminoBusqueda)
        {
            List<L_Compra> lista = new List<L_Compra>();

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarCompras", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Compra()
                        {
                            IDCompra = Convert.ToInt32(dr["id_compra"]),
                            NumeroFactura = dr["numero_factura"].ToString(),
                            FechaIngreso = Convert.ToDateTime(dr["fecha_ingreso"]),
                            Proveedor = dr["Proveedor"].ToString(),
                            Producto = dr["Producto"].ToString(),
                            UnidadMedida = dr["UnidadMedida"].ToString(),
                            Cantidad = Convert.ToDecimal(dr["cantidad"]),
                            PrecioUnitarioReal = Convert.ToDecimal(dr["PrecioUnitarioReal"]),
                            CostoPromedioActual = Convert.ToDecimal(dr["CostoPromedioActual"]),
                            TotalRealDetalle = Convert.ToDecimal(dr["TotalRealDetalle"])
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<L_Compra>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

      
        public List<ItemCombo> ListarProveedores()
        {
            List<ItemCombo> lista = new List<ItemCombo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_proveedor, razon_social FROM T_Proveedores ORDER BY razon_social", Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItemCombo()
                        {
                            Key = Convert.ToInt32(dr["id_proveedor"]),
                            Value = dr["razon_social"].ToString()
                        });
                    }
                }
            }
            catch { lista = new List<ItemCombo>(); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<ItemCombo> ListarProductos()
        {
            List<ItemCombo> lista = new List<ItemCombo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_producto, nombre FROM Producto ORDER BY nombre", Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItemCombo()
                        {
                            Key = Convert.ToInt32(dr["id_producto"]),
                            Value = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch { lista = new List<ItemCombo>(); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<ItemCombo> ListarUnidadesMedida()
        {
            List<ItemCombo> lista = new List<ItemCombo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_unidad, nombre FROM Unidad_Medida ORDER BY nombre", Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItemCombo()
                        {
                            Key = Convert.ToInt32(dr["id_unidad"]),
                            Value = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch { lista = new List<ItemCombo>(); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<ItemCombo> ListarMarcas()
        {
            List<ItemCombo> lista = new List<ItemCombo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_marca, nombre FROM Marca ORDER BY nombre", Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItemCombo()
                        {
                            Key = Convert.ToInt32(dr["id_marca"]),
                            Value = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch { lista = new List<ItemCombo>(); }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<ItemCombo> ListarCategorias()
        {
            List<ItemCombo> lista = new List<ItemCombo>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_categoria, nombre FROM Categoria ORDER BY nombre", Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItemCombo()
                        {
                            Key = Convert.ToInt32(dr["id_categoria"]),
                            Value = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch { lista = new List<ItemCombo>(); }
            finally { Conexion.cerrar(); }
            return lista;
        }
    }
}