using Capa_Presentacion.Datos;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FrmReporte : Form
    {
        string Name;
        public FrmReporte()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
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

        private void FrmReporte_Load(object sender, EventArgs e)
        {

        }
    }
}
