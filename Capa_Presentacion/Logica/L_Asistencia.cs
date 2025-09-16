using System;

namespace Capa_Presentacion.Logica
{
    public class L_Asistencia
    {
        public int IDAsistencia { get; set; }
        public int IDEmpleado { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public int? IDActividad { get; set; }
        public string NombreActividad { get; set; }
        public int? IDLote { get; set; }
        public string NombreLote { get; set; }
        public int? IDCargo { get; set; }
        public string NombreCargo { get; set; }
        public string Estado { get; set; }
        public string Justificacion { get; set; }
        public decimal? Tolvadas { get; set; }
        public decimal? HorasExtras { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraRegistro { get; set; }
    }
}
