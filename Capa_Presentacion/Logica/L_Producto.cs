namespace Capa_Presentacion.Logica
{
    public class L_Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockMinimo { get; set; } = 10.0M;
        public decimal CostoPromedio { get; set; }
        public decimal UltimoCostoFactura { get; set; }
        public string NombreUnidad { get; set; } = "";
        public string NombreCategoria { get; set; } = "";
        public int id_unidad { get; set; }
        public int id_marca { get; set; }
        public int id_categoria { get; set; }

        // NUEVO campo para controlar si se maneja stock
        public bool ControlStock { get; set; } = true;

        public L_Producto()
        {
            nombre = string.Empty;
            Descripcion = string.Empty;
            Estado = true;
            StockActual = 0M;
            CostoPromedio = 0m;
            UltimoCostoFactura = 0m;
            id_unidad = 0;
            id_marca = 0;
            id_categoria = 0;
        }
    }
}
