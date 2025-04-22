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
            ColumnaCobro = new DataGridViewButtonColumn();
            ColumnaVenta = new DataGridViewButtonColumn();
            label1 = new Label();
            button1 = new Button();
            textBoxApellidoNombre = new TextBox();
            textBoxEmail = new TextBox();
            textBoxNroTarjeta = new TextBox();
            comboBoxEstado = new ComboBox();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ColumnaCobro, ColumnaVenta });
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
            button1.Click += button1_Click;
            // 
            // textBoxApellidoNombre
            // 
            textBoxApellidoNombre.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxApellidoNombre.Location = new Point(11, 89);
            textBoxApellidoNombre.MaxLength = 100;
            textBoxApellidoNombre.Name = "textBoxApellidoNombre";
            textBoxApellidoNombre.Size = new Size(278, 25);
            textBoxApellidoNombre.TabIndex = 4;
            textBoxApellidoNombre.Enter += textBox3_Enter;
            textBoxApellidoNombre.Leave += textBox3_Leave;
            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxEmail.Location = new Point(308, 88);
            textBoxEmail.MaxLength = 100;
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(258, 25);
            textBoxEmail.TabIndex = 3;
            textBoxEmail.Enter += textBox2_Enter;
            textBoxEmail.Leave += textBox2_Leave;
            // 
            // textBoxNroTarjeta
            // 
            textBoxNroTarjeta.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNroTarjeta.Location = new Point(180, 54);
            textBoxNroTarjeta.MaxLength = 11;
            textBoxNroTarjeta.Name = "textBoxNroTarjeta";
            textBoxNroTarjeta.Size = new Size(146, 25);
            textBoxNroTarjeta.TabIndex = 2;
            textBoxNroTarjeta.Enter += textBox1_Enter;
            textBoxNroTarjeta.Leave += textBox1_Leave;
            // 
            // comboBoxEstado
            // 
            comboBoxEstado.BackColor = SystemColors.Window;
            comboBoxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEstado.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxEstado.FormattingEnabled = true;
            comboBoxEstado.Items.AddRange(new object[] { "Actívos e inactivos", "Solo activos", "Solo inactivos" });
            comboBoxEstado.Location = new Point(13, 54);
            comboBoxEstado.Name = "comboBoxEstado";
            comboBoxEstado.Size = new Size(131, 25);
            comboBoxEstado.Sorted = true;
            comboBoxEstado.TabIndex = 1;
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
            panel1.BackColor = SystemColors.Window;
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
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(button2);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(button1);
            panel3.Controls.Add(textBoxApellidoNombre);
            panel3.Controls.Add(textBoxNroTarjeta);
            panel3.Controls.Add(textBoxEmail);
            panel3.Controls.Add(comboBoxEstado);
            panel3.Location = new Point(17, 11);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1027, 130);
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
            panel2.BackColor = SystemColors.Window;
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
        private ComboBox comboBoxEstado;
        private TextBox textBoxApellidoNombre;
        private TextBox textBoxEmail;
        private TextBox textBoxNroTarjeta;
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
        private DataGridViewButtonColumn ColumnaCobro;
        private DataGridViewButtonColumn ColumnaVenta;
    }
}
