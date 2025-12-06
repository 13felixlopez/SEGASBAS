using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Data;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmLogin : Form
    {
        readonly NUser nuser = new NUser();
        readonly LUsuarios luser = new LUsuarios();
        LUsuarios parametros = new LUsuarios();
        public static string NombreA;
        public static int Iduser;
        public static string login;
        public static string Correo;
        public static string Estado;
        public static string Pass;
        public static string Roll;
        FrmPrincipal fr = new FrmPrincipal();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BT_Aceptar_Click(object sender, EventArgs e)
        {
          
            Logueo();
        }

        private void Logueo()
        {
            if (string.IsNullOrEmpty(TxtUser.Text) || string.IsNullOrEmpty(TxtPass.Text))
            {
                MessageBox.Show("Los campos estan vacios", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtUser.Focus();
            }
            else
            {
                DataTable dt = new DataTable();
                parametros.Login = TxtUser.Text;
                parametros.Pass = Encrip.Encriptar(Encrip.Encriptar(TxtPass.Text));

                try
                {
                    dt = nuser.Nusers(parametros);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        Iduser = Convert.ToInt32(dt.Rows[0][0]);
                        NombreA = dt.Rows[0][1].ToString();
                        Pass = dt.Rows[0][2].ToString();
                        Estado = dt.Rows[0][3].ToString();
                        Correo = dt.Rows[0][4].ToString();
                        login = dt.Rows[0][5].ToString();
                        Roll = dt.Rows[0][6].ToString();

                        this.Hide();
                        fr.FormClosed += (s, args) => this.Close(); // cuando cierre el nuevo, cerrar el actual
                        fr.Show();

                        TxtPass.Clear();
                        TxtUser.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void linkLabelrecuperarcontraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                
                string posibleEmail = TxtUser.Text.Trim();
                Recuperar_Contraseña frm;

                if (!string.IsNullOrEmpty(posibleEmail) && posibleEmail.Contains("@"))
                    frm = new Recuperar_Contraseña(posibleEmail); // constructor que recibe email
                else
                    frm = new Recuperar_Contraseña();

                // Abrir modal para que el usuario termine el proceso antes de volver
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir recuperación de contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
