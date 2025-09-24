using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Presentacion.Logica
{
    public class L_Proveedor
    {
        public int id_proveedor { get; set; }
        public string numero_registro { get; set; }
        public string razon_social { get; set; }
        public string numero_ruc { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public string visitador { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_registro { get; set; }
        public bool es_producto { get; set; }
        public bool es_servicio { get; set; }
    }
}