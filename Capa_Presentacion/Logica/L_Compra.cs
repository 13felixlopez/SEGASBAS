using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion.Logica
{
    public class L_Compra
    {
        public int IDCompra { get; set; }
        public int IDProveedor { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string TipoCompra { get; set; }
        public int? PlazoDias { get; set; }
        public DateTime? VencimientoFactura { get; set; }
        public string Descripcion { get; set; }
        public decimal TotalCompra { get; set; }
        public DateTime FechaRegistro { get; set; }


        public string Proveedor { get; set; }
        public string Producto { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitarioReal { get; set; } 
        public decimal TotalRealDetalle { get; set; }
        public decimal CostoPromedioActual { get; set; }
    }
}
