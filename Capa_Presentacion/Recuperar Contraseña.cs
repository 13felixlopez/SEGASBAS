using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Capa_Presentacion
{
    public partial class Recuperar_Contraseña : Form
    {
        private int idUsuario = 0;
        private string codigoGenerado = "";

     
        private readonly DRecuperacion dRec = new DRecuperacion();
        private readonly LRecuperacion lRec = new LRecuperacion();
        public Recuperar_Contraseña()
        {
            InitializeComponent();


            InicializarPantallas();

          
            ReparentearPaneles();

            btenviarcoreeo.Click -= btenviarcoreeo_Click;
            btenviarcoreeo.Click += btenviarcoreeo_Click;

            btconfirmarcodigo.Click -= btconfirmarcodigo_Click;
            btconfirmarcodigo.Click += btconfirmarcodigo_Click;

            btconfirmarcontraseña.Click -= btconfirmarcontraseña_Click;
            btconfirmarcontraseña.Click += btconfirmarcontraseña_Click;
        }

        private void InicializarPantallas()
        {
            Panelingresarcorreo.Visible = true;
            panelcodigo.Visible = false;
            panelnuevacontraseña.Visible = false;

            Panelingresarcorreo.BackColor = System.Drawing.Color.White;
            panelcodigo.BackColor = System.Drawing.Color.White;
            panelnuevacontraseña.BackColor = System.Drawing.Color.White;

            Panelingresarcorreo.BringToFront();
        }
        private void ReparentearPaneles()
        {
            try
            {
                // Evitar duplicados en Controls si ya fueron movidos
                if (!this.Controls.Contains(panelcodigo))
                {
                    this.Controls.Add(panelcodigo);
                }
                if (!this.Controls.Contains(panelnuevacontraseña))
                {
                    this.Controls.Add(panelnuevacontraseña);
                }

               
                panelcodigo.Location = Panelingresarcorreo.Location;
                panelcodigo.Size = Panelingresarcorreo.Size;
                panelcodigo.Visible = false;

                panelnuevacontraseña.Location = Panelingresarcorreo.Location;
                panelnuevacontraseña.Size = Panelingresarcorreo.Size;
                panelnuevacontraseña.Visible = false;

               
                if (this.Controls.Contains(pictureBox1))
                    pictureBox1.SendToBack();
            }
            catch
            {

            }
        }
        public Recuperar_Contraseña(string email) : this()
        {
            if (!string.IsNullOrWhiteSpace(email))
                Txtingresarcorreo.Text = email;
        }
        private void Recuperar_Contraseña_Load(object sender, EventArgs e)
        {

        }
        private bool EnviarCodigoPorCorreo(string correoDestino, string codigo, out string mensaje)
        {
            mensaje = "";
            try
            {
                string remitente = null;
                string pass = null;

              
                try
                {
                    remitente = ConfigurationManager.AppSettings["smtp_user"];
                    pass = ConfigurationManager.AppSettings["smtp_pass"];
                }
                catch
                {
                   
                }

                if (string.IsNullOrEmpty(remitente) || string.IsNullOrEmpty(pass))
                {
                    mensaje = "Configuración SMTP no encontrada. Agrega smtp_user y smtp_pass en app.config.";
                    return false;
                }

                string asunto = "Código de recuperación - San Benito";
                string cuerpo = $"Su código de recuperación es: {codigo}\r\n\r\nEste código es válido por 15 minutos.";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(remitente, "San Benito");
                    mail.To.Add(correoDestino);
                    mail.Subject = asunto;
                    mail.Body = cuerpo;
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(remitente, pass);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                mensaje = "Enviado";
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }
        private void btenviarcoreeo_Click(object sender, EventArgs e)
        {
            try
            {
                btenviarcoreeo.Enabled = false;

                string correo = Txtingresarcorreo.Text.Trim();
                if (string.IsNullOrEmpty(correo))
                {
                    MessageBox.Show("Ingrese un correo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Ingrese un correo válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                idUsuario = dRec.ObtenerIdPorCorreo(correo);
                if (idUsuario == 0)
                {
                    MessageBox.Show("El correo no está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var rnd = new Random();
                codigoGenerado = rnd.Next(100000, 999999).ToString();
                DateTime expiracion = DateTime.Now.AddMinutes(15);

                bool guardado = dRec.GuardarCodigo(idUsuario, codigoGenerado, expiracion);
                if (!guardado)
                {
                    MessageBox.Show("No se pudo generar el código. Intente más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool enviado = EnviarCodigoPorCorreo(correo, codigoGenerado, out string mensajeEnvio);
                if (!enviado)
                {
                    MessageBox.Show("Error al enviar correo: " + mensajeEnvio, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Código enviado. Revise su correo (bandeja principal y SPAM).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                Panelingresarcorreo.Visible = false;
                panelcodigo.Location = Panelingresarcorreo.Location;
                panelcodigo.Size = Panelingresarcorreo.Size;
                panelcodigo.Visible = true;
                panelcodigo.BringToFront();
                panelcodigo.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al enviar el código: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btenviarcoreeo.Enabled = true;
            }
        }

        private void btconfirmarcontraseña_Click(object sender, EventArgs e)
        {
            try
            {
                btconfirmarcontraseña.Enabled = false;

                string nueva = txtnuevacontraseña.Text.Trim();
                string confirmar = txtConfirmarcotraseña.Text.Trim();

                if (string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(confirmar))
                {
                    MessageBox.Show("Complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nueva.Length < 6)
                {
                    MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (nueva != confirmar)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (idUsuario == 0 || string.IsNullOrEmpty(codigoGenerado))
                {
                    MessageBox.Show("Operación no autorizada. Solicite el código nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Encriptar igual que en login
                string encriptada = Encrip.Encriptar(Encrip.Encriptar(nueva));

                bool actualizado = dRec.CambiarContrasena(idUsuario, encriptada);
                if (!actualizado)
                {
                    MessageBox.Show("No se pudo actualizar la contraseña. Intente más tarde.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dRec.MarcarCodigoUsado(idUsuario, codigoGenerado);

                MessageBox.Show("Contraseña actualizada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cambiar la contraseña: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btconfirmarcontraseña.Enabled = true;
            }
        }

        private void btconfirmarcodigo_Click(object sender, EventArgs e)
        {
            try
            {
                btconfirmarcodigo.Enabled = false;

                if (idUsuario == 0)
                {
                    MessageBox.Show("Primero solicite el código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string codigo = txtingresarcodigo.Text.Trim();
                if (string.IsNullOrEmpty(codigo))
                {
                    MessageBox.Show("Ingrese el código recibido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool valido = dRec.VerificarCodigo(idUsuario, codigo);
                if (!valido)
                {
                    MessageBox.Show("Código inválido o expirado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

           
                codigoGenerado = codigo;

               
                panelcodigo.Visible = false;
                panelnuevacontraseña.Location = Panelingresarcorreo.Location;
                panelnuevacontraseña.Size = Panelingresarcorreo.Size;
                panelnuevacontraseña.Visible = true;
                panelnuevacontraseña.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al verificar el código: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btconfirmarcodigo.Enabled = true;
            }
        }
    }
}
