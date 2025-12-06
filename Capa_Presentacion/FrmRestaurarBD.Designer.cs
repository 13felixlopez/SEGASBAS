namespace Capa_Presentacion
{
    partial class FrmRestaurarBD
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
            this.cmbBackups = new System.Windows.Forms.ComboBox();
            this.btnRefrescar = new FontAwesome.Sharp.IconButton();
            this.btnRestaurar = new FontAwesome.Sharp.IconButton();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbBackups
            // 
            this.cmbBackups.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBackups.FormattingEnabled = true;
            this.cmbBackups.Location = new System.Drawing.Point(12, 29);
            this.cmbBackups.Name = "cmbBackups";
            this.cmbBackups.Size = new System.Drawing.Size(365, 35);
            this.cmbBackups.TabIndex = 0;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.IconChar = FontAwesome.Sharp.IconChar.Repeat;
            this.btnRefrescar.IconColor = System.Drawing.Color.Black;
            this.btnRefrescar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRefrescar.IconSize = 30;
            this.btnRefrescar.Location = new System.Drawing.Point(383, 29);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(51, 36);
            this.btnRefrescar.TabIndex = 1;
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnRestaurar
            // 
            this.btnRestaurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnRestaurar.IconColor = System.Drawing.Color.Black;
            this.btnRestaurar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRestaurar.Location = new System.Drawing.Point(119, 77);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(106, 38);
            this.btnRestaurar.TabIndex = 2;
            this.btnRestaurar.Text = "Restaurar";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(148, 118);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "label1";
            // 
            // FrmRestaurarBD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 205);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.cmbBackups);
            this.MinimizeBox = false;
            this.Name = "FrmRestaurarBD";
            this.Text = "FrmRestaurarBD";
            this.Load += new System.EventHandler(this.FrmRestaurarBD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBackups;
        private FontAwesome.Sharp.IconButton btnRefrescar;
        private FontAwesome.Sharp.IconButton btnRestaurar;
        private System.Windows.Forms.Label lblStatus;
    }
}