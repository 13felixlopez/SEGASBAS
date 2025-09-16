using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_EstadoCultivo
    {
        public List<CE_EstadoCultivo> ObtenerTodosEstadosCultivo()
        {
            List<CE_EstadoCultivo> lista = new List<CE_EstadoCultivo>();
            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ObtenerTodosEstadosCultivo", conexion);
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new CE_EstadoCultivo
                        {
                            Id_estado_cultivo = Convert.ToInt32(reader["id_estado_cultivo"]),
                            Nombre = reader["nombre"].ToString()
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener estados de cultivo: " + ex.Message);
            }
            return lista;
        }
    }
}
