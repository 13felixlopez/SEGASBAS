using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion.Logica
{
    public class L_DetalleCompra
    {
        public int DetalleID { get; set; }
        public int CompraID { get; set; }
        public int Id_producto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal SubTotal { get; set; }

        public string NombreProducto { get; set; }
    


     

        public int? Id_unidad { get; set; }
        public int? Id_marca { get; set; }
        public int? Id_categoria { get; set; }

       

        public string NombreMarca { get; set; }
        public string NombreCategoria { get; set; }
        public string NumeroFactura { get; set; }
    }
}
