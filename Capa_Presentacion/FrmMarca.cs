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
    public partial class FrmMarca : Form
    {
        private D_Marca funciones = new D_Marca();
        private L_Marca marcaSeleccionada;
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;
        public FrmMarca()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmMarca_Load;
        }

        private void FrmMarca_Load(object sender, EventArgs e)
        {
            if (dgvMarca.Columns.Contains("id_marca"))
            {
                dgvMarca.Columns["id_marca"].Visible = false;
            }

            dgvMarca.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

          
            dgvMarca.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvMarca.GridColor = Color.DimGray;

       
            if (dgvMarca.ColumnCount > 0)
            {
                dgvMarca.Columns[dgvMarca.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            CargarMarcasPaginado();
            LimpiarControles();
        }
        private void CargarMarcasPaginado()
        {
            try
            {
                List<L_Marca> lista = funciones.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
                dgvMarca.DataSource = lista;
                ActualizarEstadoPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las marcas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarControles()
        {
            marcaSeleccionada = null;
            TxtMarca.Clear();
            BTAgregar.Text = "Guardar";
            BTAgregar.Enabled = true;
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

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtMarca.Text))
            {
                MessageBox.Show("El nombre de la marca no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_Marca oMarca = new L_Marca() { nombre = TxtMarca.Text };

            if (marcaSeleccionada == null)
            {
                mensaje = funciones.Insertar(oMarca);
            }
            else
            {
                oMarca.id_marca = marcaSeleccionada.id_marca;
                mensaje = funciones.Editar(oMarca);
            }

            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarMarcasPaginado();
            LimpiarControles();
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una marca para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtMarca.Text))
            {
                MessageBox.Show("El nombre de la marca no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_Marca oMarca = new L_Marca()
            {
                id_marca = marcaSeleccionada.id_marca,
                nombre = TxtMarca.Text
            };

            mensaje = funciones.Editar(oMarca);
            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarMarcasPaginado();
            LimpiarControles();
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (marcaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una marca para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta marca?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                string mensaje = funciones.Eliminar(marcaSeleccionada.id_marca);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarMarcasPaginado();
                LimpiarControles();
            }
        }

        private void dgvMarca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                marcaSeleccionada = (L_Marca)dgvMarca.Rows[e.RowIndex].DataBoundItem;
                TxtMarca.Text = marcaSeleccionada.nombre;
                BTEditar.Enabled = true;
                BTEliminar.Enabled = true;
            }
        }
        private void RealizarBusqueda()
        {
            try
            {
                string termino = TxtBuscar.Text.Trim();
                if (string.IsNullOrWhiteSpace(termino))
                {
                    CargarMarcasPaginado();
                }
                else
                {
                    List<L_Marca> lista = funciones.Buscar(termino);
                    dgvMarca.DataSource = lista;
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

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarMarcasPaginado();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarMarcasPaginado();
            }
        }
    }
}
