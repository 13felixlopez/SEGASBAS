namespace Capa_Presentacion.Logica
{
    public class L_Producto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }
        public string Descripcion { get; set; }

        public bool Estado { get; set; }


        public int StockActual { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal UltimoCostoFactura { get; set; }


        //public int Id_unidad { get; set; }
        //public int Id_marca { get; set; }
        //public int Id_categoria { get; set; }
    }
}
