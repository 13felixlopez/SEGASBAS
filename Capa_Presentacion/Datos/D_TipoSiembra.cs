using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_TipoSiembra
    {
        public List<L_TipoSiembra> ObtenerTodosTiposSiembra()
        {
            List<L_TipoSiembra> lista = new List<L_TipoSiembra>();

            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ObtenerTodosTiposSiembra", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new L_TipoSiembra
                    {
                        Id_tipoSiembra = Convert.ToInt32(reader["id_tipo_siembra"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tipos de siembra: " + ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}
