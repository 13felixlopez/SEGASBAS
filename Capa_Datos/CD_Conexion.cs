using System.Configuration;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class CD_Conexion
     {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["Conexión"].ConnectionString;
            return new SqlConnection(cadena);
        }

     }
}
