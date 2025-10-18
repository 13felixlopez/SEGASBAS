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
    public partial class FRMSALIDA1 : Form
    {
        private readonly D_Salida _datosSalida = new D_Salida();

        private int _paginaActual = 1;
        private const int _tamanoPagina = 10;
        private int _totalPaginas = 0;
        private int _totalRegistros = 0;
        public FRMSALIDA1()
        {
            InitializeComponent();
            CargarCombosIniciales();
            ConfigurarControlesLectura(); 
            CargarSalidas(_paginaActual);
        }


        private void FRMSALIDA1_Load(object sender, EventArgs e)
        {

        }
        private void ConfigurarControlesLectura()
        {
            TxtCostoUnitario.ReadOnly = true;
            TxtCategoria.ReadOnly = true;
            TxtUnidaddemedida.ReadOnly = true;
            dateTimeSalida.Value = DateTime.Now.Date;
        }

       
        private void CargarCombosIniciales()
        {
            try
            {
                List<string> nombresProductos = _datosSalida.ObtenerNombresProductos();
                Cb_Producto.DataSource = nombresProductos;
                Cb_Producto.SelectedIndex = -1; 
                cblote.DataSource = _datosSalida.ObtenerNombresLotes();
                cblote.SelectedIndex = -1;

                Cb_Ciclo.DataSource = _datosSalida.ObtenerDescripcionesCiclos();
                Cb_Ciclo.SelectedIndex = -1;

                CmbBuscar.Items.AddRange(new string[] { "Producto", "Destino", "Ciclo" });
                CmbBuscar.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos iniciales de combos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cb_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productoSeleccionado = Cb_Producto.Text;

            if (string.IsNullOrEmpty(productoSeleccionado))
            {
                LimpiarCamposInformacion();
                return;
            }

            try
            {
               
                L_Salida datos = _datosSalida.ObtenerDatosProductoParaFormulario(productoSeleccionado);

                if (datos != null)
                {
                   
                    TxtCostoUnitario.Text = datos.CostoUnitario.ToString("N2");
                    TxtCategoria.Text = datos.NombreCategoria;
                    TxtUnidaddemedida.Text = datos.AbreviaturaUnidad;
                }
                else
                {
                    LimpiarCamposInformacion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del producto: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LimpiarCamposInformacion()
        {
            TxtCostoUnitario.Clear();
            TxtCategoria.Clear();
            TxtUnidaddemedida.Clear();
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposRegistro()) return;

            L_Salida nuevaSalida = new L_Salida
            {
                NombreProducto = Cb_Producto.Text,
                Cantidad = int.Parse(TxtCantidad.Text),
                CostoUnitario = decimal.Parse(TxtCostoUnitario.Text),
                FechaSalida = dateTimeSalida.Value.Date,
                NombreLoteDestino = cblote.SelectedIndex > -1 ? cblote.Text : null,
                DescripcionCiclo = Cb_Ciclo.SelectedIndex > -1 ? Cb_Ciclo.Text : null,
                Descripcion = txtdescripcion.Text
            };

            string mensaje;

            try
            {
              
                if (_datosSalida.InsertarSalida(nuevaSalida, out mensaje))
                {
                    MessageBox.Show("Salida registrada con éxito y stock actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormularioTotal();
                    CargarSalidas(_paginaActual); // Recargar la grilla actual
                }
                else
                {
                    // Muestra el error de stock o validación capturado en D_Salida
                    MessageBox.Show($"Fallo en el registro: {mensaje}", "Error de Transacción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error interno al registrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarFormularioTotal()
        {
            Cb_Producto.SelectedIndex = -1;
            TxtCantidad.Clear();
            LimpiarCamposInformacion();
            cblote.SelectedIndex = -1;
            Cb_Ciclo.SelectedIndex = -1;
            txtdescripcion.Clear();
            dateTimeSalida.Value = DateTime.Now.Date;
        }
        private bool ValidarCamposRegistro()
        {
            

            if (Cb_Producto.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar el Nombre del Producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cb_Producto.Focus();
                return false;
            }

            if (!int.TryParse(TxtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una Cantidad válida y mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCantidad.Focus();
                return false;
            }

            if (!decimal.TryParse(TxtCostoUnitario.Text, out decimal costo) || costo <= 0)
            {
                MessageBox.Show("No se pudo obtener el Costo Promedio del producto. Seleccione nuevamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void CargarSalidas(int pagina)
        {
            string busqueda = TxtBuscar.Text;
            string campo = CmbBuscar.Text;

            try
            {
                List<L_Salida> lista = _datosSalida.ObtenerSalidasPaginadas(
                    pagina,
                    _tamanoPagina,
                    busqueda,
                    campo,
                    out _totalRegistros
                );

                DatagreedSalidas.DataSource = lista;

                
                _totalPaginas = (int)Math.Ceiling((double)_totalRegistros / _tamanoPagina);
                _paginaActual = pagina;
                ActualizarEstadoPaginado();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el listado de salidas: {ex.Message}", "Error de Consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarEstadoPaginado()
        {
            TxtPagina.Text = $"Página {_paginaActual} de {_totalPaginas}";
            TxtTotalPagina.Text = $"Total Registros: {_totalRegistros}";

            BtnAnterior.Enabled = (_paginaActual > 1);
            BtnSiguiente.Enabled = (_paginaActual < _totalPaginas);
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (_paginaActual > 1)
            {
                CargarSalidas(_paginaActual - 1);
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (_paginaActual < _totalPaginas)
            {
                CargarSalidas(_paginaActual + 1);
            }
        }
    }
}
