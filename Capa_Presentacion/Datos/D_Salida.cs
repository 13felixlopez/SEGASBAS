using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{

    public class D_Salida
    {
        public bool InsertarSalida(L_Salida obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_RegistrarSalida", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NombreProducto", obj.NombreProducto);
                cmd.Parameters.AddWithValue("@CantidadSalida", obj.Cantidad);
                cmd.Parameters.AddWithValue("@CostoUnitario", obj.CostoUnitario);
                cmd.Parameters.AddWithValue("@FechaSalida", obj.FechaSalida);

                cmd.Parameters.AddWithValue("@NombreLoteDestino", obj.NombreLoteDestino ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DescripcionCiclo", obj.DescripcionCiclo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DescripcionSalida", obj.Descripcion ?? (object)DBNull.Value);

                int filasAfectadas = cmd.ExecuteNonQuery();
                respuesta = filasAfectadas > 0;
            }
            catch (SqlException ex)
            {
                Mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                Mensaje = "Error general de base de datos: " + ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }
        public L_Salida ObtenerDatosProductoParaFormulario(string nombreProducto)
        {
            L_Salida datos = null;
            string consulta = @"
                SELECT
                    P.CostoPromedio,
                    C.nombre AS NombreCategoria,
                    U.abreviatura AS UnidadMedidaAbreviada
                FROM
                    Producto P
                JOIN Categoria C ON P.id_categoria = C.id_categoria
                JOIN Unidad_Medida U ON P.id_unidad = U.id_unidad
                WHERE P.nombre = @NombreProducto;";

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                cmd.Parameters.AddWithValue("@NombreProducto", nombreProducto);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        datos = new L_Salida
                        {
                            CostoUnitario = dr.GetDecimal(dr.GetOrdinal("CostoPromedio")),
                            NombreCategoria = dr["NombreCategoria"].ToString(),
                            AbreviaturaUnidad = dr["UnidadMedidaAbreviada"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos: " + ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }
            return datos;
        }
        public List<L_Salida> ObtenerSalidasPaginadas(int pagina, int tamanoPagina, string busqueda, string campo, out int totalRegistros)
        {
            List<L_Salida> lista = new List<L_Salida>();
            totalRegistros = 0;

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerSalidasPaginadas", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NumeroPagina", pagina);
                cmd.Parameters.AddWithValue("@FilasPorPagina", tamanoPagina);
                cmd.Parameters.AddWithValue("@TextoBusqueda", busqueda ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CampoBusqueda", campo);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows && dr.Read())
                    {
                        totalRegistros = Convert.ToInt32(dr["TotalFilas"]);
                    }

                    if (dr.NextResult())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new L_Salida()
                            {
                                IDSalida = Convert.ToInt32(dr["id_salida"]),
                                FechaSalida = Convert.ToDateTime(dr["FechaSalida"]),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                CostoUnitario = Convert.ToDecimal(dr["CostoUnitario"]),
                                NombreLoteDestino = dr["DestinoLote"].ToString(),
                                DescripcionCiclo = dr["Ciclo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<L_Salida>();
                totalRegistros = 0;
            }
            finally { Conexion.cerrar(); }
            return lista;
        }
        public List<string> ObtenerNombresProductos()
        {
            List<string> lista = new List<string>();
            string consulta = "SELECT nombre FROM Producto WHERE Estado = 1 ORDER BY nombre";

            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(dr["nombre"].ToString());
                    }
                }
            }
            catch { }
            finally { Conexion.cerrar(); }
            return lista;
        }
        public List<string> ObtenerNombresLotes()
        {
            List<string> lista = new List<string>();
            string consulta = "SELECT nombre FROM Lote ORDER BY nombre";
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(dr["nombre"].ToString());
                    }
                }
            }
            catch { }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<string> ObtenerDescripcionesCiclos()
        {
            List<string> lista = new List<string>();
            string consulta = "SELECT descripcion FROM Ciclo ORDER BY descripcion";
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(dr["descripcion"].ToString());
                    }
                }
            }
            catch { }
            finally { Conexion.cerrar(); }
            return lista;
        }
    }
}