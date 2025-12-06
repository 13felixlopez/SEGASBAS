using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_Presentacion.Datos
{
    public class D_Asistencia
    {
        public List<L_Asistencia> ObtenerAsistenciasPaginadas(int pagina, int tamanoPagina, out int totalRegistros)
        {
            List<L_Asistencia> lista = new List<L_Asistencia>();
            totalRegistros = 0;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerAsistenciaConPaginado", Conexion.conectar);
                cmd.Parameters.AddWithValue("pagina", pagina);
                cmd.Parameters.AddWithValue("tamanoPagina", tamanoPagina);
                cmd.Parameters.Add("TotalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Asistencia()
                        {
                            IDAsistencia = Convert.ToInt32(dr["Id_asistencia"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            HoraRegistro = (TimeSpan)dr["Hora_registro"],
                            IDEmpleado = Convert.ToInt32(dr["Id_empleado"]),
                            NombreCompletoEmpleado = dr["NombreCompletoEmpleado"].ToString(),
                            IDActividad = dr["Id_actividad"] != DBNull.Value ? (int?)Convert.ToInt32(dr["Id_actividad"]) : null,
                            NombreActividad = dr["NombreActividad"].ToString(),
                            IDLote = dr["id_lote"] != DBNull.Value ? (int?)Convert.ToInt32(dr["id_lote"]) : null,
                            NombreLote = dr["NombreLote"].ToString(),
                            IDCargo = dr["Id_cargo"] != DBNull.Value ? (int?)Convert.ToInt32(dr["Id_cargo"]) : null,
                            NombreCargo = dr["NombreCargo"].ToString(),
                            Estado = dr["Estado"].ToString(),
                            Justificacion = dr["Justificacion"].ToString(),
                            Tolvadas = dr["Tolvadas"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Tolvadas"]) : null,
                            HorasExtras = dr["HorasExtras"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["HorasExtras"]) : null
                        });
                    }
                }
                totalRegistros = Convert.ToInt32(cmd.Parameters["TotalRegistros"].Value);

            }
            catch (Exception)
            {
                lista = new List<L_Asistencia>();
                totalRegistros = 0;
            }
            finally { Conexion.cerrar(); }
            return lista;
        }
        public bool VerificarDobleAsistencia(int idEmpleado, DateTime fecha)
        {
            bool existe = false;
            try
            {
                Conexion.abrir();
          
                SqlCommand cmd = new SqlCommand("sp_VerificarDobleAsistencia", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                int resultado = (int)cmd.ExecuteScalar();
                existe = resultado > 0; 
            }
            catch (Exception)
            {
  
                existe = false;
            }
            finally
            {
                Conexion.cerrar();
            }
            return existe;
        }

        public bool InsertarAsistencia(L_Asistencia obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_InsertarAsistencia", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                cmd.Parameters.AddWithValue("@Id_empleado", obj.IDEmpleado);
                cmd.Parameters.AddWithValue("@Id_actividad", obj.IDActividad.HasValue ? obj.IDActividad.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_lote", obj.IDLote.HasValue ? obj.IDLote.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_cargo", obj.IDCargo.HasValue ? obj.IDCargo.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                cmd.Parameters.AddWithValue("@Justificacion", string.IsNullOrEmpty(obj.Justificacion) ? (object)DBNull.Value : obj.Justificacion);
                cmd.Parameters.AddWithValue("@Tolvadas", obj.Tolvadas.HasValue ? obj.Tolvadas.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HorasExtras", obj.HorasExtras.HasValue ? obj.HorasExtras.Value : (object)DBNull.Value);


                int filasAfectadas = cmd.ExecuteNonQuery();
                respuesta = filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public bool EditarAsistencia(L_Asistencia obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ActualizarAsistencia", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_asistencia", obj.IDAsistencia);
                cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                cmd.Parameters.AddWithValue("@Id_empleado", obj.IDEmpleado);
                cmd.Parameters.AddWithValue("@Id_actividad", obj.IDActividad.HasValue ? obj.IDActividad.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_lote", obj.IDLote.HasValue ? obj.IDLote.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Id_cargo", obj.IDCargo.HasValue ? obj.IDCargo.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                cmd.Parameters.AddWithValue("@Justificacion", string.IsNullOrEmpty(obj.Justificacion) ? (object)DBNull.Value : obj.Justificacion);
                cmd.Parameters.AddWithValue("@Tolvadas", obj.Tolvadas.HasValue ? obj.Tolvadas.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HorasExtras", obj.HorasExtras.HasValue ? obj.HorasExtras.Value : (object)DBNull.Value);


                int filasAfectadas = cmd.ExecuteNonQuery();
                respuesta = filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public bool EliminarAsistencia(int idAsistencia, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_EliminarAsistencia", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_asistencia", idAsistencia);

                int filasAfectadas = cmd.ExecuteNonQuery();
                respuesta = filasAfectadas > 0;

            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        public List<L_Asistencia> BuscarAsistencias(string terminoBusqueda)
        {
            List<L_Asistencia> lista = new List<L_Asistencia>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_BuscarAsistencia", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Asistencia()
                        {
                            IDAsistencia = Convert.ToInt32(dr["Id_asistencia"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            HoraRegistro = (TimeSpan)dr["Hora_registro"],
                            IDEmpleado = Convert.ToInt32(dr["Id_empleado"]),
                            NombreCompletoEmpleado = dr["NombreCompletoEmpleado"].ToString(),
                            IDActividad = dr["Id_actividad"] != DBNull.Value ? (int?)Convert.ToInt32(dr["Id_actividad"]) : null,
                            NombreActividad = dr["NombreActividad"].ToString(),
                            IDLote = dr["id_lote"] != DBNull.Value ? (int?)Convert.ToInt32(dr["id_lote"]) : null,
                            NombreLote = dr["NombreLote"].ToString(),
                            IDCargo = dr["Id_cargo"] != DBNull.Value ? (int?)Convert.ToInt32(dr["Id_cargo"]) : null,
                            NombreCargo = dr["NombreCargo"].ToString(),
                            Estado = dr["Estado"].ToString(),
                            Justificacion = dr["Justificacion"].ToString(),
                            Tolvadas = dr["Tolvadas"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["Tolvadas"]) : null,
                            HorasExtras = dr["HorasExtras"] != DBNull.Value ? (decimal?)Convert.ToDecimal(dr["HorasExtras"]) : null
                        });
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<L_Asistencia>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }


        public void ReporteAsistencia(ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                using (SqlDataAdapter da = new SqlDataAdapter("ReporteAsistencia", Conexion.conectar))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los datos de asistencia: " + ex.Message,
                    "Error MostrarLotesExcel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void ReporteAsistenciaEmpleado(
             int idEmpleado,
                 DateTime fechaDesde,
              DateTime fechaHasta,
         ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                using (SqlDataAdapter da = new SqlDataAdapter("sp_ReporteAsistenciaEmpleado", Conexion.conectar))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                    da.SelectCommand.Parameters.AddWithValue("@FechaDesde", fechaDesde.Date);
                    da.SelectCommand.Parameters.AddWithValue("@FechaHasta", fechaHasta.Date);

                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte de asistencia: " + ex.Message,
                    "Error ReporteAsistenciaEmpleado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void ReporteAsis(ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                using (SqlDataAdapter da = new SqlDataAdapter("ReporteAsis", Conexion.conectar))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los datos de asistencia: " + ex.Message,
                    "Error MostrarLotesExcel", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public DataTable ReporteHorasExtras(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable();

            try
            {
                Conexion.abrir();

                using (SqlCommand cmd = new SqlCommand("sp_ReporteHorasExtras", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte de horas extras: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.cerrar();
            }

            return dt;
        }

    }
}
