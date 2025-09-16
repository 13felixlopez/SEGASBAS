using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Capa_Presentacion.Datos
{
    public class D_EstadoCultivo
    {
        public List<L_EstadoCultivo> ObtenerTodosEstadosCultivo()
        {
            List<L_EstadoCultivo> lista = new List<L_EstadoCultivo>();
            try
            {
                Conexion.abrir();
                SqlCommand comando = new SqlCommand("sp_ObtenerTodosEstadosCultivo", Conexion.conectar);
                comando.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new L_EstadoCultivo
                    {
                        Id_estado_cultivo = Convert.ToInt32(reader["id_estado_cultivo"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener estados de cultivo: " + ex.Message);
            }
            finally
            {
                Conexion.cerrar();
            }
            return lista;
        }
    }
}
