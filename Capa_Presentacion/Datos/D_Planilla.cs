using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms; 

namespace Capa_Presentacion.Datos
{
    public class D_Planilla
    {
   
        public List<KeyValuePair<int, string>> ObtenerEmpleadosIdYNombre()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
             
                SqlCommand cmd = new SqlCommand("SELECT Id_empleado, Nombre + ' ' + Apellido AS NombreCompleto FROM Empleado ORDER BY NombreCompleto ASC", Conexion.conectar);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int id = Convert.ToInt32(dr["Id_empleado"]);
                        string nombre = dr["NombreCompleto"].ToString();
                        lista.Add(new KeyValuePair<int, string>(id, nombre));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados para planilla: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista = new List<KeyValuePair<int, string>>();
            }
            finally { Conexion.cerrar(); }
            return lista;
        }


      
        public List<L_Planilla> GenerarDetallePlanilla(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            List<L_Planilla> lista = new List<L_Planilla>();
            try
            {
                Conexion.abrir();
             
                SqlCommand cmd = new SqlCommand("sp_GenerarDetallePlanilla", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IDEmpleado", idEmpleado);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Planilla()
                        {
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Estado = dr["Estado"].ToString(),
                            Justificacion = dr["Justificacion"].ToString(),
                            HorasExtras = dr["HorasExtras"] != DBNull.Value ? Convert.ToDecimal(dr["HorasExtras"]) : 0,
                            Tolvadas = dr["Tolvadas"] != DBNull.Value ? Convert.ToDecimal(dr["Tolvadas"]) : 0,

                        
                            SalarioDiario = Convert.ToDecimal(dr["SalarioDiario"]),
                            DiasTrabajadosContables = Convert.ToInt32(dr["DiasTrabajadosContables"]),
                            DiasPagadosNoTrabajados = Convert.ToInt32(dr["DiasPagadosNoTrabajados"]),
                            NombreCompletoEmpleado = dr["NombreCompletoEmpleado"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar detalle de planilla (Verifique SP y Conexión): " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista = new List<L_Planilla>();
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public bool GuardarPlanilla(int idEmpleado, DateTime fechaInicio, DateTime fechaFin,
                            decimal salarioDiario, decimal diasTrabajados, decimal horasExtrasCantidad,
                            decimal valorHorasExtras, decimal totalDevengado, decimal deduccionINSS,
                            decimal deduccionIR, decimal incentivos, decimal vacaciones,
                            decimal otrasDeducciones, decimal totalDeducciones, decimal pagoNeto)
        {
            bool respuesta = false;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_GuardarPlanilla", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

              
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);
                cmd.Parameters.AddWithValue("@SalarioDiario", salarioDiario);
                cmd.Parameters.AddWithValue("@DiasTrabajados", diasTrabajados);
                cmd.Parameters.AddWithValue("@HorasExtrasCantidad", horasExtrasCantidad);
                cmd.Parameters.AddWithValue("@ValorHorasExtras", valorHorasExtras);
                cmd.Parameters.AddWithValue("@TotalDevengado", totalDevengado);
                cmd.Parameters.AddWithValue("@DeduccionINSS", deduccionINSS);
                cmd.Parameters.AddWithValue("@DeduccionIR", deduccionIR);
                cmd.Parameters.AddWithValue("@Incentivos", incentivos);
                cmd.Parameters.AddWithValue("@Vacaciones", vacaciones);
                cmd.Parameters.AddWithValue("@OtrasDeducciones", otrasDeducciones);
                cmd.Parameters.AddWithValue("@TotalDeducciones", totalDeducciones);
                cmd.Parameters.AddWithValue("@PagoNeto", pagoNeto);


                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar Planilla Historial: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                respuesta = false;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }
    }
}