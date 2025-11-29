using System;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; this.WindowState = FormWindowState.Maximized;

        }
        private void AbrirFormularioEnPanel(Form formulario)
        {
            try
            {
                if (pictureBoxFoto != null) pictureBoxFoto.Visible = false;

                Panelcontenedorprincipal.Controls.Clear();

                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                Panelcontenedorprincipal.Controls.Add(formulario);
                Panelcontenedorprincipal.Tag = formulario;
                formulario.Show();

                if (Panelrespaldo.Parent != this)
                {
                    this.Controls.Add(Panelrespaldo);
                }
                Panelrespaldo.BringToFront();
                if (PanelCatalogo.Parent != this)
                {
                    this.Controls.Add(PanelCatalogo);
                }
                PanelCatalogo.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir formulario: " + ex.Message);
            }
        }

        private void PanelCatalogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AjustarPanelRespaldo()
        {
            try
            {
                int panelWidth = 200;
                Panelrespaldo.Width = panelWidth;
                Panelrespaldo.AutoSize = false;
                Panelrespaldo.AutoScroll = true;

                var screenPt = BTConfiguracion.PointToScreen(new System.Drawing.Point(0, 0));
                var clientPt = this.PointToClient(screenPt);

                int offsetY = 4;
                int x = clientPt.X + BTConfiguracion.Width - Panelrespaldo.Width;
                int y = clientPt.Y + BTConfiguracion.Height + offsetY;

       
                if (x < 4) x = 4;
                if (x + Panelrespaldo.Width > this.ClientSize.Width - 4)
                    x = this.ClientSize.Width - Panelrespaldo.Width - 4;

             
                int espacioDisponibleAbajo = this.ClientSize.Height - y - 8;
                int alturaMaxima = Math.Max(100, espacioDisponibleAbajo); 
                int alturaDeseada = Panelrespaldo.PreferredSize.Height; 

                int alturaFinal = Math.Min(alturaDeseada, alturaMaxima);
            
                if (alturaFinal < 80) alturaFinal = Math.Min(80, alturaMaxima);

                Panelrespaldo.Height = alturaFinal;
                Panelrespaldo.Location = new System.Drawing.Point(x, y);

                if (Panelrespaldo.Parent != this)
                {
                    this.Controls.Add(Panelrespaldo);
                }

                Panelrespaldo.BringToFront();
                BTConfiguracion.BringToFront();
            }
            catch
            {
                // no hacemos nada si falla el cálculo
            }
        }
        private void FrmPrincipal_ClickCerrarPanel(object sender, EventArgs e)
        {
            // Solo cerramos si está visible
            if (Panelrespaldo.Visible)
            {
             
                var me = Control.MousePosition;
                var pos = this.PointToClient(me);

                if (Panelrespaldo.Bounds.Contains(pos) || BTConfiguracion.Bounds.Contains(pos))
                {
                    return;
                }

                Panelrespaldo.Visible = false;

                // remover handlers
                this.Click -= FrmPrincipal_ClickCerrarPanel;
                Panelcontenedorprincipal.Click -= FrmPrincipal_ClickCerrarPanel;
            }
        }
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Panelrespaldo.Visible = false;
            PanelCatalogo.Visible = false;
            LblUser.Text = FrmLogin.NombreA;
            if (Panelrespaldo.Parent != this)
            {
                this.Controls.Add(Panelrespaldo);
            }
            Panelrespaldo.BringToFront();
            if (PanelCatalogo.Parent != this)
            {
                this.Controls.Add(PanelCatalogo);
            }
            PanelCatalogo.BringToFront();
            BTConfiguracion.Enabled = true;
            BTConfiguracion.Visible = true;
            BTConfiguracion.BringToFront();

            BTConfiguracion.Click -= BTConfiguracion_Click;
            BTConfiguracion.Click += BTConfiguracion_Click;
        }

        private void BT_Catalogo_Click(object sender, EventArgs e)
        {

            PanelCatalogo.Visible = !PanelCatalogo.Visible;
            if (BT_Catalogo.Text == "Catálogo  🔽")
            {
                BT_Catalogo.Text = "Catálogo 🔼";
            }
            else
            {
                BT_Catalogo.Text = "Catálogo 🔽";
            }
        }

        private void BT_INICIO_Click(object sender, EventArgs e)
        {
            Panelcontenedorprincipal.Controls.Clear();

            pictureBoxFoto.Dock = DockStyle.Fill;
            pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxFoto.Visible = true;
            BtModoOscuro_Claro.Visible = true;
            BTInformacio.Visible = false;

            Panelcontenedorprincipal.Controls.Add(pictureBoxFoto);

      
            BTConfiguracion.BringToFront();
            Panelrespaldo.BringToFront();
            PanelCatalogo.BringToFront();
        }

        private void btcargo_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCargo());
        }

        private void btActividades_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmActividad());
        }

        private void BtCiclos_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCiclo());
        }

        private void BT_Empleado_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmEmpleado());
        }

        private void BT_Asistenacia_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmAsistencia());
        }

        private void BtLotes_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmLote());
        }

        private void BtCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCategoria());

        }

        private void BtUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmUsuario());
        }

        private void BtProducto_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmProducto());

        }

        private void BtMarcas_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmMarca());
        }

        private void BtUnidaddemedida_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new Frm_Unidadmedida());
        }

        private void BtProveedor_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new Frm_Proveedores());
        }

        private void Bt_Compra_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmCompras1());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FRMSALIDA1());
        }

        private void BtPlanilla_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmPlanillaAsegurado());
        }

        private void BtReporte_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new FrmReporte());
        }

        private void BTConfiguracion_Click(object sender, EventArgs e)
        {
            AjustarPanelRespaldo(); // recalcula tamaño y posición antes de mostrar
            Panelrespaldo.Visible = !Panelrespaldo.Visible;

            if (Panelrespaldo.Visible)
            {
           
                this.Click -= FrmPrincipal_ClickCerrarPanel;
                this.Click += FrmPrincipal_ClickCerrarPanel;
                Panelcontenedorprincipal.Click -= FrmPrincipal_ClickCerrarPanel;
                Panelcontenedorprincipal.Click += FrmPrincipal_ClickCerrarPanel;
            }
            else
            {
                this.Click -= FrmPrincipal_ClickCerrarPanel;
                Panelcontenedorprincipal.Click -= FrmPrincipal_ClickCerrarPanel;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmRespaldo frm = new FrmRespaldo();
            frm.ShowDialog();
        }

        private void Panelrespaldo_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void BtInformacion_Click(object sender, EventArgs e)
        {
            Frm_Informacion frm = new Frm_Informacion();
            frm.ShowDialog();
        }

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            AjustarPanelRespaldo();
        }
    }
}
