using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Capa_Presentacion.Datos
{
    public class D_Empleado
    {
        public List<KeyValuePair<int, string>> ObtenerEmpleados()
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
                        lista.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(dr["Id_empleado"]),
                            dr["NombreCompleto"].ToString()
                        ));
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<KeyValuePair<int, string>>();
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
        public DataTable BuscarEmpleados(string terminoBusqueda, string criterioBusqueda)
        {
            DataTable dt = new DataTable();
            // Se abre la conexión de manera manual, como lo haces en el resto de tu código
            Conexion.abrir();

            try
            {
                // Se usa el objeto de conexión estático
                using (SqlCommand cmd = new SqlCommand("sp_BuscarEmpleados", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@terminoBusqueda", terminoBusqueda);
                    cmd.Parameters.AddWithValue("@criterioBusqueda", criterioBusqueda);

                    // Usamos 'using' con el adaptador para asegurar su liberación
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // La capa de datos no muestra mensajes, lanza la excepción para que
                // la capa de presentación la maneje
                throw new Exception("Error al buscar empleados: " + ex.Message, ex);
            }
            finally
            {
                // Se asegura de que la conexión se cierre SIEMPRE
                Conexion.cerrar();
            }

            return dt;
        }

        public DataTable ObtenerCargos()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerCargos", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener cargos: " + ex.Message, "Error en ObtenerCargos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return dt;
        }

        public DataTable ObtenerEmpleadosPaginados(int pagina, int tamanoPagina, out int totalPaginas)
        {
            DataTable dt = new DataTable();
            totalPaginas = 0;

            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadosPaginados", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Pagina", pagina);
                    cmd.Parameters.AddWithValue("@TamanoPagina", tamanoPagina);

                    // Parámetro de salida
                    SqlParameter totalPaginasParam = cmd.Parameters.Add("@TotalPaginas", SqlDbType.Int);
                    totalPaginasParam.Direction = ParameterDirection.Output;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // Ahora sí puedes leerlo
                    if (totalPaginasParam.Value != DBNull.Value)
                        totalPaginas = Convert.ToInt32(totalPaginasParam.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener empleados con paginado: " + ex.Message, "Error en ObtenerEmpleadosPaginados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }

            return dt;
        }


        public void InsertarEmpleado(L_Empleado empleado)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_InsertarEmpleado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                cmd.Parameters.AddWithValue("@Sexo", empleado.Sexo);
                cmd.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@SalarioPorDia", empleado.SalarioPorDia);
                cmd.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                cmd.Parameters.AddWithValue("@TipoContrato", empleado.TipoContrato);
                cmd.Parameters.AddWithValue("@BeneficioSocial", empleado.BeneficioSocial);
                cmd.Parameters.AddWithValue("@Observacion", empleado.Observacion);
                cmd.Parameters.AddWithValue("@Id_cargo", empleado.Id_cargo);
                cmd.Parameters.AddWithValue("@SalarioCatorcenal", empleado.SalarioCatorcenal);
                cmd.Parameters.AddWithValue("@SalarioQuincenal", empleado.SalarioQuincenal);
                cmd.Parameters.AddWithValue("@SalarioMensual", empleado.SalarioMensual);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logró insertar empleado " + ex.Message, "Error al Insertar Empleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }

        public L_Empleado ObtenerEmpleadoPorId(int idEmpleado)
        {
            L_Empleado empleado = null;
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadoPorId", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new L_Empleado
                    {
                        Id_empleado = Convert.ToInt32(reader["Id_empleado"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Cedula = reader["Cedula"].ToString(),
                        Sexo = reader["Sexo"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        SalarioPorDia = Convert.ToDecimal(reader["SalarioPorDia"]),
                        FechaIngreso = Convert.ToDateTime(reader["FechaIngreso"]),
                        TipoContrato = reader["TipoContrato"].ToString(),
                        BeneficioSocial = reader["BeneficioSocial"].ToString(),
                        Observacion = reader["Observacion"].ToString(),
                        Id_cargo = Convert.ToInt32(reader["Id_cargo"]),
                        SalarioCatorcenal = Convert.ToDecimal(reader["SalarioCatorcenal"]),
                        SalarioQuincenal = Convert.ToDecimal(reader["SalarioQuincenal"]),
                        SalarioMensual = Convert.ToDecimal(reader["SalarioMensual"])
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al Obtener el Emplado seleccionado " + ex.Message, "Error en ObtenerEmpleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
            return empleado;
        }

        public void ActualizarEmpleado(L_Empleado empleado)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ActualizarEmpleado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_empleado", empleado.Id_empleado);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                cmd.Parameters.AddWithValue("@Cedula", empleado.Cedula);
                cmd.Parameters.AddWithValue("@Sexo", empleado.Sexo);
                cmd.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@SalarioPorDia", empleado.SalarioPorDia);
                cmd.Parameters.AddWithValue("@FechaIngreso", empleado.FechaIngreso);
                cmd.Parameters.AddWithValue("@TipoContrato", empleado.TipoContrato);
                cmd.Parameters.AddWithValue("@BeneficioSocial", empleado.BeneficioSocial);
                cmd.Parameters.AddWithValue("@Observacion", empleado.Observacion);
                cmd.Parameters.AddWithValue("@Id_cargo", empleado.Id_cargo);
                cmd.Parameters.AddWithValue("@SalarioCatorcenal", empleado.SalarioCatorcenal);
                cmd.Parameters.AddWithValue("@SalarioQuincenal", empleado.SalarioQuincenal);
                cmd.Parameters.AddWithValue("@SalarioMensual", empleado.SalarioMensual);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el empleado seleccionado " + ex.Message, "Error en ActualizarEmpleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                Conexion.abrir(); ;
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar el empleado seleccionado " + ex.Message, "Error en EliminarEmpleado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        public void ReporteEmpleado(ref DataTable dt)
        {
            try
            {
                Conexion.abrir();
                using (SqlDataAdapter da = new SqlDataAdapter("ReporteEmpleado", Conexion.conectar))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar los datos de Empleado: " + ex.Message, "Error en Cargar Datos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }
        }

        public DataTable ObtenerTodosEmpleados()
        {
            DataTable dt = new DataTable();

            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTodosEmpleados", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener empleados: " + ex.Message,
                    "Error en ObtenerTodosEmpleados", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Conexion.cerrar();
            }

            return dt;
        }
    }
}
