namespace Capa_Presentacion.Logica
{
    public class L_Producto
    {
        // Identificador
        public int id_producto { get; set; }

        // Datos básicos
        public string nombre { get; set; }
        public string Descripcion { get; set; }

        // Estado (activo/inactivo)
        public bool Estado { get; set; }

        // Stock
        public decimal StockActual { get; set; }
        public decimal StockMinimo { get; set; } = 10.0M;


        public decimal CostoPromedio { get; set; }
        public decimal UltimoCostoFactura { get; set; }

  
        public int id_unidad { get; set; }
        public int id_marca { get; set; }
        public int id_categoria { get; set; }

        public L_Producto()
        {
            nombre = string.Empty;
            Descripcion = string.Empty;
            Estado = true;
            StockActual = 0;
            StockMinimo = 0;
            CostoPromedio = 0m;
            UltimoCostoFactura = 0m;
            id_unidad = 0;
            id_marca = 0;
            id_categoria = 0;
        }
    }
}
