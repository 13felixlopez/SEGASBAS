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

        private int idPlanillaSeleccionada = 0;
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
                
            txthorasextras.ReadOnly = true;

            btguardar.Text = "Guardar Planilla";
          
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
            btguardar.Enabled = true;
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

         
            Func<TextBox, decimal> parseCurrencyOrRate = (tb) =>
            {
                string text = tb.Text.Replace("C$", "").Replace(",", "").Trim();
                return string.IsNullOrEmpty(text) ? 0m : Convert.ToDecimal(text);
            };

     
            decimal totalIncentivosManual = parseCurrencyOrRate(txtinsentivo);
            decimal totalVacaciones = parseCurrencyOrRate(textvacaciones);
            decimal totalDevengado = salarioOrdinarioBruto + valorTotalHorasExtrasCalculado + totalIncentivosManual + totalVacaciones;

            decimal deduccionINSS = totalDevengado * TASA_INSS_EMPLEADO;

           
            decimal baseGravableIR = totalDevengado - deduccionINSS;
            decimal tasaIRPorcentaje = parseCurrencyOrRate(txtIR);

    
            decimal valorIRCalculado = baseGravableIR * (tasaIRPorcentaje / 100m);

         
            decimal otrasDeducciones = parseCurrencyOrRate(txtdeducciones);

 
            decimal totalDeducciones = deduccionINSS + valorIRCalculado + otrasDeducciones;
            decimal pagoNeto = totalDevengado - totalDeducciones;

         
            Txtdiastrabajados.Text = diasTrabajados.ToString("N0");
            txtSalariopordia.Text = salarioDiario.ToString("C2");
            txtvalordehoras.Text = valorHoraExtraUnitario.ToString("C2");
            txthorasextras.Text = totalHorasExtrasReportadas.ToString("N2");


            Totalhe.Text = valorTotalHorasExtrasCalculado.ToString("C2");

         
            txtIR.Text = valorIRCalculado.ToString("C2");

            txtins.Text = deduccionINSS.ToString("C2");
            txttotaldevengado.Text = totalDevengado.ToString("C2");
            txttotaldededucciones.Text = totalDeducciones.ToString("C2");
            txtpagoneto.Text = pagoNeto.ToString("C2");
        }
        private void ConfigurarDataGridView()
        {
            Datagreedplanilla.Columns.Clear();
            Datagreedplanilla.AutoGenerateColumns = false;

            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Id_planilla", HeaderText = "ID Planilla", DataPropertyName = "Id_planilla", Visible = true }); // Visible: true
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Empleado", HeaderText = "Nombre del Trabajador", DataPropertyName = "NombreCompletoEmpleado" });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaInicio", HeaderText = "Desde", DataPropertyName = "FechaInicio", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaFin", HeaderText = "Hasta", DataPropertyName = "FechaFin", DefaultCellStyle = { Format = "dd/MM/yyyy" } });

          
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DiasTrabajados", HeaderText = "Días Trab.", DataPropertyName = "DiasTrabajados", DefaultCellStyle = { Format = "N0" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "HorasExtrasCantidad", HeaderText = "H.E. Cant.", DataPropertyName = "HorasExtrasCantidad", DefaultCellStyle = { Format = "N2" } });

            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Devengado", HeaderText = "Total Devengado", DataPropertyName = "TotalDevengado", DefaultCellStyle = { Format = "C2" } });

        
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DeduccionINSS", HeaderText = "INSS", DataPropertyName = "DeduccionINSS", DefaultCellStyle = { Format = "C2" } });
            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "DeduccionIR", HeaderText = "IR", DataPropertyName = "DeduccionIR", DefaultCellStyle = { Format = "C2" } });

            Datagreedplanilla.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PagoNeto", HeaderText = "Pago Neto", DataPropertyName = "PagoNeto", DefaultCellStyle = { Format = "C2" } });

          
            CargarHistorialPlanillas();
        }
        private void CargarHistorialPlanillas()
        {
            try
            {
                
                Datagreedplanilla.DataSource = objPlanillaDatos.ObtenerHistorialPlanillas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO AL CARGAR HISTORIAL: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Datagreedplanilla.DataSource = null;
            }
        }
        private void FrmPlanillaAsegurado_Load(object sender, EventArgs e)
        {
            LlenarComboEmpleados();
            ConfigurarDataGridView(); 
            LimpiarControlesPlanilla();

            
            CargarHistorialPlanillas();

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
            if (string.IsNullOrEmpty(txtpagoneto.Text) || Cb_Nombre.SelectedValue == null)
            {
                MessageBox.Show("Debe tener una planilla generada y un trabajador seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Usar la función parseCurrency para leer los valores de los TextBoxes
            Func<TextBox, decimal> parseCurrency = (tb) =>
            {
                string text = tb.Text.Replace("C$", "").Replace(",", "").Trim();
                return string.IsNullOrEmpty(text) ? 0m : Convert.ToDecimal(text);
            };

            // --- RECOLECCIÓN DE DATOS PARA EL CÁLCULO Y ACTUALIZACIÓN ---

            int idEmpleado = (int)Cb_Nombre.SelectedValue;
            DateTime fechaInicio = DtimeFechaInicio.Value.Date;
            DateTime fechaFin = Datetimefechafin.Value.Date;

            List<L_Planilla> detalleRecalculo = objPlanillaDatos.GenerarDetallePlanilla(idEmpleado, fechaInicio, fechaFin);


            if (detalleRecalculo != null && detalleRecalculo.Any())
            {
                CalcularPlanilla(detalleRecalculo);
            }

            decimal salarioDiario = parseCurrency(txtSalariopordia);
            decimal diasTrabajados = parseCurrency(Txtdiastrabajados);
            decimal horasExtrasCantidad = parseCurrency(txthorasextras);
            decimal valorHoraExtraUnitario = parseCurrency(txtvalordehoras);

            
            decimal incentivos = parseCurrency(txtinsentivo);
            decimal vacaciones = parseCurrency(textvacaciones);
            decimal otrasDeducciones = parseCurrency(txtdeducciones);

            decimal totalDevengado = parseCurrency(txttotaldevengado);
            decimal deduccionINSS = parseCurrency(txtins);
            decimal deduccionIR = parseCurrency(txtIR);
            decimal totalDeducciones = parseCurrency(txttotaldededucciones);
            decimal pagoNeto = parseCurrency(txtpagoneto);

            // --- LÓGICA DE GUARDAR / ACTUALIZAR ---

            bool operacionExitosa = false;
            string titulo = "";

            if (idPlanillaSeleccionada > 0)
            {
                // MODO ACTUALIZAR
                operacionExitosa = objPlanillaDatos.ActualizarPlanilla(
                    idPlanillaSeleccionada, idEmpleado, fechaInicio, fechaFin,
                    salarioDiario, diasTrabajados, horasExtrasCantidad, valorHoraExtraUnitario,
                    totalDevengado, deduccionINSS, deduccionIR, incentivos, vacaciones,
                    otrasDeducciones, totalDeducciones, pagoNeto);

                titulo = operacionExitosa ? "Actualización Exitosa" : "Fallo de Actualización";
            }
            else
            {
                // MODO GUARDAR NUEVO
                operacionExitosa = objPlanillaDatos.GuardarPlanilla(
                    idEmpleado, fechaInicio, fechaFin, salarioDiario, diasTrabajados, horasExtrasCantidad,
                    valorHoraExtraUnitario, totalDevengado, deduccionINSS, deduccionIR, incentivos, vacaciones,
                    otrasDeducciones, totalDeducciones, pagoNeto);

                titulo = operacionExitosa ? "Guardado Exitoso" : "Fallo de Guardado";
            }

            // --- MANEJO POST-OPERACIÓN (CORRECCIÓN DE SINTAXIS EN EL MENSAJE) ---

            MessageBox.Show(
                $"Planilla {(idPlanillaSeleccionada > 0 ? "actualizada" : "guardada")} exitosamente.",
                titulo,
                MessageBoxButtons.OK,
                operacionExitosa ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );

            if (operacionExitosa)
            {
                // Restablecer el estado
                this.idPlanillaSeleccionada = 0;
                this.btguardar.Text = "Guardar Planilla";

                LimpiarControlesPlanilla();
                CargarHistorialPlanillas(); // Recargar el DataGridView
            }


        }

        private void txtIR_TextChanged(object sender, EventArgs e)
        {

        }

        private void Datagreedplanilla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
           
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;
            }
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (Datagreedplanilla.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un registro de planilla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPlanillaAEliminar = Convert.ToInt32(Datagreedplanilla.SelectedRows[0].Cells["Id_planilla"].Value);

            DialogResult resultado = MessageBox.Show($"¿Está seguro de eliminar la Planilla Histórica ID: {idPlanillaAEliminar}?",
                                "Confirmar Eliminación",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

            if (resultado == DialogResult.Yes)
            {
                if (objPlanillaDatos.EliminarPlanilla(idPlanillaAEliminar))
                {
                    MessageBox.Show("Planilla eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarControlesPlanilla();
                    CargarHistorialPlanillas();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la planilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (Datagreedplanilla.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un registro de planilla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Obtener el ID
            int idPlanilla = Convert.ToInt32(Datagreedplanilla.SelectedRows[0].Cells["Id_planilla"].Value);

            // 2. Obtener el objeto completo
            L_Planilla planillaAEditar = objPlanillaDatos.ObtenerPlanillaPorId(idPlanilla);

            if (planillaAEditar != null)
            {
                // 3. Establecer modo edición
                idPlanillaSeleccionada = planillaAEditar.Id_planilla;
                btguardar.Text = "Actualizar";

                // 4. Llenar los controles:

                // Combobox (CRUCIAL: Id_empleado debe estar mapeado)
                Cb_Nombre.SelectedValue = planillaAEditar.Id_empleado;

                // Fechas y Salario
                DtimeFechaInicio.Value = planillaAEditar.FechaInicio;
                Datetimefechafin.Value = planillaAEditar.FechaFin;
                txtSalariopordia.Text = planillaAEditar.SalarioDiario.ToString("C2");

                // Valores editables/Base
                Txtdiastrabajados.Text = planillaAEditar.DiasTrabajados.ToString("N0");
                txthorasextras.Text = planillaAEditar.HorasExtrasCantidad.ToString("N2");

                // Incentivos y Deducciones Adicionales (CORREGIDO)
                txtinsentivo.Text = planillaAEditar.Incentivos.ToString("C2");
                textvacaciones.Text = planillaAEditar.Vacaciones.ToString("C2");
                txtdeducciones.Text = planillaAEditar.OtrasDeducciones.ToString("C2");

             
                txttotaldevengado.Text = planillaAEditar.TotalDevengado.ToString("C2");
                txtins.Text = planillaAEditar.DeduccionINSS.ToString("C2");
                txtIR.Text = planillaAEditar.DeduccionIR.ToString("C2");
                txttotaldededucciones.Text = planillaAEditar.TotalDeducciones.ToString("C2");
                txtpagoneto.Text = planillaAEditar.PagoNeto.ToString("C2");

                MessageBox.Show($"Planilla ID {idPlanilla} cargada para edición.", "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("No se pudo cargar el registro.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Datagreedplanilla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

           
            int idPlanilla = Convert.ToInt32(Datagreedplanilla.Rows[e.RowIndex].Cells["Id_planilla"].Value);

            L_Planilla planillaAEditar = objPlanillaDatos.ObtenerPlanillaPorId(idPlanilla);

            if (planillaAEditar != null)
            {
           
                this.idPlanillaSeleccionada = planillaAEditar.Id_planilla;
                this.btguardar.Text = "Actualizar Planilla";

              

              
                Cb_Nombre.SelectedValue = planillaAEditar.Id_empleado;

         
                DtimeFechaInicio.Value = planillaAEditar.FechaInicio;
                Datetimefechafin.Value = planillaAEditar.FechaFin;
                txtSalariopordia.Text = planillaAEditar.SalarioDiario.ToString("C2");

             
                Txtdiastrabajados.Text = planillaAEditar.DiasTrabajados.ToString("N0");
                txthorasextras.Text = planillaAEditar.HorasExtrasCantidad.ToString("N2");

                txtinsentivo.Text = planillaAEditar.Incentivos.ToString("C2");
                textvacaciones.Text = planillaAEditar.Vacaciones.ToString("C2");
                txtdeducciones.Text = planillaAEditar.OtrasDeducciones.ToString("C2");

       
                txttotaldevengado.Text = planillaAEditar.TotalDevengado.ToString("C2");
                txtins.Text = planillaAEditar.DeduccionINSS.ToString("C2");
                txtIR.Text = planillaAEditar.DeduccionIR.ToString("C2");
                txttotaldededucciones.Text = planillaAEditar.TotalDeducciones.ToString("C2");
                txtpagoneto.Text = planillaAEditar.PagoNeto.ToString("C2");

                btguardar.Enabled = true;
                MessageBox.Show($"Planilla ID {idPlanilla} cargada para edición. Presione 'Actualizar Planilla' para guardar los cambios.", "Modo Edición Activado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo cargar el registro. Revise el log de errores de BD.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
   
}
