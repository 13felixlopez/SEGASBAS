using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmAsistencia : Form
    {
        //private CN_Asistencia objNegocio = new CN_Asistencia();
        private L_Asistencia asistenciaSeleccionada;
        private int paginaActual = 1;
        private int tamanoPagina = 10;
        private int totalRegistros = 0;

        public FrmAsistencia()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Cb_estado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbactividad.SelectedIndexChanged += new EventHandler(cbactividad_SelectedIndexChanged);
            txtcantolvadas.Enabled = false;
            foreach (var cb in GroupBoxJustificacion.Controls.OfType<CheckBox>())
            {
                cb.CheckedChanged += CheckBoxJustificacion_Exclusive_CheckedChanged;
            }

            CbCargo.Enabled = true;
            Cb_estado.SelectedIndexChanged += Cb_estado_SelectedIndexChanged;
         
          

        }
       
        private void FrmAsistencia_Load(object sender, EventArgs e)
        {
            LlenarComboBoxes();
            ConfigurarDataGridView();
            CargarAsistencias();
            LimpiarControles();
            txtobservacion.Visible = false;
            label9.Visible= false;
            ConfiguracionVisual.CargarModoDesdeBD();
            CmbBuscar.Items.Clear();
            CmbBuscar.Items.Add("Todo");
            CmbBuscar.Items.Add("Empleado");
            CmbBuscar.Items.Add("Cedula");
            CmbBuscar.Items.Add("Actividad");
            CmbBuscar.Items.Add("Lote");
            CmbBuscar.Items.Add("Cargo");
            CmbBuscar.Items.Add("Estado");
            CmbBuscar.Items.Add("Fecha");
            CmbBuscar.SelectedIndex = 0; 

            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
            CmbBuscar.SelectedIndexChanged += CmbBuscar_SelectedIndexChanged;

        
            BuscarAsistencia();


        }

        private void LlenarComboBoxes()
        {
            D_Empleado objEmpleado = new D_Empleado();
            var empleados = objEmpleado.ObtenerEmpleados();
            Cb_Nombre.DataSource = empleados;
            Cb_Nombre.DisplayMember = "Value";
            Cb_Nombre.ValueMember = "Key";
            Cb_Nombre.SelectedIndex = -1;

            AutoCompleteStringCollection nombresCollection = new AutoCompleteStringCollection();
            foreach (var emp in empleados) nombresCollection.Add(emp.Value);
            Cb_Nombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Cb_Nombre.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Cb_Nombre.AutoCompleteCustomSource = nombresCollection;

            D_Actividad objActividad = new D_Actividad();
            var actividades = objActividad.ObtenerActividades();
            cbactividad.DataSource = actividades;
            cbactividad.DisplayMember = "Value";
            cbactividad.ValueMember = "Key";
            cbactividad.SelectedIndex = -1;

            AutoCompleteStringCollection actividadesCollection = new AutoCompleteStringCollection();
            foreach (var act in actividades) actividadesCollection.Add(act.Value);
            cbactividad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbactividad.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbactividad.AutoCompleteCustomSource = actividadesCollection;


            D_Lote objLote = new D_Lote();
            var lotes = objLote.ObtenerLotes();
            CMBLote.DataSource = lotes;
            CMBLote.DisplayMember = "NombreLote";
            CMBLote.ValueMember = "IDLote";
            CMBLote.SelectedIndex = -1;

            AutoCompleteStringCollection lotesCollection = new AutoCompleteStringCollection();
            foreach (var lote in lotes) lotesCollection.Add(lote.NombreLote);
            CMBLote.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CMBLote.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CMBLote.AutoCompleteCustomSource = lotesCollection;


            D_Cargo objCargo = new D_Cargo();
            var cargos = objCargo.ObtenerCargos();
            CbCargo.DataSource = cargos;
            CbCargo.DisplayMember = "Nombre";
            CbCargo.ValueMember = "Id_cargo";
            CbCargo.SelectedIndex = -1;

            AutoCompleteStringCollection cargosCollection = new AutoCompleteStringCollection();
            foreach (var cargo in cargos) cargosCollection.Add(cargo.Nombre);
            CbCargo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CbCargo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            CbCargo.AutoCompleteCustomSource = cargosCollection;

            List<string> estados = new List<string> { "Presente", "Ausente", "Tardanza" };
            Cb_estado.DataSource = estados;
            Cb_estado.SelectedIndex = 0;
        }

        private void ConfigurarDataGridView()
        {
            DatagreedAsistencia.Columns.Clear();
            DatagreedAsistencia.AutoGenerateColumns = false;

            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IDAsistencia", HeaderText = "ID", DataPropertyName = "IDAsistencia", Visible = false });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Fecha", HeaderText = "Fecha", DataPropertyName = "Fecha", DefaultCellStyle = { Format = "dd/MM/yyyy" } });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "HoraRegistro", HeaderText = "Hora", DataPropertyName = "HoraRegistro", DefaultCellStyle = { Format = "hh\\:mm" } });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreCompletoEmpleado", HeaderText = "Empleado", DataPropertyName = "NombreCompletoEmpleado" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreActividad", HeaderText = "Actividad", DataPropertyName = "NombreActividad" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreLote", HeaderText = "Lote", DataPropertyName = "NombreLote" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreCargo", HeaderText = "Cargo", DataPropertyName = "NombreCargo" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Estado", HeaderText = "Estado", DataPropertyName = "Estado" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Justificacion", HeaderText = "Justificación", DataPropertyName = "Justificacion" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Tolvadas", HeaderText = "Tolvadas", DataPropertyName = "Tolvadas" });
            DatagreedAsistencia.Columns.Add(new DataGridViewTextBoxColumn() { Name = "HorasExtras", HeaderText = "H. Extras", DataPropertyName = "HorasExtras" });
        }

        private void CargarAsistencias()
        {
            L_Asistencia parametros = new L_Asistencia();
            D_Asistencia funciones = new D_Asistencia();
            List<L_Asistencia> lista = funciones.ObtenerAsistenciasPaginadas(paginaActual, tamanoPagina, out totalRegistros);
            DatagreedAsistencia.DataSource = lista;
            ActualizarEstadoPaginacion();
        }

        private void LimpiarControles()
        {
            asistenciaSeleccionada = null;
            Cb_Nombre.SelectedIndex = -1;
            cbactividad.SelectedIndex = -1;
            CMBLote.SelectedIndex = -1;
            CbCargo.SelectedIndex = -1;
            Cb_estado.SelectedIndex = 0;
            txtcantolvadas.Clear();
            txthoraextras.Clear();
            txtobservacion.Clear();
            dateTimePicker1.Value = DateTime.Now.Date;
            BTAgregar.Text = "Guardar";
            BTEditar.Enabled = false;
            BTEliminar.Enabled = false;
            Cb_Nombre.Enabled = true;
            CbCargo.Enabled = true;

            ActualizarHabilitacionControles();
        }

        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
            BtnAnterior.Enabled = paginaActual > 1;
            BtnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void Cb_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esAusente = string.Equals(Cb_estado.SelectedItem?.ToString(), "Ausente", StringComparison.OrdinalIgnoreCase);
            CbCargo.Enabled = !esAusente;
            cbactividad.Enabled = !esAusente;
            CMBLote.Enabled = !esAusente;
            txthoraextras.Enabled = !esAusente;

            GroupBoxJustificacion.Enabled = esAusente;
            txtobservacion.Enabled = esAusente;

            if (!esAusente)
            {
                foreach (CheckBox cb in GroupBoxJustificacion.Controls.OfType<CheckBox>())
                {
                    cb.Checked = false;
                }
                txtobservacion.Clear();
            }
            if (!cbactividad.Enabled) cbactividad.SelectedIndex = -1;
            if (!CMBLote.Enabled) CMBLote.SelectedIndex = -1;
            if (!CbCargo.Enabled) CbCargo.SelectedIndex = -1;
            if (!txthoraextras.Enabled) txthoraextras.Clear();
            if (!txtcantolvadas.Enabled) txtcantolvadas.Clear();

            ActualizarHabilitacionControles();
        }
        private void CheckBoxJustificacion_Exclusive_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null) return;
            if (cb.Checked)
            {
                foreach (var other in GroupBoxJustificacion.Controls.OfType<CheckBox>())
                {
                    if (!object.ReferenceEquals(other, cb))
                    {
                        other.Checked = false;
                    }
                }

            }
            else
            {
                bool algunMarcado = GroupBoxJustificacion.Controls.OfType<CheckBox>().Any(x => x.Checked);
                txtobservacion.Enabled = !algunMarcado && (Cb_estado.SelectedItem?.ToString() == "Ausente");
            }
        }
        private void ActualizarHabilitacionControles()
        {
            bool esAusente = string.Equals(Cb_estado.SelectedItem?.ToString(), "Ausente", StringComparison.OrdinalIgnoreCase);

            GroupBoxJustificacion.Enabled = esAusente;
            txtobservacion.Enabled = esAusente && !GroupBoxJustificacion.Controls.OfType<CheckBox>().Any(cb => cb.Checked);
            if (esAusente)
            {
                cbactividad.SelectedIndex = -1;
                CMBLote.SelectedIndex = -1;
                txtcantolvadas.Clear();
                txthoraextras.Clear();

                cbactividad.Enabled = false;
                CMBLote.Enabled = false;
                txthoraextras.Enabled = false;
                txtcantolvadas.Enabled = false;
                CbCargo.Enabled = false;

                return;
            }

            cbactividad.Enabled = true;
            CMBLote.Enabled = true;
            txthoraextras.Enabled = true;
            CbCargo.Enabled = true;

            bool habilitarTolvadas = EsActividadDeCosecha();
            txtcantolvadas.Enabled = habilitarTolvadas;

            if (!habilitarTolvadas)
            {
                txtcantolvadas.Clear();
            }
        }
        private bool EsActividadDeCosecha()
        {
            if (cbactividad.SelectedValue == null)
            {
                return false;
            }

            string nombreActividad = cbactividad.Text;

            if (string.IsNullOrEmpty(nombreActividad))
            {
                return false;
            }

            string actividad = nombreActividad.ToLower();

            return actividad.Contains("corte") ||
                   actividad.Contains("cortando") ||
                   actividad.Contains("cosechando");
        }
        private void DatagreedAsistencia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtcantolvadas_TextChanged(object sender, EventArgs e)
        {

        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            D_Asistencia funciones = new D_Asistencia();
            string mensaje = string.Empty;
            string estado = Cb_estado.SelectedItem?.ToString() ?? string.Empty;
            string justificacion = string.Empty;

            if (Cb_Nombre.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un empleado para continuar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (estado == "Ausente")
            {
                bool algunaJustificacionMarcada = GroupBoxJustificacion.Controls
                    .OfType<CheckBox>()
                    .Any(cb => cb.Checked);

                bool tieneObservacionTexto = !string.IsNullOrWhiteSpace(txtobservacion.Text);

                if (!algunaJustificacionMarcada && !tieneObservacionTexto)
                {
                    MessageBox.Show("Debe seleccionar una justificación  cuando el estado es 'Ausente'.",
                        "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return; 
                }
            }

            L_Asistencia oAsistencia = new L_Asistencia()
            {
                IDEmpleado = (int)Cb_Nombre.SelectedValue,
                IDActividad = estado != "Ausente" && cbactividad.SelectedValue != null ? (int?)cbactividad.SelectedValue : null,
                IDLote = CMBLote.SelectedValue != null ? (int?)CMBLote.SelectedValue : null,
                IDCargo = CbCargo.SelectedValue != null ? (int?)CbCargo.SelectedValue : null,
                Estado = estado,
                Justificacion = justificacion,
                Tolvadas = string.IsNullOrEmpty(txtcantolvadas.Text) ? (decimal?)null : Convert.ToDecimal(txtcantolvadas.Text),
                HorasExtras = string.IsNullOrEmpty(txthoraextras.Text) ? (decimal?)null : Convert.ToDecimal(txthoraextras.Text),
                Fecha = dateTimePicker1.Value.Date
            };

            if (asistenciaSeleccionada == null)
            {
                if (funciones.VerificarDobleAsistencia(oAsistencia.IDEmpleado, oAsistencia.Fecha))
                {
                    MessageBox.Show("¡Error! El empleado **" + Cb_Nombre.Text + "** ya tiene registrada una asistencia para la fecha: **" + oAsistencia.Fecha.ToShortDateString() + "**.",
                                    "Asistencia Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (funciones.InsertarAsistencia(oAsistencia, out mensaje))
                {
                    MessageBox.Show("Asistencia guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAsistencias();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                oAsistencia.IDAsistencia = asistenciaSeleccionada.IDAsistencia;

                if (funciones.EditarAsistencia(oAsistencia, out mensaje))
                {
                    MessageBox.Show("Asistencia actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAsistencias();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            BTAgregar_Click(sender, e);
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (asistenciaSeleccionada != null)
            {
                if (MessageBox.Show("¿Está seguro de que desea eliminar esta asistencia?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    D_Asistencia funciones = new D_Asistencia();
                    string mensaje = string.Empty;
                    if (funciones.EliminarAsistencia(asistenciaSeleccionada.IDAsistencia, out mensaje))
                    {
                        MessageBox.Show("Asistencia eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarAsistencias();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        
        }

        private void DatagreedAsistencia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DatagreedAsistencia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                asistenciaSeleccionada = (L_Asistencia)DatagreedAsistencia.Rows[e.RowIndex].DataBoundItem;

  
                Cb_Nombre.SelectedValue = asistenciaSeleccionada.IDEmpleado;
                Cb_estado.SelectedItem = asistenciaSeleccionada.Estado;

         
                if (asistenciaSeleccionada.IDActividad.HasValue)
                {
                    cbactividad.SelectedValue = asistenciaSeleccionada.IDActividad.Value;
                }
                else
                {
                    cbactividad.SelectedIndex = -1; 
                }

                if (asistenciaSeleccionada.IDLote.HasValue)
                {
                    CMBLote.SelectedValue = asistenciaSeleccionada.IDLote.Value;
                }
                else
                {
                    CMBLote.SelectedIndex = -1; 
                }

                if (asistenciaSeleccionada.IDCargo.HasValue)
                {
                    CbCargo.SelectedValue = asistenciaSeleccionada.IDCargo.Value;
                }
                else
                {
                    CbCargo.SelectedIndex = -1; 
                }

                dateTimePicker1.Value = asistenciaSeleccionada.Fecha;
                txtcantolvadas.Text = asistenciaSeleccionada.Tolvadas?.ToString() ?? string.Empty;
                txthoraextras.Text = asistenciaSeleccionada.HorasExtras?.ToString() ?? string.Empty;
                txtobservacion.Text = asistenciaSeleccionada.Justificacion;

         
                foreach (CheckBox cb in GroupBoxJustificacion.Controls.OfType<CheckBox>())
                {
                    cb.Checked = false;
                }
                if (asistenciaSeleccionada.Estado == "Ausente" && !string.IsNullOrEmpty(asistenciaSeleccionada.Justificacion))
                {
                    string[] justificaciones = asistenciaSeleccionada.Justificacion.Split(new string[] { ", " }, StringSplitOptions.None);
                    foreach (CheckBox cb in GroupBoxJustificacion.Controls.OfType<CheckBox>())
                    {
                        if (justificaciones.Contains(cb.Text))
                        {
                            cb.Checked = true;
                        }
                    }
                }

               

                Cb_Nombre.Enabled = false;  

        

                BTAgregar.Text = "Actualizar";
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;

                ActualizarHabilitacionControles();
            }
        }
        


        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarAsistencias();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarAsistencias();
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string campo = (CmbBuscar.SelectedItem != null) ? CmbBuscar.SelectedItem.ToString() : "Todo";

            if (campo.Equals("Fecha", StringComparison.OrdinalIgnoreCase))
            {
              
                BuscarAsistencia(false);
            }
            else
            {
         
                BuscarAsistencia(false);
            }
        }

        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                D_Asistencia funciones = new D_Asistencia();
                List<L_Asistencia> resultados = funciones.BuscarAsistencias(TxtBuscar.Text);
                DatagreedAsistencia.DataSource = resultados;

                BtnAnterior.Enabled = false;
                BtnSiguiente.Enabled = false;
            }
        }
        private void ValidarSoloNumeros(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }


            TextBox tb = sender as TextBox;
            if (e.KeyChar == '.' && tb != null && tb.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtcantolvadas_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarSoloNumeros(sender, e);
        }

        private void txthoraextras_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarSoloNumeros(sender, e);
        }
        

        private void cbactividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarHabilitacionControles();
        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de que desea cancelar la operación y descartar los datos ingresados?",
                        "Confirmar Cancelación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
               
                LimpiarControles();
            }
        }
        private void BuscarAsistencia(bool mostrarErrores = true)
        {
            string campo = (CmbBuscar.SelectedItem != null) ? CmbBuscar.SelectedItem.ToString() : "Todo";
            string texto = TxtBuscar.Text.Trim();

            if (campo.Equals("Fecha", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(texto))
            {
                return;
            }


            if (campo.Equals("Fecha", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(texto))
            {

                var regex = new System.Text.RegularExpressions.Regex(@"^\s*\d{1,4}[/\-]\d{1,2}[/\-]\d{1,4}\s*$");
                bool tieneFormatoBasico = regex.IsMatch(texto);

           
                if (!tieneFormatoBasico && !mostrarErrores)
                    return;

                DateTime fecha;
                bool ok = DateTime.TryParseExact(texto,
                                                 new[] { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy-M-d" },
                                                 System.Globalization.CultureInfo.InvariantCulture,
                                                 System.Globalization.DateTimeStyles.None,
                                                 out fecha);
                if (!ok)
                {
                    ok = DateTime.TryParse(texto, out fecha);
                }

                if (!ok)
                {
                    if (mostrarErrores)
                        MessageBox.Show("Formato de fecha inválido. Usa dd/MM/yyyy (ej. 29/11/2025) o yyyy-MM-dd.",
                                        "Fecha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                texto = fecha.ToString("yyyy-MM-dd");
            }

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_BuscarAsistencia", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    cmd.Parameters.AddWithValue("@Campo", campo);
                    cmd.Parameters.AddWithValue("@Texto", texto);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }

                DatagreedAsistencia.DataSource = dt;
                DatagreedAsistencia.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                if (mostrarErrores)
                    MessageBox.Show("Error al buscar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void CmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarAsistencia();
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                e.SuppressKeyPress = true;
                BuscarAsistencia();
            }
        }
    }
}
