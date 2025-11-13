using Capa_Presentacion.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmPlanillaAsegurado : Form
    {
        private D_Planilla objPlanillaDatos = new D_Planilla();

        private const decimal HORAS_LABORALES_DIA = 8.0m;
        private const decimal TASA_MULTIPLICADOR_HE = 2.00m; 
        private const decimal TASA_INSS_EMPLEADO = 0.0700m;
        public FrmPlanillaAsegurado()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            txtSalariopordia.ReadOnly = true;
            Txtdiastrabajados.ReadOnly = true;
            txtvalordehoras.ReadOnly = true;
            txtins.ReadOnly = true;
            txttotaldevengado.ReadOnly = true;
            txttotaldededucciones.ReadOnly = true;
            txtpagoneto.ReadOnly = true;
        }
        private void LlenarComboEmpleados()
        {
         
            var empleados = objPlanillaDatos.ObtenerEmpleadosIdYNombre();

            Cb_Nombre.DataSource = empleados;
            Cb_Nombre.DisplayMember = "Value";
            Cb_Nombre.ValueMember = "Key";

            Cb_Nombre.SelectedIndex = -1;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void LimpiarControlesPlanilla()
        {
            Cb_Nombre.SelectedIndex = -1;
            DtimeFechaInicio.Value = DateTime.Now.Date.AddDays(-15);
            Datetimefechafin.Value = DateTime.Now.Date;

          
            txtSalariopordia.Clear();
            Txtdiastrabajados.Clear();
            textvacaciones.Clear();
            txthorasextras.Clear();
            txtvalordehoras.Clear();
            txtIR.Clear();
            txtinsentivo.Clear();
            txtdeducciones.Clear();
            txttotaldevengado.Clear();
            txttotaldededucciones.Clear();
            txtins.Clear();
            txtpagoneto.Clear();

            Datagreedplanilla.DataSource = null;
            BTAgregar.Text = "Generar";
            BTEditar.Enabled = false;
            BTEliminar.Enabled = false;
        }
 

private void CalcularPlanilla(List<L_Planilla> detalle)
        {
            if (!detalle.Any())
            {
                LimpiarControlesPlanilla();
                MessageBox.Show("No se encontró asistencia para el período.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal salarioDiario = detalle.First().SalarioDiario;
            decimal valorHoraOrdinaria = salarioDiario / HORAS_LABORALES_DIA;
            decimal valorHoraExtraUnitario = valorHoraOrdinaria * TASA_MULTIPLICADOR_HE;

            decimal diasTrabajados = 0;
            decimal diasPagadosNoTrabajados = 0;
            decimal valorTotalHorasExtrasCalculado = 0;
            decimal totalHorasExtrasReportadas = 0;

            foreach (var d in detalle)
            {
                diasTrabajados += d.DiasTrabajadosContables;
                diasPagadosNoTrabajados += d.DiasPagadosNoTrabajados;

            
                d.ValorHorasExtrasDia = d.HorasExtras * valorHoraExtraUnitario;
                valorTotalHorasExtrasCalculado += d.ValorHorasExtrasDia; 
                totalHorasExtrasReportadas += d.HorasExtras;

             
                if (d.DiasTrabajadosContables == 1 || d.DiasPagadosNoTrabajados == 1)
                {
                    d.PagoBrutoDia = salarioDiario;
                    d.TipoPagoDia = d.DiasTrabajadosContables == 1 ? "Trabajado" : "Subsidiado";
                }
                else
                {
                    d.PagoBrutoDia = 0m;
                    d.TipoPagoDia = "No Pagado";
                }
            }

       
            decimal diasTotalesPagados = diasTrabajados + diasPagadosNoTrabajados;
            decimal salarioOrdinarioBruto = diasTotalesPagados * salarioDiario;

            decimal totalIncentivosManual = string.IsNullOrEmpty(txtinsentivo.Text) ? 0m : Convert.ToDecimal(txtinsentivo.Text);
            decimal totalVacaciones = string.IsNullOrEmpty(textvacaciones.Text) ? 0m : Convert.ToDecimal(textvacaciones.Text);
            decimal deduccionIR = string.IsNullOrEmpty(txtIR.Text) ? 0m : Convert.ToDecimal(txtIR.Text);
            decimal otrasDeducciones = string.IsNullOrEmpty(txtdeducciones.Text) ? 0m : Convert.ToDecimal(txtdeducciones.Text);

       
            decimal totalDevengado = salarioOrdinarioBruto + valorTotalHorasExtrasCalculado + totalIncentivosManual + totalVacaciones;

            decimal deduccionINSS = totalDevengado * TASA_INSS_EMPLEADO;
            decimal totalDeducciones = deduccionINSS + deduccionIR + otrasDeducciones;

     
            decimal pagoNeto = totalDevengado - totalDeducciones;

        
            Txtdiastrabajados.Text = diasTrabajados.ToString("N0");
            txtSalariopordia.Text = salarioDiario.ToString("C2");

           
            txtvalordehoras.Text = valorHoraExtraUnitario.ToString("C2");
            txthorasextras.Text = totalHorasExtrasReportadas.ToString("N2");

            Totalhe.Text = valorTotalHorasExtrasCalculado.ToString("C2");

            txtins.Text = deduccionINSS.ToString("C2");
            txttotaldevengado.Text = totalDevengado.ToString("C2");
            txttotaldededucciones.Text = totalDeducciones.ToString("C2");
            txtpagoneto.Text = pagoNeto.ToString("C2");

           
            Datagreedplanilla.DataSource = null;
            Datagreedplanilla.DataSource = detalle;
        }
        private void ConfigurarDataGridView()
        {
            Datagreedplanilla.Columns.Clear();
            Datagreedplanilla.AutoGenerateColumns = false;

        
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Fecha", HeaderText = "Fecha", DataPropertyName = "Fecha", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado" });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoPagoDia", HeaderText = "Tipo Pago", DataPropertyName = "TipoPagoDia" });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "H_Extras", HeaderText = "H. Extras (Cant.)", DataPropertyName = "HorasExtras", DefaultCellStyle = { Format = "N2" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ValorHE_Dia", HeaderText = "Valor HE Día", DataPropertyName = "ValorHorasExtrasDia", DefaultCellStyle = { Format = "C2" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PagoBrutoDia", HeaderText = "Pago Bruto Día", DataPropertyName = "PagoBrutoDia", DefaultCellStyle = { Format = "C2" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Justificacion", HeaderText = "Justificación", DataPropertyName = "Justificacion" });
        }
        private void FrmPlanillaAsegurado_Load(object sender, EventArgs e)
        {
            LlenarComboEmpleados();
            ConfigurarDataGridView();
            LimpiarControlesPlanilla();

        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la operación y limpiar todos los datos de cálculo?",
                        "Confirmar Cancelación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimpiarControlesPlanilla();
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (Cb_Nombre.SelectedValue == null || (int)Cb_Nombre.SelectedValue == 0)
            {
                MessageBox.Show("Debe seleccionar un trabajador.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEmpleado = (int)Cb_Nombre.SelectedValue;
            DateTime fechaInicio = DtimeFechaInicio.Value.Date;
            DateTime fechaFin = Datetimefechafin.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Error de Filtro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            List<L_Planilla> detalle = objPlanillaDatos.GenerarDetallePlanilla(idEmpleado, fechaInicio, fechaFin);

            CalcularPlanilla(detalle);
        }

        private void btguardar_Click(object sender, EventArgs e)
        {
            if (BTEditar.Text == "Guardar Planilla" && Datagreedplanilla.DataSource != null)
            {
                if (Cb_Nombre.SelectedValue == null || string.IsNullOrEmpty(txtpagoneto.Text))
                {
                    MessageBox.Show("Primero debe generar la planilla.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Desea registrar esta planilla de pago final en el historial?",
                                    "Confirmar Guardado",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Func<TextBox, decimal> parseCurrency = (tb) =>
                        {
                            string text = tb.Text.Replace("C$", "").Replace(",", "").Trim();
                            return string.IsNullOrEmpty(text) ? 0m : Convert.ToDecimal(text);
                        };

                        // --- RECOLECCIÓN DE DATOS CALCULADOS DE LOS TEXTBOXES ---
                        int idEmpleado = (int)Cb_Nombre.SelectedValue;
                        DateTime fechaInicio = DtimeFechaInicio.Value.Date;
                        DateTime fechaFin = Datetimefechafin.Value.Date;

                        decimal salarioDiario = parseCurrency(txtSalariopordia);
                        decimal diasTrabajados = parseCurrency(Txtdiastrabajados);
                        decimal horasExtrasCantidad = parseCurrency(txthorasextras);
                        decimal valorHoraExtraUnitario = parseCurrency(txtvalordehoras);
                        decimal totalDevengado = parseCurrency(txttotaldevengado);
                        decimal deduccionINSS = parseCurrency(txtins);
                        decimal totalDeducciones = parseCurrency(txttotaldededucciones);
                        decimal pagoNeto = parseCurrency(txtpagoneto);

                        decimal incentivos = parseCurrency(txtinsentivo);
                        decimal vacaciones = parseCurrency(textvacaciones);
                        decimal deduccionIR = parseCurrency(txtIR);
                        decimal otrasDeducciones = parseCurrency(txtdeducciones);

                    
                        bool exito = objPlanillaDatos.GuardarPlanilla(
                            idEmpleado, fechaInicio, fechaFin, salarioDiario, diasTrabajados, horasExtrasCantidad,
                            valorHoraExtraUnitario, totalDevengado, deduccionINSS, deduccionIR, incentivos, vacaciones,
                            otrasDeducciones, totalDeducciones, pagoNeto
                        );

                        if (exito)
                        {
                            MessageBox.Show("Planilla guardada exitosamente en el historial.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarControlesPlanilla();
                        }
                        else
                        {
                          
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al preparar los datos para guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }
    }
}
