using System;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private Timer timerInactividad = new Timer();
        private int segundosInactivo = 0;
        public FrmPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; this.WindowState = FormWindowState.Maximized;
            this.MouseClick += ResetInactividad;
            Panelcontenedorprincipal.MouseClick += ResetInactividad;
            PanelBotonesdemenu.MouseClick += ResetInactividad;
            PanelCatalogo.MouseClick += ResetInactividad;
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
            }
        }
        private void FrmPrincipal_ClickCerrarPanel(object sender, EventArgs e)
        {
            var me = Control.MousePosition;
            var pos = this.PointToClient(me);

            if (Panelrespaldo.Visible && (Panelrespaldo.Bounds.Contains(pos) || BTConfiguracion.Bounds.Contains(pos)))
                return;

            if (PanelCatalogo.Visible && (PanelCatalogo.Bounds.Contains(pos) || BT_Catalogo.Bounds.Contains(pos)))
                return;
            if (Panelrespaldo.Visible) Panelrespaldo.Visible = false;
            if (PanelCatalogo.Visible) PanelCatalogo.Visible = false;

            this.Click -= FrmPrincipal_ClickCerrarPanel;
            Panelcontenedorprincipal.Click -= FrmPrincipal_ClickCerrarPanel;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

            if (PanelBotonesdemenu != null && PanelCatalogo != null)
            {
                PanelCatalogo.Dock = DockStyle.Top;
                PanelCatalogo.AutoScroll = true;
                PanelCatalogo.Visible = false;
                PanelCatalogo.BackColor = PanelBotonesdemenu.BackColor;

                foreach (Control c in PanelCatalogo.Controls)
                    c.Dock = DockStyle.Top;
            }


            Panelrespaldo.Visible = false;
            LblUser.Text = FrmLogin.NombreA;
            BTConfiguracion.Enabled = true;
            BTConfiguracion.Visible = true;
            BTConfiguracion.BringToFront();

            timerInactividad.Interval = 1000;
            timerInactividad.Tick += TimerInactividad_Tick;
            timerInactividad.Start();
        }

        private void TimerInactividad_Tick(object sender, EventArgs e)
        {
            segundosInactivo++;

            // cuando pasan 10 segundos sin actividad
            if (segundosInactivo >= 10)
            {
                if (PanelCatalogo.Visible)
                {
                    PanelCatalogo.Visible = false;
                    BT_Catalogo.Text = "Catálogo  🔽";
                }
                if (Panelrespaldo.Visible)
                {
                    Panelrespaldo.Visible = false;
                }

                segundosInactivo = 0; // Reiniciar contador
            }
        }
        private void ResetInactividad(object sender, MouseEventArgs e)
        {
            segundosInactivo = 0;
        }
        private void BT_Catalogo_Click(object sender, EventArgs e)
        {
            PanelCatalogo.Visible = !PanelCatalogo.Visible;

            // Obtener texto base (sin flechas)
            string textoBase = "Catálogo";

            // Asignar la flecha correcta según si está abierto o cerrado
            if (PanelCatalogo.Visible)
                BT_Catalogo.Text = textoBase + "  🔼";   // flecha hacia arriba
            else
                BT_Catalogo.Text = textoBase + "  🔽";   // flecha hacia abajo
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
        }
        //private void AjustarPanelCatalogo()
        //{
        //    try
        //    {
        //        // Tamaño fijo/limite (ajusta ancho/alto según tu diseño)
        //        int ancho = 220; // ancho del panel catálogo
        //        int maxAltura = Math.Max(120, this.ClientSize.Height - 120); // evita ocupar toda la pantalla

        //        PanelCatalogo.Width = ancho;
        //        PanelCatalogo.AutoSize = false;
        //        PanelCatalogo.AutoScroll = true;

        //        // Obtener posición del botón BT_Catalogo en coordenadas del formulario
        //        var screenPt = BT_Catalogo.PointToScreen(new System.Drawing.Point(0, 0));
        //        var clientPt = this.PointToClient(screenPt);

        //        // Posicionar debajo del botón
        //        int offsetY = 4;
        //        int x = clientPt.X;
        //        int y = clientPt.Y + BT_Catalogo.Height + offsetY;

        //        // Ajustes para que no salga de la ventana
        //        if (x < 4) x = 4;
        //        if (x + PanelCatalogo.Width > this.ClientSize.Width - 4)
        //            x = this.ClientSize.Width - PanelCatalogo.Width - 4;

        //        // Altura deseada (según contenido) limitada por pantalla
        //        int alturaDeseada = PanelCatalogo.PreferredSize.Height;
        //        int alturaFinal = Math.Min(alturaDeseada, maxAltura);
        //        if (alturaFinal < 80) alturaFinal = Math.Min(80, maxAltura);

        //        PanelCatalogo.Size = new System.Drawing.Size(PanelCatalogo.Width, alturaFinal);
        //        PanelCatalogo.Location = new System.Drawing.Point(x, y);

        //        // Asegurarnos que esté en el formulario y al frente
        //        if (PanelCatalogo.Parent != this) this.Controls.Add(PanelCatalogo);
        //        PanelCatalogo.BringToFront();
        //    }
        //    catch
        //    {
        //        // no hacer nada si falla el cálculo
        //    }
        //}
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

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
       
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Hide();

            FrmLogin login = new FrmLogin();
            login.Show();
        }

        private void BtRestaurar_Click(object sender, EventArgs e)
        {
            FrmRestaurarBD frm = new FrmRestaurarBD();
            frm.ShowDialog();
        }
    }
}
