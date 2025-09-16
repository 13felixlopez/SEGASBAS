using System;

namespace Capa_Presentacion.Logica
{
    public class L_Empleado
    {
        public int Id_empleado { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal SalarioPorDia { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string TipoContrato { get; set; }
        public string BeneficioSocial { get; set; }
        public string Observacion { get; set; }
        public int Id_cargo { get; set; }
        public decimal SalarioCatorcenal { get; set; }
        public decimal SalarioQuincenal { get; set; }
        public decimal SalarioMensual { get; set; }
    }
}
