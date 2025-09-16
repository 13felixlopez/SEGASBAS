using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class CD_Empleado
    {
        public List<KeyValuePair<int, string>> ObtenerEmpleados()
        {
            List<KeyValuePair<int, string>> lista = new List<KeyValuePair<int, string>>();
            try
            {
                using (SqlConnection oConexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("SELECT Id_empleado, Nombre + ' ' + Apellido AS NombreCompleto FROM Empleado ORDER BY NombreCompleto ASC", oConexion);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();

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
            }
            catch (Exception)
            {
                lista = new List<KeyValuePair<int, string>>();
            }
            return lista;
        }
        
        public DataTable ObtenerCargos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerCargos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable ObtenerEmpleadosPaginados(int pagina, int tamanoPagina, out int totalPaginas)
        {
            DataTable dt = new DataTable();
            totalPaginas = 0;
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadosPaginados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Pagina", pagina);
                cmd.Parameters.AddWithValue("@TamanoPagina", tamanoPagina);
                SqlParameter totalPaginasParam = cmd.Parameters.Add("@TotalPaginas", SqlDbType.Int);
                totalPaginasParam.Direction = ParameterDirection.Output;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                totalPaginas = Convert.ToInt32(totalPaginasParam.Value);
            }
            return dt;
        }

        public void InsertarEmpleado(CE_Empleado empleado)
        {
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarEmpleado", conexion);
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
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public CE_Empleado ObtenerEmpleadoPorId(int idEmpleado)
        {
            CE_Empleado empleado = null;
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleadoPorId", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado = new CE_Empleado
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
            return empleado;
        }

        public void ActualizarEmpleado(CE_Empleado empleado)
        {
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ActualizarEmpleado", conexion);
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
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_empleado", idEmpleado);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
