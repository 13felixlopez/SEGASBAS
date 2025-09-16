using System;

namespace Capa_Presentacion.Logica
{
    public class L_Lote
    {
        public int IDLote { get; set; }
        public string NombreLote { get; set; }
        public string Manzanaje { get; set; }
        public int? IDTipoSiembra { get; set; }
        public string TipoSiembra { get; set; }
        public DateTime? FechaSiembra { get; set; }
        public DateTime? FechaCorte { get; set; }
        public int IDEstadoCultivo { get; set; }
        public string EstadoCultivo { get; set; }
        public int? IDCiclo { get; set; }
        public string Ciclo { get; set; }
        public string Observacion { get; set; }
        public string VariedadArroz { get; set; }
    }
}
