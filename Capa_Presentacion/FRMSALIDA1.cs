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
        private int _totalPaginas = 1;
        private int _totalRegistros = 0;
        public FRMSALIDA1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            this.Load += FRMSALIDA1_Load;
            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
        }


        private void FRMSALIDA1_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombosIniciales();
                ConfigurarControlesLectura();
                CargarSalidas(_paginaActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar formulario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // Productos, Lotes y Ciclos desde D_Salida
                List<string> productos = _datosSalida.ObtenerNombresProductos();
                Cb_Producto.DataSource = productos;
                Cb_Producto.SelectedIndex = -1;

                List<string> lotes = _datosSalida.ObtenerNombresLotes();
                cblote.DataSource = lotes;
                cblote.SelectedIndex = -1;

                List<string> ciclos = _datosSalida.ObtenerDescripcionesCiclos();
                Cb_Ciclo.DataSource = ciclos;
                Cb_Ciclo.SelectedIndex = -1;

                // opciones de búsqueda
                CmbBuscar.Items.Clear();
                CmbBuscar.Items.AddRange(new string[] { "Producto", "Destino", "Ciclo" });
                CmbBuscar.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando combos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cb_Producto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string nombre = Cb_Producto.Text;
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    LimpiarCamposInformacion();
                    return;
                }

                L_Salida datos = _datosSalida.ObtenerDatosProductoParaFormulario(nombre);
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
                MessageBox.Show("Error al obtener datos del producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   

        }
        private void LimpiarCamposInformacion()
        {
            TxtCostoUnitario.Text = "";
            TxtCategoria.Text = "";
            TxtUnidaddemedida.Text = "";
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposRegistro()) return;

            try
            {
                var nueva = new L_Salida
                {
                    NombreProducto = Cb_Producto.Text,
                    Cantidad = int.Parse(TxtCantidad.Text.Trim()),
                    CostoUnitario = decimal.Parse(TxtCostoUnitario.Text.Trim()),
                    FechaSalida = dateTimeSalida.Value.Date,
                    NombreLoteDestino = cblote.SelectedIndex > -1 ? cblote.Text : null,
                    DescripcionCiclo = Cb_Ciclo.SelectedIndex > -1 ? Cb_Ciclo.Text : null,
                    Descripcion = txtdescripcion.Text?.Trim()
                };

                string mensaje;
                bool ok = _datosSalida.InsertarSalida(nueva, out mensaje);

                if (ok)
                {
                    MessageBox.Show("Salida registrada y stock actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormularioTotal();
                    CargarSalidas(_paginaActual);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la salida: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar salida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarFormularioTotal()
        {
            Cb_Producto.SelectedIndex = -1;
            TxtCantidad.Text = "";
            txtdescripcion.Text = "";
            dateTimeSalida.Value = DateTime.Now.Date;
            cblote.SelectedIndex = -1;
            Cb_Ciclo.SelectedIndex = -1;
            LimpiarCamposInformacion();
        }
        private bool ValidarCamposRegistro()
        {
            // Producto seleccionado
            if (Cb_Producto.SelectedIndex == -1 || string.IsNullOrWhiteSpace(Cb_Producto.Text))
            {
                MessageBox.Show("Debe seleccionar el Nombre del Producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Cb_Producto.Focus();
                return false;
            }

            // Cantidad válida
            if (!int.TryParse(TxtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una Cantidad válida y mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCantidad.Focus();
                return false;
            }

            // Costo válido (ya lo tenías antes)
            if (!decimal.TryParse(TxtCostoUnitario.Text.Trim(), out decimal costo) || costo <= 0)
            {
                MessageBox.Show("No se pudo obtener el Costo Promedio del producto. Seleccione nuevamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // --- NUEVA VALIDACIÓN: stock suficiente ---
            try
            {
                // obtiene stock desde D_Salida
                int stockActual = _datosSalida.ObtenerStockActualPorNombre(Cb_Producto.Text);

                if (stockActual <= 0)
                {
                    MessageBox.Show($"El producto seleccionado tiene stock disponible: {stockActual}. No puede registrar salidas.", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (cantidad > stockActual)
                {
                    MessageBox.Show($"La cantidad solicitada ({cantidad}) excede el stock disponible ({stockActual}).", "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtCantidad.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void CargarSalidas(int pagina)
        {
            string termino = TxtBuscar.Text?.Trim() ?? "";
            string campo = CmbBuscar.Text;

            try
            {
                List<L_Salida> lista = _datosSalida.ObtenerSalidasPaginadas(pagina, _tamanoPagina, termino, campo, out _totalRegistros);

                DatagreedSalidas.DataSource = lista;
                // Opcional: ocultar columnas que no quieras mostrar
                DatagreedSalidas.Columns["IdProducto"].Visible = false;

                _totalPaginas = Math.Max(1, (int)Math.Ceiling((double)_totalRegistros / _tamanoPagina));
                _paginaActual = pagina;
                ActualizarEstadoPaginado();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando salidas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ActualizarEstadoPaginado()
        {
            TxtPagina.Text = _paginaActual.ToString();
            TxtTotalPagina.Text = _totalRegistros.ToString();
            BtnAnterior.Enabled = _paginaActual > 1;
            BtnSiguiente.Enabled = _paginaActual < _totalPaginas;
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

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
          
            CargarSalidas(1);
        }
    }
}
