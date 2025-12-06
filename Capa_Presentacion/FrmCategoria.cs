using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

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
            dgvcategoria.AutoGenerateColumns = true;
            dgvcategoria.ReadOnly = true;

        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            dgvcategoria.CellBorderStyle = DataGridViewCellBorderStyle.Single;

           
            dgvcategoria.GridColor = Color.DimGray;
            ConfigurarDataGridView();
            CargarCategoriasPaginadas();
            LimpiarControles();
            
            if (dgvcategoria.Columns.Contains("id_categoria"))
            {
                dgvcategoria.Columns["id_categoria"].Visible = false;
            }

            dgvcategoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

    
            if (dgvcategoria.ColumnCount > 0)
            {
                dgvcategoria.Columns[dgvcategoria.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void ConfigurarDataGridView()
        {

           
            dgvcategoria.Columns.Clear();
          
            dgvcategoria.AutoGenerateColumns = false;

            
            dgvcategoria.Columns.Add(new DataGridViewTextBoxColumn() { Name = "id_categoria", HeaderText = "ID", DataPropertyName = "id_categoria", Visible = false });

       
            dgvcategoria.Columns.Add(new DataGridViewTextBoxColumn() { Name = "nombre", HeaderText = "Categoría", DataPropertyName = "nombre" });

           
            dgvcategoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
                mensaje = funciones.Insertar(oCategoria);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategoriasPaginadas();
                LimpiarControles();
            }
            else
            {
                oCategoria.id_categoria = categoriaSeleccionada.id_categoria;
                mensaje = funciones.Editar(oCategoria);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategoriasPaginadas();
                LimpiarControles();
            }
        }

        private void dgvcategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
            
                categoriaSeleccionada = (L_Categoria)dgvcategoria.Rows[e.RowIndex].DataBoundItem;

               
                TxtCategoria.Text = categoriaSeleccionada.nombre;

                BTAgregar.Text = "Actualizar";

               
                BTEliminar.Enabled = true;
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
                        string mensaje = funciones.Eliminar(categoriaSeleccionada.id_categoria);
                        MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCategoriasPaginadas();
                        LimpiarControles();
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

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (categoriaSeleccionada == null)
            {
                MessageBox.Show("Selecciona una categoría para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar esta categoría?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                mensaje = funciones.Eliminar(categoriaSeleccionada.id_categoria);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCategoriasPaginadas();
                LimpiarControles();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetter(e.KeyChar) &&
          !char.IsWhiteSpace(e.KeyChar) &&
          e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
