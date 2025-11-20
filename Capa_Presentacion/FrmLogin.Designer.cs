namespace Capa_Presentacion
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.TxtUser = new System.Windows.Forms.TextBox();
            this.TxtPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabelrecuperarcontraseña = new System.Windows.Forms.LinkLabel();
            this.BT_Aceptar = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtUser
            // 
            this.TxtUser.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUser.Location = new System.Drawing.Point(599, 144);
            this.TxtUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Size = new System.Drawing.Size(451, 34);
            this.TxtUser.TabIndex = 1;
            // 
            // TxtPass
            // 
            this.TxtPass.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPass.Location = new System.Drawing.Point(599, 231);
            this.TxtPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtPass.Name = "TxtPass";
            this.TxtPass.PasswordChar = '*';
            this.TxtPass.Size = new System.Drawing.Size(451, 34);
            this.TxtPass.TabIndex = 2;
            this.TxtPass.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(768, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 27);
            this.label1.TabIndex = 4;
            this.label1.Text = "Bienvenido ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(597, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 27);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(597, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "Contraseña:";
            // 
            // linkLabelrecuperarcontraseña
            // 
            this.linkLabelrecuperarcontraseña.AutoSize = true;
            this.linkLabelrecuperarcontraseña.Location = new System.Drawing.Point(728, 359);
            this.linkLabelrecuperarcontraseña.Name = "linkLabelrecuperarcontraseña";
            this.linkLabelrecuperarcontraseña.Size = new System.Drawing.Size(183, 16);
            this.linkLabelrecuperarcontraseña.TabIndex = 7;
            this.linkLabelrecuperarcontraseña.TabStop = true;
            this.linkLabelrecuperarcontraseña.Text = "Has Olvidado la Contraseña?";
            this.linkLabelrecuperarcontraseña.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelrecuperarcontraseña_LinkClicked);
            // 
            // BT_Aceptar
            // 
            this.BT_Aceptar.BackColor = System.Drawing.Color.Green;
            this.BT_Aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BT_Aceptar.ForeColor = System.Drawing.Color.Black;
            this.BT_Aceptar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.BT_Aceptar.IconColor = System.Drawing.Color.Green;
            this.BT_Aceptar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BT_Aceptar.Location = new System.Drawing.Point(745, 297);
            this.BT_Aceptar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BT_Aceptar.Name = "BT_Aceptar";
            this.BT_Aceptar.Size = new System.Drawing.Size(155, 39);
            this.BT_Aceptar.TabIndex = 3;
            this.BT_Aceptar.Text = "Entrar";
            this.BT_Aceptar.UseVisualStyleBackColor = false;
            this.BT_Aceptar.Click += new System.EventHandler(this.BT_Aceptar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(580, 425);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.BT_Aceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1061, 425);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.linkLabelrecuperarcontraseña);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_Aceptar);
            this.Controls.Add(this.TxtPass);
            this.Controls.Add(this.TxtUser);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLogin";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtUser;
        private System.Windows.Forms.TextBox TxtPass;
        private FontAwesome.Sharp.IconButton BT_Aceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabelrecuperarcontraseña;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}