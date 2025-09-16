using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class CD_TipoSiembra
    {
        public List<CE_TipoSiembra> ObtenerTodosTiposSiembra()
        {
            List<CE_TipoSiembra> lista = new List<CE_TipoSiembra>();

            try
            {
                using (SqlConnection conexion = CD_Conexion.ObtenerConexion())
                {
                    SqlCommand comando = new SqlCommand("sp_ObtenerTodosTiposSiembra", conexion);
                    comando.CommandType = CommandType.StoredProcedure;

                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        lista.Add(new CE_TipoSiembra
                        {
                            Id_tipoSiembra = Convert.ToInt32(reader["id_tipo_siembra"]),
                            Nombre = reader["nombre"].ToString()
                        });
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tipos de siembra: " + ex.Message);
            }

            return lista;
        }
    }
}