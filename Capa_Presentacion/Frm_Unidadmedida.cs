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
            // Oculta la columna del ID para el usuario si existe
            if (dgvUnidad.Columns.Contains("id_unidad"))
            {
                dgvUnidad.Columns["id_unidad"].Visible = false;
            }

    
            dgvUnidad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

       
            if (dgvUnidad.Columns.Contains("abreviatura"))
            {
                dgvUnidad.Columns["abreviatura"].Visible = true;
                dgvUnidad.Columns["abreviatura"].HeaderText = "Abreviatura";
            }

  
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
            ConfigurarDataGridView();
            CargarUnidadesPaginadas();
            LimpiarControles();
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (string.IsNullOrWhiteSpace(TxtUnidadMedida.Text))
            {
                MessageBox.Show("El nombre de la unidad de medida no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreUnidad = TxtUnidadMedida.Text;
            string abreviaturaUnidad = "";


            switch (nombreUnidad.ToLower())
            {
                case "litro":
                case "litros":
                    abreviaturaUnidad = "lts";
                    break;
                case "mililitro":
                case "mililitros":
                    abreviaturaUnidad = "ml";
                    break;
                case "miligramo":
                case "miligramos":
                    abreviaturaUnidad = "mg";
                    break;
                case "gramo":
                case "gramos":
                    abreviaturaUnidad = "gr";
                    break;
                case "kilogramo":
                case "kilogramos":
                    abreviaturaUnidad = "kg";
                    break;
                case "quintal":
                case "quintales":
                    abreviaturaUnidad = "qq";
                    break;
                case "libra":
                case "libras":
                    abreviaturaUnidad = "lb";
                    break;
                case "galon":
                case "galones":
                    abreviaturaUnidad = "gal";
                    break;
                case "onza":
                case "onzas":
                    abreviaturaUnidad = "oz";
                    break;
                case "metro":
                case "metros":
                    abreviaturaUnidad = "m";
                    break;
                case "centimetro":
                case "centimetros":
                    abreviaturaUnidad = "cm";
                    break;
                case "centimetro cubico":
                case "centimetros cubicos":
                    abreviaturaUnidad = "cm3";
                    break;
                case "yarda":
                case "yardas":
                    abreviaturaUnidad = "yd";
                    break;
                case "pulgada":
                case "pulgadas":
                    abreviaturaUnidad = "in";
                    break;
                case "unidad":
                case "unidades":
                    abreviaturaUnidad = "ud";
                    break;
                case "pieza":
                case "piezas":
                    abreviaturaUnidad = "pz";
                    break;
                case "docena":
                case "docenas":
                    abreviaturaUnidad = "doc";
                    break;
                default:
        
                    if (nombreUnidad.Length > 0)
                    {
                        abreviaturaUnidad = nombreUnidad.Substring(0, Math.Min(3, nombreUnidad.Length)).ToUpper();
                    }
                    break;
            }

            L_UnidadMedida oUnidad = new L_UnidadMedida() { nombre = nombreUnidad, abreviatura = abreviaturaUnidad };

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

            string nombreUnidad = TxtUnidadMedida.Text;
            string abreviaturaUnidad = string.Empty;
            string[] palabras = nombreUnidad.Split(' ');
            foreach (string palabra in palabras)
            {
                if (!string.IsNullOrWhiteSpace(palabra))
                {
                    abreviaturaUnidad += palabra[0];
                }
            }
            abreviaturaUnidad = abreviaturaUnidad.ToUpper();

            L_UnidadMedida oUnidad = new L_UnidadMedida()
            {
                id_unidad = unidadSeleccionada.id_unidad,
                nombre = nombreUnidad,
                abreviatura = abreviaturaUnidad
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
