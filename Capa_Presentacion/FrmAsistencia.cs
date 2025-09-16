using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmAsistencia : Form
    {

        private CN_Asistencia objNegocio = new CN_Asistencia();
        private L_Asistencia asistenciaSeleccionada;
        private int paginaActual = 1;
        private int tamanoPagina = 10;
        private int totalRegistros = 0;
        public FrmAsistencia()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            Cb_estado.DropDownStyle = ComboBoxStyle.DropDownList;

        }



        private void FrmAsistencia_Load(object sender, EventArgs e)
        {
            LlenarComboBoxes();
            ConfigurarDataGridView();
            CargarAsistencias();
            LimpiarControles();
        }

        private void LlenarComboBoxes()
        {
            CD_Empleado objEmpleado = new CD_Empleado();
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

            // ------------------- Actividades -------------------
            CD_Actividad objActividad = new CD_Actividad();
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


            CD_Lote objLote = new CD_Lote();
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


            CD_Cargo objCargo = new CD_Cargo();
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
            bool esAusente = Cb_estado.SelectedItem?.ToString() == "Ausente";
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


            cbactividad.Enabled = !esAusente;
            CMBLote.Enabled = !esAusente;
            txtcantolvadas.Enabled = !esAusente;
            txthoraextras.Enabled = !esAusente;


            if (!cbactividad.Enabled) cbactividad.SelectedIndex = -1;
            if (!CMBLote.Enabled) CMBLote.SelectedIndex = -1;
            if (!txtcantolvadas.Enabled) txtcantolvadas.Clear();
            if (!txthoraextras.Enabled) txthoraextras.Clear();
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
            if (estado == "Ausente")
            {
                justificacion = string.Join(", ", GroupBoxJustificacion.Controls.OfType<CheckBox>().Where(cb => cb.Checked).Select(cb => cb.Text));
            }

            L_Asistencia oAsistencia = new L_Asistencia()
            {
                IDEmpleado = Cb_Nombre.SelectedValue != null ? (int)Cb_Nombre.SelectedValue : 0,
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
                // Operación: EDITAR
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
                    string mensaje = string.Empty;
                    if (objNegocio.EliminarAsistencia(asistenciaSeleccionada.IDAsistencia, out mensaje))
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

                // Asignar valores a los controles
                Cb_Nombre.SelectedValue = asistenciaSeleccionada.IDEmpleado;
                Cb_estado.SelectedItem = asistenciaSeleccionada.Estado;

                // Lógica corregida para campos que pueden ser nulos
                if (asistenciaSeleccionada.IDActividad.HasValue)
                {
                    cbactividad.SelectedValue = asistenciaSeleccionada.IDActividad.Value;
                }
                else
                {
                    cbactividad.SelectedIndex = -1; // Limpia la selección
                }

                if (asistenciaSeleccionada.IDLote.HasValue)
                {
                    CMBLote.SelectedValue = asistenciaSeleccionada.IDLote.Value;
                }
                else
                {
                    CMBLote.SelectedIndex = -1; // Limpia la selección
                }

                if (asistenciaSeleccionada.IDCargo.HasValue)
                {
                    CbCargo.SelectedValue = asistenciaSeleccionada.IDCargo.Value;
                }
                else
                {
                    CbCargo.SelectedIndex = -1; // Limpia la selección
                }

                dateTimePicker1.Value = asistenciaSeleccionada.Fecha;
                txtcantolvadas.Text = asistenciaSeleccionada.Tolvadas?.ToString() ?? string.Empty;
                txthoraextras.Text = asistenciaSeleccionada.HorasExtras?.ToString() ?? string.Empty;
                txtobservacion.Text = asistenciaSeleccionada.Justificacion; // Ahora usas Observacion en lugar de Justificacion

                // Lógica para la justificación
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

                BTAgregar.Text = "Actualizar";
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;
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
            if (string.IsNullOrEmpty(TxtBuscar.Text))
            {
                CargarAsistencias();
            }
        }

        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                List<CE_Asistencia> resultados = objNegocio.BuscarAsistencias(TxtBuscar.Text);
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
    }
}
