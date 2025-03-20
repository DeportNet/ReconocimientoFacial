namespace DeportnetOffline
{
    partial class VistaSocios
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            columnaNombreYApellido = new DataGridViewTextBoxColumn();
            ColumnaNroTarjeta = new DataGridViewTextBoxColumn();
            ColumnaDni = new DataGridViewTextBoxColumn();
            ColumnaEmail = new DataGridViewTextBoxColumn();
            ColumnaEdad = new DataGridViewTextBoxColumn();
            ColumnaSexo = new DataGridViewTextBoxColumn();
            ColumnaCategoria = new DataGridViewTextBoxColumn();
            ColumnaEstado = new DataGridViewTextBoxColumn();
            ColumnaCobro = new DataGridViewButtonColumn();
            ColumnaVenta = new DataGridViewButtonColumn();
            label1 = new Label();
            button1 = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            panel3 = new Panel();
            button2 = new Button();
            panel2 = new Panel();
            labelCantPaginas = new Label();
            botonAntPaginacion = new Button();
            botonSgtPaginacion = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { columnaNombreYApellido, ColumnaNroTarjeta, ColumnaDni, ColumnaEmail, ColumnaEdad, ColumnaSexo, ColumnaCategoria, ColumnaEstado, ColumnaCobro, ColumnaVenta });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Location = new Point(17, 3);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(1131, 504);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.Paint += DataGridView1_Paint;
            // 
            // columnaNombreYApellido
            // 
            columnaNombreYApellido.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            columnaNombreYApellido.FillWeight = 69.0842F;
            columnaNombreYApellido.HeaderText = "Nombre y Apellido";
            columnaNombreYApellido.Name = "columnaNombreYApellido";
            columnaNombreYApellido.ReadOnly = true;
            columnaNombreYApellido.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaNroTarjeta
            // 
            ColumnaNroTarjeta.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnaNroTarjeta.FillWeight = 69.0842F;
            ColumnaNroTarjeta.HeaderText = "Nro. Tarjeta";
            ColumnaNroTarjeta.Name = "ColumnaNroTarjeta";
            ColumnaNroTarjeta.ReadOnly = true;
            ColumnaNroTarjeta.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaDni
            // 
            ColumnaDni.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnaDni.FillWeight = 69.0842F;
            ColumnaDni.HeaderText = "DNI";
            ColumnaDni.Name = "ColumnaDni";
            ColumnaDni.ReadOnly = true;
            ColumnaDni.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaEmail
            // 
            ColumnaEmail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnaEmail.FillWeight = 69.0842F;
            ColumnaEmail.HeaderText = "Email";
            ColumnaEmail.Name = "ColumnaEmail";
            ColumnaEmail.ReadOnly = true;
            ColumnaEmail.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaEdad
            // 
            ColumnaEdad.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaEdad.FillWeight = 187.9842F;
            ColumnaEdad.HeaderText = "Edad";
            ColumnaEdad.Name = "ColumnaEdad";
            ColumnaEdad.ReadOnly = true;
            ColumnaEdad.Resizable = DataGridViewTriState.False;
            ColumnaEdad.Width = 80;
            // 
            // ColumnaSexo
            // 
            ColumnaSexo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaSexo.FillWeight = 228.426422F;
            ColumnaSexo.HeaderText = "Sexo";
            ColumnaSexo.Name = "ColumnaSexo";
            ColumnaSexo.ReadOnly = true;
            ColumnaSexo.Resizable = DataGridViewTriState.False;
            ColumnaSexo.Width = 80;
            // 
            // ColumnaCategoria
            // 
            ColumnaCategoria.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaCategoria.FillWeight = 69.0842F;
            ColumnaCategoria.HeaderText = "Categoria";
            ColumnaCategoria.Name = "ColumnaCategoria";
            ColumnaCategoria.ReadOnly = true;
            ColumnaCategoria.Resizable = DataGridViewTriState.False;
            ColumnaCategoria.Width = 110;
            // 
            // ColumnaEstado
            // 
            ColumnaEstado.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaEstado.FillWeight = 69.0842F;
            ColumnaEstado.HeaderText = "Estado";
            ColumnaEstado.Name = "ColumnaEstado";
            ColumnaEstado.ReadOnly = true;
            ColumnaEstado.Resizable = DataGridViewTriState.False;
            ColumnaEstado.Width = 148;
            // 
            // ColumnaCobro
            // 
            ColumnaCobro.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaCobro.FillWeight = 69.0842F;
            ColumnaCobro.HeaderText = "Cobrar";
            ColumnaCobro.Name = "ColumnaCobro";
            ColumnaCobro.ReadOnly = true;
            ColumnaCobro.Resizable = DataGridViewTriState.False;
            ColumnaCobro.Text = "Cobrar";
            ColumnaCobro.UseColumnTextForButtonValue = true;
            ColumnaCobro.Width = 60;
            // 
            // ColumnaVenta
            // 
            ColumnaVenta.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ColumnaVenta.HeaderText = "Vender";
            ColumnaVenta.Name = "ColumnaVenta";
            ColumnaVenta.ReadOnly = true;
            ColumnaVenta.Resizable = DataGridViewTriState.False;
            ColumnaVenta.Text = "Vender";
            ColumnaVenta.UseColumnTextForButtonValue = true;
            ColumnaVenta.Width = 60;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 8);
            label1.Name = "label1";
            label1.Size = new Size(262, 37);
            label1.TabIndex = 6;
            label1.Text = "Busqueda de legajos";
            // 
            // button1
            // 
            button1.Location = new Point(592, 82);
            button1.Name = "button1";
            button1.Size = new Size(83, 35);
            button1.TabIndex = 5;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(11, 89);
            textBox3.MaxLength = 100;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(278, 25);
            textBox3.TabIndex = 4;
            textBox3.Text = "Apellido y Nombre";
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(308, 88);
            textBox2.MaxLength = 100;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(258, 25);
            textBox2.TabIndex = 3;
            textBox2.Text = "Email";
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(420, 53);
            textBox1.MaxLength = 11;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(146, 25);
            textBox1.TabIndex = 2;
            textBox1.Text = "Nro tarjeta o DNI";
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Actívos e inactivos", "Solo activos", "Solo inactivos" });
            comboBox2.Location = new Point(253, 53);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(131, 25);
            comboBox2.Sorted = true;
            comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(11, 53);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 25);
            comboBox1.TabIndex = 3;
            comboBox1.Enter += ComboBox1_Enter;
            comboBox1.Leave += ComboBox1_Leave;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1160, 706);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.WhiteSmoke;
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1160, 150);
            panel1.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Gainsboro;
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(comboBox1);
            panel3.Controls.Add(textBox3);
            panel3.Controls.Add(textBox1);
            panel3.Controls.Add(textBox2);
            panel3.Controls.Add(comboBox2);
            panel3.Location = new Point(17, 11);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(957, 130);
            panel3.TabIndex = 8;
            // 
            // button2
            // 
            button2.Location = new Point(708, 83);
            button2.Name = "button2";
            button2.Size = new Size(160, 33);
            button2.TabIndex = 7;
            button2.Text = "Nuevo legajo";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.AutoSize = true;
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(labelCantPaginas);
            panel2.Controls.Add(dataGridView1);
            panel2.Controls.Add(botonAntPaginacion);
            panel2.Controls.Add(botonSgtPaginacion);
            panel2.Location = new Point(0, 150);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1160, 556);
            panel2.TabIndex = 2;
            // 
            // labelCantPaginas
            // 
            labelCantPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelCantPaginas.AutoSize = true;
            labelCantPaginas.Location = new Point(1018, 517);
            labelCantPaginas.Name = "labelCantPaginas";
            labelCantPaginas.Size = new Size(37, 15);
            labelCantPaginas.TabIndex = 3;
            labelCantPaginas.Text = "------";
            // 
            // botonAntPaginacion
            // 
            botonAntPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAntPaginacion.Location = new Point(976, 513);
            botonAntPaginacion.Name = "botonAntPaginacion";
            botonAntPaginacion.Size = new Size(36, 23);
            botonAntPaginacion.TabIndex = 2;
            botonAntPaginacion.Text = "<--";
            botonAntPaginacion.UseVisualStyleBackColor = true;
            // 
            // botonSgtPaginacion
            // 
            botonSgtPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonSgtPaginacion.Location = new Point(1110, 513);
            botonSgtPaginacion.Name = "botonSgtPaginacion";
            botonSgtPaginacion.Size = new Size(38, 23);
            botonSgtPaginacion.TabIndex = 1;
            botonSgtPaginacion.Text = "-->";
            botonSgtPaginacion.UseVisualStyleBackColor = true;
            // 
            // VistaSocios
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.ActiveCaption;
            Controls.Add(tableLayoutPanel1);
            Name = "VistaSocios";
            Size = new Size(1160, 706);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox comboBox2;
        internal ComboBox comboBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Button botonSgtPaginacion;
        private Button botonAntPaginacion;
        private Label labelCantPaginas;
        private Button button2;
        private Panel panel3;
        private DataGridViewTextBoxColumn columnaNombreYApellido;
        private DataGridViewTextBoxColumn ColumnaNroTarjeta;
        private DataGridViewTextBoxColumn ColumnaDni;
        private DataGridViewTextBoxColumn ColumnaEmail;
        private DataGridViewTextBoxColumn ColumnaEdad;
        private DataGridViewTextBoxColumn ColumnaSexo;
        private DataGridViewTextBoxColumn ColumnaCategoria;
        private DataGridViewTextBoxColumn ColumnaEstado;
        private DataGridViewButtonColumn ColumnaCobro;
        private DataGridViewButtonColumn ColumnaVenta;
    }
}
