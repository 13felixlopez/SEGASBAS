using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmLote : Form
    {
        private int paginaActual = 1;
        private int totalPaginas = 0;
        private int totalRegistros = 0;
        private const int tamanoPagina = 10;
        private L_Lote loteSeleccionado = null;


        private BindingSource bsLotes = new BindingSource();
        public FrmLote()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

  
            DatagreedLote.CellDoubleClick += DatagreedLote_CellDoubleClick;
            DatagreedLote.CellClick += DatagreedLote_CellClick;

            BTAgregar.Click += BTAgregar_Click;
            BTEditar.Click += BTEditar_Click;
            BTEliminar.Click += BTEliminar_Click;
            BTCancelar.Click += BTCancelar_Click;
            BtnAnterior.Click += BtnAnterior_Click;
            BtnSiguiente.Click += BtnSiguiente_Click;
            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
            CBEstadoCultivo.SelectedIndexChanged += CBEstadoCultivo_SelectedIndexChanged;

            CBEstadoCultivo.DropDownStyle = ComboBoxStyle.DropDownList;

            SetPlaceholder();

        }

        private void SetPlaceholder()
        {
            TxtObservacion.Text = "Notas adicionales sobre el lote";
            TxtObservacion.ForeColor = Color.Gray;
        }

        private void FrmLote_Load(object sender, EventArgs e)
        {
            LlenarComboBoxes();
            ConfigurarDataGridView();
            ConfigurarFiltrosBusqueda();
            CargarLotes();


            TxtObservacion.Enter += TxtObservacion_Enter;
            TxtObservacion.Leave += TxtObservacion_Leave;


        }
        private void ConfigurarBinding()
        {
        
            DatagreedLote.AutoGenerateColumns = false;
            DatagreedLote.DataSource = bsLotes;
        }

        private void LlenarComboBoxes()
        {
            D_Generico objGenerico = new D_Generico();

            
            CbCiclo.DataSource = objGenerico.ObtenerCiclos();
            CbCiclo.DisplayMember = "Value";
            CbCiclo.ValueMember = "Key";
            CbCiclo.SelectedIndex = -1;

            CBTipoSiembra.DataSource = objGenerico.ObtenerTiposSiembra();
            CBTipoSiembra.DisplayMember = "Value";
            CBTipoSiembra.ValueMember = "Key";
            CBTipoSiembra.SelectedIndex = -1;

            CBEstadoCultivo.DataSource = objGenerico.ObtenerEstadosCultivo();
            CBEstadoCultivo.DisplayMember = "Value";
            CBEstadoCultivo.ValueMember = "Key";
            CBEstadoCultivo.SelectedIndex = -1;
        }

        private void ConfigurarFiltrosBusqueda()
        {
            List<KeyValuePair<string, string>> opciones = new List<KeyValuePair<string, string>>();
            opciones.Add(new KeyValuePair<string, string>("Nombre Lote", "NombreLote"));
            opciones.Add(new KeyValuePair<string, string>("Manzanaje", "Manzanaje"));
            opciones.Add(new KeyValuePair<string, string>("Tipo de Siembra", "TipoSiembra"));
            opciones.Add(new KeyValuePair<string, string>("Estado de Cultivo", "EstadoCultivo"));
            opciones.Add(new KeyValuePair<string, string>("Ciclo", "Ciclo"));
            opciones.Add(new KeyValuePair<string, string>("Observación", "Observacion"));

            CmbBuscar.DataSource = opciones;
            CmbBuscar.DisplayMember = "Key";
            CmbBuscar.ValueMember = "Value";
            CmbBuscar.SelectedIndex = 0;
        }

        private void ConfigurarDataGridView()
        {
            DatagreedLote.Columns.Clear();
            DatagreedLote.AutoGenerateColumns = false;

         
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IDLote", HeaderText = "ID Lote", DataPropertyName = "IDLote", Visible = false });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreLote", HeaderText = "Nombre", DataPropertyName = "NombreLote", Width = 200 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Manzanaje", HeaderText = "Manzanaje", DataPropertyName = "Manzanaje", Width = 120 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "VariedadArroz", HeaderText = "Variedad Arroz", DataPropertyName = "VariedadArroz", Width = 150 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoSiembra", HeaderText = "Tipo Siembra", DataPropertyName = "TipoSiembra", Width = 140 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EstadoCultivo", HeaderText = "Estado Cultivo", DataPropertyName = "EstadoCultivo", Width = 140 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Ciclo", HeaderText = "Ciclo", DataPropertyName = "Ciclo", Width = 100 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaSiembra", HeaderText = "F. Siembra", DataPropertyName = "FechaSiembra", Width = 120 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaCorte", HeaderText = "F. Corte", DataPropertyName = "FechaCorte", Width = 120 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Observacion", HeaderText = "Observación", DataPropertyName = "Observacion", Width = 220 });

            DatagreedLote.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DatagreedLote.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void CargarLotes()
        {
            try
    {
                D_Lote objNegocio = new D_Lote();

                object resultado = objNegocio.ObtenerLotesPaginados(paginaActual, tamanoPagina, out totalRegistros);

                
                totalPaginas = totalRegistros > 0 ? (int)Math.Ceiling((double)totalRegistros / tamanoPagina) : 1;
                TxtPagina.Text = paginaActual.ToString();
                TxtTotalPagina.Text = totalPaginas.ToString();


                if (resultado == null)
                {
                    bsLotes.DataSource = null;
                    DatagreedLote.DataSource = bsLotes;
                    MessageBox.Show("La consulta devolvió null (sin datos). Revisa D_Lote.ObtenerLotesPaginados().", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (resultado is List<L_Lote> lista)
                {
                    bsLotes.DataSource = lista;
                    DatagreedLote.DataSource = bsLotes;

                  
                    return;
                }

      
                if (resultado is System.Data.DataTable dt)
                {
                

                    bsLotes.DataSource = dt;
                    DatagreedLote.DataSource = bsLotes;

                    return;
                }

              
                var tipo = resultado.GetType();
             
                bsLotes.DataSource = resultado;
                DatagreedLote.DataSource = bsLotes;


            }
               catch (Exception ex)
                {
                MessageBox.Show("Error al cargar lotes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        private void CBEstadoCultivo_SelectedIndexChanged(object sender, EventArgs e)
        {
              if (CBEstadoCultivo.SelectedItem == null) return;

            var selected = (KeyValuePair<int, string>)CBEstadoCultivo.SelectedItem;
            string estadoSeleccionado = selected.Value;

            bool esNinguno = estadoSeleccionado == "Ninguno";
            bool esCosecha = estadoSeleccionado == "En Cosecha";

            LbtipoDeCienmbra.Enabled = !esNinguno;
            CBTipoSiembra.Enabled = !esNinguno;
            label1.Enabled = !esNinguno;
            dateTimePickerSiembra.Enabled = !esNinguno;

            label4.Enabled = esCosecha;
            dateTimePickerCorte.Enabled = esCosecha;
        }
        private void BuscarLotes(string textoBusqueda, string columnaBusqueda)
        {


            D_Lote objNegocio = new D_Lote();
            int total;

            List<L_Lote> listaLotes = objNegocio.ObtenerLotesPaginados(1, totalRegistros, out total);

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CargarLotes();
                return;
            }

            List<L_Lote> listaFiltrada = new List<L_Lote>();

            foreach (L_Lote lote in listaLotes)
            {
                if (lote.GetType().GetProperty(columnaBusqueda).GetValue(lote, null).ToString().ToLower().Contains(textoBusqueda.ToLower()))
                {
                    listaFiltrada.Add(lote);
                }
            }
            DatagreedLote.DataSource = listaFiltrada;
            TxtPagina.Text = "1";
            TxtTotalPagina.Text = "1";
        }



        private void BTAgregar_Click(object sender, EventArgs e)
        {

            if (CBEstadoCultivo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un 'Estado del Cultivo'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;
            D_Lote objNegocio = new D_Lote();

            string observacion = (TxtObservacion.Text == "Notas adicionales sobre el lote") ? "" : TxtObservacion.Text;

            L_Lote oLote = new L_Lote()
            {
                IDLote = 0,
                NombreLote = TxtNombreLote.Text.Trim(),
                Manzanaje = TxtManzanaje.Text.Trim(),
                IDTipoSiembra = CBTipoSiembra.SelectedValue != null ? (int?)CBTipoSiembra.SelectedValue : null,
                IDEstadoCultivo = (int)((KeyValuePair<int, string>)CBEstadoCultivo.SelectedItem).Key,
                IDCiclo = CbCiclo.SelectedValue != null ? (int?)CbCiclo.SelectedValue : null,
                Observacion = observacion,
                VariedadArroz = TxtVariedadeArroz.Text.Trim(),
                FechaSiembra = dateTimePickerSiembra.Checked ? (DateTime?)dateTimePickerSiembra.Value.Date : null,
                FechaCorte = dateTimePickerCorte.Checked ? (DateTime?)dateTimePickerCorte.Value.Date : null
            };

            if (objNegocio.InsertarLote(oLote, out mensaje))
            {
                MessageBox.Show("Lote guardado correctamente.");
                LimpiarControles();
                CargarLotes(); 
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void LimpiarControles()
        {
            loteSeleccionado = null;
            TxtNombreLote.Text = string.Empty;
            TxtManzanaje.Text = string.Empty;
            CbCiclo.SelectedIndex = -1;
            CBTipoSiembra.SelectedIndex = -1;
            CBEstadoCultivo.SelectedIndex = -1;
            TxtObservacion.Text = string.Empty;
            TxtVariedadeArroz.Text = string.Empty;
            dateTimePickerSiembra.Value = DateTime.Now;
            dateTimePickerSiembra.Checked = false;
            dateTimePickerCorte.Value = DateTime.Now;
            dateTimePickerCorte.Checked = false;

      
            SetPlaceholder();

        
            BTAgregar.Enabled = true;
            BTEditar.Enabled = false;
            BTEliminar.Enabled = false;
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (loteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un lote de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;
            D_Lote objNegocio = new D_Lote();

            L_Lote oLote = new L_Lote()
            {
                IDLote = loteSeleccionado.IDLote,
                NombreLote = TxtNombreLote.Text.Trim(),
                Manzanaje = TxtManzanaje.Text.Trim(),
                IDTipoSiembra = CBTipoSiembra.SelectedValue != null ? (int?)CBTipoSiembra.SelectedValue : null,
                IDEstadoCultivo = (int)((KeyValuePair<int, string>)CBEstadoCultivo.SelectedItem).Key,
                IDCiclo = CbCiclo.SelectedValue != null ? (int?)CbCiclo.SelectedValue : null,
                Observacion = (TxtObservacion.Text == "Notas adicionales sobre el lote") ? "" : TxtObservacion.Text,
                VariedadArroz = TxtVariedadeArroz.Text.Trim(),
                FechaSiembra = dateTimePickerSiembra.Checked ? (DateTime?)dateTimePickerSiembra.Value.Date : null,
                FechaCorte = dateTimePickerCorte.Checked ? (DateTime?)dateTimePickerCorte.Value.Date : null
            };

            if (objNegocio.ActualizarLote(oLote, out mensaje))
            {
                MessageBox.Show("Lote actualizado correctamente.");
                LimpiarControles();
                CargarLotes();
            }
            else
            {
                MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (loteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un lote de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje;
            D_Lote objNegocio = new D_Lote();

            if (MessageBox.Show("¿Está seguro de eliminar este lote?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (objNegocio.EliminarLote(loteSeleccionado.IDLote, out mensaje))
                {
                    MessageBox.Show("Lote eliminado correctamente.");
                    LimpiarControles();
                    CargarLotes();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {

            string textoBusqueda = TxtBuscar.Text.Trim();
            string columnaBusqueda = string.Empty;

            if (CmbBuscar.SelectedItem != null)
            {
                var selectedPair = (KeyValuePair<string, string>)CmbBuscar.SelectedItem;
                columnaBusqueda = selectedPair.Value;
            }
            else
            {
                columnaBusqueda = "NombreLote";
            }

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CargarLotes();
                return;
            }

            D_Lote objNegocio = new D_Lote();
            int total;
            List<L_Lote> listaLotes = objNegocio.ObtenerLotesPaginados(1, totalRegistros, out total);

            var listaFiltrada = listaLotes
                .Where(lote =>
                {
                    var prop = lote.GetType().GetProperty(columnaBusqueda);
                    if (prop != null)
                    {
                        var val = prop.GetValue(lote, null);
                        return val != null && val.ToString().ToLower().Contains(textoBusqueda.ToLower());
                    }
                    return false;
                })
                .ToList();

            bsLotes.DataSource = listaFiltrada;
            TxtPagina.Text = "1";
            TxtTotalPagina.Text = "1";
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarLotes();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarLotes();
            }
        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            BTAgregar.Enabled = true;
        }

        private void DatagreedLote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = this.DatagreedLote.Rows[e.RowIndex];

            // Usar los nombres de columna que definimos
            TxtNombreLote.Text = fila.Cells["NombreLote"].Value?.ToString() ?? string.Empty;
            TxtManzanaje.Text = fila.Cells["Manzanaje"].Value?.ToString() ?? string.Empty;
            TxtObservacion.Text = fila.Cells["Observacion"].Value?.ToString() ?? string.Empty;
            TxtVariedadeArroz.Text = fila.Cells["VariedadArroz"].Value?.ToString() ?? string.Empty;

            // Intentar setear selected values por Id (si existen en las columnas)
            if (fila.Cells["TipoSiembra"].Value != null)
            {
                // si tu fuente tiene el id en otra columna, preferible usar esa columna id, por ejemplo "id_tipo_siembra"
                // Si no existe, tratamos de buscar por texto:
                int idx = CBTipoSiembra.FindStringExact(fila.Cells["TipoSiembra"].Value.ToString());
                CBTipoSiembra.SelectedIndex = idx >= 0 ? idx : -1;
            }
            else
            {
                CBTipoSiembra.SelectedIndex = -1;
            }

            if (fila.Cells["EstadoCultivo"].Value != null)
            {
                int idx = CBEstadoCultivo.FindStringExact(fila.Cells["EstadoCultivo"].Value.ToString());
                CBEstadoCultivo.SelectedIndex = idx >= 0 ? idx : -1;
            }
            else
            {
                CBEstadoCultivo.SelectedIndex = -1;
            }

            if (fila.Cells["Ciclo"].Value != null)
            {
                int idx = CbCiclo.FindStringExact(fila.Cells["Ciclo"].Value.ToString());
                CbCiclo.SelectedIndex = idx >= 0 ? idx : -1;
            }
            else
            {
                CbCiclo.SelectedIndex = -1;
            }

            // Fechas
            if (fila.Cells["FechaSiembra"].Value != null && DateTime.TryParse(fila.Cells["FechaSiembra"].Value.ToString(), out DateTime fSiembra))
            {
                dateTimePickerSiembra.Value = fSiembra;
                dateTimePickerSiembra.Checked = true;
            }
            else
            {
                dateTimePickerSiembra.Checked = false;
            }

            if (fila.Cells["FechaCorte"].Value != null && DateTime.TryParse(fila.Cells["FechaCorte"].Value.ToString(), out DateTime fCorte))
            {
                dateTimePickerCorte.Value = fCorte;
                dateTimePickerCorte.Checked = true;
            }
            else
            {
                dateTimePickerCorte.Checked = false;
            }
        }

        private void DatagreedLote_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = DatagreedLote.Rows[e.RowIndex];

            loteSeleccionado = new L_Lote()
            {
                IDLote = Convert.ToInt32(row.Cells["IDLote"].Value),
                NombreLote = row.Cells["NombreLote"].Value?.ToString(),
                Manzanaje = row.Cells["Manzanaje"].Value?.ToString(),
                TipoSiembra = row.Cells["TipoSiembra"].Value?.ToString(),
                EstadoCultivo = row.Cells["EstadoCultivo"].Value?.ToString(),
                Ciclo = row.Cells["Ciclo"].Value?.ToString(),
                FechaSiembra = row.Cells["FechaSiembra"].Value as DateTime?,
                FechaCorte = row.Cells["FechaCorte"].Value as DateTime?,
                VariedadArroz = row.Cells["VariedadArroz"].Value?.ToString(),
                Observacion = row.Cells["Observacion"].Value?.ToString()
            };

            // Cargar controles
            TxtNombreLote.Text = loteSeleccionado.NombreLote;
            TxtManzanaje.Text = loteSeleccionado.Manzanaje;
            TxtVariedadeArroz.Text = loteSeleccionado.VariedadArroz;
            TxtObservacion.Text = string.IsNullOrEmpty(loteSeleccionado.Observacion) ? "" : loteSeleccionado.Observacion;

            // Preferible setear SelectedValue si tus ComboBoxes contienen KeyValuePair con Key=ID
            if (!string.IsNullOrEmpty(loteSeleccionado.TipoSiembra))
            {
                int idx = CBTipoSiembra.FindStringExact(loteSeleccionado.TipoSiembra);
                CBTipoSiembra.SelectedIndex = idx >= 0 ? idx : -1;
            }

            if (!string.IsNullOrEmpty(loteSeleccionado.EstadoCultivo))
            {
                int idx = CBEstadoCultivo.FindStringExact(loteSeleccionado.EstadoCultivo);
                CBEstadoCultivo.SelectedIndex = idx >= 0 ? idx : -1;
            }

            if (!string.IsNullOrEmpty(loteSeleccionado.Ciclo))
            {
                int idx = CbCiclo.FindStringExact(loteSeleccionado.Ciclo);
                CbCiclo.SelectedIndex = idx >= 0 ? idx : -1;
            }

            if (loteSeleccionado.FechaSiembra.HasValue)
                dateTimePickerSiembra.Value = loteSeleccionado.FechaSiembra.Value;

            if (loteSeleccionado.FechaCorte.HasValue)
                dateTimePickerCorte.Value = loteSeleccionado.FechaCorte.Value;

           
            BTAgregar.Enabled = false;
            BTEditar.Enabled = true;
            BTEliminar.Enabled = true;
        }

        private void BTInformacion_Click(object sender, EventArgs e)
        {
            FrmMapa frmMapa = new FrmMapa();

            frmMapa.ShowDialog();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtObservacion_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TxtObservacion_Enter(object sender, EventArgs e)
        {
            if (TxtObservacion.Text == "Notas adicionales sobre el lote")
            {
                TxtObservacion.Text = "";
                TxtObservacion.ForeColor = Color.Black;
            }
        }

        private void TxtObservacion_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtObservacion.Text))
            {
                SetPlaceholder();
            }
        }

        private void BTAgregar_Click_1(object sender, EventArgs e)
        {
            string observacion = (TxtObservacion.Text == "Notas adicionales sobre el lote")
                        ? ""
                        : TxtObservacion.Text;
        }

        private void TxtManzanaje_Enter(object sender, EventArgs e)
        {
            if (TxtObservacion.Text == "Notas adicionales sobre el lote")
            {
                TxtObservacion.Text = "";
                TxtObservacion.ForeColor = Color.Black;
            }
        }

        private void PanelLote_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
