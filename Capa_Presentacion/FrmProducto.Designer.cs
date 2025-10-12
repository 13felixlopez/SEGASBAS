namespace Capa_Presentacion
{
    partial class FrmProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProducto));
            this.TxtBuscar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnAnterior = new FontAwesome.Sharp.IconButton();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtTotalPagina = new System.Windows.Forms.TextBox();
            this.TxtPagina = new System.Windows.Forms.TextBox();
            this.BtnSiguiente = new FontAwesome.Sharp.IconButton();
            this.BTEliminar = new FontAwesome.Sharp.IconButton();
            this.BTEditar = new FontAwesome.Sharp.IconButton();
            this.BTAgregar = new FontAwesome.Sharp.IconButton();
            this.dgvProducto = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtProducto = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtBuscar
            // 
            this.TxtBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuscar.Location = new System.Drawing.Point(226, 57);
            this.TxtBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TxtBuscar.Multiline = true;
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.Size = new System.Drawing.Size(171, 27);
            this.TxtBuscar.TabIndex = 111;
            this.TxtBuscar.TextChanged += new System.EventHandler(this.TxtBuscar_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(224, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 112;
            this.label7.Text = "Buscar Por:";
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
            this.BtnAnterior.Location = new System.Drawing.Point(87, 299);
            this.BtnAnterior.Name = "BtnAnterior";
            this.BtnAnterior.Size = new System.Drawing.Size(30, 30);
            this.BtnAnterior.TabIndex = 106;
            this.BtnAnterior.UseVisualStyleBackColor = false;
            this.BtnAnterior.Click += new System.EventHandler(this.BtnAnterior_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(32, 301);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 21);
            this.label8.TabIndex = 110;
            this.label8.Text = "Página";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtTotalPagina
            // 
            this.TxtTotalPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtTotalPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTotalPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTotalPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtTotalPagina.Location = new System.Drawing.Point(226, 301);
            this.TxtTotalPagina.Name = "TxtTotalPagina";
            this.TxtTotalPagina.ReadOnly = true;
            this.TxtTotalPagina.Size = new System.Drawing.Size(36, 29);
            this.TxtTotalPagina.TabIndex = 109;
            // 
            // TxtPagina
            // 
            this.TxtPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtPagina.Location = new System.Drawing.Point(123, 301);
            this.TxtPagina.Name = "TxtPagina";
            this.TxtPagina.Size = new System.Drawing.Size(36, 29);
            this.TxtPagina.TabIndex = 108;
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
            this.BtnSiguiente.Location = new System.Drawing.Point(164, 298);
            this.BtnSiguiente.Name = "BtnSiguiente";
            this.BtnSiguiente.Size = new System.Drawing.Size(30, 30);
            this.BtnSiguiente.TabIndex = 107;
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
            this.BTEliminar.Location = new System.Drawing.Point(549, 299);
            this.BTEliminar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTEliminar.Name = "BTEliminar";
            this.BTEliminar.Size = new System.Drawing.Size(96, 31);
            this.BTEliminar.TabIndex = 104;
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
            this.BTEditar.Location = new System.Drawing.Point(446, 298);
            this.BTEditar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTEditar.Name = "BTEditar";
            this.BTEditar.Size = new System.Drawing.Size(99, 31);
            this.BTEditar.TabIndex = 105;
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
            this.BTAgregar.Location = new System.Drawing.Point(341, 297);
            this.BTAgregar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTAgregar.Name = "BTAgregar";
            this.BTAgregar.Size = new System.Drawing.Size(101, 32);
            this.BTAgregar.TabIndex = 103;
            this.BTAgregar.Text = "Agregar";
            this.BTAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTAgregar.UseVisualStyleBackColor = false;
            this.BTAgregar.Click += new System.EventHandler(this.BTAgregar_Click);
            // 
            // dgvProducto
            // 
            this.dgvProducto.AllowUserToAddRows = false;
            this.dgvProducto.AllowUserToDeleteRows = false;
            this.dgvProducto.AllowUserToOrderColumns = true;
            this.dgvProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducto.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProducto.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducto.Location = new System.Drawing.Point(32, 91);
            this.dgvProducto.Name = "dgvProducto";
            this.dgvProducto.ReadOnly = true;
            this.dgvProducto.RowHeadersVisible = false;
            this.dgvProducto.RowHeadersWidth = 51;
            this.dgvProducto.Size = new System.Drawing.Size(613, 197);
            this.dgvProducto.TabIndex = 102;
            this.dgvProducto.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducto_CellClick);
            this.dgvProducto.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducto_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 101;
            this.label1.Text = "Nombre de Producto";
            // 
            // TxtProducto
            // 
            this.TxtProducto.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtProducto.Location = new System.Drawing.Point(32, 57);
            this.TxtProducto.Multiline = true;
            this.TxtProducto.Name = "TxtProducto";
            this.TxtProducto.Size = new System.Drawing.Size(164, 27);
            this.TxtProducto.TabIndex = 100;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(652, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 113;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // FrmProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(686, 366);
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
            this.Controls.Add(this.dgvProducto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtProducto);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmProducto";
            this.Text = "FrmProducto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProducto_FormClosing);
            this.Load += new System.EventHandler(this.FrmProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducto)).EndInit();
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
        private System.Windows.Forms.DataGridView dgvProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtProducto;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}