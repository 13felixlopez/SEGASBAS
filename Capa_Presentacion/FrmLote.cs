using Capa_Datos;
using Capa_Entidad;
using Capa_Negocio;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmLote : Form
    {
        private int paginaActual = 1;
        private int totalPaginas = 0;
        private int totalRegistros = 0;
        private const int tamanoPagina = 10;
        private CE_Lote loteSeleccionado = null;
        public FrmLote()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            DatagreedLote.CellDoubleClick += DatagreedLote_CellDoubleClick;
            BTAgregar.Click += BTAgregar_Click;
            BTEditar.Click += BTEditar_Click;
            BTEliminar.Click += BTEliminar_Click;
            BTCancelar.Click += BTCancelar_Click;
            BtnAnterior.Click += BtnAnterior_Click;
            BtnSiguiente.Click += BtnSiguiente_Click;
            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
            CBEstadoCultivo.SelectedIndexChanged += CBEstadoCultivo_SelectedIndexChanged;
            CBEstadoCultivo.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void FrmLote_Load(object sender, EventArgs e)
        {
            LlenarComboBoxes();
            ConfigurarDataGridView();
            ConfigurarFiltrosBusqueda();
            CargarLotes();

        }
        private void LlenarComboBoxes()
        {
            CD_Generico objGenerico = new CD_Generico();

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
        }

        private void ConfigurarDataGridView()
        {
            DatagreedLote.Columns.Clear();
            DatagreedLote.AutoGenerateColumns = false;

            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IDLote", HeaderText = "ID Lote", DataPropertyName = "IDLote", Visible = false });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreLote", HeaderText = "Nombre", DataPropertyName = "NombreLote", Width = 100 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Manzanaje", HeaderText = "Manzanaje", DataPropertyName = "Manzanaje", Width = 100 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoSiembra", HeaderText = "Tipo Siembra", DataPropertyName = "TipoSiembra", Width = 120 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EstadoCultivo", HeaderText = "Estado Cultivo", DataPropertyName = "EstadoCultivo", Width = 120 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Ciclo", HeaderText = "Ciclo", DataPropertyName = "Ciclo", Width = 80 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaSiembra", HeaderText = "F. Siembra", DataPropertyName = "FechaSiembra", Width = 100 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaCorte", HeaderText = "F. Corte", DataPropertyName = "FechaCorte", Width = 100 });
            DatagreedLote.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Observacion", HeaderText = "Observación", DataPropertyName = "Observacion", Width = 200 });

            DatagreedLote.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DatagreedLote.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void CargarLotes()
        {
            CN_Lote objNegocio = new CN_Lote();
            DatagreedLote.DataSource = objNegocio.ObtenerLotesPaginados(paginaActual, tamanoPagina, out totalRegistros);
            totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
        }
        private void CBEstadoCultivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBEstadoCultivo.SelectedValue != null)
            {
                string estadoSeleccionado = ((KeyValuePair<int, string>)CBEstadoCultivo.SelectedItem).Value;

                bool esNinguno = estadoSeleccionado == "Ninguno";
                bool esCosecha = estadoSeleccionado == "En Cosecha";

        
                LbtipoDeCienmbra.Enabled = !esNinguno;
                CBTipoSiembra.Enabled = !esNinguno;
                label1.Enabled = !esNinguno;
                dateTimePickerSiembra.Enabled = !esNinguno;

     
                label4.Enabled = esCosecha;
                dateTimePickerCorte.Enabled = esCosecha;
            }
        }
        private void BuscarLotes(string textoBusqueda, string columnaBusqueda)
        {
          

            CN_Lote objNegocio = new CN_Lote();
            int total;
       
            List<CE_Lote> listaLotes = objNegocio.ObtenerLotesPaginados(1, totalRegistros, out total);

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CargarLotes();
                return;
            }

            List<CE_Lote> listaFiltrada = new List<CE_Lote>();

            foreach (CE_Lote lote in listaLotes)
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
            CN_Lote objNegocio = new CN_Lote();

            CE_Lote oLote = new CE_Lote()
            {
                IDLote = 0,
                NombreLote = TxtNombreLote.Text,
                Manzanaje = TxtManzanaje.Text,
                IDTipoSiembra = CBTipoSiembra.SelectedValue != null ? (int?)CBTipoSiembra.SelectedValue : null,
                IDEstadoCultivo = (int)CBEstadoCultivo.SelectedValue,
                IDCiclo = CbCiclo.SelectedValue != null ? (int?)CbCiclo.SelectedValue : null,
                Observacion = TxtObservacion.Text
            };

            if (objNegocio.InsertarLote(oLote, out mensaje))
            {
                MessageBox.Show("Lote guardado correctamente.");
                CargarLotes();
                LimpiarControles();
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
            dateTimePickerSiembra.Value = DateTime.Now;
            dateTimePickerCorte.Value = DateTime.Now;
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (loteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un lote de la tabla para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;
            CN_Lote objNegocio = new CN_Lote();

            CE_Lote oLote = new CE_Lote()
            {
                IDLote = loteSeleccionado.IDLote,
                NombreLote = TxtNombreLote.Text,
                Manzanaje = TxtManzanaje.Text,
                IDTipoSiembra = CBTipoSiembra.SelectedValue != null ? (int?)CBTipoSiembra.SelectedValue : null,
                IDEstadoCultivo = (int)CBEstadoCultivo.SelectedValue,
                IDCiclo = CbCiclo.SelectedValue != null ? (int?)CbCiclo.SelectedValue : null,
                Observacion = TxtObservacion.Text
            };

            if (objNegocio.ActualizarLote(oLote, out mensaje))
            {
                MessageBox.Show("Lote actualizado correctamente.");
                CargarLotes();
                LimpiarControles();
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
            CN_Lote objNegocio = new CN_Lote();

            if (MessageBox.Show("¿Está seguro de eliminar este lote?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (objNegocio.EliminarLote(loteSeleccionado.IDLote, out mensaje))
                {
                    MessageBox.Show("Lote eliminado correctamente.");
                    CargarLotes();
                    LimpiarControles();
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

       
            CN_Lote objNegocio = new CN_Lote();
            int total;
            List<CE_Lote> listaLotes = objNegocio.ObtenerLotesPaginados(1, totalRegistros, out total);

            List<CE_Lote> listaFiltrada = listaLotes
                .Where(lote =>
                {
                    var propiedad = lote.GetType().GetProperty(columnaBusqueda);
                    if (propiedad != null)
                    {
                        var valor = propiedad.GetValue(lote, null);
                        return valor != null && valor.ToString().ToLower().Contains(textoBusqueda.ToLower());
                    }
                    return false;
                })
                .ToList();

            DatagreedLote.DataSource = listaFiltrada;
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

            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = this.DatagreedLote.Rows[e.RowIndex];

                TxtNombreLote.Text = fila.Cells["nombre"].Value?.ToString();
                TxtManzanaje.Text = fila.Cells["manzanaje"].Value?.ToString();
                TxtObservacion.Text = fila.Cells["observacion"].Value?.ToString();

                if (fila.Cells["id_tipo_siembra"].Value != DBNull.Value)
                {
                    CBTipoSiembra.SelectedValue = Convert.ToInt32(fila.Cells["id_tipo_siembra"].Value);
                }
                else
                {
                    CBTipoSiembra.SelectedIndex = -1;
                }

                if (fila.Cells["id_estado_cultivo"].Value != DBNull.Value)
                {
                    CBEstadoCultivo.SelectedValue = Convert.ToInt32(fila.Cells["id_estado_cultivo"].Value);
                }
                else
                {
                    CBEstadoCultivo.SelectedIndex = -1;
                }

                if (fila.Cells["id_ciclo"].Value != DBNull.Value)
                {
                    CbCiclo.SelectedValue = Convert.ToInt32(fila.Cells["id_ciclo"].Value);
                }
                else
                {
                    CbCiclo.SelectedIndex = -1;
                }

                if (fila.Cells["fecha_siembra"].Value != DBNull.Value)
                {
                    dateTimePickerSiembra.Value = Convert.ToDateTime(fila.Cells["fecha_siembra"].Value);
                    dateTimePickerSiembra.Checked = true;
                }
                else
                {
                    dateTimePickerSiembra.Checked = false;
                }

                if (fila.Cells["fecha_corte"].Value != DBNull.Value)
                {
                    dateTimePickerCorte.Value = Convert.ToDateTime(fila.Cells["fecha_corte"].Value);
                    dateTimePickerCorte.Checked = true;
                }
                else
                {
                    dateTimePickerCorte.Checked = false;
                }
            }
        }

        private void DatagreedLote_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DatagreedLote.Rows[e.RowIndex];
                loteSeleccionado = new CE_Lote()
                {
                    IDLote = Convert.ToInt32(row.Cells["IDLote"].Value),
                    NombreLote = row.Cells["NombreLote"].Value.ToString(),
                    Manzanaje = row.Cells["Manzanaje"].Value.ToString(),
                    TipoSiembra = row.Cells["TipoSiembra"].Value.ToString(),
                    EstadoCultivo = row.Cells["EstadoCultivo"].Value.ToString(),
                    Ciclo = row.Cells["Ciclo"].Value.ToString(),
                    FechaSiembra = row.Cells["FechaSiembra"].Value as DateTime?,
                    FechaCorte = row.Cells["FechaCorte"].Value as DateTime?,
                    Observacion = row.Cells["Observacion"].Value.ToString()
                };

                TxtNombreLote.Text = loteSeleccionado.NombreLote;
                TxtManzanaje.Text = loteSeleccionado.Manzanaje;
                TxtObservacion.Text = loteSeleccionado.Observacion;

                CBTipoSiembra.SelectedIndex = CBTipoSiembra.FindStringExact(loteSeleccionado.TipoSiembra);
                CBEstadoCultivo.SelectedIndex = CBEstadoCultivo.FindStringExact(loteSeleccionado.EstadoCultivo);
                CbCiclo.SelectedIndex = CbCiclo.FindStringExact(loteSeleccionado.Ciclo);

                if (loteSeleccionado.FechaSiembra.HasValue)
                    dateTimePickerSiembra.Value = loteSeleccionado.FechaSiembra.Value;
                if (loteSeleccionado.FechaCorte.HasValue)
                    dateTimePickerCorte.Value = loteSeleccionado.FechaCorte.Value;

          
                BTAgregar.Enabled = false;

              
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;
            }
        }
    }
}
