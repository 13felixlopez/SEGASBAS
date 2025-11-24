namespace Capa_Presentacion.Logica
{
    public class L_Salida
    {
        public int IDSalida { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public System.DateTime FechaSalida { get; set; }
        public string NombreLoteDestino { get; set; }
        public string DescripcionCiclo { get; set; }
        public string Descripcion { get; set; }

        // Campos auxiliares para mostrar en la UI
        public string NombreCategoria { get; set; }
        public string AbreviaturaUnidad { get; set; }
    }
}
