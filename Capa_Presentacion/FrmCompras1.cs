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

        private BindingSource bsItems = new BindingSource();

        private bool facturaBloqueada = false;

        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        public FrmCompras1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            
            bsItems.DataSource = itemsTemporales;

         
            DatagreeditemsCompra.AutoGenerateColumns = false;
            DatagreeditemsCompra.DataSource = bsItems;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CargarTiposCompra()
        {
            if (!CbTipoCompra.Items.Contains("Contado"))
                CbTipoCompra.Items.Add("Contado");
            if (!CbTipoCompra.Items.Contains("Crédito"))
                CbTipoCompra.Items.Add("Crédito");

            CbTipoCompra.SelectedItem = "Contado";
        }


        private void FrmCompras1_Load(object sender, EventArgs e)
        {
            CargarTiposCompra();
            CargarCombos();
            InicializarGrillaTemporal();
            CargarHistorialCompras(paginaActual);

            datetimeVencimiento.ShowUpDown = true;
            datetimeVencimiento.Enabled = false;

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

            Cb_Producto.DropDownStyle = ComboBoxStyle.DropDown;
            Cb_Producto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Cb_Producto.AutoCompleteSource = AutoCompleteSource.ListItems;

            Cb_Proveedor.DropDownStyle = ComboBoxStyle.DropDown;
            Cb_Proveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Cb_Proveedor.AutoCompleteSource = AutoCompleteSource.ListItems;

            CbTipoCompra.DropDownStyle = ComboBoxStyle.DropDownList;
            CbUnidadmedida.DropDownStyle = ComboBoxStyle.DropDownList;

            Cb_Proveedor.KeyPress += Combo_NoNumeros_KeyPress;
            CbMarca.KeyPress += SoloLetras_KeyPress;
            CbCategoria.KeyPress += SoloLetras_KeyPress;
            TxtCantidad.KeyPress += SoloNumeros_KeyPress;
            TxtCostoUnitario.KeyPress += SoloNumeros_KeyPress;
            CbTipoCompra.DropDownStyle = ComboBoxStyle.DropDownList;

            itemAEditar = null;
            SetEditingMode(false);

            try { CbTipoCompra.SelectedIndexChanged -= CbTipoCompra_SelectedIndexChanged; } catch { }
            CbTipoCompra.SelectedIndexChanged += CbTipoCompra_SelectedIndexChanged;

            try { numericPlazos.ValueChanged -= numericPlazos_ValueChanged; } catch { }
            numericPlazos.ValueChanged += numericPlazos_ValueChanged;
        }
        private void SoloLetras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
        }
        private void SetEditingMode(bool editing)
        {
   
            BTAgregar.Enabled = !editing; 
            btañadiritem.Enabled = !editing;    

          
            if (editing)
            {
                btañadiritem.BackColor = System.Drawing.Color.Gray;
                BTAgregar.BackColor = System.Drawing.Color.Gray;
            }
            else
            {
                btañadiritem.BackColor = System.Drawing.Color.Green;
                BTAgregar.BackColor = System.Drawing.Color.Green;
            }
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
     
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;

        
            TextBox txt = sender as TextBox;
            if (e.KeyChar == '.' && txt.Text.Contains("."))
                e.Handled = true;
        }

        private void Combo_NoNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = true;  
        }
        private void DesbloquearCabecera()
        {
            facturaBloqueada = false;
            TxtNumeFactura.Enabled = true;
            Cb_Proveedor.Enabled = true;

            CbTipoCompra.Enabled = true;
            numericPlazos.Enabled = false;
            numericPlazos.Value = 0;

            CbTipoCompra.SelectedItem = "Contado";
            datetimeVencimiento.Enabled = false;
            datetimeVencimiento.Checked = false;
        }
        private void RefrescarGrillaYTotal()
        {
           
            bsItems.ResetBindings(false);

          
            DatagreeditemsCompra.ClearSelection();
            try
            {
                if (DatagreeditemsCompra.Rows.Count == 0)
                    DatagreeditemsCompra.CurrentCell = null;
            }
            catch { }

            ActualizarTotalFactura();
        }
        private void InicializarGrillaTemporal()
        {
            DatagreeditemsCompra.SuspendLayout();

            DatagreeditemsCompra.Columns.Clear();
            DatagreeditemsCompra.AutoGenerateColumns = false;

            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NumeroFactura", HeaderText = "Factura" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProveedor", HeaderText = "Proveedor" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreProducto", HeaderText = "Producto" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreMarca", HeaderText = "Marca" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NombreCategoria", HeaderText = "Categoría" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Cantidad", HeaderText = "Cantidad" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PrecioCompra", HeaderText = "Precio" });
            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SubTotal", HeaderText = "SubTotal" });

            DatagreeditemsCompra.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id_producto", Visible = false });

         
            bsItems.DataSource = itemsTemporales;
            DatagreeditemsCompra.DataSource = bsItems;

            DatagreeditemsCompra.ClearSelection();
            try
            {
                if (DatagreeditemsCompra.Rows.Count == 0)
                    DatagreeditemsCompra.CurrentCell = null;
            }
            catch { }

            DatagreeditemsCompra.ResumeLayout();
        }
        private void CargarCombos()
        {
            string catalogoActual = "Inicio";

            try
            {
                catalogoActual = "Proveedor";
                DataTable dtProveedor = _datosCatalogo.ListarProveedoresCatalogo();
                if (dtProveedor != null && dtProveedor.Rows.Count > 0)
                {
                    Cb_Proveedor.DataSource = dtProveedor;
                    Cb_Proveedor.ValueMember = "id_proveedor";
                    Cb_Proveedor.DisplayMember = "razon_social";
                    Cb_Proveedor.SelectedIndex = -1;
                    Cb_Proveedor.Enabled = true;
                }
                else
                {
                    Cb_Proveedor.DataSource = null;
                    Cb_Proveedor.Enabled = false;
                }

                catalogoActual = "Producto";
                DataTable dtProducto = _datosCatalogo.ListarProductosCatalogo();
                if (dtProducto != null && dtProducto.Rows.Count > 0)
                {
                    Cb_Producto.DataSource = dtProducto;
                    Cb_Producto.ValueMember = "id_producto";
                    Cb_Producto.DisplayMember = "nombre";
                    Cb_Producto.SelectedIndex = -1;
                    Cb_Producto.Enabled = true;
                }
                else
                {
                    Cb_Producto.DataSource = null;
                    Cb_Producto.Enabled = false;
                }

                catalogoActual = "Marca";
                DataTable dtMarca = _datosCatalogo.ListarMarcasCatalogo();
                if (dtMarca != null)
                {
                    CbMarca.DataSource = dtMarca;
                    CbMarca.ValueMember = "id_marca";
                    CbMarca.DisplayMember = "nombre";
                    CbMarca.SelectedIndex = -1;
                }

                catalogoActual = "Categoria";
                DataTable dtCategoria = _datosCatalogo.ListarCategoriasCatalogo();
                if (dtCategoria != null)
                {
                    CbCategoria.DataSource = dtCategoria;
                    CbCategoria.ValueMember = "id_categoria";
                    CbCategoria.DisplayMember = "nombre";
                    CbCategoria.SelectedIndex = -1;
                }

                catalogoActual = "Unidad de Medida";
                DataTable dtUnidad = _datosCatalogo.ListarUnidadesMedidaCatalogo();
                if (dtUnidad != null)
                {
                    CbUnidadmedida.DataSource = dtUnidad;
                    CbUnidadmedida.ValueMember = "id_unidad";
                    CbUnidadmedida.DisplayMember = "nombre";
                    CbUnidadmedida.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando catálogo '{catalogoActual}'.\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Ingrese número de factura.");
                TxtNumeFactura.Focus();
                return;
            }

            if (Cb_Proveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar proveedor.");
                Cb_Proveedor.Focus();
                return;
            }

            if (Cb_Producto.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar producto.");
                Cb_Producto.Focus();
                return;
            }

            if (!decimal.TryParse(TxtCantidad.Text, out decimal cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                TxtCantidad.Focus();
                return;
            }

            if (!decimal.TryParse(TxtCostoUnitario.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Precio inválido.");
                TxtCostoUnitario.Focus();
                return;
            }

            decimal subtotal = cantidad * precio;

            itemsTemporales.Add(new L_DetalleCompra()
            {
                NumeroFactura = TxtNumeFactura.Text,
                NombreProveedor = Cb_Proveedor.Text,
                Id_producto = Convert.ToInt32(Cb_Producto.SelectedValue),
                NombreProducto = Cb_Producto.Text,
                NombreMarca = CbMarca.Text,
                NombreCategoria = CbCategoria.Text,
                Cantidad = cantidad,
                PrecioCompra = precio,
                SubTotal = subtotal
            });

            RefrescarGrillaYTotal();

        
            if (!facturaBloqueada)
            {
                TxtNumeFactura.Enabled = false;
                Cb_Proveedor.Enabled = false;
                CbTipoCompra.Enabled = false;
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
            var tipoCompra = (CbTipoCompra.Text ?? string.Empty).Trim().ToUpper();

            if (tipoCompra == "CRÉDITO" || tipoCompra == "CREDITO")
            {
                numericPlazos.Enabled = true;

                if (numericPlazos.Value > 0)
                {
                    DateTime fechaIngreso = DateTime.Now.Date;
                    DateTime fechaVencimiento = fechaIngreso.AddDays((int)numericPlazos.Value);
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
                numericPlazos.Value = 0;
                datetimeVencimiento.Checked = false;
            }
        }


        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (itemsTemporales.Count == 0)
            {
                MessageBox.Show("No hay ítems en la compra.");
                return;
            }

            if (Cb_Proveedor.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un proveedor válido.");
                return;
            }

            int idUsuarioLogeado = 3;

            L_Compra cabecera = new L_Compra
            {
                Id_proveedor = Convert.ToInt32(Cb_Proveedor.SelectedValue),
                FechaIngreso = DateTime.Now.Date,
                NumeroFactura = TxtNumeFactura.Text,
                TipoCompra = CbTipoCompra.Text,
                VencimientoFactura = datetimeVencimiento.Checked ? datetimeVencimiento.Value.Date : (DateTime?)null,
                Plazo = numericPlazos.Value > 0 ? (int?)Convert.ToInt32(numericPlazos.Value) : null,
                DescripcionGeneral = txtdescripcion.Text,
                IdUser = idUsuarioLogeado
            };

            string mensaje = "";
            int nuevaCompraId = _datosCompra.InsertarCabeceraCompra(cabecera, out mensaje);

            if (nuevaCompraId <= 0)
            {
                MessageBox.Show("Error guardando compra: " + mensaje);
                return;
            }

            foreach (var item in itemsTemporales)
            {
                item.CompraID = nuevaCompraId;
                _datosCompra.InsertarDetalleYActualizarCosto(item);
            }

            MessageBox.Show("Compra registrada correctamente.");

            itemsTemporales.Clear();
            RefrescarGrillaYTotal();
            LimpiarCamposCabecera();

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
            DialogResult r = MessageBox.Show(
                "¿Está seguro que desea cancelar la compra actual?\nSe perderán todos los ítems.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes)
                return;

       
            Cb_Proveedor.SelectedIndex = -1;
            Cb_Producto.SelectedIndex = -1;
            CbMarca.SelectedIndex = -1;
            CbCategoria.SelectedIndex = -1;
            CbUnidadmedida.SelectedIndex = -1;

            TxtNumeFactura.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
            TxtCantidad.Text = string.Empty;
            TxtCostoUnitario.Text = string.Empty;

            itemAEditar = null;
            SetEditingMode(false);

            CbTipoCompra.SelectedItem = "Contado";
            numericPlazos.Value = 0;
            numericPlazos.Enabled = false;

  
            datetimeVencimiento.Value = DateTime.Now;
            datetimeVencimiento.Checked = false;
            datetimeVencimiento.Enabled = false;

            itemsTemporales.Clear();
            RefrescarGrillaYTotal();

            facturaBloqueada = false;
            TxtNumeFactura.Enabled = true;
            Cb_Proveedor.Enabled = true;
            CbTipoCompra.Enabled = true;

            ActualizarTotalFactura();
        }

        private void DomainupPlazos_SelectedItemChanged(object sender, EventArgs e)
        {
            CalcularVencimiento();
        }

        private void CbTipoCompra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbTipoCompra.SelectedIndex < 0) return;
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
            if (DatagreeditemsCompra.CurrentRow == null || DatagreeditemsCompra.CurrentRow.Index < 0)
            {
                MessageBox.Show("Por favor, selecciona un ítem para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int indiceFila = DatagreeditemsCompra.CurrentRow.Index;

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este ítem de la lista temporal?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                if (indiceFila >= 0 && indiceFila < itemsTemporales.Count)
                {
                    itemsTemporales.RemoveAt(indiceFila);
                    RefrescarGrillaYTotal();
                    MessageBox.Show("Ítem eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (itemsTemporales.Count == 0)
                    {
                        DesbloquearCabecera();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar: índice inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefrescarGrillaYTotal();
                }

            }
        }

        private void DatagreeditemsCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= itemsTemporales.Count)
                return;


            itemAEditar = itemsTemporales[e.RowIndex];

            Cb_Producto.SelectedValue = itemAEditar.Id_producto;
            CbMarca.SelectedValue = itemAEditar.Id_marca ?? -1;
            CbCategoria.SelectedValue = itemAEditar.Id_categoria ?? -1;
            CbUnidadmedida.SelectedValue = itemAEditar.Id_unidad ?? -1;

            TxtCantidad.Text = itemAEditar.Cantidad.ToString();
            TxtCostoUnitario.Text = itemAEditar.PrecioCompra.ToString();


            itemsTemporales.RemoveAt(e.RowIndex);
            RefrescarGrillaYTotal();

   
            SetEditingMode(true);

            MessageBox.Show("Elemento cargado para edición. Modifica los campos y presiona 'Añadir Item' para guardar los cambios, o 'Cancelar' para abortar.", "Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void numericPlazos_ValueChanged(object sender, EventArgs e)
        {
            CalcularVencimiento();
        }
    }
}
