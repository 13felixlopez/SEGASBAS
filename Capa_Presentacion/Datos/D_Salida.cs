using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Capa_Presentacion.Datos
{
    public class D_Salida
    {
        // ------------------------------
        // Helpers: obtener id por nombre
        // ------------------------------
        private int ObtenerIdProductoPorNombre(string nombre)
        {
            int id = 0;
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id_producto FROM Producto WHERE nombre = @nombre", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value)
                        id = Convert.ToInt32(res);
                }
            }
            finally { Conexion.cerrar(); }
            return id;
        }

        // ------------------------------
        // Métodos para llenar combos
        // ------------------------------
        public List<string> ObtenerNombresProductos()
        {
            List<string> lista = new List<string>();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM Producto WHERE Estado = 1 ORDER BY nombre", Conexion.conectar))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(dr["nombre"].ToString());
                        }
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<string> ObtenerNombresLotes()
        {
            List<string> lista = new List<string>();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT nombre FROM Lote ORDER BY nombre", Conexion.conectar))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(dr["nombre"].ToString());
                        }
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        public List<string> ObtenerDescripcionesCiclos()
        {
            List<string> lista = new List<string>();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT descripcion FROM Ciclo ORDER BY descripcion", Conexion.conectar))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(dr["descripcion"].ToString());
                        }
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return lista;
        }

        // ------------------------------
        // Obtener datos del producto (para rellenar campos: costo, categoria, unidad)
        // ------------------------------
        public L_Salida ObtenerDatosProductoParaFormulario(string nombreProducto)
        {
            L_Salida salida = null;
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    SELECT p.id_producto, p.nombre, p.CostoPromedio, c.nombre AS NombreCategoria, u.abreviatura AS AbreviaturaUnidad
                    FROM Producto p
                    LEFT JOIN Categoria c ON p.id_categoria = c.id_categoria
                    LEFT JOIN Unidad_Medida u ON p.id_unidad = u.id_unidad
                    WHERE p.nombre = @nombre
                ", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreProducto);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            salida = new L_Salida
                            {
                                IdProducto = dr["id_producto"] != DBNull.Value ? Convert.ToInt32(dr["id_producto"]) : 0,
                                NombreProducto = dr["nombre"].ToString(),
                                CostoUnitario = dr["CostoPromedio"] != DBNull.Value ? Convert.ToDecimal(dr["CostoPromedio"]) : 0M,
                                NombreCategoria = dr["NombreCategoria"] != DBNull.Value ? dr["NombreCategoria"].ToString() : "",
                                AbreviaturaUnidad = dr["AbreviaturaUnidad"] != DBNull.Value ? dr["AbreviaturaUnidad"].ToString() : ""
                            };
                        }
                    }
                }
            }
            finally { Conexion.cerrar(); }
            return salida;
        }

        // ------------------------------
        // Insertar salida (llama al SP transaccional que creaste)
        // Firma: bool InsertarSalida(L_Salida salida, out string mensaje)
        // Devuelve true si ok, false y mensaje si SP devolvió error
        // ------------------------------
        public bool InsertarSalida(L_Salida salida, out string mensaje)
        {
            mensaje = "";
            if (salida == null)
            {
                mensaje = "Datos de salida inválidos.";
                return false;
            }

            // Obtener id de producto (tu formulario manda nombre)
            int idProducto = ObtenerIdProductoPorNombre(salida.NombreProducto);
            if (idProducto == 0)
            {
                mensaje = "No se encontró el producto seleccionado.";
                return false;
            }

            // ---> Obtener id de lote y ciclo ANTES de abrir la conexión compartida
            int idLote = 0;
            int idCiclo = 0;
            if (!string.IsNullOrEmpty(salida.NombreLoteDestino))
                idLote = ObtenerIdLotePorNombre(salida.NombreLoteDestino); // usa su propia abrir/cerrar
            if (!string.IsNullOrEmpty(salida.DescripcionCiclo))
                idCiclo = ObtenerIdCicloPorDescripcion(salida.DescripcionCiclo); // usa su propia abrir/cerrar

            // Ahora abrimos la conexión UNA VEZ y ejecutamos el SP sin llamar a helpers que cierren la conexión
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertarSalida", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@p_id_lote_destino", idLote); // idLote calculado previamente (0 si no)
                    cmd.Parameters.AddWithValue("@p_id_ciclo", idCiclo);       // idCiclo (0 si no)

                    cmd.Parameters.AddWithValue("@p_Cantidad", salida.Cantidad);
                    cmd.Parameters.AddWithValue("@p_CostoUnitario", salida.CostoUnitario);
                    cmd.Parameters.AddWithValue("@p_FechaSalida", salida.FechaSalida);
                    cmd.Parameters.AddWithValue("@p_TipoCompra", "");
                    cmd.Parameters.AddWithValue("@p_Descripcion", string.IsNullOrEmpty(salida.Descripcion) ? "" : salida.Descripcion);
                    cmd.Parameters.AddWithValue("@p_Usuario", Environment.UserName);

                    SqlParameter outRes = new SqlParameter("@p_resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outMsg = new SqlParameter("@p_mensaje", SqlDbType.VarChar, 500) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outRes);
                    cmd.Parameters.Add(outMsg);

                    cmd.ExecuteNonQuery();

                    int resultado = outRes.Value != DBNull.Value && outRes.Value != null ? Convert.ToInt32(outRes.Value) : 0;
                    mensaje = outMsg.Value?.ToString() ?? "";

                    return resultado == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en InsertarSalida: " + ex.Message, ex);
            }
            finally
            {
                Conexion.cerrar();
            }
        }
        // ------------------------------
        // Obtener id de lote / ciclo por nombre/descripcion (helpers)
        // ------------------------------
        private int ObtenerIdLotePorNombre(string nombreLote)
        {
            if (string.IsNullOrEmpty(nombreLote)) return 0;
            int id = 0;
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id_lote FROM Lote WHERE nombre = @nombre", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreLote);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) id = Convert.ToInt32(res);
                }
            }
            finally { Conexion.cerrar(); }
            return id;
        }

        private int ObtenerIdCicloPorDescripcion(string desc)
        {
            if (string.IsNullOrEmpty(desc)) return 0;
            int id = 0;
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 id_ciclo FROM Ciclo WHERE descripcion = @desc", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@desc", desc);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) id = Convert.ToInt32(res);
                }
            }
            finally { Conexion.cerrar(); }
            return id;
        }
        public int ObtenerStockActualPorNombre(string nombreProducto)
        {
            int stock = 0;
            if (string.IsNullOrWhiteSpace(nombreProducto)) return 0;

            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 ISNULL(StockActual, 0) FROM Producto WHERE nombre = @nombre", Conexion.conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreProducto);
                    object res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) stock = Convert.ToInt32(res);
                }
            }
            finally
            {
                Conexion.cerrar();
            }
            return stock;
        }
        // ------------------------------
        // Listado paginado de salidas (C#-side paging usando sp_ListarSalidas)
        // ------------------------------
        // firma que usas: ObtenerSalidasPaginadas(pagina, tamano, busqueda, campo, out totalRegistros)
        public List<L_Salida> ObtenerSalidasPaginadas(int pagina, int tamano, string textoBusqueda, string campoBusqueda, out int totalRegistros)
        {
            totalRegistros = 0;
            List<L_Salida> lista = new List<L_Salida>();

            DataTable dt = new DataTable();
            Conexion.abrir();
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_ListarSalidas", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally { Conexion.cerrar(); }

            // Transformar a lista de L_Salida
            List<L_Salida> todos = new List<L_Salida>();
            foreach (DataRow r in dt.Rows)
            {
                var item = new L_Salida
                {
                    IDSalida = r.Table.Columns.Contains("id_salida") && r["id_salida"] != DBNull.Value ? Convert.ToInt32(r["id_salida"]) : 0,
                    IdProducto = r.Table.Columns.Contains("id_producto") && r["id_producto"] != DBNull.Value ? Convert.ToInt32(r["id_producto"]) : 0,
                    NombreProducto = r.Table.Columns.Contains("Producto") ? r["Producto"].ToString() : (r.Table.Columns.Contains("NombreProducto") ? r["NombreProducto"].ToString() : ""),
                    NombreLoteDestino = r.Table.Columns.Contains("LoteDestino") ? r["LoteDestino"].ToString() : (r.Table.Columns.Contains("NombreLoteDestino") ? r["NombreLoteDestino"].ToString() : ""),
                    Cantidad = r.Table.Columns.Contains("Cantidad") && r["Cantidad"] != DBNull.Value ? Convert.ToInt32(r["Cantidad"]) : 0,
                    CostoUnitario = r.Table.Columns.Contains("CostoUnitario") && r["CostoUnitario"] != DBNull.Value ? Convert.ToDecimal(r["CostoUnitario"]) : 0M,
                    FechaSalida = r.Table.Columns.Contains("FechaSalida") && r["FechaSalida"] != DBNull.Value ? Convert.ToDateTime(r["FechaSalida"]) : DateTime.MinValue,
                    Descripcion = r.Table.Columns.Contains("Descripcion") ? r["Descripcion"].ToString() : ""
                };
                todos.Add(item);
            }


            // Aplicar filtro si se solicitó
            IEnumerable<L_Salida> filtrados = todos;
            if (!string.IsNullOrWhiteSpace(textoBusqueda) && !string.IsNullOrWhiteSpace(campoBusqueda))
            {
                string tb = textoBusqueda.Trim().ToLower();
                switch (campoBusqueda)
                {
                    case "Producto":
                        filtrados = todos.FindAll(x => (x.NombreProducto ?? "").ToLower().Contains(tb));
                        break;
                    case "Destino":
                        filtrados = todos.FindAll(x => (x.NombreLoteDestino ?? "").ToLower().Contains(tb));
                        break;
                    case "Ciclo":
                        filtrados = todos.FindAll(x => (x.DescripcionCiclo ?? "").ToLower().Contains(tb));
                        break;
                    default:
                        // búsqueda general
                        filtrados = todos.FindAll(x =>
                            (x.NombreProducto ?? "").ToLower().Contains(tb) ||
                            (x.NombreLoteDestino ?? "").ToLower().Contains(tb) ||
                            (x.Descripcion ?? "").ToLower().Contains(tb));
                        break;
                }
            }

            totalRegistros = filtrados == null ? 0 : System.Linq.Enumerable.Count(filtrados);

            // Paginación
            int skip = (pagina - 1) * tamano;
            var page = System.Linq.Enumerable.Skip(filtrados, skip).Take(tamano);

            lista.AddRange(page);

            return lista;
        }
    }
}
