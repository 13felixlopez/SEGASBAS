using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            this.TxtRazonSocial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRazonSocial_KeyPress);
            this.TxtVisitador.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtVisitador_KeyPress);
            this.TxtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTelefono_KeyPress);
            this.TxtTelefono.TextChanged += new System.EventHandler(this.TxtTelefono_TextChanged);
            this.checkProducto.CheckedChanged += new System.EventHandler(this.checkProducto_CheckedChanged);
            this.CheckServicio.CheckedChanged += new System.EventHandler(this.CheckServicio_CheckedChanged);
            this.BTCancelar.Click += new System.EventHandler(this.BTCancelar_Click);
    
        }

        private void LbObservacion_Click(object sender, EventArgs e)
        {

        }
        private void ActualizarEstadoPaginacion()
        {
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / tamanoPagina);
            TxtPagina.Text = paginaActual.ToString();
            TxtTotalPagina.Text = totalPaginas.ToString();
            BtnAnteriorProveedor.Enabled = paginaActual > 1;
            BtnSiguienteProveedor.Enabled = paginaActual < totalPaginas;
        }
        private void CargarProveedoresPaginados()
        {
            try
            {
              
                List<L_Proveedor> lista = funciones.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
                DgvProveedores.DataSource = lista;

                
                ConfigurarDataGridView();

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

            
            DgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
            DgvProveedores.AllowUserToOrderColumns = false;
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

    
            try
            {
          
                string ultimoNumero = funciones.ObtenerUltimoNumeroRegistro();

                int ultimoNumeroInt = 0;
         
                int.TryParse(ultimoNumero, out ultimoNumeroInt);

       
                int nuevoNumero = ultimoNumeroInt + 1;

   
                TxtNumeroRegistro.Text = nuevoNumero.ToString("D2");

               
                TxtNumeroRegistro.ReadOnly = true;
            }
            catch (Exception ex)
            {
                TxtNumeroRegistro.Text = "01";
                TxtNumeroRegistro.ReadOnly = true;
                MessageBox.Show("Error al generar el número de registro automático: " + ex.Message,
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
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
        private bool EsCorreoValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; 

          
            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patron);
        }
        private void BTAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtRazonSocial.Text) || string.IsNullOrWhiteSpace(TxtNRuc.Text))
            {
                MessageBox.Show("Razón Social y Número de RUC son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!EsCorreoValido(TxtCorreo.Text))
            {
                MessageBox.Show("El formato del Correo Electrónico es inválido. Por favor, corríjalo.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCorreo.Focus();
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

 
            try
            {
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
            }
            catch (Exception ex)
            {
              
                MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

          
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
                try
                {
                    string mensaje = funciones.Eliminar(proveedorSeleccionado.id_proveedor);
                    MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProveedoresPaginados();
                    LimpiarControles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidarNumeroRuc(string ruc)
        {
            if (string.IsNullOrWhiteSpace(ruc)) return false;

          
            string cleanRuc = new string(ruc.Where(c => char.IsLetterOrDigit(c)).ToArray());

            if (cleanRuc.Length < 14 || cleanRuc.Length > 17)
            {
                return false;
            }

            int letterCount = cleanRuc.Count(char.IsLetter);
            if (letterCount != 1)
            {
                return false; 
            }

            char firstChar = cleanRuc[0];
            char lastChar = cleanRuc[cleanRuc.Length - 1];

            bool letterIsAtStart = char.IsLetter(firstChar);
            bool letterIsAtEnd = char.IsLetter(lastChar);


            if (!letterIsAtStart && !letterIsAtEnd)
            {
                return false;
            }

  
            if (letterIsAtStart)
            {
                return cleanRuc.Substring(1).All(char.IsDigit);
            }

       
            if (letterIsAtEnd)
            {
                return cleanRuc.Substring(0, cleanRuc.Length - 1).All(char.IsDigit);
            }

            return false;
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

        private void TxtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtVisitador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTelefono_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int originalLength = textBox.Text.Length;
            int originalCursor = textBox.SelectionStart;

            string cleanText = new string(textBox.Text.Where(char.IsDigit).ToArray());
            if (cleanText.Length > 8)
            {
                cleanText = cleanText.Substring(0, 8);
            }

            string formattedText = cleanText;

        
            if (cleanText.Length > 4)
            {
                formattedText = cleanText.Insert(4, "-");
            }

            if (textBox.Text != formattedText)
            {
                textBox.Text = formattedText;


                int newCursor = originalCursor + (formattedText.Length - originalLength);
                if (newCursor < 0) newCursor = 0;
                if (newCursor > formattedText.Length) newCursor = formattedText.Length;
                textBox.SelectionStart = newCursor;
            }
        }

        private void TxtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void checkProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProducto.Checked)
            {
                CheckServicio.Checked = false;
            }
        }

        private void CheckServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckServicio.Checked)
            {
                checkProducto.Checked = false;
            }
        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
