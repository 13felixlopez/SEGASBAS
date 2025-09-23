using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
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
    public partial class Frm_Unidadmedida : Form
    {
        private D_UnidadMedida funciones = new D_UnidadMedida();
        private L_UnidadMedida unidadSeleccionada;
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;

        public Frm_Unidadmedida()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += Frm_Unidadmedida_Load;
        }
        private void ConfigurarDataGridView()
        {
            dgvUnidad.Columns.Clear();
            dgvUnidad.AutoGenerateColumns = false;

            dgvUnidad.Columns.Add(new DataGridViewTextBoxColumn() { Name = "id_unidad", HeaderText = "ID", DataPropertyName = "id_unidad", Visible = false });
            dgvUnidad.Columns.Add(new DataGridViewTextBoxColumn() { Name = "nombre", HeaderText = "Unidad de Medida", DataPropertyName = "nombre" });

            dgvUnidad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvUnidad.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvUnidad.GridColor = Color.DimGray;

            if (dgvUnidad.ColumnCount > 0)
            {
                dgvUnidad.Columns[dgvUnidad.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void CargarUnidadesPaginadas()
        {
            try
            {
                List<L_UnidadMedida> lista = funciones.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
                dgvUnidad.DataSource = lista;
                ActualizarEstadoPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las unidades de medida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
            BtnAnterior.Enabled = paginaActual > 1;
            BtnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void LimpiarControles()
        {
            unidadSeleccionada = null;
            TxtUnidadMedida.Clear();
            BTAgregar.Text = "Guardar";
            BTAgregar.Enabled = true;
            BTEditar.Enabled = false;
            BTEliminar.Enabled = false;
        }
        private void Frm_Unidadmedida_Load(object sender, EventArgs e)
        {

        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtUnidadMedida.Text))
            {
                MessageBox.Show("El nombre de la unidad de medida no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_UnidadMedida oUnidad = new L_UnidadMedida() { nombre = TxtUnidadMedida.Text };

            if (unidadSeleccionada == null)
            {
                mensaje = funciones.Insertar(oUnidad);
            }
            else
            {
                oUnidad.id_unidad = unidadSeleccionada.id_unidad;
                mensaje = funciones.Editar(oUnidad);
            }

            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarUnidadesPaginadas();
            LimpiarControles();
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (unidadSeleccionada == null)
            {
                MessageBox.Show("Selecciona una unidad de medida para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtUnidadMedida.Text))
            {
                MessageBox.Show("El nombre de la unidad de medida no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_UnidadMedida oUnidad = new L_UnidadMedida()
            {
                id_unidad = unidadSeleccionada.id_unidad,
                nombre = TxtUnidadMedida.Text
            };

            mensaje = funciones.Editar(oUnidad);
            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarUnidadesPaginadas();
            LimpiarControles();
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (unidadSeleccionada == null)
            {
                MessageBox.Show("Selecciona una unidad de medida para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta unidad de medida?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                string mensaje = funciones.Eliminar(unidadSeleccionada.id_unidad);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarUnidadesPaginadas();
                LimpiarControles();
            }
        }
        private void RealizarBusqueda()
        {
            try
            {
                string termino = TxtBuscar.Text.Trim();
                if (string.IsNullOrWhiteSpace(termino))
                {
                    CargarUnidadesPaginadas();
                }
                else
                {
                    List<L_UnidadMedida> lista = funciones.Buscar(termino);
                    dgvUnidad.DataSource = lista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        private void dgvUnidad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                unidadSeleccionada = (L_UnidadMedida)dgvUnidad.Rows[e.RowIndex].DataBoundItem;
                TxtUnidadMedida.Text = unidadSeleccionada.nombre;
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarUnidadesPaginadas();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarUnidadesPaginadas();
            }
        }
    }
}
