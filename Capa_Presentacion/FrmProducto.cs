using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmProducto : Form
    {
        private bool abiertoDesdeBtProducto = false;
      

        private D_Producto funciones = new D_Producto();
        private L_Producto productoSeleccionado;
        private int paginaActual = 1;
        private const int tamañoPagina = 10;
        private int totalRegistros = 0;
        public FrmProducto()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmProducto_Load;



            this.FormClosing += FrmProducto_FormClosing;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {

            if (dgvProducto.Columns.Contains("id_producto"))
            {
                dgvProducto.Columns["id_producto"].Visible = false;
            }


         
            CargarProductosPaginado();
            LimpiarControles();

        }
        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
            BtnAnterior.Enabled = paginaActual > 1;
            BtnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void LimpiarControles()
        {
            productoSeleccionado = null;
            TxtProducto.Clear();
            BTAgregar.Text = "Agregar";
            BTAgregar.Enabled = true;
            BTEditar.Enabled = false;
            BTEliminar.Enabled = false;
        }
        private void CargarProductosPaginado()
        {
            try
            {
                List<L_Producto> lista = funciones.ListarPaginado(paginaActual, tamañoPagina, out totalRegistros);
                dgvProducto.DataSource = lista;
                ActualizarEstadoPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarProductosPaginado();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarProductosPaginado();
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_Producto oProducto = new L_Producto() { nombre = TxtProducto.Text };

            if (productoSeleccionado == null)
            {

                mensaje = funciones.Insertar(oProducto);
            }
            else
            {

                oProducto.id_producto = productoSeleccionado.id_producto;
                mensaje = funciones.Editar(oProducto);
            }

            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarProductosPaginado();
            LimpiarControles();
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_Producto oProducto = new L_Producto()
            {
                id_producto = productoSeleccionado.id_producto,
                nombre = TxtProducto.Text
            };

            mensaje = funciones.Editar(oProducto);
            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarProductosPaginado();
            LimpiarControles();
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                string mensaje = funciones.Eliminar(productoSeleccionado.id_producto);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductosPaginado();
                LimpiarControles();
            }

        }

        private void dgvProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducto.Rows[e.RowIndex];
                productoSeleccionado = new L_Producto()
                {
                    id_producto = Convert.ToInt32(row.Cells["id_producto"].Value),
                    nombre = row.Cells["nombre"].Value.ToString()
                };

                TxtProducto.Text = productoSeleccionado.nombre;

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
                    CargarProductosPaginado();
                }
                else
                {
                    dgvProducto.DataSource = funciones.Buscar(termino);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (abiertoDesdeBtProducto)
            {

                MessageBox.Show("No se puede cerrar porque fue abierto desde BtProducto.");
                e.Cancel = true;
            }
        }
    }
}
