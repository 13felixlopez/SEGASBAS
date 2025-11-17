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

    public int Id_planilla { get; set; }

    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }

    public int Id_empleado { get; set; } 
    public decimal TotalDeducciones { get; set; }

    public decimal DeduccionINSS { get; set; } 
    public decimal DeduccionIR { get; set; }

    public decimal Incentivos { get; set; } 
    public decimal Vacaciones { get; set; } 
    public decimal OtrasDeducciones { get; set; }

    public decimal DiasTrabajados { get; set; } 
    public decimal HorasExtrasCantidad { get; set; }
    public decimal TotalDevengado { get; set; }
    public decimal PagoNeto { get; set; }
}