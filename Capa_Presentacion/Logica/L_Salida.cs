using System;

namespace Capa_Presentacion.Logica
{
    public class L_Salida
    {
 
        public int IDSalida { get; set; }
        public DateTime FechaSalida { get; set; }
        public int Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }


        public string Descripcion { get; set; }

     
        public string NombreProducto { get; set; }
        public string NombreLoteDestino { get; set; }
        public string DescripcionCiclo { get; set; }
        public string NombreCategoria { get; set; }
        public string AbreviaturaUnidad { get; set; }
    }
}