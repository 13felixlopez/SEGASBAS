using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmEmpleado : Form
    {
        private D_Empleado _logica = new D_Empleado();
        //private CN_Empleado _logica = new CN_Empleado();
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int idEmpleadoSeleccionado = 0;
        public FrmEmpleado()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmEmpleado_Load;
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            CargarCargos();
            LlenarComboSexo();
            CargarEmpleadosPaginados();
        }
        private void CargarCargos()
        {
            // Aquí se usa el método que llama a tu SP existente
            CB_Cargo.DataSource = _logica.ObtenerCargos();
            CB_Cargo.DisplayMember = "Nombre";
            CB_Cargo.ValueMember = "Id_cargo";
        }

        private void LlenarComboSexo()
        {
            CBSexo.Items.Clear();

            CBSexo.Items.Add("Masculino");
            CBSexo.Items.Add("Femenino");
            CBSexo.Items.Add("Otro");

            CBSexo.SelectedIndex = 0;
        }
        private void AjustarDataGridView()
        {

            if (DatagreedEmpleado.Columns.Contains("Id_empleado"))
            {
                DatagreedEmpleado.Columns["Id_empleado"].Visible = false;
            }


            DatagreedEmpleado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void CargarEmpleadosPaginados()
        {
            try
            {
                int totalPaginas;
                DatagreedEmpleado.DataSource = _logica.ObtenerEmpleadosPaginados(paginaActual, tamanoPagina, out totalPaginas);

                // ¡Aquí llamamos al nuevo método!
                AjustarDataGridView();

                TxtPagina.Text = paginaActual.ToString();
                TxtTotalPagina.Text = totalPaginas.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DatagreedEmpleado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CargarCalculosSalario()
        {
            if (decimal.TryParse(Txtsalariopordia.Text, out decimal salarioPorDia))
            {
                decimal salarioQuincenal = 0;
                decimal salarioCatorcenal = 0;
                decimal salarioMensual = 0;

                if (checQuincenal.Checked)
                {
                    salarioQuincenal = salarioPorDia * 15m;
                    salarioMensual = salarioQuincenal * 2;
                    LBSalario.Text = $"Salario Quincenal: {salarioQuincenal:C2} | Mensual: {salarioMensual:C2}";
                }
                else if (checCatorcenal.Checked)
                {
                    salarioCatorcenal = salarioPorDia * 14m;
                    salarioMensual = salarioCatorcenal * 2;
                    LBSalario.Text = $"Salario Catorcenal: {salarioCatorcenal:C2} | Mensual: {salarioMensual:C2}";
                }
                else
                {
                    LBSalario.Text = "Seleccione un tipo de salario.";
                }
            }
            else
            {
                LBSalario.Text = "Salario: 0.00";
            }
        }
        private void DatagreedEmpleado_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Txtsalariopordia_TextChanged(object sender, EventArgs e)
        {
            CargarCalculosSalario();
        }

        private void checQuincenal_CheckedChanged(object sender, EventArgs e)
        {
            if (checQuincenal.Checked)
            {
                checCatorcenal.Checked = false;
                CargarCalculosSalario();
            }
        }

        private void checCatorcenal_CheckedChanged(object sender, EventArgs e)
        {
            if (checCatorcenal.Checked)
            {
                checQuincenal.Checked = false;
                CargarCalculosSalario();
            }
        }
        private L_Empleado ObtenerDatosDelFormulario()
        {
            decimal salarioPorDia = decimal.Parse(Txtsalariopordia.Text);
            L_Empleado empleado = new L_Empleado
            {
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Cedula = TxtCedula.Text,
                Sexo = CBSexo.Text,
                Direccion = TxtDireccion.Text,
                Telefono = TxtTelefono.Text,
                SalarioPorDia = salarioPorDia,
                FechaIngreso = dateTimePickerFechaIngres.Value,
                TipoContrato = ObtenerTipoContrato(),
                BeneficioSocial = ObtenerBeneficioSocial(),
                Observacion = TxtObservacion.Text,
                Id_cargo = (int)CB_Cargo.SelectedValue,
                SalarioCatorcenal = checCatorcenal.Checked ? salarioPorDia * 14 : 0,
                SalarioQuincenal = checQuincenal.Checked ? salarioPorDia * 15 : 0,
                SalarioMensual = (checCatorcenal.Checked ? salarioPorDia * 14 : (checQuincenal.Checked ? salarioPorDia * 15 : 0)) * 2
            };
            return empleado;
        }

        private string ObtenerTipoContrato()
        {
            if (checkPermanente.Checked) return "Permanente";
            if (checkTemporalVaron.Checked) return "Temporal Varon";
            return null;
        }

        private string ObtenerBeneficioSocial()
        {
            if (checkAsegurados.Checked) return "Asegurados";
            if (checkNoAsegurados.Checked) return "No Asegurados";
            return null;
        }

        private void LimpiarCampos()
        {
            TxtNombre.Clear();
            TxtApellido.Clear();
            TxtCedula.Clear();
            TxtDireccion.Clear();
            TxtTelefono.Clear();
            Txtsalariopordia.Clear();
            TxtObservacion.Clear();
            checkPermanente.Checked = false;
            checkTemporalVaron.Checked = false;
            checkAsegurados.Checked = false;
            checkNoAsegurados.Checked = false;
            checQuincenal.Checked = false;
            checCatorcenal.Checked = false;
            idEmpleadoSeleccionado = 0;

            BTAgregar.Enabled = true;
            BTEditar.Enabled = false;
        }
        private void BTAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                L_Empleado empleado = ObtenerDatosDelFormulario();
                _logica.InsertarEmpleado(empleado);
                MessageBox.Show("Empleado agregado exitosamente.", "Éxito");
                CargarEmpleadosPaginados();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar empleado: " + ex.Message, "Error");
            }
        }

        private void BTEditar_Click(object sender, EventArgs e)
        {

            try
            {
                if (idEmpleadoSeleccionado == 0)
                {
                    MessageBox.Show("Selecciona un empleado para editar.", "Advertencia");
                    return;
                }
                L_Empleado empleado = ObtenerDatosDelFormulario();
                empleado.Id_empleado = idEmpleadoSeleccionado;
                _logica.ActualizarEmpleado(empleado);
                MessageBox.Show("Empleado actualizado exitosamente.", "Éxito");
                CargarEmpleadosPaginados();
                LimpiarCampos();
                idEmpleadoSeleccionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar empleado: " + ex.Message, "Error");
            }
        }

        private void BTEliminar_Click(object sender, EventArgs e)
        {
            if (DatagreedEmpleado.SelectedRows.Count > 0)
            {
                idEmpleadoSeleccionado = Convert.ToInt32(DatagreedEmpleado.SelectedRows[0].Cells["Id_empleado"].Value);
                _logica.EliminarEmpleado(idEmpleadoSeleccionado);
                MessageBox.Show("Empleado eliminado exitosamente.", "Éxito");
                CargarEmpleadosPaginados();
            }
        }

        private void BtnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1)
            {
                paginaActual--;
                CargarEmpleadosPaginados();
            }
        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < int.Parse(TxtTotalPagina.Text))
            {
                paginaActual++;
                CargarEmpleadosPaginados();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BTCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idEmpleadoSeleccionado = 0;
        }

        private void DatagreedEmpleado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DatagreedEmpleado.Rows[e.RowIndex];
                idEmpleadoSeleccionado = Convert.ToInt32(row.Cells["Id_empleado"].Value);

                L_Empleado empleado = _logica.ObtenerEmpleadoPorId(idEmpleadoSeleccionado);
                if (empleado != null)
                {
                    TxtNombre.Text = empleado.Nombre;
                    TxtApellido.Text = empleado.Apellido;
                    TxtCedula.Text = empleado.Cedula;
                    CBSexo.Text = empleado.Sexo;
                    TxtDireccion.Text = empleado.Direccion;
                    TxtTelefono.Text = empleado.Telefono;
                    Txtsalariopordia.Text = empleado.SalarioPorDia.ToString();
                    dateTimePickerFechaIngres.Value = empleado.FechaIngreso;
                    checkPermanente.Checked = (empleado.TipoContrato == "Permanente");
                    checkTemporalVaron.Checked = (empleado.TipoContrato == "Temporal Varon");
                    checkAsegurados.Checked = (empleado.BeneficioSocial == "Asegurados");
                    checkNoAsegurados.Checked = (empleado.BeneficioSocial == "No Asegurados");
                    CB_Cargo.SelectedValue = empleado.Id_cargo;
                }


                BTAgregar.Enabled = false;
                BTEditar.Enabled = true;
            }
        }
    }
}
