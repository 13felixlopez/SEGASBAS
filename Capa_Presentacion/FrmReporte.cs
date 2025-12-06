using Capa_Presentacion.Datos;
using iTextSharp.text.pdf;
using iTextSharp.text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmReporte : Form
    {
        string Name;
        private D_Producto datosProducto = new D_Producto();
        public FrmReporte()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            cbEmpleadoAsis.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbEmpleadoAsis.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void BtnAsistencia_Click(object sender, EventArgs e)
        {
            DtDatos.DataSource = null;
            D_Asistencia funcion = new D_Asistencia();
            DataTable dt = new DataTable();
            funcion.ReporteAsistencia(ref dt);
            DtDatos.DataSource = dt;
            Name = "Asistencia ";
        }

        private void BtnDescargar_Click(object sender, EventArgs e)
        {
            string fe = Name + DateTime.Now.Day + "_" + DateTime.Today.Month + "_" + DateTime.Today.Year + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second;
            SaveExcelFile(DtDatos, fe);
        }
        private void ExportToExcel(DataGridView dataGridView, string fileName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Asistencia");

            // Crear el formato de fecha
            ICellStyle dateCellStyle = workbook.CreateCellStyle();
            short dateFormat = workbook.CreateDataFormat().GetFormat("yyyy-MM-dd"); // Formato de fecha
            dateCellStyle.DataFormat = dateFormat;

            // Crear la fila de encabezado con columnas visibles
            IRow headerRow = sheet.CreateRow(0);
            int visibleColumnIndex = 0;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.Visible)
                {
                    ICell cell = headerRow.CreateCell(visibleColumnIndex++);
                    cell.SetCellValue(column.HeaderText);
                }
            }

            // Llenar las filas con datos del DataGridView solo de columnas visibles
            int rowIndex = 1;
            foreach (DataGridViewRow gridViewRow in dataGridView.Rows)
            {
                IRow row = sheet.CreateRow(rowIndex++);
                visibleColumnIndex = 0;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    if (column.Visible)
                    {
                        ICell cell = row.CreateCell(visibleColumnIndex++);
                        var cellValue = gridViewRow.Cells[column.Index].Value;

                        if (cellValue is DateTime dateValue)
                        {
                            // Formatear como fecha
                            cell.SetCellValue(dateValue);
                            cell.CellStyle = dateCellStyle;
                        }
                        else if (decimal.TryParse(cellValue?.ToString(), out decimal numericValue))
                        {
                            // Formatear como número
                            cell.SetCellValue((double)numericValue);
                        }
                        else
                        {
                            cell.SetCellValue(cellValue?.ToString() ?? string.Empty);
                        }
                    }
                }
            }

            // Guardar el archivo Excel
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            }
        }


        private void SaveExcelFile(DataGridView dataGridView, string defaultFileName)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Guardar archivo Excel";
                saveFileDialog.FileName = defaultFileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    ExportToExcel(dataGridView, fileName);

                    DialogResult result = MessageBox.Show("Archivo Excel creado y guardado exitosamente. ¿Deseas abrir el archivo?", "Exportación Completada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
                    }
                }
            }
        }

        private void BtnEmpleado_Click(object sender, EventArgs e)
        {
            DtDatos.DataSource = null;
            D_Empleado funcion = new D_Empleado();
            DataTable dt = new DataTable();
            funcion.ReporteEmpleado(ref dt);
            DtDatos.DataSource = dt;
            Name = "Empleados ";
        }

        private void BtAsis_Click(object sender, EventArgs e)
        {
            DtDatos.DataSource = null;
           D_Asistencia funcion = new D_Asistencia();
            DataTable dt = new DataTable();
            funcion.ReporteAsis(ref dt);
            DtDatos.DataSource = dt;
            Name = "Asis ";
        }
        private void CargarProductosKardex()
        {
            try
            {
                DataTable dt = datosProducto.ObtenerProductosCatalogo();
                cbProducto.DataSource = dt;
                cbProducto.DisplayMember = "nombre";
                cbProducto.ValueMember = "id_producto";
                cbProducto.SelectedIndex = -1;   // nada seleccionado al inicio
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos para Kardex: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public DataTable ObtenerTodosEmpleados()
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTodosEmpleados", Conexion.conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally
            {
                Conexion.cerrar();
            }

            return dt;
        }
        private void CargarEmpleadosAsistencia()
        {
            try
            {
                D_Empleado funcion = new D_Empleado();
                DataTable dt = funcion.ObtenerTodosEmpleados();

                cbEmpleadoAsis.DataSource = dt;
                cbEmpleadoAsis.DisplayMember = "nombre_completo"; 
                cbEmpleadoAsis.ValueMember = "id_empleado";
                cbEmpleadoAsis.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empleados: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmReporte_Load(object sender, EventArgs e)
        {
            CargarProductosKardex();
            CargarEmpleadosAsistencia();
            CargarProductosKardex();
        }
        private void ExportToPDF(DataGridView dgv, string fileName)
        {
            try
            {
          
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));

                doc.Open();

                var font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 10);

                // Agregar título
                Paragraph title = new Paragraph("Reporte generado - " + DateTime.Now.ToString("dd/MM/yyyy"), font);
                title.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                title.SpacingAfter = 20;
                doc.Add(title);

                // Tabla con la misma cantidad de columnas visibles del DataGridView
                PdfPTable table = new PdfPTable(dgv.Columns.Cast<DataGridViewColumn>().Count(c => c.Visible));
                table.WidthPercentage = 100;

                // Encabezados
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, font));
                        cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                        table.AddCell(cell);
                    }
                }

                // Filas
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Visible)
                            {
                                table.AddCell(new Phrase(cell.Value?.ToString() ?? "", font));
                            }
                        }
                    }
                }

                doc.Add(table);
                doc.Close();

                MessageBox.Show("PDF generado exitosamente.", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Preguntar si abrir
                Process.Start(new ProcessStartInfo(fileName) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKardex_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un producto para generar el Kardex.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProducto = Convert.ToInt32(cbProducto.SelectedValue);

            DateTime? fechaInicio = null;
            DateTime? fechaFin = null;

            if (chkRangoFechas.Checked)
            {
                fechaInicio = dtpDesde.Value.Date;
                fechaFin = dtpHasta.Value.Date;
            }

            try
            {
                DtDatos.DataSource = null;

                DataTable dt = datosProducto.ObtenerKardex(idProducto, fechaInicio, fechaFin);
                DtDatos.DataSource = dt;

                // Opcional: ocultar columnas internas para que se vea “serio”
                if (DtDatos.Columns.Contains("id_producto"))
                    DtDatos.Columns["id_producto"].Visible = false;

                Name = "Kardex_" + cbProducto.Text + "_";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar Kardex: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAsisEmpleado_Click(object sender, EventArgs e)
        {
            if (cbEmpleadoAsis.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un empleado.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEmpleado = Convert.ToInt32(cbEmpleadoAsis.SelectedValue);
            DateTime desde = dtpDesdeAsis.Value.Date;
            DateTime hasta = dtpHastaAsis.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la final.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            D_Asistencia da = new D_Asistencia();
            DataTable dt = new DataTable();
            da.ReporteAsistenciaEmpleado(idEmpleado, desde, hasta, ref dt);

            DtDatos.DataSource = dt;

            if (DtDatos.Columns.Contains("Empleado"))
                DtDatos.Columns["Empleado"].HeaderText = "Empleado";
            if (DtDatos.Columns.Contains("Fecha"))
                DtDatos.Columns["Fecha"].HeaderText = "Fecha";
            if (DtDatos.Columns.Contains("Estado"))
                DtDatos.Columns["Estado"].HeaderText = "Estado";
            if (DtDatos.Columns.Contains("DiaTrabajado"))
                DtDatos.Columns["DiaTrabajado"].HeaderText = "Día trabajado";
            if (DtDatos.Columns.Contains("Justificacion"))
                DtDatos.Columns["Justificacion"].HeaderText = "Justificación";

            Name = "AsistenciaEmpleado_";
        }

        private void BtnDescargarPDF_Click(object sender, EventArgs e)
        {
            string fe = Name + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Archivo PDF|*.pdf";
                saveFile.FileName = fe;

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    ExportToPDF(DtDatos, saveFile.FileName);
                }
            }
        }

        private void cbEmpleadoAsis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void OcultarColumnas()
        {
            if (DtDatos.Columns.Contains("Id_empleado"))
                DtDatos.Columns["Id_empleado"].Visible = false;
        }
        private void BtnHorasExtras_Click(object sender, EventArgs e)
        {
            DtDatos.DataSource = null;

            DateTime fechaInicio = dtpDesdeHE.Value.Date;
            DateTime fechaFin = dtpHastaHE.Value.Date;

            if (fechaInicio > fechaFin)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la fecha final.",
                    "Rango inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            D_Asistencia funcion = new D_Asistencia();
            DataTable dt = funcion.ReporteHorasExtras(fechaInicio, fechaFin);

            DtDatos.DataSource = dt;

            // Formato bonito
            if (DtDatos.Columns.Contains("TotalHorasExtras"))
                DtDatos.Columns["TotalHorasExtras"].DefaultCellStyle.Format = "N2";

            if (DtDatos.Columns.Contains("Empleado"))
                DtDatos.Columns["Empleado"].HeaderText = "Nombre del trabajador";

            // Nombre base para exportar
            Name = "HorasExtras_";

            OcultarColumnas();
        }
    }
}
