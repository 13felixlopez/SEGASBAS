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
        private L_DetalleCompra itemAEditar = null;
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
            DatagreeditemsCompra.CellDoubleClick += DatagreeditemsCompra_CellDoubleClick;
           // Cb_Proveedor.DropDownStyle = ComboBoxStyle.DropDownList;
           //Cb_Producto.DropDownStyle = ComboBoxStyle.DropDownList;
            // DatagreeditemsCompra.Columns.Clear();


            //DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMarca", HeaderText = "Marca" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCategoria", HeaderText = "Categoría" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioCompra", HeaderText = "Precio Unitario" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubTotal", HeaderText = "Sub Total" });
           // DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroFactura", HeaderText = "No. Factura" });


           
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
            datetimeVencimiento.ShowUpDown = true;
            datetimeVencimiento.Enabled = false;
            InicializarGrillaTemporal();
            ActualizarTotalFactura();
            Cb_Proveedor.DropDownStyle = ComboBoxStyle.DropDown;
            Cb_Proveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Cb_Proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
            Cb_Producto.DropDownStyle = ComboBoxStyle.DropDown; 
            Cb_Producto.AutoCompleteMode = AutoCompleteMode.SuggestAppend; 
            Cb_Producto.AutoCompleteSource = AutoCompleteSource.ListItems;
            CbMarca.DropDownStyle = ComboBoxStyle.DropDown;
            CbMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CbMarca.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void DesbloquearCabecera()
        {
     
            facturaBloqueada = false;

            TxtNumeFactura.Enabled = true;
            Cb_Proveedor.Enabled = true;

          
            CbTipoCompra.Enabled = true;
            numericPlazos.Enabled = false;
            numericPlazos.Text = "0";

           
            CbTipoCompra.SelectedItem = "Contado";
            datetimeVencimiento.Enabled = false;
            datetimeVencimiento.Checked = false;
        }
        private void RefrescarGrillaYTotal()
        {
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;

          
            ActualizarTotalFactura();
        }
        private void InicializarGrillaTemporal()
        {
            DatagreeditemsCompra.Columns.Clear();
            DatagreeditemsCompra.AutoGenerateColumns = false;
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroFactura", HeaderText = "No. Factura" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProveedor", HeaderText = "Proveedor" }); 
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMarca", HeaderText = "Marca" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCategoria", HeaderText = "Categoría" });
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

            this.Invoke((MethodInvoker)delegate
            {
                Cb_Proveedor.SelectedIndex = -1;
                Cb_Producto.SelectedIndex = -1;
                CbMarca.SelectedIndex = -1;
                CbCategoria.SelectedIndex = -1;
                CbUnidadmedida.SelectedIndex = -1;
            });
        }
        private void ActualizarTotalFactura()
        {
            decimal total = 0;
            foreach (var item in itemsTemporales)
            {
                total += item.SubTotal;
            }
            lbtotalfactura.Text = total.ToString("C");

        }
        private void btañadiritem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNumeFactura.Text))
            {
                MessageBox.Show("Debe ingresar el número de factura.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TxtNumeFactura.Focus();
                return;
            }
            if (Cb_Proveedor.SelectedValue == null || Convert.ToInt32(Cb_Proveedor.SelectedValue) <= 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cb_Proveedor.Focus();
                return;
            }

            string tipoCompra = CbTipoCompra.Text.Trim().ToUpper();

            if (tipoCompra == "CRÉDITO" || tipoCompra == "CREDITO")
            {
                DateTime fechaIngreso = DateTime.Now.Date;
                if (!datetimeVencimiento.Checked || datetimeVencimiento.Value.Date <= fechaIngreso)
                {
                    MessageBox.Show("Para una compra a Crédito, debe especificar un Plazo válido que resulte en una Fecha de Vencimiento futura.", "Advertencia de Crédito", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    numericPlazos.Focus();
                    return;
                }
            }
            if (Cb_Producto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar el Producto a comprar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cb_Producto.Focus();
                return;
            }
            if (CbUnidadmedida.SelectedValue == null || Convert.ToInt32(CbUnidadmedida.SelectedValue) <= 0)
            {
                MessageBox.Show("Debe seleccionar la Unidad de Medida para el producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CbUnidadmedida.Focus();
                return;
            }

            if (!decimal.TryParse(TxtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una Cantidad válida y mayor a cero.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtCantidad.Focus();
                return;
            }

            if (!decimal.TryParse(TxtCostoUnitario.Text, out decimal precioCompra) || precioCompra <= 0)
            {
                MessageBox.Show("Ingrese un Precio Unitario de compra válido y mayor a cero.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtCostoUnitario.Focus();
                return;
            }
            int idProd = Convert.ToInt32(Cb_Producto.SelectedValue);
            int? idUnidad = CbUnidadmedida.SelectedValue != null ? Convert.ToInt32(CbUnidadmedida.SelectedValue) : (int?)null;
            int? idMarca = CbMarca.SelectedValue != null ? Convert.ToInt32(CbMarca.SelectedValue) : (int?)null;
            int? idCategoria = CbCategoria.SelectedValue != null ? Convert.ToInt32(CbCategoria.SelectedValue) : (int?)null;
            string numFactura = TxtNumeFactura.Text.Trim();
            string nombreProveedor = Cb_Proveedor.Text;
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

                NumeroFactura = numFactura,
                NombreProveedor = nombreProveedor
            });
            if (Cb_Proveedor.SelectedValue == null || Convert.ToInt32(Cb_Proveedor.SelectedValue) <= 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cb_Proveedor.Focus();
                return;
            }
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;
            ActualizarTotalFactura();
            if (!facturaBloqueada)
            {
                TxtNumeFactura.Enabled = false;
                Cb_Proveedor.Enabled = false;
                CbTipoCompra.Enabled = false;
                numericPlazos.Enabled = false; 
                datetimeVencimiento.Enabled = false;

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

            itemsTemporales.Clear();
            DatagreeditemsCompra.DataSource = null;
            DatagreeditemsCompra.DataSource = itemsTemporales;
            facturaBloqueada = false;
            TxtNumeFactura.Enabled = true;
            Cb_Proveedor.Enabled = true;
            CbTipoCompra.Enabled = true;

            ActualizarTotalFactura();
            CbTipoCompra.SelectedItem = "Contado";
            CalcularVencimiento();
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

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (DatagreeditemsCompra.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona un ítem para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int indiceFila = DatagreeditemsCompra.CurrentRow.Index;

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este ítem de la lista temporal?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
              
                itemsTemporales.RemoveAt(indiceFila);

               
                RefrescarGrillaYTotal();

                MessageBox.Show("Ítem eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                if (itemsTemporales.Count == 0)
                {
                    DesbloquearCabecera();
                }
            }

        }

        private void DatagreeditemsCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Salir si el índice es inválido

            // Obtener el objeto L_DetalleCompra de la fila
            // Esta línea es segura ya que se comprobó e.RowIndex >= 0
            L_DetalleCompra itemAEditar = itemsTemporales[e.RowIndex];

            try
            {
         
                Cb_Producto.SelectedValue = itemAEditar.Id_producto > 0 ? (object)itemAEditar.Id_producto : null;


                CbMarca.SelectedValue = itemAEditar.Id_marca.GetValueOrDefault() > 0 ? (object)itemAEditar.Id_marca.Value : null;

                CbCategoria.SelectedValue = itemAEditar.Id_categoria.GetValueOrDefault() > 0 ? (object)itemAEditar.Id_categoria.Value : null;

                CbUnidadmedida.SelectedValue = itemAEditar.Id_unidad.GetValueOrDefault() > 0 ? (object)itemAEditar.Id_unidad.Value : null;

                TxtCantidad.Text = itemAEditar.Cantidad.ToString();
                TxtCostoUnitario.Text = itemAEditar.PrecioCompra.ToString();

                itemsTemporales.RemoveAt(e.RowIndex);
                RefrescarGrillaYTotal();

                MessageBox.Show("Ítem cargado para edición. Modifica los campos y haz clic en 'Añadir Ítem'.", "Modificando Ítem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
              
                MessageBox.Show("Error al cargar el ítem para edición. Confirme que el ID del producto y los catálogos existen en la base de datos. Detalle: " + ex.Message, "Error de Edición", MessageBoxButtons.OK, MessageBoxIcon.Error);

               
                itemsTemporales.Insert(e.RowIndex, itemAEditar);
                RefrescarGrillaYTotal();
            }
        }

        
    }
}
