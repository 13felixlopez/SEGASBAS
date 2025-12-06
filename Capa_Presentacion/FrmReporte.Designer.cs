namespace Capa_Presentacion
{
    partial class FrmReporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporte));
            this.PlOpciones = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnKardex = new System.Windows.Forms.Button();
            this.chkRangoFechas = new System.Windows.Forms.CheckBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cbProducto = new System.Windows.Forms.ComboBox();
            this.BtAsis = new System.Windows.Forms.Button();
            this.BtnEmpleado = new System.Windows.Forms.Button();
            this.BtnGeneral = new System.Windows.Forms.Button();
            this.BtnDescargar = new System.Windows.Forms.Button();
            this.DtDatos = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnAsisEmpleado = new System.Windows.Forms.Button();
            this.dtpHastaAsis = new System.Windows.Forms.DateTimePicker();
            this.dtpDesdeAsis = new System.Windows.Forms.DateTimePicker();
            this.cbEmpleadoAsis = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnDescargarPDF = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BtnHorasExtras = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpHastaHE = new System.Windows.Forms.DateTimePicker();
            this.dtpDesdeHE = new System.Windows.Forms.DateTimePicker();
            this.PlOpciones.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtDatos)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlOpciones
            // 
            this.PlOpciones.BackColor = System.Drawing.Color.Navy;
            this.PlOpciones.Controls.Add(this.panel3);
            this.PlOpciones.Controls.Add(this.BtnDescargarPDF);
            this.PlOpciones.Controls.Add(this.panel2);
            this.PlOpciones.Controls.Add(this.panel1);
            this.PlOpciones.Controls.Add(this.BtAsis);
            this.PlOpciones.Controls.Add(this.BtnEmpleado);
            this.PlOpciones.Controls.Add(this.BtnGeneral);
            this.PlOpciones.Controls.Add(this.BtnDescargar);
            this.PlOpciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.PlOpciones.Location = new System.Drawing.Point(0, 0);
            this.PlOpciones.Name = "PlOpciones";
            this.PlOpciones.Size = new System.Drawing.Size(1694, 313);
            this.PlOpciones.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BtnKardex);
            this.panel1.Controls.Add(this.chkRangoFechas);
            this.panel1.Controls.Add(this.dtpHasta);
            this.panel1.Controls.Add(this.dtpDesde);
            this.panel1.Controls.Add(this.cbProducto);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(297, 295);
            this.panel1.TabIndex = 2;
            // 
            // BtnKardex
            // 
            this.BtnKardex.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKardex.Location = new System.Drawing.Point(73, 255);
            this.BtnKardex.Name = "BtnKardex";
            this.BtnKardex.Size = new System.Drawing.Size(130, 33);
            this.BtnKardex.TabIndex = 7;
            this.BtnKardex.Text = "Aceptar";
            this.BtnKardex.UseVisualStyleBackColor = true;
            this.BtnKardex.Click += new System.EventHandler(this.BtnKardex_Click);
            // 
            // chkRangoFechas
            // 
            this.chkRangoFechas.AutoSize = true;
            this.chkRangoFechas.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chkRangoFechas.Location = new System.Drawing.Point(97, 219);
            this.chkRangoFechas.Name = "chkRangoFechas";
            this.chkRangoFechas.Size = new System.Drawing.Size(70, 20);
            this.chkRangoFechas.TabIndex = 6;
            this.chkRangoFechas.Text = "Rango";
            this.chkRangoFechas.UseVisualStyleBackColor = true;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(3, 183);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(287, 30);
            this.dtpHasta.TabIndex = 5;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(3, 123);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(287, 30);
            this.dtpDesde.TabIndex = 4;
            // 
            // cbProducto
            // 
            this.cbProducto.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProducto.FormattingEnabled = true;
            this.cbProducto.Location = new System.Drawing.Point(3, 46);
            this.cbProducto.Name = "cbProducto";
            this.cbProducto.Size = new System.Drawing.Size(287, 31);
            this.cbProducto.TabIndex = 3;
            this.cbProducto.SelectedIndexChanged += new System.EventHandler(this.cbProducto_SelectedIndexChanged);
            // 
            // BtAsis
            // 
            this.BtAsis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtAsis.Location = new System.Drawing.Point(644, 222);
            this.BtAsis.Name = "BtAsis";
            this.BtAsis.Size = new System.Drawing.Size(285, 79);
            this.BtAsis.TabIndex = 1;
            this.BtAsis.Text = "Asistencia";
            this.BtAsis.UseVisualStyleBackColor = true;
            this.BtAsis.Click += new System.EventHandler(this.BtAsis_Click);
            // 
            // BtnEmpleado
            // 
            this.BtnEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmpleado.Location = new System.Drawing.Point(644, 116);
            this.BtnEmpleado.Name = "BtnEmpleado";
            this.BtnEmpleado.Size = new System.Drawing.Size(285, 79);
            this.BtnEmpleado.TabIndex = 0;
            this.BtnEmpleado.Text = "Empleado";
            this.BtnEmpleado.UseVisualStyleBackColor = true;
            this.BtnEmpleado.Click += new System.EventHandler(this.BtnEmpleado_Click);
            // 
            // BtnGeneral
            // 
            this.BtnGeneral.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGeneral.Location = new System.Drawing.Point(644, 12);
            this.BtnGeneral.Name = "BtnGeneral";
            this.BtnGeneral.Size = new System.Drawing.Size(285, 79);
            this.BtnGeneral.TabIndex = 0;
            this.BtnGeneral.Text = "General";
            this.BtnGeneral.UseVisualStyleBackColor = true;
            this.BtnGeneral.Click += new System.EventHandler(this.BtnAsistencia_Click);
            // 
            // BtnDescargar
            // 
            this.BtnDescargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDescargar.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDescargar.Image = ((System.Drawing.Image)(resources.GetObject("BtnDescargar.Image")));
            this.BtnDescargar.Location = new System.Drawing.Point(1607, 12);
            this.BtnDescargar.Name = "BtnDescargar";
            this.BtnDescargar.Size = new System.Drawing.Size(75, 79);
            this.BtnDescargar.TabIndex = 0;
            this.BtnDescargar.UseVisualStyleBackColor = true;
            this.BtnDescargar.Click += new System.EventHandler(this.BtnDescargar_Click);
            // 
            // DtDatos
            // 
            this.DtDatos.AllowUserToAddRows = false;
            this.DtDatos.AllowUserToDeleteRows = false;
            this.DtDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DtDatos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DtDatos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DtDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DtDatos.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.DtDatos.Location = new System.Drawing.Point(0, 313);
            this.DtDatos.Name = "DtDatos";
            this.DtDatos.ReadOnly = true;
            this.DtDatos.RowHeadersWidth = 51;
            this.DtDatos.RowTemplate.Height = 24;
            this.DtDatos.Size = new System.Drawing.Size(1694, 392);
            this.DtDatos.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.BtnAsisEmpleado);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpHastaAsis);
            this.panel2.Controls.Add(this.dtpDesdeAsis);
            this.panel2.Controls.Add(this.cbEmpleadoAsis);
            this.panel2.Location = new System.Drawing.Point(330, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(297, 295);
            this.panel2.TabIndex = 3;
            // 
            // BtnAsisEmpleado
            // 
            this.BtnAsisEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAsisEmpleado.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnAsisEmpleado.Location = new System.Drawing.Point(84, 254);
            this.BtnAsisEmpleado.Name = "BtnAsisEmpleado";
            this.BtnAsisEmpleado.Size = new System.Drawing.Size(114, 33);
            this.BtnAsisEmpleado.TabIndex = 7;
            this.BtnAsisEmpleado.Text = "Aceptar";
            this.BtnAsisEmpleado.UseVisualStyleBackColor = true;
            this.BtnAsisEmpleado.Click += new System.EventHandler(this.BtnAsisEmpleado_Click);
            // 
            // dtpHastaAsis
            // 
            this.dtpHastaAsis.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHastaAsis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaAsis.Location = new System.Drawing.Point(3, 183);
            this.dtpHastaAsis.Name = "dtpHastaAsis";
            this.dtpHastaAsis.Size = new System.Drawing.Size(287, 31);
            this.dtpHastaAsis.TabIndex = 5;
            // 
            // dtpDesdeAsis
            // 
            this.dtpDesdeAsis.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesdeAsis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeAsis.Location = new System.Drawing.Point(3, 123);
            this.dtpDesdeAsis.Name = "dtpDesdeAsis";
            this.dtpDesdeAsis.Size = new System.Drawing.Size(287, 31);
            this.dtpDesdeAsis.TabIndex = 4;
            // 
            // cbEmpleadoAsis
            // 
            this.cbEmpleadoAsis.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEmpleadoAsis.FormattingEnabled = true;
            this.cbEmpleadoAsis.Location = new System.Drawing.Point(3, 45);
            this.cbEmpleadoAsis.Name = "cbEmpleadoAsis";
            this.cbEmpleadoAsis.Size = new System.Drawing.Size(287, 32);
            this.cbEmpleadoAsis.TabIndex = 3;
            this.cbEmpleadoAsis.SelectedIndexChanged += new System.EventHandler(this.cbEmpleadoAsis_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(14, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Kardex de Salidas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(3, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Desde:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(3, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(12, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Hasta:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(6, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Desde:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(235, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Asistencia por Persona";
            // 
            // BtnDescargarPDF
            // 
            this.BtnDescargarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDescargarPDF.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDescargarPDF.Image = ((System.Drawing.Image)(resources.GetObject("BtnDescargarPDF.Image")));
            this.BtnDescargarPDF.Location = new System.Drawing.Point(1607, 97);
            this.BtnDescargarPDF.Name = "BtnDescargarPDF";
            this.BtnDescargarPDF.Size = new System.Drawing.Size(75, 79);
            this.BtnDescargarPDF.TabIndex = 4;
            this.BtnDescargarPDF.UseVisualStyleBackColor = true;
            this.BtnDescargarPDF.Click += new System.EventHandler(this.BtnDescargarPDF_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.BtnHorasExtras);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.dtpHastaHE);
            this.panel3.Controls.Add(this.dtpDesdeHE);
            this.panel3.Location = new System.Drawing.Point(954, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(297, 295);
            this.panel3.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(79, 1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 25);
            this.label7.TabIndex = 13;
            this.label7.Text = "Horas Extras";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(4, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 25);
            this.label8.TabIndex = 12;
            this.label8.Text = "Hasta:";
            // 
            // BtnHorasExtras
            // 
            this.BtnHorasExtras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHorasExtras.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnHorasExtras.Location = new System.Drawing.Point(84, 254);
            this.BtnHorasExtras.Name = "BtnHorasExtras";
            this.BtnHorasExtras.Size = new System.Drawing.Size(114, 33);
            this.BtnHorasExtras.TabIndex = 7;
            this.BtnHorasExtras.Text = "Aceptar";
            this.BtnHorasExtras.UseVisualStyleBackColor = true;
            this.BtnHorasExtras.Click += new System.EventHandler(this.BtnHorasExtras_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(-2, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 25);
            this.label9.TabIndex = 11;
            this.label9.Text = "Desde:";
            // 
            // dtpHastaHE
            // 
            this.dtpHastaHE.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHastaHE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHastaHE.Location = new System.Drawing.Point(3, 171);
            this.dtpHastaHE.Name = "dtpHastaHE";
            this.dtpHastaHE.Size = new System.Drawing.Size(287, 31);
            this.dtpHastaHE.TabIndex = 5;
            // 
            // dtpDesdeHE
            // 
            this.dtpDesdeHE.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDesdeHE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesdeHE.Location = new System.Drawing.Point(-2, 79);
            this.dtpDesdeHE.Name = "dtpDesdeHE";
            this.dtpDesdeHE.Size = new System.Drawing.Size(287, 31);
            this.dtpDesdeHE.TabIndex = 4;
            // 
            // FrmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1694, 705);
            this.Controls.Add(this.DtDatos);
            this.Controls.Add(this.PlOpciones);
            this.Name = "FrmReporte";
            this.Text = "FrmReporte";
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            this.PlOpciones.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtDatos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PlOpciones;
        private System.Windows.Forms.Button BtnGeneral;
        private System.Windows.Forms.Button BtnDescargar;
        private System.Windows.Forms.DataGridView DtDatos;
        private System.Windows.Forms.Button BtnEmpleado;
        private System.Windows.Forms.Button BtAsis;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnKardex;
        private System.Windows.Forms.CheckBox chkRangoFechas;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.ComboBox cbProducto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnAsisEmpleado;
        private System.Windows.Forms.DateTimePicker dtpHastaAsis;
        private System.Windows.Forms.DateTimePicker dtpDesdeAsis;
        private System.Windows.Forms.ComboBox cbEmpleadoAsis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnDescargarPDF;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BtnHorasExtras;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpHastaHE;
        private System.Windows.Forms.DateTimePicker dtpDesdeHE;
    }
}