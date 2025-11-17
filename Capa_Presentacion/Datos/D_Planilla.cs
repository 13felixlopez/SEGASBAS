using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;

namespace Capa_Presentacion.Datos
{
    public class D_Planilla
    {
        // ========================= 1. CARGAR EMPLEADOS =========================
        public List<KeyValuePair<int, string>> ObtenerEmpleadosIdYNombre()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("SELECT Id_empleado, Nombre + ' ' + Apellido AS NombreCompleto FROM Empleado ORDER BY NombreCompleto ASC", Conexion.conectar);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr["Id_empleado"]), dr["NombreCompleto"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista = new List<KeyValuePair<int, string>>();
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        // ========================= 2. GENERAR DETALLE (Asumido) =========================
        public List<L_Planilla> GenerarDetallePlanilla(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            // *** ESTE ES TU CÓDIGO DE GENERAR DETALLE QUE YA FUNCIONA ***
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
                            // Nota: Los campos de PagoBrutoDia, ValorHorasExtrasDia, TipoPagoDia se calculan en la Capa de Presentación
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar detalle de planilla: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista = new List<L_Planilla>();
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        // ========================= 3. OBTENER HISTORIAL (LECTURA) =========================
        public List<L_Planilla> ObtenerHistorialPlanillas()
        {
            List<L_Planilla> lista = new List<L_Planilla>();
            try
            {
                Conexion.abrir();

                string consulta = @"
                    SELECT 
                        H.*, 
                        ISNULL(E.Nombre + ' ' + E.Apellido, 'ID: ' + CAST(H.Id_empleado AS VARCHAR)) AS NombreCompletoEmpleado 
                    FROM 
                        PlanillaHistorial H 
                    LEFT JOIN  
                        Empleado E ON H.Id_empleado = E.Id_empleado 
                    ORDER BY 
                        H.FechaCalculo DESC";

                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new L_Planilla()
                        {
                            Id_planilla = Convert.ToInt32(dr["Id_planilla"]),
                            Id_empleado = Convert.ToInt32(dr["Id_empleado"]),
                            NombreCompletoEmpleado = dr["NombreCompletoEmpleado"].ToString(),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),

                            DiasTrabajados = dr["DiasTrabajados"] != DBNull.Value ? Convert.ToDecimal(dr["DiasTrabajados"]) : 0m,
                            HorasExtrasCantidad = dr["HorasExtrasCantidad"] != DBNull.Value ? Convert.ToDecimal(dr["HorasExtrasCantidad"]) : 0m,
                            Incentivos = dr["Incentivos"] != DBNull.Value ? Convert.ToDecimal(dr["Incentivos"]) : 0m,
                            Vacaciones = dr["Vacaciones"] != DBNull.Value ? Convert.ToDecimal(dr["Vacaciones"]) : 0m,
                            OtrasDeducciones = dr["OtrasDeducciones"] != DBNull.Value ? Convert.ToDecimal(dr["OtrasDeducciones"]) : 0m,
                            DeduccionINSS = dr["DeduccionINSS"] != DBNull.Value ? Convert.ToDecimal(dr["DeduccionINSS"]) : 0m,
                            DeduccionIR = dr["DeduccionIR"] != DBNull.Value ? Convert.ToDecimal(dr["DeduccionIR"]) : 0m,
                            TotalDeducciones = dr["TotalDeducciones"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDeducciones"]) : 0m,
                            TotalDevengado = Convert.ToDecimal(dr["TotalDevengado"]),
                            PagoNeto = Convert.ToDecimal(dr["PagoNeto"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO AL CARGAR HISTORIAL: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista = new List<L_Planilla>();
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        // ========================= 4. OBTENER REGISTRO POR ID (PARA EDICIÓN) =========================
        public L_Planilla ObtenerPlanillaPorId(int idPlanilla)
        {
            L_Planilla oPlanilla = null;
            try
            {
                Conexion.abrir();

                string consulta = @"
                    SELECT H.*, E.Nombre + ' ' + E.Apellido AS NombreCompletoEmpleado 
                    FROM PlanillaHistorial H 
                    LEFT JOIN Empleado E ON H.Id_empleado = E.Id_empleado 
                    WHERE H.Id_planilla = @Id_planilla";

                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id_planilla", idPlanilla);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        oPlanilla = new L_Planilla
                        {
                            Id_planilla = Convert.ToInt32(dr["Id_planilla"]),
                            Id_empleado = Convert.ToInt32(dr["Id_empleado"]),
                            FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                            FechaFin = Convert.ToDateTime(dr["FechaFin"]),
                            SalarioDiario = dr["SalarioDiario"] != DBNull.Value ? Convert.ToDecimal(dr["SalarioDiario"]) : 0m,
                            DiasTrabajados = dr["DiasTrabajados"] != DBNull.Value ? Convert.ToDecimal(dr["DiasTrabajados"]) : 0m,
                            HorasExtrasCantidad = dr["HorasExtrasCantidad"] != DBNull.Value ? Convert.ToDecimal(dr["HorasExtrasCantidad"]) : 0m,
                            Incentivos = dr["Incentivos"] != DBNull.Value ? Convert.ToDecimal(dr["Incentivos"]) : 0m,
                            Vacaciones = dr["Vacaciones"] != DBNull.Value ? Convert.ToDecimal(dr["Vacaciones"]) : 0m,
                            OtrasDeducciones = dr["OtrasDeducciones"] != DBNull.Value ? Convert.ToDecimal(dr["OtrasDeducciones"]) : 0m,
                            TotalDevengado = dr["TotalDevengado"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDevengado"]) : 0m,
                            DeduccionINSS = dr["DeduccionINSS"] != DBNull.Value ? Convert.ToDecimal(dr["DeduccionINSS"]) : 0m,
                            DeduccionIR = dr["DeduccionIR"] != DBNull.Value ? Convert.ToDecimal(dr["DeduccionIR"]) : 0m,
                            TotalDeducciones = dr["TotalDeducciones"] != DBNull.Value ? Convert.ToDecimal(dr["TotalDeducciones"]) : 0m,
                            PagoNeto = dr["PagoNeto"] != DBNull.Value ? Convert.ToDecimal(dr["PagoNeto"]) : 0m
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar planilla por ID: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                oPlanilla = null;
            }
            finally { Conexion.cerrar(); }
            return oPlanilla;
        }

        // ========================= 5. GUARDAR PLANILLA (Asumido) =========================
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

                // Parámetros INT y DATE
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                // Parámetros DECIMALES
                cmd.Parameters.Add("@SalarioDiario", SqlDbType.Decimal).Value = salarioDiario;
                cmd.Parameters.Add("@DiasTrabajados", SqlDbType.Decimal).Value = diasTrabajados;
                cmd.Parameters.Add("@HorasExtrasCantidad", SqlDbType.Decimal).Value = horasExtrasCantidad;
                cmd.Parameters.Add("@ValorHorasExtras", SqlDbType.Decimal).Value = valorHorasExtras;
                cmd.Parameters.Add("@TotalDevengado", SqlDbType.Decimal).Value = totalDevengado;
                cmd.Parameters.Add("@DeduccionINSS", SqlDbType.Decimal).Value = deduccionINSS;
                cmd.Parameters.Add("@DeduccionIR", SqlDbType.Decimal).Value = deduccionIR;
                cmd.Parameters.Add("@Incentivos", SqlDbType.Decimal).Value = incentivos;
                cmd.Parameters.Add("@Vacaciones", SqlDbType.Decimal).Value = vacaciones;
                cmd.Parameters.Add("@OtrasDeducciones", SqlDbType.Decimal).Value = otrasDeducciones;
                cmd.Parameters.Add("@TotalDeducciones", SqlDbType.Decimal).Value = totalDeducciones;
                cmd.Parameters.Add("@PagoNeto", SqlDbType.Decimal).Value = pagoNeto;

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO DE GUARDADO: " + ex.Message, "Error en Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                respuesta = false;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }

        // ========================= 6. ACTUALIZAR PLANILLA (SQL DIRECTO) =========================
        public bool ActualizarPlanilla(int idPlanilla, int idEmpleado, DateTime fechaInicio, DateTime fechaFin,
                                      decimal salarioDiario, decimal diasTrabajados, decimal horasExtrasCantidad,
                                      decimal valorHorasExtras, decimal totalDevengado, decimal deduccionINSS,
                                      decimal deduccionIR, decimal incentivos, decimal vacaciones,
                                      decimal otrasDeducciones, decimal totalDeducciones, decimal pagoNeto)
        {
            bool respuesta = false;
            try
            {
                Conexion.abrir();

                string consulta = @"
                    UPDATE PlanillaHistorial SET
                        Id_empleado = @Id_empleado,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin,
                        SalarioDiario = @SalarioDiario,
                        DiasTrabajados = @DiasTrabajados,
                        HorasExtrasCantidad = @HorasExtrasCantidad,
                        ValorHorasExtras = @ValorHorasExtras,
                        TotalDevengado = @TotalDevengado,
                        DeduccionINSS = @DeduccionINSS,
                        DeduccionIR = @DeduccionIR,
                        Incentivos = @Incentivos,
                        Vacaciones = @Vacaciones,
                        OtrasDeducciones = @OtrasDeducciones,
                        TotalDeducciones = @TotalDeducciones,
                        PagoNeto = @PagoNeto
                    WHERE Id_planilla = @Id_planilla";

                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.AddWithValue("@Id_planilla", idPlanilla);
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                cmd.Parameters.Add("@SalarioDiario", SqlDbType.Decimal).Value = salarioDiario;
                cmd.Parameters.Add("@DiasTrabajados", SqlDbType.Decimal).Value = diasTrabajados;
                cmd.Parameters.Add("@HorasExtrasCantidad", SqlDbType.Decimal).Value = horasExtrasCantidad;
                cmd.Parameters.Add("@ValorHorasExtras", SqlDbType.Decimal).Value = valorHorasExtras;
                cmd.Parameters.Add("@TotalDevengado", SqlDbType.Decimal).Value = totalDevengado;
                cmd.Parameters.Add("@DeduccionINSS", SqlDbType.Decimal).Value = deduccionINSS;
                cmd.Parameters.Add("@DeduccionIR", SqlDbType.Decimal).Value = deduccionIR;
                cmd.Parameters.Add("@Incentivos", SqlDbType.Decimal).Value = incentivos;
                cmd.Parameters.Add("@Vacaciones", SqlDbType.Decimal).Value = vacaciones;
                cmd.Parameters.Add("@OtrasDeducciones", SqlDbType.Decimal).Value = otrasDeducciones;
                cmd.Parameters.Add("@TotalDeducciones", SqlDbType.Decimal).Value = totalDeducciones;
                cmd.Parameters.Add("@PagoNeto", SqlDbType.Decimal).Value = pagoNeto;

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO DETALLADO SQL (Actualizar): " + ex.Message, "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                respuesta = false;
            }
            finally
            {
                Conexion.cerrar();
            }
            return respuesta;
        }


        public bool EliminarPlanilla(int idPlanilla)
        {
            bool respuesta = false;
            try
            {
                Conexion.abrir();

                string consulta = "DELETE FROM PlanillaHistorial WHERE Id_planilla = @IdPlanilla";
                SqlCommand cmd = new SqlCommand(consulta, Conexion.conectar);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IdPlanilla", idPlanilla);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO DE ELIMINACIÓN SQL: " + ex.Message, "ERROR CRÍTICO", MessageBoxButtons.OK, MessageBoxIcon.Error);
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