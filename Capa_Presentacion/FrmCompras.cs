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
    public partial class FrmCompras : Form
    {
        private D_Compra objDCompra = new D_Compra();
        private DataTable dtDetalleCompra = new DataTable();

        private int PaginaActual = 1;
        private const int TamanoPagina = 10;
        private int TotalRegistros = 0;
        private int TotalPaginas = 0;
        private int idCompraEditando = 0;
        public FrmCompras()
        {
            InitializeComponent();
            ConfigurarDetalle();
            CargarCombos();
            CargarDatosPaginados(1);

            BTAgregar.Click += BTAgregar_Click;
            btañadiritem.Click += btañadiritem_Click;
            BTCancelar.Click += BTCancelar_Click;

            BtnAnterior.Click += BtnAnterior_Click;
            BtnSiguiente.Click += BtnSiguiente_Click;
            TxtBuscar.TextChanged += TxtBuscar_TextChanged;
        }
        private void CargarCombos()
        {

            var proveedores = objDCompra.ListarProveedores();
            Cb_Proveedor.DataSource = proveedores;
            Cb_Proveedor.DisplayMember = "Value";
            Cb_Proveedor.ValueMember = "Key";
         
            if (proveedores.Count > 0)
            {
                Cb_Proveedor.SelectedIndex = 0; 
            }
            else
            {
                Cb_Proveedor.SelectedIndex = -1;
            }


            var productos = objDCompra.ListarProductos();
            Cb_Producto.DataSource = productos; 
            Cb_Producto.DisplayMember = "Value";
            Cb_Producto.ValueMember = "Key";
            Cb_Producto.SelectedIndex = -1;

          
            var unidades = objDCompra.ListarUnidadesMedida();
            CbUnidadmedida.DataSource = unidades; 
            CbUnidadmedida.DisplayMember = "Value";
            CbUnidadmedida.ValueMember = "Key";
            CbUnidadmedida.SelectedIndex = -1;

            
            var marcas = objDCompra.ListarMarcas();
            CbMarca.DataSource = marcas; 
            CbMarca.DisplayMember = "Value";
            CbMarca.ValueMember = "Key";
            CbMarca.SelectedIndex = -1;

            
            var categorias = objDCompra.ListarCategorias();
            CbCategoria.DataSource = categorias; 
            CbCategoria.DisplayMember = "Value";
            CbCategoria.ValueMember = "Key";
            CbCategoria.SelectedIndex = -1;

         
            CbTipoCompra.Items.Clear();
            CbTipoCompra.Items.Add("Contado");
            CbTipoCompra.Items.Add("Crédito");
            CbTipoCompra.SelectedIndex = 0;

        
            dateTimePicker3.Value = DateTime.Today;
            datetimeVencimiento.Value = DateTime.Today;
        }
        private void CargarDatosPaginados(int pagina)
        {
            
            List<L_Compra> lista = objDCompra.ObtenerComprasPaginadas(pagina, TamanoPagina, out TotalRegistros);

            TotalPaginas = (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);
            PaginaActual = pagina;


            DatagreedCompra.DataSource = lista;

         
        }
        private void ConfigurarDetalle()
        {
            DatagreedCompra.DataSource = dtDetalleCompra;
            dtDetalleCompra.Columns.Add("id_producto", typeof(int));
            dtDetalleCompra.Columns.Add("Producto", typeof(string));
            dtDetalleCompra.Columns.Add("id_unidad", typeof(int));
            dtDetalleCompra.Columns.Add("UnidadMedida", typeof(string));
            dtDetalleCompra.Columns.Add("id_marca", typeof(int));
            dtDetalleCompra.Columns.Add("Marca", typeof(string));
            dtDetalleCompra.Columns.Add("id_categoria", typeof(int));
            dtDetalleCompra.Columns.Add("Categoria", typeof(string));
            dtDetalleCompra.Columns.Add("cantidad", typeof(decimal));
            dtDetalleCompra.Columns.Add("PrecioUnitarioReal", typeof(decimal));
            dtDetalleCompra.Columns.Add("TotalReal", typeof(decimal));

            // dgvDetalleItems.DataSource = dtDetalleCompra; 
        }
        private void BTEliminar_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtAñadirproducto_Click(object sender, EventArgs e)
        {
            FrmProducto nuevoFormulario = new FrmProducto();
            nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
            nuevoFormulario.BackColor = Color.LightGray;
            nuevoFormulario.ShowDialog();
        }

        private void btañadirProveedor_Click(object sender, EventArgs e)
        {
            Frm_Proveedores nuevoFormulario = new Frm_Proveedores();
            nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
            nuevoFormulario.BackColor = Color.LightGray;
            nuevoFormulario.Controls["panel1"].BackColor = Color.LightGray;
            nuevoFormulario.ShowDialog();
        }

        private void btañadirmarca_Click(object sender, EventArgs e)
        {
            FrmMarca nuevoFormulario = new FrmMarca();
            nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
            nuevoFormulario.BackColor = Color.LightGray;
            nuevoFormulario.ShowDialog();
       
        }

        private void btañadircategoria_Click(object sender, EventArgs e)
        {
            FrmCategoria nuevoFormulario = new FrmCategoria();
            nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
            nuevoFormulario.BackColor = Color.LightGray;
            nuevoFormulario.ShowDialog();
            
        }

        private void btañadirunidaddemedida_Click(object sender, EventArgs e)
        {
            Frm_Unidadmedida nuevoFormulario = new Frm_Unidadmedida();
            nuevoFormulario.StartPosition = FormStartPosition.CenterScreen;
            nuevoFormulario.BackColor = Color.LightGray;
            nuevoFormulario.ShowDialog();
            
        }

        private void btañadiritem_Click(object sender, EventArgs e)
        {
       
            decimal cantidad, precioUnitario;
            if (Cb_Producto.SelectedValue == null ||
                !decimal.TryParse(DomaiUpCantidad.Text, out cantidad) ||
                !decimal.TryParse(domainUpPrecioCompra.Text, out precioUnitario) ||
                cantidad <= 0 || precioUnitario <= 0)
            {
                MessageBox.Show("Debe seleccionar un producto, e ingresar Cantidad y Precio válidos (mayores a cero).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            int idProducto = Convert.ToInt32(Cb_Producto.SelectedValue);

            if (dtDetalleCompra.AsEnumerable().Any(row => row.Field<int>("id_producto") == idProducto))
            {
                MessageBox.Show("El producto ya está en la lista de detalle.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

          
            int idUnidad = CbUnidadmedida.SelectedValue != null ? Convert.ToInt32(CbUnidadmedida.SelectedValue) : 0;
            int idMarca = CbMarca.SelectedValue != null ? Convert.ToInt32(CbMarca.SelectedValue) : 0;
            int idCategoria = CbCategoria.SelectedValue != null ? Convert.ToInt32(CbCategoria.SelectedValue) : 0;

          
            dtDetalleCompra.Rows.Add(
                idProducto,
                Cb_Producto.Text,
                idUnidad,
                CbUnidadmedida.Text,
                idMarca,
                CbMarca.Text,
                idCategoria,
                CbCategoria.Text,
                cantidad,
                precioUnitario,
                cantidad * precioUnitario 
            );

     
          

            LimpiarCamposDetalle();
        }
       
        private void BTAgregar_Click(object sender, EventArgs e)
        {
           
            if (Cb_Proveedor.SelectedValue == null || string.IsNullOrEmpty(TxtNumeFactura.Text) || dtDetalleCompra.Rows.Count == 0)
            {
                MessageBox.Show("Faltan datos de la Cabecera (Proveedor/Factura) o no se han agregado productos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

         
            int? plazoDias = string.IsNullOrEmpty(DomainupPlazos.Text) ? (int?)null : Convert.ToInt32(DomainupPlazos.Text);

            L_Compra objCompra = new L_Compra()
            {
                IDProveedor = Convert.ToInt32(Cb_Proveedor.SelectedValue),
                NumeroFactura = TxtNumeFactura.Text,
                FechaIngreso = dateTimePicker3.Value.Date,
                TipoCompra = CbTipoCompra.Text,
                PlazoDias = plazoDias,
                VencimientoFactura = datetimeVencimiento.Value.Date,
                Descripcion = txtdescripcion.Text
            };

            
            DataTable dtParaEnvio = PrepararDataTableParaSQL(dtDetalleCompra);

            string mensaje = "";

            
            if (objDCompra.InsertarCompra(objCompra, dtParaEnvio, out mensaje))
            {
                MessageBox.Show(mensaje, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
                CargarDatosPaginados(1); 
            }
            else
            {
                MessageBox.Show($"Error al guardar: {mensaje}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            string termino = TxtBuscar.Text.Trim();
            if (string.IsNullOrEmpty(termino))
            {
                CargarDatosPaginados(1);
            }
            else
            {
                List<L_Compra> lista = objDCompra.BuscarCompras(termino);
                DatagreedCompra.DataSource = lista;

             
                BtnAnterior.Enabled = false;
                BtnSiguiente.Enabled = false;
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (PaginaActual > 1) CargarDatosPaginados(PaginaActual - 1);
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (PaginaActual < TotalPaginas) CargarDatosPaginados(PaginaActual + 1);
        }
        private void LimpiarCamposDetalle()
        {
            Cb_Producto.SelectedIndex = -1;
            CbUnidadmedida.SelectedIndex = -1;
            CbMarca.SelectedIndex = -1;
            CbCategoria.SelectedIndex = -1;
            DomaiUpCantidad.Text = "1";
            domainUpPrecioCompra.Text = "0.00";
        }
        private void LimpiarFormulario()
        {
            Cb_Proveedor.SelectedIndex = -1;
            TxtNumeFactura.Clear();
            txtdescripcion.Clear();
            DomainupPlazos.Text = "0";

            dateTimePicker3.Value = DateTime.Today;
            datetimeVencimiento.Value = DateTime.Today;

            dtDetalleCompra.Clear();
            LimpiarCamposDetalle();

            idCompraEditando = 0;
            BTAgregar.Text = "Agregar";
            BTEditar.Enabled = false; 
            BTEliminar.Enabled = false; 
        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            CargarDatosPaginados(1);
        }
        private DataTable PrepararDataTableParaSQL(DataTable dtOrigen)
        {
            // ESTRUCTURA EXACTA DEL TIPO DE TABLA DE SQL: TipoDetalleCompra
            DataTable dt = new DataTable();
            dt.Columns.Add("id_producto", typeof(int));
            dt.Columns.Add("id_unidad", typeof(int));
            dt.Columns.Add("id_marca", typeof(int));
            dt.Columns.Add("id_categoria", typeof(int));
            dt.Columns.Add("cantidad", typeof(decimal));
            dt.Columns.Add("PrecioUnitarioReal", typeof(decimal));
            dt.Columns.Add("TotalReal", typeof(decimal));

            foreach (DataRow row in dtOrigen.Rows)
            {
                dt.Rows.Add(
                    row.Field<int>("id_producto"),
                    row.Field<int>("id_unidad"),
                    row.Field<int>("id_marca"),
                    row.Field<int>("id_categoria"),
                    row.Field<decimal>("cantidad"),
                    row.Field<decimal>("PrecioUnitarioReal"),
                    row.Field<decimal>("TotalReal")
                );
            }
            return dt;
        }
    }
}
