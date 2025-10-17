using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Compra
    {
       
        public int InsertarCabeceraCompra(L_Compra obj, out string Mensaje)
        {
            int compraIdGenerado = 0;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SP_InsertarCabeceraCompra", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

          
                cmd.Parameters.AddWithValue("@id_proveedor", obj.Id_proveedor);
                cmd.Parameters.AddWithValue("@FechaIngreso", obj.FechaIngreso);
                cmd.Parameters.AddWithValue("@NumeroFactura", obj.NumeroFactura);
                cmd.Parameters.AddWithValue("@TipoCompra", obj.TipoCompra);
                cmd.Parameters.AddWithValue("@VencimientoFactura", obj.VencimientoFactura.HasValue ? obj.VencimientoFactura.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Plazo", obj.Plazo.HasValue ? obj.Plazo.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DescripcionGeneral", string.IsNullOrEmpty(obj.DescripcionGeneral) ? (object)DBNull.Value : obj.DescripcionGeneral);
                cmd.Parameters.AddWithValue("@IdUser", obj.IdUser.HasValue ? obj.IdUser.Value : (object)DBNull.Value);

               
                SqlParameter outParam = cmd.Parameters.Add("@CompraID_OUTPUT", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                compraIdGenerado = Convert.ToInt32(outParam.Value);
            }
            catch (Exception ex)
            {
                Mensaje = "Error en la cabecera: " + ex.Message;
            }
            finally { Conexion.cerrar(); }
            return compraIdGenerado;
        }

        public void InsertarDetalleYActualizarCosto(L_DetalleCompra obj)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SP_InsertarDetalleYActualizarCosto", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CompraID", obj.CompraID);
                cmd.Parameters.AddWithValue("@id_producto", obj.Id_producto);
                cmd.Parameters.AddWithValue("@CantidadComprada", obj.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioCompra", obj.PrecioCompra);

                cmd.ExecuteNonQuery();
            }
            finally { Conexion.cerrar(); }
        }


        public List<L_Compra> ListarComprasPaginado(int numPagina, int tamanoPagina, string textoBusqueda, out int totalPaginas)
        {
            List<L_Compra> lista = new List<L_Compra>();
            totalPaginas = 1;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SP_ListarComprasPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NumPagina", numPagina);
                cmd.Parameters.AddWithValue("@TamañoPagina", tamanoPagina);
                cmd.Parameters.AddWithValue("@TextoBusqueda", string.IsNullOrEmpty(textoBusqueda) ? (object)DBNull.Value : textoBusqueda);

                SqlParameter totalPaginasParam = cmd.Parameters.Add("@TotalPaginas", SqlDbType.Int);
                totalPaginasParam.Direction = ParameterDirection.Output;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Compra()
                        {
                            CompraID = Convert.ToInt32(dr["CompraID"]),
                            RazonSocialProveedor = dr["Proveedor"].ToString(),
                            FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),
                            NumeroFactura = dr["NumeroFactura"].ToString(),
                            TotalCompra = Convert.ToDecimal(dr["TotalCompra"])
                        });
                    }
                }

                if (totalPaginasParam.Value != DBNull.Value)
                {
                    totalPaginas = Convert.ToInt32(totalPaginasParam.Value);
                }
            }
            catch (Exception)
            {
                lista = new List<L_Compra>();
                totalPaginas = 1;
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

    }
}