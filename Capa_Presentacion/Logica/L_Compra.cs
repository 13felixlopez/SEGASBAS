using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion.Logica
{
    public class L_Compra
    {
        public int CompraID { get; set; }
        public int Id_proveedor { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string NumeroFactura { get; set; }
        public string TipoCompra { get; set; }
        public DateTime? VencimientoFactura { get; set; }
        public int? Plazo { get; set; }
        public decimal TotalCompra { get; set; }
        public int? IdUser { get; set; }
        public string DescripcionGeneral { get; set; }

        public string RazonSocialProveedor { get; set; }
    }
}
