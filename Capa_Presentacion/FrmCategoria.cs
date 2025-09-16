using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmCategoria : Form
    {
        //private CN_Categoria funciones = new CN_Categoria();
        D_Categoria funciones = new D_Categoria();
        private L_Categoria categoriaSeleccionada;
        private int paginaActual = 1;
        private int tamanoPagina = 10;
        private int totalRegistros = 0;
        public FrmCategoria()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarCategoriasPaginadas();
            LimpiarControles();
        }
        private void ConfigurarDataGridView()
        {
            dgvcategoria.Columns.Clear();
            dgvcategoria.AutoGenerateColumns = false;

            // Columna para el ID de la categoría (oculta)
            dgvcategoria.Columns.Add(new DataGridViewTextBoxColumn() { Name = "id_categoria", HeaderText = "ID", DataPropertyName = "id_categoria", Visible = false });

            // Columna para el nombre de la categoría
            dgvcategoria.Columns.Add(new DataGridViewTextBoxColumn() { Name = "nombre", HeaderText = "Categoría", DataPropertyName = "nombre" });

            // Columna para el botón de editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.HeaderText = "Editar";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.Name = "btnEditar";
            dgvcategoria.Columns.Add(btnEditar);

            // Columna para el botón de eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.Name = "btnEliminar";
            dgvcategoria.Columns.Add(btnEliminar);
        }

        private void CargarCategoriasPaginadas()
        {
            List<L_Categoria> lista = funciones.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
            dgvcategoria.DataSource = lista;
            ActualizarEstadoPaginacion();
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
            categoriaSeleccionada = null;
            TxtCategoria.Text = string.Empty;
            BTAgregar.Text = "Guardar";
        }


        private void BTAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtCategoria.Text))
            {
                MessageBox.Show("El nombre de la categoría no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            L_Categoria oCategoria = new L_Categoria() { nombre = TxtCategoria.Text };

            if (categoriaSeleccionada == null)
            {
                int resultado = funciones.Insertar(oCategoria, out mensaje);
                if (resultado > 0)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCategoriasPaginadas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                oCategoria.id_categoria = categoriaSeleccionada.id_categoria;
                bool resultado = funciones.Editar(oCategoria, out mensaje);
                if (resultado)
                {
                    MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarCategoriasPaginadas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvcategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                categoriaSeleccionada = (L_Categoria)dgvcategoria.Rows[e.RowIndex].DataBoundItem;
                TxtCategoria.Text = categoriaSeleccionada.nombre;
                BTAgregar.Text = "Actualizar";
            }
        }

        private void dgvcategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvcategoria.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (dgvcategoria.Columns[e.ColumnIndex].Name == "btnEliminar")
                {
                    categoriaSeleccionada = (L_Categoria)dgvcategoria.Rows[e.RowIndex].DataBoundItem;
                    if (MessageBox.Show("¿Está seguro de que desea eliminar esta categoría?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string mensaje = string.Empty;
                        if (funciones.Eliminar(categoriaSeleccionada.id_categoria, out mensaje))
                        {
                            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarCategoriasPaginadas();
                            LimpiarControles();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtBuscar.Text))
            {
                dgvcategoria.DataSource = funciones.Buscar(TxtBuscar.Text);
                BtnAnterior.Enabled = false;
                BtnSiguiente.Enabled = false;
            }
            else
            {
                CargarCategoriasPaginadas();
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {

            if (paginaActual > 1)
            {
                paginaActual--;
                CargarCategoriasPaginadas();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarCategoriasPaginadas();
            }
        }
    }
}
