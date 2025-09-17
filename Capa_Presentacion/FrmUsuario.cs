using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmUsuario : Form
    {
        int IdUser;
        string Estado;
        string login;
        string contraseñaGenerada;
        private bool labelsVerdes = false;
        public FrmUsuario()
        {
            InitializeComponent();
            ValidarPermisos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarUsuarios();
            MostrarModulos();
            PlPass.Visible = true;
            contraseñaGenerada = Validaciones.GenerarContraseñaAleatoria();
            txtContraseña.Text = contraseñaGenerada;
            Validaciones.ActualizarVisibilidadEtiquetas(contraseñaGenerada, lblMayu, lblMin, lblNum);
        }
        private void MostrarModulos()
        {
            DModulos funcion = new DModulos();
            DataTable dt = new DataTable();
            funcion.MostrarModulos(ref dt);
            DtPermisos.DataSource = dt;
            DtPermisos.Columns["IdModulo"].Visible = false;
        }
        private void Limpiar()
        {
            txtNombreApellidos.Clear();
            txtCorreo.Clear();
            txtContraseña.Clear();
            TxtUser.Clear();
            Plheader.Visible = false;
        }
        private void HabilitarPaneles()
        {
            PlRegistro.Visible = true;
            PlRegistro.Dock = DockStyle.Fill;
            PlRegistro.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PlRegistro.Visible = false;
            Plheader.Visible = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreApellidos.Text))
            {
                if (!string.IsNullOrEmpty(txtCorreo.Text))
                {
                    if (!string.IsNullOrEmpty(txtContraseña.Text))
                    {
                        InsertarUser();
                    }
                    else
                    {
                        MessageBox.Show("La contraseña ingresada no cumple con los requerimientos necesarios", "ERROR Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el correo", "Error Correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ingrese el nombre", "Error Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InsertarUser()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.NombreA = txtNombreApellidos.Text;
            parametros.Pass = Encrip.Encriptar(Encrip.Encriptar(txtContraseña.Text));
            parametros.Correo = txtCorreo.Text;
            parametros.Login = TxtUser.Text;
            parametros.Roll = TxtRoll.Text;
            if (funcion.InsertarUsuario(parametros) == true)
            {
                ObtenerIdUser();
                InsertarPermisos();
                Plheader.Visible = true;
            }
        }
        private void ObtenerIdUser()
        {
            DUsuarios funcion = new DUsuarios();
            funcion.ObtenerIdUser(ref IdUser, TxtUser.Text);
        }
        private void InsertarPermisos()
        {
            foreach (DataGridViewRow row in DtPermisos.Rows)
            {
                int IdModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if (marcado == true)
                {
                    LPermisos parametros = new LPermisos();
                    DPermisos funcion = new DPermisos();
                    parametros.IdModulo = IdModulo;
                    parametros.IdUser = IdUser;
                    funcion.InsertarPermisos(parametros);
                }
            }
            MostrarUsuarios();
            PlRegistro.Visible = false;
        }
        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            DUsuarios funcion = new DUsuarios();
            funcion.MostrarUsuarios(ref dt);
            DtUser.DataSource = dt;
            DiseñarDtvUsuarios();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }
        private void DiseñarDtvUsuarios()
        {
            Base.DiseñoDtv(ref DtUser);
            DtUser.Columns["IdUser"].Visible = false;
            DtUser.Columns["Pass"].Visible = false;
        }

        private void DtUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DtUser.Columns["Editar"].Index)
            {
                ObtenerEstado();
                ObtenerDatos();
            }
            if (e.ColumnIndex == DtUser.Columns["Eliminar"].Index)
            {
                string admin = DtUser.SelectedCells[5].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Realmente desea eliminar este Registro?", "Eliminando registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.OK)
                {
                    if (admin != "Admin")
                    {
                        capturarIdUsuario();
                        EliminarUser();
                    }
                    else
                    {
                        MessageBox.Show("No se puede eliminar el usuario Admin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void capturarIdUsuario()
        {
            IdUser = Convert.ToInt32(DtUser.SelectedCells[3].Value);
            login = DtUser.SelectedCells[6].Value.ToString();
        }
        private void ObtenerDatos()
        {
            Limpiar();
            capturarIdUsuario();
            txtNombreApellidos.Text = DtUser.SelectedCells[4].Value.ToString();
            TxtUser.Text = DtUser.SelectedCells[6].Value.ToString();
            txtCorreo.Text = DtUser.SelectedCells[5].Value.ToString();
            TxtRoll.Text = DtUser.SelectedCells[9].Value.ToString();
            if (TxtUser.Text == "Admin")
            {
                TxtUser.Enabled = false;
                DtPermisos.Enabled = false;
            }
            else
            {
                TxtUser.Enabled = true;
                DtPermisos.Enabled = true;
            }
            txtContraseña.Text = Encrip.DesEncriptar(Encrip.DesEncriptar(DtUser.SelectedCells[7].Value.ToString()));
            PlPass.Visible = false;
            PlRegistro.Visible = true;
            PlRegistro.Dock = DockStyle.Fill;
            btnActualizar.Visible = true;
            btnGuardar.Visible = false;
            MostrarModulos();
            MostrarPermisos();
        }
        private void MostrarPermisos()
        {
            DataTable dt = new DataTable();
            DPermisos funcion = new DPermisos();
            LPermisos parametros = new LPermisos();
            parametros.IdUser = IdUser;
            funcion.MostrarPermisos(ref dt, parametros);
            foreach (DataRow rowPermisos in dt.Rows)
            {
                int idmoduloPermisos = Convert.ToInt32(rowPermisos["IdModulo"]);
                foreach (DataGridViewRow rowModulos in DtPermisos.Rows)
                {
                    int Idmodulo = Convert.ToInt32(rowModulos.Cells["IdModulo"].Value);
                    if (idmoduloPermisos == Idmodulo)
                    {
                        rowModulos.Cells[0].Value = true;
                    }
                }
            }
        }
        private void EliminarUser()
        {
            string nombrecolumna = "IdUser";
            int indice = DtUser.Columns[nombrecolumna].Index;
            IdUser = int.Parse(DtUser.SelectedCells[indice].Value.ToString());
            DUsuarios funcion = new DUsuarios();
            if (funcion.EliminarUser(IdUser) == true)
            {
                MostrarUsuarios();
            }
        }
        private void ObtenerEstado()
        {
            Estado = DtUser.SelectedCells[8].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombreApellidos.Text))
            {
                if (!string.IsNullOrEmpty(TxtUser.Text))
                {
                    if (!string.IsNullOrEmpty(txtContraseña.Text))
                    {
                        if (!string.IsNullOrEmpty(txtCorreo.Text))
                        {
                            EditarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Ingrese un Correo");

                        }

                    }
                    else
                    {
                        MessageBox.Show("Ingrese la contraseña");


                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Usuario");


                }
            }
            else
            {

                MessageBox.Show("Ingrese el Nombre");
            }
        }
        private void EditarUsuarios()
        {
            LUsuarios parametros = new LUsuarios();
            DUsuarios funcion = new DUsuarios();
            parametros.IdUser = IdUser;
            parametros.NombreA = txtNombreApellidos.Text;
            parametros.Login = TxtUser.Text;
            parametros.Pass = Encrip.Encriptar(Encrip.Encriptar(txtContraseña.Text));
            parametros.Correo = txtCorreo.Text;
            parametros.Roll = TxtRoll.Text;
            if (funcion.EditarUser(parametros) == true)
            {
                EliminarPermisos();
                InsertarPermisos();
            }
        }
        private void EliminarPermisos()
        {
            LPermisos parametros = new LPermisos();
            DPermisos funcion = new DPermisos();
            parametros.IdUser = IdUser;
            funcion.EliminarPermisos(parametros);
        }
        #region Validaciones
        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            string contra = txtContraseña.Text;
            Validaciones.ActualizarVisibilidadEtiquetas(contra, lblMayu, lblMin, lblNum);

            //// Verificar si la contraseña cumple con los criterios
            bool cumpleCriterios = Validaciones.ContraseñaCumpleCriterios(contra) && contra.Length >= 6;

            // Cambiar el color del Label según si cumple los criterios
            LblPass.ForeColor = cumpleCriterios ? Color.Green : Color.Red;
            // Actualizar los colores de los labels y validar si todos están en verde
            labelsVerdes = Validaciones.ValidarLabelsVerdes(lblNombreApellidos, LblUser, LblPass, lblCorreo);

            // Habilitar o deshabilitar el botón btnGuardar
            btnGuardar.Enabled = cumpleCriterios && labelsVerdes;
            btnActualizar.Enabled = cumpleCriterios && labelsVerdes;
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            if (textBox == TxtUser)
            {
                LblUser.ForeColor = string.IsNullOrEmpty(TxtUser.Text) ? Color.Red : Color.Green;

                // Actualizar los colores de los labels y validar si todos están en verde
                labelsVerdes = Validaciones.ValidarLabelsVerdes(lblNombreApellidos, LblUser, LblPass, lblCorreo);

                // Habilitar o deshabilitar el botón guardar según el estado de los labels
                btnGuardar.Enabled = labelsVerdes;
                btnActualizar.Enabled = labelsVerdes;
            }

            if (textBox == txtNombreApellidos)
            {
                lblNombreApellidos.ForeColor = string.IsNullOrEmpty(txtNombreApellidos.Text) ? Color.Red : Color.Green;

                // Actualizar los colores de los labels y validar si todos están en verde
                labelsVerdes = Validaciones.ValidarLabelsVerdes(lblNombreApellidos, LblUser, LblPass, lblCorreo);

                // Habilitar o deshabilitar el botón guardar según el estado de los labels
                btnGuardar.Enabled = labelsVerdes;
                btnActualizar.Enabled = labelsVerdes;
            }

            if (textBox == txtContraseña)
            {
                if (int.TryParse(txtContraseña.Text, out int celular) && celular.ToString().Length >= 8)
                {
                    LblPass.ForeColor = Color.Green;
                }
                else
                {
                    LblPass.ForeColor = Color.Red;
                }
                // Actualizar los colores de los labels y validar si todos están en verde
                labelsVerdes = Validaciones.ValidarLabelsVerdes(lblNombreApellidos, LblUser, LblPass, lblCorreo);

                // Habilitar o deshabilitar el botón guardar según el estado de los labels
                btnGuardar.Enabled = labelsVerdes;
                btnActualizar.Enabled = labelsVerdes;
            }

            if (textBox == txtCorreo)
            {
                lblCorreo.ForeColor = Validaciones.EsCorreoValido(txtCorreo.Text) ? Color.Green : Color.Red;
                // Actualizar los colores de los labels y validar si todos están en verde
                labelsVerdes = Validaciones.ValidarLabelsVerdes(lblNombreApellidos, LblUser, LblPass, lblCorreo);

                // Habilitar o deshabilitar el botón guardar según el estado de los labels
                btnGuardar.Enabled = labelsVerdes;
                btnActualizar.Enabled = labelsVerdes;
            }
        }
        #endregion
        private void ValidarPermisos()
        {
            if (!FrmLogin.login.Trim().StartsWith("Admin", StringComparison.OrdinalIgnoreCase))
            {
                DataTable dt = new DataTable();
                DModulos funcion = new DModulos();
                LPermisos parametros = new LPermisos();
                parametros.IdUser = FrmLogin.Iduser;
                funcion.PermisosUser(ref dt, FrmLogin.Iduser);
                foreach (DataRow rowpermisos in dt.Rows)
                {
                    string Modulo = Convert.ToString(rowpermisos["Modulo"]);

                    if (Modulo == "Usuarios")
                    {
                        DtUser.Enabled = true;
                    }
                }
            }
            else
            {
                DtUser.Enabled = true;
            }
        }

        private void TxtRoll_TextChanged(object sender, EventArgs e)
        {
            if (TxtRoll.Text.Length < 2) return;

            var cargos = new DUsuarios().BuscarRoll(TxtRoll.Text.Trim()); // ✅
            var ac = new AutoCompleteStringCollection();
            ac.AddRange(cargos.ToArray());
            TxtRoll.AutoCompleteCustomSource = ac;
        }

        private void BtnMostrartodos_Click(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }
    }
}
