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

        private bool facturaBloqueada = false;


        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        public FrmCompras1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            DatagreeditemsCompra.AutoGenerateColumns = false;
            CbTipoCompra.TextChanged += (sender, e) => CalcularVencimiento();
            numericPlazos.TextChanged += (sender, e) => CalcularVencimiento();

            CbTipoCompra.TextChanged += (sender, e) => CalcularVencimiento();
            numericPlazos.TextChanged += (sender, e) => CalcularVencimiento();
            DatagreeditemsCompra.Columns.Clear();

          
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMarca", HeaderText = "Marca" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCategoria", HeaderText = "Categoría" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioCompra", HeaderText = "Precio Unitario" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubTotal", HeaderText = "Sub Total" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroFactura", HeaderText = "No. Factura" });


           
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id_producto", HeaderText = "ID Producto", Visible = false });
            this.StartPosition = FormStartPosition.CenterScreen;


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CargarTiposCompra()
        {
        
            CbTipoCompra.Items.Add("Contado");
            CbTipoCompra.Items.Add("Crédito");

            
            CbTipoCompra.SelectedItem = "Contado";
        }

        private void FrmCompras1_Load(object sender, EventArgs e)
        {
            CargarTiposCompra(); 
            CargarCombos();
            CargarHistorialCompras(paginaActual);
            InicializarGrillaTemporal();
            datetimeVencimiento.ShowUpDown = true;
            datetimeVencimiento.Enabled = false;
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
            if (string.IsNullOrWhiteSpace(TxtNumeFactura.Text))
            {
                MessageBox.Show("Debe ingresar el número de factura antes de agregar ítems.", "Advertencia");
                return;
            }
            if (!decimal.TryParse(TxtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida y mayor a cero.", "Error de Formato");
                return;
            }
            if (!decimal.TryParse(TxtCostoUnitario.Text, out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Ingrese un precio de compra válido y mayor a cero.", "Error de Formato");
                return;
            }

         
            int idProd = Convert.ToInt32(Cb_Producto.SelectedValue);
            int? idUnidad = CbUnidadmedida.SelectedValue != null ? Convert.ToInt32(CbUnidadmedida.SelectedValue) : (int?)null;
            int? idMarca = CbMarca.SelectedValue != null ? Convert.ToInt32(CbMarca.SelectedValue) : (int?)null;
            int? idCategoria = CbCategoria.SelectedValue != null ? Convert.ToInt32(CbCategoria.SelectedValue) : (int?)null;
            string numFactura = TxtNumeFactura.Text.Trim();

            decimal subTotal = cantidad * precioCompra;


            itemsTemporales.Add(new L_DetalleCompra()
            {
                Id_producto = idProd,
                NombreProducto = Cb_Producto.Text,
                Cantidad = cantidad,
                PrecioCompra = precioCompra,
                SubTotal = subTotal,
                
                Id_unidad = idUnidad,
                Id_marca = idMarca,
                Id_categoria = idCategoria,
                
                NombreMarca = CbMarca.Text, 
                NombreCategoria = CbCategoria.Text, 
                NumeroFactura = numFactura
            });

            
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;

          
            if (!facturaBloqueada)
            {
                TxtNumeFactura.Enabled = false;
                facturaBloqueada = true;
            }

           
            TxtCantidad.Text = "1";
            TxtCostoUnitario.Text = "0";
            Cb_Producto.SelectedIndex = -1;
            CbMarca.SelectedIndex = -1;
            CbCategoria.SelectedIndex = -1;
            CbUnidadmedida.SelectedIndex = -1;


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

          
            paginaActual = pagina;

           
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();

            BtnAnterior.Enabled = (paginaActual > 1);
            BtnSiguiente.Enabled = (paginaActual < totalPaginas);

         
            
        }


        private void CalcularVencimiento()
        {
            string tipoCompra = CbTipoCompra.Text.Trim().ToUpper();

            if (tipoCompra == "CRÉDITO" || tipoCompra == "CREDITO")
            {

                numericPlazos.Enabled = true;

                if (int.TryParse(numericPlazos.Text, out int plazoDias) && plazoDias > 0)
                {

                    DateTime fechaIngreso = DateTime.Now.Date;


                    DateTime fechaVencimiento = fechaIngreso.AddDays(plazoDias);


                    datetimeVencimiento.Value = fechaVencimiento;
                    datetimeVencimiento.Checked = true;
                }
                else
                {
               
                    datetimeVencimiento.Checked = false;
                }
            }
            else
            {

                numericPlazos.Enabled = false;


                numericPlazos.Text = "0";

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
            if (int.TryParse(numericPlazos.Text, out int tempPlazo) && tempPlazo > 0)
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

        
            TxtCantidad.Text = "1";
            TxtCostoUnitario.Text = "0";
            numericPlazos.Text = "0"; 

       
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

        private void DomaiUpCantidad_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void datetimeVencimiento_ValueChanged(object sender, EventArgs e)
        {
            datetimeVencimiento.Enabled = false;
           
        }
      
        private void btañadirProveedor_Click(object sender, EventArgs e)
        {
            Frm_Proveedores form2 = new Frm_Proveedores();
            form2.BackColor = Color.DarkGray;
            form2.StartPosition = FormStartPosition.CenterScreen;
            if (form2.Controls["panel1"] is Panel panel)
            {
                panel.BackColor = Color.DarkGray; 
            }
         
            if (form2.Controls["Labelpaginaproveedor"] is Label lbl)
            {
                lbl.BackColor = Color.DarkGray;
                lbl.ForeColor = Color.Black;
            }

            if (form2.Controls["BtnAnteriorProveedor"] is Button btn)
            {
                btn.BackColor = Color.DarkGray;
                btn.ForeColor = Color.Black;
            }

            if (form2.Controls["BtnSiguienteProveedor"] is Label lb)
            {
                lb.BackColor = Color.DarkGray;
                lb.ForeColor = Color.Black;
            }

            if (form2.Controls["Lbpaginadodeproveedor"] is Button bt)
            {
                bt.BackColor = Color.DarkGray;
                bt.ForeColor = Color.Black;
            }
            form2.ShowDialog();
        }

        private void BtAñadirproducto_Click(object sender, EventArgs e)
        {
            FrmProducto form2 = new FrmProducto();
            form2.BackColor = Color.DarkGray;
            form2.StartPosition = FormStartPosition.CenterScreen;
            if (form2.Controls["panel1"] is Panel panel)
            {
                panel.BackColor = Color.DarkGray;
            }
            form2.ShowDialog();
        }

        private void btañadirmarca_Click(object sender, EventArgs e)
        {
            FrmMarca form2 = new FrmMarca();
            form2.BackColor = Color.DarkGray;
            form2.StartPosition = FormStartPosition.CenterScreen;
            if (form2.Controls["panel1"] is Panel panel)
            {
                panel.BackColor = Color.DarkGray;
            }
            form2.ShowDialog();
        }

        private void btañadirunidaddemedida_Click(object sender, EventArgs e)
        {
            Frm_Unidadmedida form2 = new Frm_Unidadmedida();
            form2.BackColor = Color.DarkGray;
            form2.StartPosition = FormStartPosition.CenterScreen;
            if (form2.Controls["panel1"] is Panel panel)
            {
                panel.BackColor = Color.DarkGray;
            }
            form2.ShowDialog();
        }

        private void btañadircategoria_Click(object sender, EventArgs e)
        {
            FrmCategoria form2 = new FrmCategoria();
            form2.BackColor = Color.DarkGray;
            form2.StartPosition = FormStartPosition.CenterScreen;
            if (form2.Controls["panel1"] is Panel panel)
            {
                panel.BackColor = Color.DarkGray;
            }
            form2.ShowDialog();
        }
    }

}
