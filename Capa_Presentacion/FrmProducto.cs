using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace Capa_Presentacion
{
    public partial class FrmProducto : Form
    {
        private bool abiertoDesdeBtProducto = false;

        private D_Producto funciones = new D_Producto();
        private L_Producto productoSeleccionado;
        private int paginaActual = 1;
        private const int tamañoPagina = 10;
        private int totalRegistros = 0;

        // Búsqueda debounce
        private System.Windows.Forms.Timer searchTimer;
        private const int SearchDebounceMs = 400;
        public FrmProducto()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmProducto_Load;
            this.FormClosing += FrmProducto_FormClosing;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {

            searchTimer = new System.Windows.Forms.Timer();
            searchTimer.Interval = SearchDebounceMs;
            searchTimer.Tick += SearchTimer_Tick;

            // Enlazar eventos de búsqueda (si no están enlazados en designer)
            if (TxtBuscar != null)
            {
                TxtBuscar.TextChanged += TxtBuscar_TextChanged;
                TxtBuscar.KeyDown += TxtBuscar_KeyDown;
            }

            if (dgvProducto != null)
            {
                dgvProducto.CellClick += dgvProducto_CellClick;
                dgvProducto.DataBindingComplete += dgvProducto_DataBindingComplete;
                dgvProducto.CellEndEdit += dgvProducto_CellEndEdit;
                dgvProducto.CellDoubleClick += dgvProducto_CellDoubleClick;
            }

            // Ajustes iniciales
            CargarProductosPaginado();
            LimpiarControles();

        }
        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = Math.Max(1, (int)Math.Ceiling((double)totalRegistros / tamañoPagina));
            if (TxtPagina != null) TxtPagina.Text = paginaActual.ToString();
            if (TxtTotalPagina != null) TxtTotalPagina.Text = totalPaginas.ToString();
            if (BtnAnterior != null) BtnAnterior.Enabled = paginaActual > 1;
            if (BtnSiguiente != null) BtnSiguiente.Enabled = paginaActual < totalPaginas;
        }
        private void LimpiarControles()
        {
            productoSeleccionado = null;
            if (TxtProducto != null) TxtProducto.Clear();
            if (BTAgregar != null) BTAgregar.Text = "Agregar";
            if (BTAgregar != null) BTAgregar.Enabled = true;
            if (BTEditar != null) BTEditar.Enabled = false;
            if (BTEliminar != null) BTEliminar.Enabled = false;
        }
        private void CargarProductosPaginado()
        {
            try
            {
                List<L_Producto> lista = funciones.ListarPaginado(paginaActual, tamañoPagina, out totalRegistros);
                if (dgvProducto != null)
                {
                    dgvProducto.DataSource = lista;
                    // ocultar columna id si existe
                    if (dgvProducto.Columns.Contains("id_producto"))
                        dgvProducto.Columns["id_producto"].Visible = false;
                }
                ActualizarEstadoPaginacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarProductosPaginado();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamañoPagina);
            if (paginaActual < totalPaginas)
            {
                paginaActual++;
                CargarProductosPaginado();
            }
        }

        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (TxtProducto == null || string.IsNullOrWhiteSpace(TxtProducto.Text))
            {
                MessageBox.Show("El nombre del producto no puede estar vacío.");
                return;
            }

            L_Producto p = new L_Producto()
            {
                nombre = TxtProducto.Text.Trim(),
                Descripcion = "",
                id_categoria = 0,
                id_unidad = 0,
                StockMinimo = 10,
                ControlStock = true
            };

            string mensaje = funciones.InsertarExt(p);

            MessageBox.Show(mensaje);
            CargarProductosPaginado();
            LimpiarControles();
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para editar.");
                return;
            }

            L_Producto p = funciones.ObtenerProductoPorId(productoSeleccionado.id_producto);

            p.nombre = TxtProducto.Text.Trim();
            // Si tienes comboboxes de categoria/unidad los asignas aquí
            // p.id_categoria = ...
            // p.id_unidad = ...
            // p.ControlStock = ...

            string mensaje = funciones.EditarExt(p);

            MessageBox.Show(mensaje);
            CargarProductosPaginado();
            LimpiarControles();
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                string mensaje = funciones.Eliminar(productoSeleccionado.id_producto);
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarProductosPaginado();
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
                    paginaActual = 1;
                    CargarProductosPaginado();
                    return;
                }

                // Intentamos usar BuscarAvanzado (más columnas); si falla, caemos en Buscar simple
                DataTable dt = null;
                try
                {
                    dt = funciones.BuscarAvanzado(termino);
                }
                catch
                {
                    dt = funciones.Buscar(termino);
                }

                dgvProducto.DataSource = dt;

                FormatearYResaltarGridDespuesDeBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatearYResaltarGridDespuesDeBusqueda()
        {
            try
            {
                if (dgvProducto == null) return;

                if (dgvProducto.Columns.Contains("StockActual"))
                    dgvProducto.Columns["StockActual"].DefaultCellStyle.Format = "N0";
                if (dgvProducto.Columns.Contains("StockMinimo"))
                    dgvProducto.Columns["StockMinimo"].DefaultCellStyle.Format = "N0";
                if (dgvProducto.Columns.Contains("CostoPromedio"))
                    dgvProducto.Columns["CostoPromedio"].DefaultCellStyle.Format = "N2";
                if (dgvProducto.Columns.Contains("UltimoCostoFactura"))
                    dgvProducto.Columns["UltimoCostoFactura"].DefaultCellStyle.Format = "N2";

                foreach (DataGridViewRow row in dgvProducto.Rows)
                {
                    decimal stock = 0M;
                    int min = 0;

                    if (dgvProducto.Columns.Contains("StockActual") && row.Cells["StockActual"].Value != null && row.Cells["StockActual"].Value != DBNull.Value)
                        decimal.TryParse(row.Cells["StockActual"].Value.ToString(), out stock);

                    if (dgvProducto.Columns.Contains("StockMinimo") && row.Cells["StockMinimo"].Value != null && row.Cells["StockMinimo"].Value != DBNull.Value)
                        int.TryParse(row.Cells["StockMinimo"].Value.ToString(), out min);

                    // Si no existe columna Estado la ignoramos, pero coloreamos
                    bool desiredEstado = stock > 0M;

                    if (stock <= 0M)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    }
                    else
                    {
                        if (min > 0 && stock <= min)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                        else
                            row.DefaultCellStyle.BackColor = Color.White;
                    }

                    // Sincronizar estado en BD solo si hay columna Estado y existe id_producto
                    if (dgvProducto.Columns.Contains("Estado") && row.Cells["id_producto"].Value != null)
                    {
                        bool currentEstado = true;
                        object cellVal = row.Cells["Estado"].Value;
                        if (cellVal == null || cellVal == DBNull.Value) currentEstado = true;
                        else
                        {
                            bool.TryParse(cellVal.ToString(), out currentEstado);
                        }

                        int id = Convert.ToInt32(row.Cells["id_producto"].Value);

                        if (currentEstado != desiredEstado)
                        {
                            // hacemos la actualización en BD (solo cambia el flag Estado, no borramos)
                            try
                            {
                                string resp = funciones.ActualizarEstado(id, desiredEstado);
                                // actualizar celda visual
                                row.Cells["Estado"].Value = desiredEstado;
                            }
                            catch
                            {
                                // si falla, lo ignoramos para no romper la interfaz; podrías loguearlo
                            }
                        }
                    }
                }
            }
            catch
            {
                // no bloqueante
            }
        }

        private void dgvProducto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducto != null)
            {
                DataGridViewRow row = dgvProducto.Rows[e.RowIndex];

                if (row.Cells["id_producto"].Value == null) return;

                int id;
                if (!int.TryParse(row.Cells["id_producto"].Value.ToString(), out id)) return;

                productoSeleccionado = new L_Producto()
                {
                    id_producto = id,
                    nombre = row.Cells["nombre"].Value?.ToString() ?? ""
                };

                if (TxtProducto != null) TxtProducto.Text = productoSeleccionado.nombre;
                if (BTEditar != null) BTEditar.Enabled = true;
                if (BTEliminar != null) BTEliminar.Enabled = true;
            }
        }
       

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            RealizarBusqueda();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (abiertoDesdeBtProducto)
            {
                MessageBox.Show("No se puede cerrar porque fue abierto desde BtProducto.");
                e.Cancel = true;
            }
        }

        private void TxtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchTimer.Stop();
                RealizarBusqueda();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                TxtBuscar.Clear();
                searchTimer.Stop();
                paginaActual = 1;
                CargarProductosPaginado();
            }
        }

        private void dgvProducto_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            FormatearYResaltarGridDespuesDeBusqueda();

        }

        private void dgvProducto_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvProducto.Rows[e.RowIndex];
            if (row.Cells["id_producto"].Value == null) return;

            int id = Convert.ToInt32(row.Cells["id_producto"].Value);
            L_Producto p = new L_Producto();
            p.id_producto = id;

            string col = dgvProducto.Columns[e.ColumnIndex].Name;

            switch (col)
            {
                case "StockMinimo":
                    if (decimal.TryParse(row.Cells[col].Value?.ToString(), out decimal nuevoMin))
                        p.StockMinimo = nuevoMin;
                    break;

                case "Estado":
                    if (bool.TryParse(row.Cells[col].Value?.ToString(), out bool nuevoEstado))
                        p.Estado = nuevoEstado;
                    break;

                case "ControlStock":
                    if (bool.TryParse(row.Cells[col].Value?.ToString(), out bool controlStockVal))
                        p.ControlStock = controlStockVal;
                    break;

                case "NombreCategoria":
                case "id_categoria":
                    if (int.TryParse(row.Cells["id_categoria"].Value?.ToString(), out int idcat))
                        p.id_categoria = idcat;
                    break;
            }

            string mensaje = funciones.EditarExt(p);
            MessageBox.Show(mensaje);
            CargarProductosPaginado();
        }

        private void dgvProducto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvProducto.Rows[e.RowIndex];
            if (row.Cells["id_producto"].Value == null) return;

            productoSeleccionado = new L_Producto
            {
                id_producto = Convert.ToInt32(row.Cells["id_producto"].Value),
                nombre = row.Cells["nombre"].Value?.ToString() ?? "",
                Descripcion = row.Cells["Descripcion"].Value?.ToString() ?? "",
                StockActual = row.Cells["StockActual"].Value == DBNull.Value ? 0 : Convert.ToDecimal(row.Cells["StockActual"].Value),
                StockMinimo = row.Cells["StockMinimo"].Value == DBNull.Value ? 0 : Convert.ToDecimal(row.Cells["StockMinimo"].Value),
                Estado = row.Cells["Estado"].Value == DBNull.Value ? true : Convert.ToBoolean(row.Cells["Estado"].Value)
            };

            TxtProducto.Text = productoSeleccionado.nombre;
            BTEditar.Enabled = true;
            BTEliminar.Enabled = true;
        }
    }
}
