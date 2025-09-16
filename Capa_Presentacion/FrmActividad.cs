using Capa_Presentacion.Logica;
using Datos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmActividad : Form
    {
        //private CN_Actividad cnActividad = new CN_Actividad();
        D_Actividad cnActividad = new D_Actividad();
        L_Actividad parametros = new L_Actividad();
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;
        public FrmActividad()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void CargarActividades()
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                List<L_Actividad> listaActividades = cnActividad.ObtenerActividadesConPaginado(paginaActual, tamanoPagina, out totalRegistros);
                dgvactividad.DataSource = listaActividades;

                if (dgvactividad.Columns.Contains("Id_actividad"))
                {
                    dgvactividad.Columns["Id_actividad"].Visible = false;
                }

                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
                TxtPagina.Text = paginaActual.ToString();
                TxtTotalPagina.Text = totalPaginas.ToString();

                BtnAnterior.Enabled = (paginaActual > 1);
                BtnSiguiente.Enabled = (paginaActual < totalPaginas);
            }
        }
        private void FrmActividad_Load(object sender, EventArgs e)
        {
            if (dgvactividad.Columns.Contains("Id_actividad"))
            {
                dgvactividad.Columns["Id_actividad"].Visible = false;
            }


            dgvactividad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvactividad.ColumnCount > 0)
            {
                dgvactividad.Columns[dgvactividad.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            CargarActividades();

        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarActividades();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarActividades();
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
                        CargarActividades();
                    }
                }
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBuscar.Text))
            {
                CargarActividades();
            }
            else
            {
                dgvactividad.DataSource = cnActividad.BuscarActividades(TxtBuscar.Text);
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            parametros.Descripcion = TxtActividad.Text;
            string respuesta = cnActividad.InsertarActividad(parametros);

            if (respuesta.Equals("OK"))
            {
                MessageBox.Show("Actividad agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtActividad.Clear();
                CargarActividades();
            }
            else
            {
                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (dgvactividad.SelectedRows.Count > 0)
            {
                parametros.Id_actividad = Convert.ToInt32(dgvactividad.SelectedRows[0].Cells["Id_actividad"].Value);
                parametros.Descripcion = TxtActividad.Text;

                string respuesta = cnActividad.ActualizarActividad(parametros);

                if (respuesta.Equals("OK"))
                {
                    MessageBox.Show("Actividad editada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtActividad.Clear();
                    CargarActividades();
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
            if (dgvactividad.SelectedRows.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que quieres eliminar esta actividad?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int idActividad = Convert.ToInt32(dgvactividad.SelectedRows[0].Cells["Id_actividad"].Value);
                    string respuesta = cnActividad.EliminarActividad(idActividad);

                    if (respuesta.Equals("OK"))
                    {
                        MessageBox.Show("Actividad eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarActividades();
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
