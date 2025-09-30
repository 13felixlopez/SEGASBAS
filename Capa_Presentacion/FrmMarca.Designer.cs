namespace Capa_Presentacion
{
    partial class FrmMarca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMarca));
            this.TxtBuscar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtTotalPagina = new System.Windows.Forms.TextBox();
            this.TxtPagina = new System.Windows.Forms.TextBox();
            this.dgvMarca = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtMarca = new System.Windows.Forms.TextBox();
            this.BtnAnterior = new FontAwesome.Sharp.IconButton();
            this.BtnSiguiente = new FontAwesome.Sharp.IconButton();
            this.BTEliminar = new FontAwesome.Sharp.IconButton();
            this.BTEditar = new FontAwesome.Sharp.IconButton();
            this.BTAgregar = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtBuscar
            // 
            this.TxtBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuscar.Location = new System.Drawing.Point(332, 70);
            this.TxtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtBuscar.Multiline = true;
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.Size = new System.Drawing.Size(227, 32);
            this.TxtBuscar.TabIndex = 124;
            this.TxtBuscar.TextChanged += new System.EventHandler(this.TxtBuscar_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(328, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 24);
            this.label7.TabIndex = 125;
            this.label7.Text = "Buscar Por:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(72, 370);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 28);
            this.label8.TabIndex = 123;
            this.label8.Text = "Página";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtTotalPagina
            // 
            this.TxtTotalPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtTotalPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTotalPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTotalPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtTotalPagina.Location = new System.Drawing.Point(332, 371);
            this.TxtTotalPagina.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTotalPagina.Name = "TxtTotalPagina";
            this.TxtTotalPagina.ReadOnly = true;
            this.TxtTotalPagina.Size = new System.Drawing.Size(47, 34);
            this.TxtTotalPagina.TabIndex = 122;
            // 
            // TxtPagina
            // 
            this.TxtPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtPagina.Location = new System.Drawing.Point(194, 370);
            this.TxtPagina.Margin = new System.Windows.Forms.Padding(4);
            this.TxtPagina.Name = "TxtPagina";
            this.TxtPagina.Size = new System.Drawing.Size(47, 34);
            this.TxtPagina.TabIndex = 121;
            // 
            // dgvMarca
            // 
            this.dgvMarca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMarca.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMarca.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMarca.Location = new System.Drawing.Point(72, 112);
            this.dgvMarca.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMarca.Name = "dgvMarca";
            this.dgvMarca.RowHeadersWidth = 51;
            this.dgvMarca.Size = new System.Drawing.Size(817, 242);
            this.dgvMarca.TabIndex = 115;
            this.dgvMarca.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMarca_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 114;
            this.label1.Text = "Marca";
            // 
            // TxtMarca
            // 
            this.TxtMarca.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMarca.Location = new System.Drawing.Point(72, 70);
            this.TxtMarca.Margin = new System.Windows.Forms.Padding(4);
            this.TxtMarca.Multiline = true;
            this.TxtMarca.Name = "TxtMarca";
            this.TxtMarca.Size = new System.Drawing.Size(217, 32);
            this.TxtMarca.TabIndex = 113;
            // 
            // BtnAnterior
            // 
            this.BtnAnterior.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAnterior.BackColor = System.Drawing.Color.Transparent;
            this.BtnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAnterior.FlatAppearance.BorderSize = 0;
            this.BtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAnterior.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            this.BtnAnterior.IconColor = System.Drawing.Color.Black;
            this.BtnAnterior.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnAnterior.IconSize = 28;
            this.BtnAnterior.Location = new System.Drawing.Point(146, 368);
            this.BtnAnterior.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAnterior.Name = "BtnAnterior";
            this.BtnAnterior.Size = new System.Drawing.Size(40, 37);
            this.BtnAnterior.TabIndex = 119;
            this.BtnAnterior.UseVisualStyleBackColor = false;
            this.BtnAnterior.Click += new System.EventHandler(this.BtnAnterior_Click);
            // 
            // BtnSiguiente
            // 
            this.BtnSiguiente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSiguiente.BackColor = System.Drawing.Color.Transparent;
            this.BtnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSiguiente.FlatAppearance.BorderSize = 0;
            this.BtnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSiguiente.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
            this.BtnSiguiente.IconColor = System.Drawing.Color.Black;
            this.BtnSiguiente.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BtnSiguiente.IconSize = 28;
            this.BtnSiguiente.Location = new System.Drawing.Point(249, 367);
            this.BtnSiguiente.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSiguiente.Name = "BtnSiguiente";
            this.BtnSiguiente.Size = new System.Drawing.Size(40, 37);
            this.BtnSiguiente.TabIndex = 120;
            this.BtnSiguiente.UseVisualStyleBackColor = false;
            this.BtnSiguiente.Click += new System.EventHandler(this.BtnSiguiente_Click);
            // 
            // BTEliminar
            // 
            this.BTEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BTEliminar.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BTEliminar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.BTEliminar.IconColor = System.Drawing.Color.White;
            this.BTEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTEliminar.IconSize = 25;
            this.BTEliminar.Location = new System.Drawing.Point(762, 368);
            this.BTEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTEliminar.Name = "BTEliminar";
            this.BTEliminar.Size = new System.Drawing.Size(128, 38);
            this.BTEliminar.TabIndex = 117;
            this.BTEliminar.Text = "Eliminar";
            this.BTEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTEliminar.UseVisualStyleBackColor = false;
            this.BTEliminar.Click += new System.EventHandler(this.BTEliminar_Click);
            // 
            // BTEditar
            // 
            this.BTEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTEditar.BackColor = System.Drawing.Color.MediumBlue;
            this.BTEditar.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTEditar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BTEditar.IconChar = FontAwesome.Sharp.IconChar.Pencil;
            this.BTEditar.IconColor = System.Drawing.Color.White;
            this.BTEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTEditar.IconSize = 25;
            this.BTEditar.Location = new System.Drawing.Point(625, 367);
            this.BTEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTEditar.Name = "BTEditar";
            this.BTEditar.Size = new System.Drawing.Size(132, 38);
            this.BTEditar.TabIndex = 118;
            this.BTEditar.Text = "Editar";
            this.BTEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTEditar.UseVisualStyleBackColor = false;
            this.BTEditar.Click += new System.EventHandler(this.BTEditar_Click);
            // 
            // BTAgregar
            // 
            this.BTAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTAgregar.BackColor = System.Drawing.Color.Green;
            this.BTAgregar.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTAgregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BTAgregar.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            this.BTAgregar.IconColor = System.Drawing.Color.White;
            this.BTAgregar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.BTAgregar.IconSize = 25;
            this.BTAgregar.Location = new System.Drawing.Point(485, 365);
            this.BTAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTAgregar.Name = "BTAgregar";
            this.BTAgregar.Size = new System.Drawing.Size(135, 39);
            this.BTAgregar.TabIndex = 116;
            this.BTAgregar.Text = "Agregar";
            this.BTAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTAgregar.UseVisualStyleBackColor = false;
            this.BTAgregar.Click += new System.EventHandler(this.BTAgregar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(910, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 126;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FrmMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(956, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TxtBuscar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnAnterior);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TxtTotalPagina);
            this.Controls.Add(this.TxtPagina);
            this.Controls.Add(this.BtnSiguiente);
            this.Controls.Add(this.BTEliminar);
            this.Controls.Add(this.BTEditar);
            this.Controls.Add(this.BTAgregar);
            this.Controls.Add(this.dgvMarca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtMarca);
            this.Name = "FrmMarca";
            this.Text = "FrmMarca";
            this.Load += new System.EventHandler(this.FrmMarca_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMarca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBuscar;
        private System.Windows.Forms.Label label7;
        private FontAwesome.Sharp.IconButton BtnAnterior;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtTotalPagina;
        private System.Windows.Forms.TextBox TxtPagina;
        private FontAwesome.Sharp.IconButton BtnSiguiente;
        private FontAwesome.Sharp.IconButton BTEliminar;
        private FontAwesome.Sharp.IconButton BTEditar;
        private FontAwesome.Sharp.IconButton BTAgregar;
        private System.Windows.Forms.DataGridView dgvMarca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtMarca;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}