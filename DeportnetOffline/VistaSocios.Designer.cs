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
            columnaAciones = new DataGridViewButtonColumn();
            splitContainer1 = new SplitContainer();
            label1 = new Label();
            button1 = new Button();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            labelCantPaginas = new Label();
            botonAntPaginacion = new Button();
            botonSgtPaginacion = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { columnaNombreYApellido, ColumnaNroTarjeta, ColumnaDni, ColumnaEmail, ColumnaEdad, ColumnaSexo, ColumnaCategoria, ColumnaEstado, columnaAciones });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.Location = new Point(0, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(1094, 540);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // columnaNombreYApellido
            // 
            columnaNombreYApellido.HeaderText = "Nombre y Apellido";
            columnaNombreYApellido.Name = "columnaNombreYApellido";
            columnaNombreYApellido.ReadOnly = true;
            columnaNombreYApellido.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaNroTarjeta
            // 
            ColumnaNroTarjeta.HeaderText = "Nro. Tarjeta";
            ColumnaNroTarjeta.Name = "ColumnaNroTarjeta";
            ColumnaNroTarjeta.ReadOnly = true;
            ColumnaNroTarjeta.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaDni
            // 
            ColumnaDni.HeaderText = "DNI";
            ColumnaDni.Name = "ColumnaDni";
            ColumnaDni.ReadOnly = true;
            ColumnaDni.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaEmail
            // 
            ColumnaEmail.HeaderText = "Email";
            ColumnaEmail.Name = "ColumnaEmail";
            ColumnaEmail.ReadOnly = true;
            ColumnaEmail.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaEdad
            // 
            ColumnaEdad.HeaderText = "Edad";
            ColumnaEdad.Name = "ColumnaEdad";
            ColumnaEdad.ReadOnly = true;
            ColumnaEdad.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaSexo
            // 
            ColumnaSexo.HeaderText = "Sexo";
            ColumnaSexo.Name = "ColumnaSexo";
            ColumnaSexo.ReadOnly = true;
            ColumnaSexo.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaCategoria
            // 
            ColumnaCategoria.HeaderText = "Categoria";
            ColumnaCategoria.Name = "ColumnaCategoria";
            ColumnaCategoria.ReadOnly = true;
            ColumnaCategoria.Resizable = DataGridViewTriState.False;
            // 
            // ColumnaEstado
            // 
            ColumnaEstado.HeaderText = "Estado";
            ColumnaEstado.Name = "ColumnaEstado";
            ColumnaEstado.ReadOnly = true;
            ColumnaEstado.Resizable = DataGridViewTriState.False;
            // 
            // columnaAciones
            // 
            columnaAciones.HeaderText = "Acciones";
            columnaAciones.Name = "columnaAciones";
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(textBox3);
            splitContainer1.Panel1.Controls.Add(textBox2);
            splitContainer1.Panel1.Controls.Add(textBox1);
            splitContainer1.Panel1.Controls.Add(comboBox2);
            splitContainer1.Panel1.Controls.Add(comboBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(labelCantPaginas);
            splitContainer1.Panel2.Controls.Add(botonAntPaginacion);
            splitContainer1.Panel2.Controls.Add(botonSgtPaginacion);
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Panel2.Paint += splitContainer1_Panel2_Paint;
            splitContainer1.Size = new Size(1094, 716);
            splitContainer1.SplitterDistance = 128;
            splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 11);
            label1.Name = "label1";
            label1.Size = new Size(262, 37);
            label1.TabIndex = 6;
            label1.Text = "Busqueda de legajos";
            // 
            // button1
            // 
            button1.Location = new Point(519, 90);
            button1.Name = "button1";
            button1.Size = new Size(83, 35);
            button1.TabIndex = 5;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox3.Location = new Point(284, 95);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(206, 25);
            textBox3.TabIndex = 4;
            textBox3.Text = "Apellido y Nombre";
            textBox3.Enter += textBox3_Enter;
            textBox3.Leave += textBox3_Leave;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(23, 95);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(217, 25);
            textBox2.TabIndex = 3;
            textBox2.Text = "Email";
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(456, 54);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(146, 25);
            textBox1.TabIndex = 2;
            textBox1.Text = "Nro tarjeta o DNI";
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Actívos e inactivos", "Solo activos", "Solo inactivos" });
            comboBox2.Location = new Point(284, 54);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(131, 25);
            comboBox2.Sorted = true;
            comboBox2.TabIndex = 1;
            comboBox2.SelectedIndex = 0;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(23, 54);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(200, 25);
            comboBox1.TabIndex = 0;
            comboBox1.Enter += ComboBox1_Enter;
            comboBox1.Leave += ComboBox1_Leave;
            // 
            // labelCantPaginas
            // 
            labelCantPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelCantPaginas.AutoSize = true;
            labelCantPaginas.Location = new Point(951, 553);
            labelCantPaginas.Name = "labelCantPaginas";
            labelCantPaginas.Size = new Size(37, 15);
            labelCantPaginas.TabIndex = 3;
            labelCantPaginas.Text = "------";
            // 
            // botonAntPaginacion
            // 
            botonAntPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAntPaginacion.Location = new Point(909, 549);
            botonAntPaginacion.Name = "botonAntPaginacion";
            botonAntPaginacion.Size = new Size(36, 23);
            botonAntPaginacion.TabIndex = 2;
            botonAntPaginacion.Text = "<--";
            botonAntPaginacion.UseVisualStyleBackColor = true;
            // 
            // botonSgtPaginacion
            // 
            botonSgtPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonSgtPaginacion.Location = new Point(1043, 549);
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
            Controls.Add(splitContainer1);
            Name = "VistaSocios";
            Size = new Size(1100, 722);
            Load += VistaSocios_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private SplitContainer splitContainer1;
        private Label labelCantPaginas;
        private Button botonAntPaginacion;
        private Button botonSgtPaginacion;
        private DataGridViewTextBoxColumn columnaNombreYApellido;
        private DataGridViewTextBoxColumn ColumnaNroTarjeta;
        private DataGridViewTextBoxColumn ColumnaDni;
        private DataGridViewTextBoxColumn ColumnaEmail;
        private DataGridViewTextBoxColumn ColumnaEdad;
        private DataGridViewTextBoxColumn ColumnaSexo;
        private DataGridViewTextBoxColumn ColumnaCategoria;
        private DataGridViewTextBoxColumn ColumnaEstado;
        private DataGridViewButtonColumn columnaAciones;
        private ComboBox comboBox2;
        internal ComboBox comboBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button1;
        private Label label1;
    }
}
