using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class Conexion
        
    {
        public static string conexion = @"Data Source = DESKTOP-RD302L4\SQLEXPRESS;Initial Catalog = Sega#1;Integrated Security=True";
        //public static string conexion = @"Data Source = ROA\SQLEXPRESS;Initial Catalog = Sega#1;Integrated Security=True";
        //public static string conexion = @"Data Source=192.168.13.13;Initial Catalog=Sega#1;User ID=FerLop;Password=1316;";
        public static SqlConnection conectar = new SqlConnection(conexion);
        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }
        public static void cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
