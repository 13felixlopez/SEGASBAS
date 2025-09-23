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
            pictureBoxFoto.Visible = false;

            Panelcontenedorprincipal.Controls.Clear();


            formulario.TopLevel = false;
            formulario.Dock = DockStyle.Fill;
            Panelcontenedorprincipal.Controls.Add(formulario);
            Panelcontenedorprincipal.Tag = formulario;
            formulario.Show();
        }

        private void PanelCatalogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            PanelCatalogo.Visible = false;
            LblUser.Text = FrmLogin.NombreA;
        }

        private void BT_Catalogo_Click(object sender, EventArgs e)
        {

            PanelCatalogo.Visible = !PanelCatalogo.Visible;

        }

        private void BT_INICIO_Click(object sender, EventArgs e)
        {
            Panelcontenedorprincipal.Controls.Clear();


            pictureBoxFoto.Dock = DockStyle.Fill;
            pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxFoto.Visible = true;
            BtModoOscuro_Claro.Visible = true;
            BTInformacio.Visible = true;



            Panelcontenedorprincipal.Controls.Add(pictureBoxFoto);
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
    }
}
