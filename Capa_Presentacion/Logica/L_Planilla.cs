using System;

public class L_Planilla
{

    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public string Justificacion { get; set; }
    public decimal HorasExtras { get; set; }
    public decimal Tolvadas { get; set; }
    public decimal SalarioDiario { get; set; }
    public string NombreCompletoEmpleado { get; set; }

   
    public int DiasTrabajadosContables { get; set; }
    public int DiasPagadosNoTrabajados { get; set; }

    
    public decimal PagoBrutoDia { get; set; }
    public decimal ValorHorasExtrasDia { get; set; }
    public string TipoPagoDia { get; set; }
}