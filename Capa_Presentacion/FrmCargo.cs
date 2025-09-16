using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmCargo : Form
    {
        //private CN_Cargo cnCargo = new CN_Cargo();
        D_Cargo funciones = new D_Cargo();
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;
        public FrmCargo()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;


        }
        private void CargarCargos()
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                List<L_Cargo> listaCargos = funciones.ObtenerCargosConPaginado(paginaActual, tamanoPagina, out totalRegistros);
                dgvcargo.DataSource = listaCargos;


                if (dgvcargo.Columns.Contains("Id_cargo"))
                {
                    dgvcargo.Columns["Id_cargo"].Visible = false;
                }

                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);


                TxtPagina.Text = paginaActual.ToString();
                TxtTotalPagina.Text = totalPaginas.ToString();

                BtnAnterior.Enabled = (paginaActual > 1);
                BtnSiguiente.Enabled = (paginaActual < totalPaginas);
            }
        }

        private void dgvcargo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmCargo_Load(object sender, EventArgs e)
        {
            if (dgvcargo.Columns.Contains("Id_actividad"))
            {
                dgvcargo.Columns["Id_actividad"].Visible = false;
            }


            dgvcargo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvcargo.ColumnCount > 0)
            {
                dgvcargo.Columns[dgvcargo.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            CargarCargos();

        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarCargos();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarCargos();
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                CargarCargos();
            }
            else
            {

                dgvcargo.DataSource = funciones.BuscarCargos(TxtBuscar.Text);
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            L_Cargo parametros = new L_Cargo();
            parametros.Nombre = TxtCargo.Text;
            string respuesta = funciones.InsertarCargo(parametros);

            if (respuesta.Equals("OK"))
            {
                MessageBox.Show("Cargo agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtCargo.Clear();
                CargarCargos();
            }
            else
            {
                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (dgvcargo.SelectedRows.Count > 0)
            {
                L_Cargo parametros = new L_Cargo();
                parametros.Id_cargo = Convert.ToInt32(dgvcargo.SelectedRows[0].Cells["Id_cargo"].Value);
                parametros.Nombre = TxtCargo.Text;

                string respuesta = funciones.ActualizarCargo(parametros);

                if (respuesta.Equals("OK"))
                {
                    MessageBox.Show("Cargo editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCargo.Clear();
                    CargarCargos();
                }
                else
                {
                    MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (dgvcargo.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este cargo?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int idCargo = Convert.ToInt32(dgvcargo.SelectedRows[0].Cells["Id_cargo"].Value);
                    string respuesta = funciones.EliminarCargo(idCargo);

                    if (respuesta.Equals("OK"))
                    {
                        MessageBox.Show("Cargo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCargos();
                    }
                    else
                    {
                        MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona una fila para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtPagina_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtPagina.Text))
            {
                int nuevaPagina;

                if (int.TryParse(TxtPagina.Text, out nuevaPagina))
                {
                    int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);

                    if (nuevaPagina >= 1 && nuevaPagina <= totalPaginas)
                    {
                        paginaActual = nuevaPagina;
                        CargarCargos();
                    }
                }
            }
        }
    }
}
