using Capa_Presentacion.Datos;
using Capa_Presentacion.Logica;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmEmpleado : Form
    {
        private D_Empleado _logica = new D_Empleado();
     
        private int paginaActual = 1;
        private const int tamanoPagina = 10;
        private int idEmpleadoSeleccionado = 0;
        
        public FrmEmpleado()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Load += FrmEmpleado_Load;
            CBSexo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            CargarCargos();
            LlenarComboSexo();
            CargarEmpleadosPaginados();
            CargarCriteriosBusqueda();
           
        }
        private void CargarCargos()
        {
          
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
               
                if (string.IsNullOrWhiteSpace(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtApellido.Text))
                {
                    MessageBox.Show("Los campos 'Nombre' y 'Apellido' son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string telefonoSinGuiones = new string(TxtTelefono.Text.Where(char.IsDigit).ToArray());
                if (!string.IsNullOrWhiteSpace(TxtTelefono.Text) && telefonoSinGuiones.Length != 8)
                {
                    MessageBox.Show("El número de teléfono debe tener exactamente 8 dígitos.", "Validación de Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                string cedulaLimpia = new string(TxtCedula.Text.Where(c => char.IsDigit(c) || char.IsLetter(c)).ToArray());

                if (!string.IsNullOrWhiteSpace(TxtCedula.Text))
                {
                    if (cedulaLimpia.Length != 14)
                    {
                        MessageBox.Show("La cédula debe tener 13 dígitos y una letra.", "Validación de Cédula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string digitosCedula = cedulaLimpia.Substring(0, 13);
                    if (!digitosCedula.All(char.IsDigit))
                    {
                        MessageBox.Show("Los primeros 13 caracteres de la cédula deben ser dígitos.", "Validación de Cédula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    char letraCedula = cedulaLimpia.Last();
                    if (!char.IsLetter(letraCedula) || !char.IsUpper(letraCedula))
                    {
                        MessageBox.Show("El último carácter de la cédula debe ser una letra mayúscula.", "Validación de Cédula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

       
                L_Empleado empleado = ObtenerDatosDelFormulario();

            
                empleado.Telefono = telefonoSinGuiones;
                empleado.Cedula = cedulaLimpia;

               
                _logica.InsertarEmpleado(empleado);

                MessageBox.Show("Empleado agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarEmpleadosPaginados();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
         
            if (e.RowIndex < 0 || e.RowIndex >= DatagreedEmpleado.Rows.Count)
            {
                return; 
            }

            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los datos del empleado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtTelefono_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = TxtTelefono.SelectionStart;

  
            string digitos = new string(TxtTelefono.Text.Where(char.IsDigit).ToArray());

            
            if (digitos.Length > 8)
            {
                digitos = digitos.Substring(0, 8);
            }

            string numeroFormateado = "";
            if (digitos.Length > 0)
            {
                numeroFormateado += digitos.Substring(0, Math.Min(4, digitos.Length));
            }
            if (digitos.Length > 4)
            {
                numeroFormateado += "-" + digitos.Substring(4, Math.Min(4, digitos.Length - 4));
            }

            if (TxtTelefono.Text != numeroFormateado)
            {
                TxtTelefono.Text = numeroFormateado;

                if (cursorPosition > 4)
                {
                    TxtTelefono.SelectionStart = Math.Min(cursorPosition + 1, TxtTelefono.Text.Length);
                }
                else
                {
                    TxtTelefono.SelectionStart = Math.Min(cursorPosition, TxtTelefono.Text.Length);
                }
            }
        }

        private void TxtCedula_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = TxtCedula.SelectionStart;

            string textoLimpio = new string(TxtCedula.Text.Where(c => char.IsLetterOrDigit(c)).ToArray());

            if (textoLimpio.Length > 14)
            {
                textoLimpio = textoLimpio.Substring(0, 14);
            }

            string cedulaFormateada = "";
            if (textoLimpio.Length > 0)
            {
                cedulaFormateada += textoLimpio.Substring(0, Math.Min(3, textoLimpio.Length));
            }
            if (textoLimpio.Length > 3)
            {
                cedulaFormateada += "-" + textoLimpio.Substring(3, Math.Min(6, textoLimpio.Length - 3));
            }
            if (textoLimpio.Length > 9)
            {
                cedulaFormateada += "-" + textoLimpio.Substring(9, Math.Min(4, textoLimpio.Length - 9));
            }
       
            if (textoLimpio.Length > 13)
            {
                cedulaFormateada += char.ToUpper(textoLimpio[13]);
            }

         
            if (TxtCedula.Text != cedulaFormateada)
            {
                TxtCedula.Text = cedulaFormateada;

                int guionesAgregados = 0;
                if (cursorPosition > 3) guionesAgregados++;
                if (cursorPosition > 10) guionesAgregados++;
              
                if (cursorPosition > 14) guionesAgregados++;

                TxtCedula.SelectionStart = Math.Max(0, Math.Min(cursorPosition + guionesAgregados, TxtCedula.Text.Length));
            }
        }

        private void TxtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (TxtCedula.Text.Length >= 16 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; 
            }
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true; 
            }
        }

        private void TxtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                e.Handled = true; 
            }
        }

        private void Txtsalariopordia_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {

                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                   
                    if (Txtsalariopordia.Text.Contains(",") || Txtsalariopordia.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = true; 
                }
            }
        }

        private void checkPermanente_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPermanente.Checked)
            {
                checkTemporalVaron.Checked = false;
                checkTemporalMujer.Checked = false; 
                checkDilleros.Checked = false;       
                                                     
            }
        }

        private void checkTemporalVaron_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTemporalVaron.Checked)
            {
                checkPermanente.Checked = false;
                checkTemporalMujer.Checked = false; 
                checkDilleros.Checked = false;
            }
        }

        private void checkAsegurados_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAsegurados.Checked)
            {
                checkNoAsegurados.Checked = false;
            }
        }

        private void checkNoAsegurados_CheckedChanged(object sender, EventArgs e)
        {
            if (checkNoAsegurados.Checked)
            {
                checkAsegurados.Checked = false;
            }
        }

        private void checkTemporalMujer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTemporalMujer.Checked)
            {
                checkPermanente.Checked = false;
                checkTemporalVaron.Checked = false; 
                checkDilleros.Checked = false;

            }
        }

        private void checkDilleros_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDilleros.Checked)
            {
                checkPermanente.Checked = false;
                checkTemporalVaron.Checked = false; 
                checkTemporalMujer.Checked = false;
            }
        }
        private void CargarCriteriosBusqueda()
        {
          
            CmbBuscar.Items.Clear();
           
            CmbBuscar.Items.Add("Nombre");
            CmbBuscar.Items.Add("Cedula");
     
            CmbBuscar.SelectedIndex = 0;
        }
        private string terminoBusqueda = "";
        private void RealizarBusqueda()
        {
            try
            {

                string criterio = CmbBuscar.Text;
               
                string termino = TxtBuscar.Text.Trim();

                if (string.IsNullOrWhiteSpace(termino))
                {
                    CargarEmpleadosPaginados();
                }
                else
                {
      
                    DatagreedEmpleado.DataSource = _logica.BuscarEmpleados(termino, criterio);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error en la búsqueda: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            terminoBusqueda = TxtBuscar.Text.Trim();
            RealizarBusqueda();

        }

        private void CmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }
    }
}
