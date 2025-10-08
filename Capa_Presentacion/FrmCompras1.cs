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
    public partial class FrmCompras1 : Form
    {
        private readonly D_Compra _datosCompra = new D_Compra();
        private readonly D_Catalogo _datosCatalogo = new D_Catalogo();
        private List<L_DetalleCompra> itemsTemporales = new List<L_DetalleCompra>();

      
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        public FrmCompras1()
        {
            InitializeComponent();
            DatagreeditemsCompra.AutoGenerateColumns = false;
            CbTipoCompra.TextChanged += (sender, e) => CalcularVencimiento();
            DomainupPlazos.TextChanged += (sender, e) => CalcularVencimiento();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmCompras1_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarHistorialCompras(paginaActual);
            InicializarGrillaTemporal();
        }
        private void InicializarGrillaTemporal()
        {

            DatagreeditemsCompra.Columns.Clear();

         
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioCompra", HeaderText = "Precio Unitario" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubTotal", HeaderText = "Sub Total" });

           
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id_producto", HeaderText = "ID Producto", Visible = false });

          
            DatagreeditemsCompra.DataSource = itemsTemporales;
        }
        private void CargarCombos()
        {
         
            string catalogoActual = "Inicio";

            try
            {

                catalogoActual = "Proveedor";
                DataTable dtProveedor = _datosCatalogo.ListarProveedoresCatalogo();

              
                if (dtProveedor.Rows.Count > 0)
                {
                    Cb_Proveedor.DataSource = dtProveedor;
                    Cb_Proveedor.ValueMember = "id_proveedor"; 
                    Cb_Proveedor.DisplayMember = "razon_social"; 
                    Cb_Proveedor.Enabled = true;
                }
                else
                {
                   
                    MessageBox.Show("Advertencia: No se encontraron proveedores.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cb_Proveedor.Enabled = false;
                }


                catalogoActual = "Producto";
                DataTable dtProducto = _datosCatalogo.ListarProductosCatalogo();

              
                if (dtProducto.Rows.Count > 0)
                {
                    Cb_Producto.DataSource = dtProducto;
                    Cb_Producto.ValueMember = "id_producto";
                    Cb_Producto.DisplayMember = "nombre"; 
                    Cb_Producto.Enabled = true;
                }
                else
                {
                  
                    MessageBox.Show("No se encontraron productos en la base de datos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cb_Producto.Enabled = false; 
                }



                catalogoActual = "Marca";
                DataTable dtMarca = _datosCatalogo.ListarMarcasCatalogo();
                CbMarca.DataSource = dtMarca;
                CbMarca.ValueMember = "id_marca";
                CbMarca.DisplayMember = "nombre";


              
                catalogoActual = "Categoria";
                DataTable dtCategoria = _datosCatalogo.ListarCategoriasCatalogo();
                CbCategoria.DataSource = dtCategoria;
                CbCategoria.ValueMember = "id_categoria";
                CbCategoria.DisplayMember = "nombre";


              
                catalogoActual = "Unidad de Medida";
                DataTable dtUnidad = _datosCatalogo.ListarUnidadesMedidaCatalogo();
                CbUnidadmedida.DataSource = dtUnidad;
                CbUnidadmedida.ValueMember = "id_unidad";
                CbUnidadmedida.DisplayMember = "nombre";


                Cb_Proveedor.SelectedIndex = -1;
                Cb_Producto.SelectedIndex = -1;
                CbMarca.SelectedIndex = -1;
                CbCategoria.SelectedIndex = -1;
                CbUnidadmedida.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error al cargar el catálogo '{catalogoActual}'.\n\nDetalle: {ex.Message}", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btañadiritem_Click(object sender, EventArgs e)
        {

            if (Cb_Producto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar un producto.", "Advertencia");
                return;
            }

            if (!int.TryParse(DomaiUpCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida y mayor a cero.", "Error de Formato");
                return;
            }

            if (!decimal.TryParse(domainUpPrecioCompra.Text, out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Ingrese un precio de compra válido y mayor a cero.", "Error de Formato");
                return;
            }

       
            int idProd = Convert.ToInt32(Cb_Producto.SelectedValue);
            decimal subTotal = cantidad * precioCompra;

          
            itemsTemporales.Add(new L_DetalleCompra()
            {
                Id_producto = idProd,
                NombreProducto = Cb_Producto.Text,
                Cantidad = cantidad,
                PrecioCompra = precioCompra,
                SubTotal = subTotal
            });

            
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;

       
            DomaiUpCantidad.Text = "1";
            domainUpPrecioCompra.Text = "0";
            Cb_Producto.SelectedIndex = -1;

        }
        private void CargarHistorialCompras(int pagina)
        {
            int totalPaginas;
        
            string terminoBusqueda = TxtBuscar.Text;

            List<L_Compra> lista = _datosCompra.ListarComprasPaginado(
                pagina,
                tamanoPagina,
                terminoBusqueda,
                out totalPaginas
            );

            Datagreedguardarcompra.DataSource = lista;
            paginaActual = pagina;

           
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();

            BtnAnterior.Enabled = (paginaActual > 1);
            BtnSiguiente.Enabled = (paginaActual < totalPaginas);

         
            if (Datagreedguardarcompra.Columns.Contains("CompraID"))
                Datagreedguardarcompra.Columns["CompraID"].Visible = false;
        }


        private void CalcularVencimiento()
        {

            if (CbTipoCompra.Text.ToUpper() == "CRÉDITO" || CbTipoCompra.Text.ToUpper() == "CREDITO")
            {
             
                if (int.TryParse(DomainupPlazos.Text, out int plazoDias) && plazoDias > 0)
                {
                 
                    DateTime fechaIngreso = DateTime.Now.Date;

                 
                    DateTime fechaVencimiento = fechaIngreso.AddDays(plazoDias);

                  
                    datetimeVencimiento.Value = fechaVencimiento;
                    datetimeVencimiento.Checked = true;
                }
                else
                {
                    // Si el plazo es 0 o inválido
                    datetimeVencimiento.Checked = false;
                }
            }
            else
            {
                // Si es Contado, la fecha de vencimiento es irrelevante
                datetimeVencimiento.Checked = false;
            }
        }
        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (itemsTemporales.Count == 0 || Cb_Proveedor.SelectedValue == null || string.IsNullOrWhiteSpace(TxtNumeFactura.Text))
            {
                MessageBox.Show("Debe completar la cabecera (Proveedor, No. Factura) y añadir ítems.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? plazoValor = null;
            if (int.TryParse(DomainupPlazos.Text, out int tempPlazo) && tempPlazo > 0)
            {
                plazoValor = tempPlazo;
            }

            try
            {
                int idUsuarioLogeado = 3;

                L_Compra cabecera = new L_Compra
                {
                    Id_proveedor = Convert.ToInt32(Cb_Proveedor.SelectedValue),
                    FechaIngreso = DateTime.Now.Date,
                    NumeroFactura = TxtNumeFactura.Text,
                    TipoCompra = CbTipoCompra.Text,
                    VencimientoFactura = datetimeVencimiento.Checked ? datetimeVencimiento.Value.Date : (DateTime?)null,
                    Plazo = plazoValor, 
                    DescripcionGeneral = txtdescripcion.Text,
                    IdUser = idUsuarioLogeado
                };

                string mensaje = "";
                int nuevaCompraId = _datosCompra.InsertarCabeceraCompra(cabecera, out mensaje);

                if (nuevaCompraId > 0)
                {
               
                    foreach (var item in itemsTemporales)
                    {
                        item.CompraID = nuevaCompraId;
                     
                        _datosCompra.InsertarDetalleYActualizarCosto(item);
                    }

                    MessageBox.Show("Compra registrada, Stock y Costo Promedio (Precio de Salida) actualizados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 
                    itemsTemporales.Clear();
                    DatagreeditemsCompra.DataSource = null;
                    CargarHistorialCompras(1); 
                    LimpiarCamposCabecera();
                }
                else
                {
                    MessageBox.Show("Fallo al registrar la cabecera: " + mensaje, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico en la transacción de guardado: " + ex.Message, "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
                CargarHistorialCompras(paginaActual - 1);
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TxtTotalPagina.Text, out int totalPaginas))
            {
                if (paginaActual < totalPaginas)
                    CargarHistorialCompras(paginaActual + 1);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarHistorialCompras(1);
        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCamposCabecera();
        }
        private void LimpiarCamposCabecera()
        {
         
            Cb_Proveedor.SelectedIndex = -1;
            Cb_Producto.SelectedIndex = -1;
            TxtNumeFactura.Text = string.Empty;
            txtdescripcion.Text = string.Empty;

        
            DomaiUpCantidad.Text = "1";
            domainUpPrecioCompra.Text = "0";
            DomainupPlazos.Text = "0"; 

       
            itemsTemporales.Clear();
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;
        }

        private void DomainupPlazos_SelectedItemChanged(object sender, EventArgs e)
        {
            CalcularVencimiento();
        }

        private void CbTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcularVencimiento();
        }
    }

}
