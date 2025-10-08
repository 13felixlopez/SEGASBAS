using System.Data;
using System.Data.SqlClient;

namespace Capa_Presentacion.Datos
{
    public class D_Catalogo
    {
  
        public DataTable ListarProductosCatalogo()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodosProductosCatalogo", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch { dt = new DataTable(); }
            finally { Conexion.cerrar(); }
            return dt;
        }

     
        public DataTable ListarProveedoresCatalogo()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodosProveedoresCatalogo", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch { dt = new DataTable(); }
            finally { Conexion.cerrar(); }
            return dt;
        }

   
        public DataTable ListarMarcasCatalogo()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodasMarcas", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch { dt = new DataTable(); }
            finally { Conexion.cerrar(); }
            return dt;
        }

     
        public DataTable ListarCategoriasCatalogo()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("sp_ObtenerTodasCategorias", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch { dt = new DataTable(); }
            finally { Conexion.cerrar(); }
            return dt;
        }

     
        public DataTable ListarUnidadesMedidaCatalogo()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                
                SqlCommand cmd = new SqlCommand("sp_ListarUnidadesPaginado", Conexion.conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@paginaActual", 1);
                cmd.Parameters.AddWithValue("@tamanoPagina", 9999); 
                cmd.Parameters.Add("@totalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            catch { dt = new DataTable(); }
            finally { Conexion.cerrar(); }
            return dt;
        }
    }
}