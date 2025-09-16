using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmCiclo : Form
    {
        //private CN_Ciclo cnCiclo = new CN_Ciclo();
        D_Ciclo cnCiclo = new D_Ciclo();
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;

        public FrmCiclo()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void CargarCiclos()
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                List<L_Ciclo> listaCiclos = cnCiclo.ObtenerCiclosConPaginado(paginaActual, tamanoPagina, out totalRegistros);
                dgvciclo.DataSource = listaCiclos;

                if (dgvciclo.Columns.Contains("Id_ciclo"))
                {
                    dgvciclo.Columns["Id_ciclo"].Visible = false;
                }

                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
                TxtPagina.Text = paginaActual.ToString();
                TxtTotalPagina.Text = totalPaginas.ToString();

                BtnAnterior.Enabled = (paginaActual > 1);
                BtnSiguiente.Enabled = (paginaActual < totalPaginas);
            }
        }


        private void FrmCiclo_Load(object sender, EventArgs e)
        {
            if (dgvciclo.Columns.Contains("Id_ciclo"))
            {
                dgvciclo.Columns["Id_ciclo"].Visible = false;
            }




            dgvciclo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvciclo.ColumnCount > 0)
            {
                dgvciclo.Columns[dgvciclo.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            CargarCiclos();
        }

        private void dgvciclo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvciclo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow fila = dgvciclo.Rows[e.RowIndex];

                TxtCiclo.Text = fila.Cells["descripcion"].Value.ToString();
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarCiclos();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarCiclos();
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
                        CargarCiclos();
                    }
                }
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                CargarCiclos();
            }
            else
            {
                dgvciclo.DataSource = cnCiclo.BuscarCiclos(TxtBuscar.Text);
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            L_Ciclo parametros = new L_Ciclo();
            parametros.Descripcion = TxtCiclo.Text;
            string respuesta = cnCiclo.InsertarCiclo(parametros);

            if (respuesta.Equals("OK"))
            {
                MessageBox.Show("Ciclo agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtCiclo.Clear();
                CargarCiclos();
            }
            else
            {
                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (dgvciclo.SelectedRows.Count > 0)
            {
                L_Ciclo parametros = new L_Ciclo();
                parametros.Id_ciclo = Convert.ToInt32(dgvciclo.SelectedRows[0].Cells["Id_ciclo"].Value);
                parametros.Descripcion = TxtCiclo.Text.Trim();


                if (string.IsNullOrWhiteSpace(parametros.Descripcion))
                {
                    MessageBox.Show("Ingrese una descripción válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                string respuesta = cnCiclo.ActualizarCiclo(parametros);

                if (respuesta.Equals("OK"))
                {
                    MessageBox.Show("Ciclo editado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCiclo.Clear();
                    CargarCiclos();
                }
                else
                {
                    MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un ciclo para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (dgvciclo.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este ciclo?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int idCiclo = Convert.ToInt32(dgvciclo.SelectedRows[0].Cells["Id_ciclo"].Value);
                    string respuesta = cnCiclo.EliminarCiclo(idCiclo);

                    if (respuesta.Equals("OK"))
                    {
                        MessageBox.Show("Ciclo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCiclos();
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
    }

}
