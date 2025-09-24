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
    public partial class Frm_Proveedores : Form
    {
        private D_Proveedores funciones = new D_Proveedores();
        private L_Proveedor proveedorSeleccionado;
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int totalRegistros = 0;

        public Frm_Proveedores()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void LbObservacion_Click(object sender, EventArgs e)
        {

        }
        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
            BtnAnterior.Enabled = paginaActual > 1;
            BtnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void CargarProveedoresPaginados()
        {
            try
            {
                List<L_Proveedor> lista = funciones.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
                DgvProveedores.DataSource = lista;
                ActualizarEstadoPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
           
            if (DgvProveedores.Columns.Contains("id_proveedor"))
            {
                DgvProveedores.Columns["id_proveedor"].Visible = false;
            }
        }

        private void Frm_Proveedores_Load(object sender, EventArgs e)
        {

            ConfigurarDataGridView();
            CargarProveedoresPaginados();
            LimpiarControles();
        }
        private void LimpiarControles()
        {
            proveedorSeleccionado = null;
          
            string ultimoNumero = funciones.ObtenerUltimoNumeroRegistro();
            int nuevoNumero = Convert.ToInt32(ultimoNumero) + 1;
       
            TxtNumeroRegistro.Text = nuevoNumero.ToString().PadLeft(4, '0');

            TxtRazonSocial.Clear();
            TxtNRuc.Clear();
            TxtCorreo.Clear();
            TxtTelefono.Clear();
            TxtVisitador.Clear();
            TxtObservacion.Clear();
            dateTimePickerFechaIngres.Value = DateTime.Now;
            checkProducto.Checked = false;
            CheckServicio.Checked = false;

            BTAgregar.Text = "Guardar";
            BTAgregar.Enabled = true;
         
            BTEliminar.Enabled = false;
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtRazonSocial.Text) || string.IsNullOrWhiteSpace(TxtNRuc.Text))
            {
                MessageBox.Show("Razón Social y Número de RUC son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string mensaje = string.Empty;
            L_Proveedor oProveedor = new L_Proveedor()
            {
                numero_registro = TxtNumeroRegistro.Text,
                razon_social = TxtRazonSocial.Text,
                numero_ruc = TxtNRuc.Text,
                correo_electronico = TxtCorreo.Text,
                telefono = TxtTelefono.Text,
                visitador = TxtVisitador.Text,
                observacion = TxtObservacion.Text,
                fecha_registro = dateTimePickerFechaIngres.Value,
                es_producto = checkProducto.Checked,
                es_servicio = CheckServicio.Checked
            };

           
            if (proveedorSeleccionado == null)
            {
                mensaje = funciones.Insertar(oProveedor);
            }
            else
            {
                oProveedor.id_proveedor = proveedorSeleccionado.id_proveedor;
                mensaje = funciones.Editar(oProveedor);
            }

            MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarProveedoresPaginados();
            LimpiarControles();
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (proveedorSeleccionado == null)
            {
                MessageBox.Show("Selecciona un proveedor para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este proveedor?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                string mensaje = funciones.Eliminar(proveedorSeleccionado.id_proveedor);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProveedoresPaginados();
                LimpiarControles();
            }
        }

        private void DgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                proveedorSeleccionado = (L_Proveedor)DgvProveedores.Rows[e.RowIndex].DataBoundItem;
                TxtNumeroRegistro.Text = proveedorSeleccionado.numero_registro;
                TxtRazonSocial.Text = proveedorSeleccionado.razon_social;
                TxtNRuc.Text = proveedorSeleccionado.numero_ruc;
                TxtCorreo.Text = proveedorSeleccionado.correo_electronico;
                TxtTelefono.Text = proveedorSeleccionado.telefono;
                TxtVisitador.Text = proveedorSeleccionado.visitador;
                TxtObservacion.Text = proveedorSeleccionado.observacion;
                dateTimePickerFechaIngres.Value = proveedorSeleccionado.fecha_registro;
                checkProducto.Checked = proveedorSeleccionado.es_producto;
                CheckServicio.Checked = proveedorSeleccionado.es_servicio;

                BTAgregar.Text = "Editar";
              
                BTEliminar.Enabled = true;
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string termino = TxtBuscar.Text.Trim();
                if (string.IsNullOrWhiteSpace(termino))
                {
                    CargarProveedoresPaginados();
                }
                else
                {
                    List<L_Proveedor> lista = funciones.Buscar(termino);
                    DgvProveedores.DataSource = lista;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarProveedoresPaginados();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarProveedoresPaginados();
            }
        }

        private void TxtNumeroRegistro_TextChanged(object sender, EventArgs e)
        {
            TxtNumeroRegistro.ReadOnly = true;
        }
    }
}
