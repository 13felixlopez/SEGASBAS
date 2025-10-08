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
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; } 
        public decimal SubTotal { get; set; }

        public string NombreProducto { get; set; }
    }
}
