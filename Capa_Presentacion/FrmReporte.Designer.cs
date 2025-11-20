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
            this.BtnEmpleado = new System.Windows.Forms.Button();
            this.BtnGeneral = new System.Windows.Forms.Button();
            this.BtnDescargar = new System.Windows.Forms.Button();
            this.DtDatos = new System.Windows.Forms.DataGridView();
            this.BtAsis = new System.Windows.Forms.Button();
            this.PlOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DtDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // PlOpciones
            // 
            this.PlOpciones.BackColor = System.Drawing.Color.Navy;
            this.PlOpciones.Controls.Add(this.BtAsis);
            this.PlOpciones.Controls.Add(this.BtnEmpleado);
            this.PlOpciones.Controls.Add(this.BtnGeneral);
            this.PlOpciones.Controls.Add(this.BtnDescargar);
            this.PlOpciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.PlOpciones.Location = new System.Drawing.Point(0, 0);
            this.PlOpciones.Name = "PlOpciones";
            this.PlOpciones.Size = new System.Drawing.Size(1694, 100);
            this.PlOpciones.TabIndex = 0;
            // 
            // BtnEmpleado
            // 
            this.BtnEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEmpleado.Location = new System.Drawing.Point(331, 12);
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
            this.BtnGeneral.Location = new System.Drawing.Point(12, 12);
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
            this.DtDatos.Location = new System.Drawing.Point(0, 100);
            this.DtDatos.Name = "DtDatos";
            this.DtDatos.ReadOnly = true;
            this.DtDatos.RowHeadersWidth = 51;
            this.DtDatos.RowTemplate.Height = 24;
            this.DtDatos.Size = new System.Drawing.Size(1694, 605);
            this.DtDatos.TabIndex = 1;
            // 
            // BtAsis
            // 
            this.BtAsis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtAsis.Location = new System.Drawing.Point(644, 12);
            this.BtAsis.Name = "BtAsis";
            this.BtAsis.Size = new System.Drawing.Size(285, 79);
            this.BtAsis.TabIndex = 1;
            this.BtAsis.Text = "Asistencia";
            this.BtAsis.UseVisualStyleBackColor = true;
            this.BtAsis.Click += new System.EventHandler(this.BtAsis_Click);
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
            ((System.ComponentModel.ISupportInitialize)(this.DtDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PlOpciones;
        private System.Windows.Forms.Button BtnGeneral;
        private System.Windows.Forms.Button BtnDescargar;
        private System.Windows.Forms.DataGridView DtDatos;
        private System.Windows.Forms.Button BtnEmpleado;
        private System.Windows.Forms.Button BtAsis;
    }
}