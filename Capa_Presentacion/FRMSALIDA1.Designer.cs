namespace Capa_Presentacion
{
    partial class FRMSALIDA1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRMSALIDA1));
            this.lbtotalfactura = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtTotalPagina = new System.Windows.Forms.TextBox();
            this.DatagreedSalidas = new System.Windows.Forms.DataGridView();
            this.TxtPagina = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TxtBuscar = new System.Windows.Forms.TextBox();
            this.BtnSiguiente = new FontAwesome.Sharp.IconButton();
            this.CmbBuscar = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.BtnAnterior = new FontAwesome.Sharp.IconButton();
            this.BTAgregar = new FontAwesome.Sharp.IconButton();
            this.BTCancelar = new FontAwesome.Sharp.IconButton();
            this.BTEditar = new FontAwesome.Sharp.IconButton();
            this.BTEliminar = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Cb_Ciclo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cblote = new System.Windows.Forms.ComboBox();
            this.TxtCostoUnitario = new System.Windows.Forms.TextBox();
            this.TxtCantidad = new System.Windows.Forms.TextBox();
            this.btañadirunidaddemedida = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimeSalida = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Cb_Producto = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdescripcion = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TxtCategoria = new System.Windows.Forms.TextBox();
            this.TxtUnidaddemedida = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DatagreedSalidas)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btañadirunidaddemedida)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbtotalfactura
            // 
            this.lbtotalfactura.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbtotalfactura.AutoSize = true;
            this.lbtotalfactura.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtotalfactura.Location = new System.Drawing.Point(829, 800);
            this.lbtotalfactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbtotalfactura.Name = "lbtotalfactura";
            this.lbtotalfactura.Size = new System.Drawing.Size(32, 24);
            this.lbtotalfactura.TabIndex = 171;
            this.lbtotalfactura.Text = "00";
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(691, 800);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(126, 24);
            this.label18.TabIndex = 170;
            this.label18.Text = "Total Factura:";
            // 
            // TxtTotalPagina
            // 
            this.TxtTotalPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtTotalPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtTotalPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtTotalPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtTotalPagina.Location = new System.Drawing.Point(370, 794);
            this.TxtTotalPagina.Margin = new System.Windows.Forms.Padding(4);
            this.TxtTotalPagina.Name = "TxtTotalPagina";
            this.TxtTotalPagina.ReadOnly = true;
            this.TxtTotalPagina.Size = new System.Drawing.Size(47, 34);
            this.TxtTotalPagina.TabIndex = 167;
            // 
            // DatagreedSalidas
            // 
            this.DatagreedSalidas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DatagreedSalidas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DatagreedSalidas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DatagreedSalidas.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DatagreedSalidas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DatagreedSalidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DatagreedSalidas.Location = new System.Drawing.Point(22, 474);
            this.DatagreedSalidas.Margin = new System.Windows.Forms.Padding(4);
            this.DatagreedSalidas.Name = "DatagreedSalidas";
            this.DatagreedSalidas.RowHeadersWidth = 51;
            this.DatagreedSalidas.Size = new System.Drawing.Size(1689, 311);
            this.DatagreedSalidas.TabIndex = 161;
            // 
            // TxtPagina
            // 
            this.TxtPagina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtPagina.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPagina.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TxtPagina.Location = new System.Drawing.Point(174, 794);
            this.TxtPagina.Margin = new System.Windows.Forms.Padding(4);
            this.TxtPagina.Name = "TxtPagina";
            this.TxtPagina.Size = new System.Drawing.Size(47, 34);
            this.TxtPagina.TabIndex = 166;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(30, 428);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 26);
            this.label12.TabIndex = 160;
            this.label12.Text = "Buscar Por:";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(305, 798);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 28);
            this.label13.TabIndex = 168;
            this.label13.Text = "De";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtBuscar
            // 
            this.TxtBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBuscar.Location = new System.Drawing.Point(174, 425);
            this.TxtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TxtBuscar.Multiline = true;
            this.TxtBuscar.Name = "TxtBuscar";
            this.TxtBuscar.Size = new System.Drawing.Size(288, 34);
            this.TxtBuscar.TabIndex = 159;
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
            this.BtnSiguiente.Location = new System.Drawing.Point(257, 794);
            this.BtnSiguiente.Margin = new System.Windows.Forms.Padding(4);
            this.BtnSiguiente.Name = "BtnSiguiente";
            this.BtnSiguiente.Size = new System.Drawing.Size(40, 37);
            this.BtnSiguiente.TabIndex = 165;
            this.BtnSiguiente.UseVisualStyleBackColor = false;
            this.BtnSiguiente.Click += new System.EventHandler(this.BtnSiguiente_Click);
            // 
            // CmbBuscar
            // 
            this.CmbBuscar.Font = new System.Drawing.Font("Microsoft YaHei", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBuscar.FormattingEnabled = true;
            this.CmbBuscar.Location = new System.Drawing.Point(485, 423);
            this.CmbBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.CmbBuscar.Name = "CmbBuscar";
            this.CmbBuscar.Size = new System.Drawing.Size(276, 38);
            this.CmbBuscar.TabIndex = 158;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(23, 798);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 28);
            this.label14.TabIndex = 169;
            this.label14.Text = "Página";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.BtnAnterior.Location = new System.Drawing.Point(107, 794);
            this.BtnAnterior.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAnterior.Name = "BtnAnterior";
            this.BtnAnterior.Size = new System.Drawing.Size(40, 37);
            this.BtnAnterior.TabIndex = 164;
            this.BtnAnterior.UseVisualStyleBackColor = false;
            this.BtnAnterior.Click += new System.EventHandler(this.BtnAnterior_Click);
            // 
            // BTAgregar
            // 
            this.BTAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTAgregar.BackColor = System.Drawing.Color.Green;
            this.BTAgregar.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTAgregar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BTAgregar.IconChar = FontAwesome.Sharp.IconChar.FileCircleCheck;
            this.BTAgregar.IconColor = System.Drawing.Color.White;
            this.BTAgregar.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this.BTAgregar.IconSize = 30;
            this.BTAgregar.Location = new System.Drawing.Point(1378, 329);
            this.BTAgregar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTAgregar.Name = "BTAgregar";
            this.BTAgregar.Size = new System.Drawing.Size(160, 42);
            this.BTAgregar.TabIndex = 142;
            this.BTAgregar.Text = "Agregar";
            this.BTAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTAgregar.UseVisualStyleBackColor = false;
            this.BTAgregar.Click += new System.EventHandler(this.BTAgregar_Click);
            // 
            // BTCancelar
            // 
            this.BTCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTCancelar.BackColor = System.Drawing.Color.Orange;
            this.BTCancelar.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTCancelar.ForeColor = System.Drawing.Color.White;
            this.BTCancelar.IconChar = FontAwesome.Sharp.IconChar.CircleXmark;
            this.BTCancelar.IconColor = System.Drawing.Color.White;
            this.BTCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTCancelar.IconSize = 30;
            this.BTCancelar.Location = new System.Drawing.Point(1551, 324);
            this.BTCancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTCancelar.Name = "BTCancelar";
            this.BTCancelar.Size = new System.Drawing.Size(156, 47);
            this.BTCancelar.TabIndex = 44;
            this.BTCancelar.Text = "Cancelar";
            this.BTCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTCancelar.UseVisualStyleBackColor = false;
            // 
            // BTEditar
            // 
            this.BTEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTEditar.BackColor = System.Drawing.Color.MediumBlue;
            this.BTEditar.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTEditar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BTEditar.IconChar = FontAwesome.Sharp.IconChar.Pencil;
            this.BTEditar.IconColor = System.Drawing.Color.White;
            this.BTEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTEditar.IconSize = 30;
            this.BTEditar.Location = new System.Drawing.Point(1403, 794);
            this.BTEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTEditar.Name = "BTEditar";
            this.BTEditar.Size = new System.Drawing.Size(153, 43);
            this.BTEditar.TabIndex = 163;
            this.BTEditar.Text = "Editar";
            this.BTEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTEditar.UseVisualStyleBackColor = false;
            // 
            // BTEliminar
            // 
            this.BTEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BTEliminar.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTEliminar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BTEliminar.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            this.BTEliminar.IconColor = System.Drawing.Color.White;
            this.BTEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BTEliminar.IconSize = 30;
            this.BTEliminar.Location = new System.Drawing.Point(1563, 791);
            this.BTEliminar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BTEliminar.Name = "BTEliminar";
            this.BTEliminar.Size = new System.Drawing.Size(156, 46);
            this.BTEliminar.TabIndex = 162;
            this.BTEliminar.Text = "Eliminar";
            this.BTEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BTEliminar.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.TxtUnidaddemedida);
            this.panel1.Controls.Add(this.TxtCategoria);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Cb_Ciclo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cblote);
            this.panel1.Controls.Add(this.TxtCostoUnitario);
            this.panel1.Controls.Add(this.TxtCantidad);
            this.panel1.Controls.Add(this.btañadirunidaddemedida);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.dateTimeSalida);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.BTAgregar);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.BTCancelar);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Cb_Producto);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtdescripcion);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1717, 387);
            this.panel1.TabIndex = 157;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1101, 129);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 24);
            this.label4.TabIndex = 78;
            this.label4.Text = "Ciclo";
            // 
            // Cb_Ciclo
            // 
            this.Cb_Ciclo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Ciclo.FormattingEnabled = true;
            this.Cb_Ciclo.Location = new System.Drawing.Point(1105, 157);
            this.Cb_Ciclo.Margin = new System.Windows.Forms.Padding(4);
            this.Cb_Ciclo.Name = "Cb_Ciclo";
            this.Cb_Ciclo.Size = new System.Drawing.Size(240, 35);
            this.Cb_Ciclo.TabIndex = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(837, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 76;
            this.label2.Text = "Destino (Lote)";
            // 
            // cblote
            // 
            this.cblote.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cblote.FormattingEnabled = true;
            this.cblote.Location = new System.Drawing.Point(841, 157);
            this.cblote.Margin = new System.Windows.Forms.Padding(4);
            this.cblote.Name = "cblote";
            this.cblote.Size = new System.Drawing.Size(240, 35);
            this.cblote.TabIndex = 75;
            // 
            // TxtCostoUnitario
            // 
            this.TxtCostoUnitario.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCostoUnitario.Location = new System.Drawing.Point(841, 94);
            this.TxtCostoUnitario.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCostoUnitario.Multiline = true;
            this.TxtCostoUnitario.Name = "TxtCostoUnitario";
            this.TxtCostoUnitario.Size = new System.Drawing.Size(240, 32);
            this.TxtCostoUnitario.TabIndex = 74;
            // 
            // TxtCantidad
            // 
            this.TxtCantidad.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCantidad.Location = new System.Drawing.Point(1105, 94);
            this.TxtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCantidad.Multiline = true;
            this.TxtCantidad.Name = "TxtCantidad";
            this.TxtCantidad.Size = new System.Drawing.Size(136, 32);
            this.TxtCantidad.TabIndex = 73;
            // 
            // btañadirunidaddemedida
            // 
            this.btañadirunidaddemedida.Image = ((System.Drawing.Image)(resources.GetObject("btañadirunidaddemedida.Image")));
            this.btañadirunidaddemedida.Location = new System.Drawing.Point(1501, 94);
            this.btañadirunidaddemedida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btañadirunidaddemedida.Name = "btañadirunidaddemedida";
            this.btañadirunidaddemedida.Size = new System.Drawing.Size(37, 34);
            this.btañadirunidaddemedida.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btañadirunidaddemedida.TabIndex = 69;
            this.btañadirunidaddemedida.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1266, 64);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(172, 24);
            this.label17.TabIndex = 67;
            this.label17.Text = "Unidad de Medida";
            // 
            // dateTimeSalida
            // 
            this.dateTimeSalida.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeSalida.Location = new System.Drawing.Point(347, 154);
            this.dateTimeSalida.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimeSalida.Name = "dateTimeSalida";
            this.dateTimeSalida.Size = new System.Drawing.Size(225, 34);
            this.dateTimeSalida.TabIndex = 57;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(343, 130);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 24);
            this.label10.TabIndex = 55;
            this.label10.Text = "Fecha de Salida";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(837, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 24);
            this.label5.TabIndex = 52;
            this.label5.Text = "Costo Unitario (C$)";
            // 
            // label9
            // 
            this.label9.AllowDrop = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(343, 215);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 24);
            this.label9.TabIndex = 17;
            this.label9.Text = "Descripcion";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1101, 64);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "Cantidad";
            // 
            // Cb_Producto
            // 
            this.Cb_Producto.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cb_Producto.FormattingEnabled = true;
            this.Cb_Producto.Location = new System.Drawing.Point(347, 91);
            this.Cb_Producto.Margin = new System.Windows.Forms.Padding(4);
            this.Cb_Producto.Name = "Cb_Producto";
            this.Cb_Producto.Size = new System.Drawing.Size(225, 35);
            this.Cb_Producto.TabIndex = 13;
            this.Cb_Producto.SelectedIndexChanged += new System.EventHandler(this.Cb_Producto_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(342, 64);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(198, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Nombre del Producto";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(576, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Categorìa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(836, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Gestion de Salidas";
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescripcion.Location = new System.Drawing.Point(342, 243);
            this.txtdescripcion.Margin = new System.Windows.Forms.Padding(4);
            this.txtdescripcion.Multiline = true;
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.Size = new System.Drawing.Size(302, 32);
            this.txtdescripcion.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 379);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // TxtCategoria
            // 
            this.TxtCategoria.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCategoria.Location = new System.Drawing.Point(580, 94);
            this.TxtCategoria.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCategoria.Multiline = true;
            this.TxtCategoria.Name = "TxtCategoria";
            this.TxtCategoria.Size = new System.Drawing.Size(240, 32);
            this.TxtCategoria.TabIndex = 143;
            // 
            // TxtUnidaddemedida
            // 
            this.TxtUnidaddemedida.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUnidaddemedida.Location = new System.Drawing.Point(1249, 94);
            this.TxtUnidaddemedida.Margin = new System.Windows.Forms.Padding(4);
            this.TxtUnidaddemedida.Multiline = true;
            this.TxtUnidaddemedida.Name = "TxtUnidaddemedida";
            this.TxtUnidaddemedida.Size = new System.Drawing.Size(240, 32);
            this.TxtUnidaddemedida.TabIndex = 144;
            // 
            // FRMSALIDA1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1717, 847);
            this.Controls.Add(this.lbtotalfactura);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.TxtTotalPagina);
            this.Controls.Add(this.DatagreedSalidas);
            this.Controls.Add(this.TxtPagina);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.TxtBuscar);
            this.Controls.Add(this.BtnSiguiente);
            this.Controls.Add(this.CmbBuscar);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.BtnAnterior);
            this.Controls.Add(this.BTEditar);
            this.Controls.Add(this.BTEliminar);
            this.Controls.Add(this.panel1);
            this.Name = "FRMSALIDA1";
            this.Text = "FRMSALIDA1";
            this.Load += new System.EventHandler(this.FRMSALIDA1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatagreedSalidas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btañadirunidaddemedida)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbtotalfactura;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TxtTotalPagina;
        private System.Windows.Forms.DataGridView DatagreedSalidas;
        private System.Windows.Forms.TextBox TxtPagina;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox TxtBuscar;
        private FontAwesome.Sharp.IconButton BtnSiguiente;
        private System.Windows.Forms.ComboBox CmbBuscar;
        private System.Windows.Forms.Label label14;
        private FontAwesome.Sharp.IconButton BtnAnterior;
        private FontAwesome.Sharp.IconButton BTAgregar;
        private FontAwesome.Sharp.IconButton BTCancelar;
        private FontAwesome.Sharp.IconButton BTEditar;
        private FontAwesome.Sharp.IconButton BTEliminar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Cb_Ciclo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cblote;
        private System.Windows.Forms.TextBox TxtCostoUnitario;
        private System.Windows.Forms.TextBox TxtCantidad;
        private System.Windows.Forms.PictureBox btañadirunidaddemedida;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimeSalida;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Cb_Producto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdescripcion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TxtCategoria;
        private System.Windows.Forms.TextBox TxtUnidaddemedida;
    }
}