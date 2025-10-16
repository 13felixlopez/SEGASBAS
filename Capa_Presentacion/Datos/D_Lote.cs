using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Lote
    {
        public List<L_Lote> ObtenerLotes()
        {
            List<L_Lote> lista = new List<L_Lote>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT id_lote, nombre FROM Lote ORDER BY nombre ASC", Conexion.conectar);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Lote()
                        {
                            IDLote = Convert.ToInt32(dr["id_lote"]),
                            NombreLote = dr["nombre"].ToString()
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<L_Lote>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public List<L_Lote> ObtenerLotesPaginados(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Lote> lista = new List<L_Lote>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerLotesConPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                SqlParameter totalRegParam = new SqlParameter("@total_registros", SqlDbType.Int);
                totalRegParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(totalRegParam);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Lote()
                        {
                            IDLote = Convert.ToInt32(dr["id_lote"]),
                            NombreLote = dr["nombre"].ToString(),
                            Manzanaje = dr["manzanaje"].ToString(),
                            TipoSiembra = dr["tipo_siembra"].ToString(),
                            EstadoCultivo = dr["estado_cultivo"].ToString(),
                            Ciclo = dr["ciclo"].ToString(),
                            FechaSiembra = dr["fecha_siembra"] as DateTime?,
                            FechaCorte = dr["fecha_corte"] as DateTime?,
                            Observacion = dr["observacion"].ToString(),
                            VariedadArroz = dr["VariedadArroz"].ToString()
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(cmd.Parameters["@total_registros"].Value);
            }
            catch { lista = new List<L_Lote>(); totalRegistros = 0; }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public bool InsertarLote(L_Lote oLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_InsertarLote", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", oLote.NombreLote);
                cmd.Parameters.AddWithValue("@manzanaje", oLote.Manzanaje);
                cmd.Parameters.AddWithValue("@id_tipo_siembra", oLote.IDTipoSiembra.HasValue ? (object)oLote.IDTipoSiembra.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@fecha_siembra", oLote.FechaSiembra.HasValue ? (object)oLote.FechaSiembra.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_estado_cultivo", oLote.IDEstadoCultivo);
                cmd.Parameters.AddWithValue("@id_ciclo", oLote.IDCiclo.HasValue ? (object)oLote.IDCiclo.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@observacion", oLote.Observacion);
                cmd.Parameters.AddWithValue("@VariedadArroz", oLote.VariedadArroz);
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally
            {
                Conexion.cerrar();
            }
            return resultado;
        }

        public bool ActualizarLote(L_Lote oLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ActualizarLote", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_lote", oLote.IDLote);
                cmd.Parameters.AddWithValue("@nombre", oLote.NombreLote);
                cmd.Parameters.AddWithValue("@manzanaje", oLote.Manzanaje);
                cmd.Parameters.AddWithValue("@id_tipo_siembra", oLote.IDTipoSiembra.HasValue ? (object)oLote.IDTipoSiembra.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_estado_cultivo", oLote.IDEstadoCultivo);
                cmd.Parameters.AddWithValue("@id_ciclo", oLote.IDCiclo.HasValue ? (object)oLote.IDCiclo.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@observacion", oLote.Observacion);
                cmd.Parameters.AddWithValue("@VariedadArroz", oLote.VariedadArroz);
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally
            {
                Conexion.cerrar();
            }
            return resultado;
        }

        public bool EliminarLote(int idLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_EliminarLote", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_lote", idLote);
                int filasAfectadas = cmd.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;
                else
                    mensaje = "No se pudo eliminar el lote.";
            }
            catch (Exception ex) { mensaje = ex.Message; }
            finally
            {
                Conexion.cerrar();
            }
            return resultado;
        }
    }
    public class D_Generico
    {
        public List<KeyValuePair<int, string>> ObtenerCiclos()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodosCiclos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(dr["id_ciclo"]),
                            dr["descripcion"].ToString()
                        ));
                    }
                }
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public List<KeyValuePair<int, string>> ObtenerEstadosCultivo()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodosEstadosCultivo", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(dr["id_estado_cultivo"]),
                            dr["nombre"].ToString()
                        ));
                    }
                }
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }

        public List<KeyValuePair<int, string>> ObtenerTiposSiembra()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodosTiposSiembra", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(dr["id_tipo_siembra"]),
                            dr["nombre"].ToString()
                        ));
                    }
                }
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}
