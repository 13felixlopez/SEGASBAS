using System;
using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;
using System.Collections.Generic;

namespace Capa_Datos
{
    public class CD_Lote
    {
        public List<Capa_Entidad.CE_Lote> ObtenerLotes()
        {
            List<Capa_Entidad.CE_Lote> lista = new List<Capa_Entidad.CE_Lote>();
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SELECT id_lote, nombre FROM Lote ORDER BY nombre ASC", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Capa_Entidad.CE_Lote()
                            {
                                IDLote = Convert.ToInt32(dr["id_lote"]),
                                NombreLote = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Capa_Entidad.CE_Lote>();
            }
            return lista;
        }
        public List<CE_Lote> ObtenerLotesPaginados(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<CE_Lote> lista = new List<CE_Lote>();
            totalRegistros = 0;
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerLotesConPaginado", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pagina", pagina);
                    cmd.Parameters.AddWithValue("@tamanoPagina", tamanoPagina);
                    SqlParameter totalRegParam = new SqlParameter("@total_registros", SqlDbType.Int);
                    totalRegParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(totalRegParam);
                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CE_Lote()
                            {
                                IDLote = Convert.ToInt32(dr["id_lote"]),
                                NombreLote = dr["nombre"].ToString(),
                                Manzanaje = dr["manzanaje"].ToString(),
                                TipoSiembra = dr["tipo_siembra"].ToString(),
                                EstadoCultivo = dr["estado_cultivo"].ToString(),
                                Ciclo = dr["ciclo"].ToString(),
                                FechaSiembra = dr["fecha_siembra"] as DateTime?,
                                FechaCorte = dr["fecha_corte"] as DateTime?,
                                Observacion = dr["observacion"].ToString()
                            });
                        }
                    }
                    totalRegistros = Convert.ToInt32(cmd.Parameters["@total_registros"].Value);
                }
            }
            catch { lista = new List<CE_Lote>(); totalRegistros = 0; }
            return lista;
        }

        public bool InsertarLote(CE_Lote oLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertarLote", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", oLote.NombreLote);
                    cmd.Parameters.AddWithValue("@manzanaje", oLote.Manzanaje);
                    cmd.Parameters.AddWithValue("@id_tipo_siembra", oLote.IDTipoSiembra.HasValue ? (object)oLote.IDTipoSiembra.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@fecha_siembra", oLote.FechaSiembra.HasValue ? (object)oLote.FechaSiembra.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_estado_cultivo", oLote.IDEstadoCultivo);
                    cmd.Parameters.AddWithValue("@id_ciclo", oLote.IDCiclo.HasValue ? (object)oLote.IDCiclo.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", oLote.Observacion);
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return resultado;
        }

        public bool ActualizarLote(CE_Lote oLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ActualizarLote", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_lote", oLote.IDLote);
                    cmd.Parameters.AddWithValue("@nombre", oLote.NombreLote);
                    cmd.Parameters.AddWithValue("@manzanaje", oLote.Manzanaje);
                    cmd.Parameters.AddWithValue("@id_tipo_siembra", oLote.IDTipoSiembra.HasValue ? (object)oLote.IDTipoSiembra.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_estado_cultivo", oLote.IDEstadoCultivo);
                    cmd.Parameters.AddWithValue("@id_ciclo", oLote.IDCiclo.HasValue ? (object)oLote.IDCiclo.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@observacion", oLote.Observacion);
                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = true;
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return resultado;
        }

        public bool EliminarLote(int idLote, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarLote", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_lote", idLote);
                    oconexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;
                    else
                        mensaje = "No se pudo eliminar el lote.";
                }
            }
            catch (Exception ex) { mensaje = ex.Message; }
            return resultado;
        }
    }

    public class CD_Generico
    {
        public List<KeyValuePair<int, string>> ObtenerCiclos()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerTodosCiclos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
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
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            return lista;
        }

        public List<KeyValuePair<int, string>> ObtenerEstadosCultivo()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerTodosEstadosCultivo", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
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
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            return lista;
        }

        public List<KeyValuePair<int, string>> ObtenerTiposSiembra()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection oconexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerTodosTiposSiembra", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
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
            }
            catch { lista = new List<KeyValuePair<int, string>>(); }
            return lista;
        }
    }
}