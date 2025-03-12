namespace DeportnetOffline
{
    partial class vistaAltaLegajos
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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            labelCantPaginas = new Label();
            botonAntPaginacion = new Button();
            botonSgtPaginacion = new Button();
            dataGridView1 = new DataGridView();
            columnaNombreApellido = new DataGridViewTextBoxColumn();
            columnaNroTarjeta = new DataGridViewTextBoxColumn();
            columnaDNI = new DataGridViewTextBoxColumn();
            columnaEmail = new DataGridViewTextBoxColumn();
            columnaSexo = new DataGridViewTextBoxColumn();
            columnaEdad = new DataGridViewTextBoxColumn();
            columnaEstado = new DataGridViewTextBoxColumn();
            columnaSincronziado = new DataGridViewTextBoxColumn();
            columnaFechaHoraSincro = new DataGridViewTextBoxColumn();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1100, 700);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1094, 114);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI Semibold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(1094, 114);
            label1.TabIndex = 0;
            label1.Text = "Alta de legajos offline";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(labelCantPaginas);
            panel2.Controls.Add(botonAntPaginacion);
            panel2.Controls.Add(botonSgtPaginacion);
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 123);
            panel2.Name = "panel2";
            panel2.Size = new Size(1094, 574);
            panel2.TabIndex = 1;
            // 
            // labelCantPaginas
            // 
            labelCantPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelCantPaginas.AutoSize = true;
            labelCantPaginas.Location = new Point(897, 513);
            labelCantPaginas.Name = "labelCantPaginas";
            labelCantPaginas.Size = new Size(37, 15);
            labelCantPaginas.TabIndex = 9;
            labelCantPaginas.Text = "------";
            // 
            // botonAntPaginacion
            // 
            botonAntPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAntPaginacion.Location = new Point(855, 509);
            botonAntPaginacion.Name = "botonAntPaginacion";
            botonAntPaginacion.Size = new Size(36, 23);
            botonAntPaginacion.TabIndex = 8;
            botonAntPaginacion.Text = "<--";
            botonAntPaginacion.UseVisualStyleBackColor = true;
            // 
            // botonSgtPaginacion
            // 
            botonSgtPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonSgtPaginacion.Location = new Point(989, 509);
            botonSgtPaginacion.Name = "botonSgtPaginacion";
            botonSgtPaginacion.Size = new Size(38, 23);
            botonSgtPaginacion.TabIndex = 7;
            botonSgtPaginacion.Text = "-->";
            botonSgtPaginacion.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { columnaNombreApellido, columnaNroTarjeta, columnaDNI, columnaEmail, columnaSexo, columnaEdad, columnaEstado, columnaSincronziado, columnaFechaHoraSincro });
            dataGridView1.Location = new Point(43, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(1000, 500);
            dataGridView1.TabIndex = 0;
            // 
            // columnaNombreApellido
            // 
            columnaNombreApellido.HeaderText = "Nombre y Apellido";
            columnaNombreApellido.Name = "columnaNombreApellido";
            columnaNombreApellido.ReadOnly = true;
            // 
            // columnaNroTarjeta
            // 
            columnaNroTarjeta.HeaderText = "Nro. Tarjeta";
            columnaNroTarjeta.Name = "columnaNroTarjeta";
            columnaNroTarjeta.ReadOnly = true;
            // 
            // columnaDNI
            // 
            columnaDNI.HeaderText = "DNI";
            columnaDNI.Name = "columnaDNI";
            columnaDNI.ReadOnly = true;
            // 
            // columnaEmail
            // 
            columnaEmail.HeaderText = "Email";
            columnaEmail.Name = "columnaEmail";
            columnaEmail.ReadOnly = true;
            // 
            // columnaSexo
            // 
            columnaSexo.HeaderText = "Sexo";
            columnaSexo.Name = "columnaSexo";
            columnaSexo.ReadOnly = true;
            // 
            // columnaEdad
            // 
            columnaEdad.HeaderText = "Edad";
            columnaEdad.Name = "columnaEdad";
            columnaEdad.ReadOnly = true;
            // 
            // columnaEstado
            // 
            columnaEstado.HeaderText = "Estado";
            columnaEstado.Name = "columnaEstado";
            columnaEstado.ReadOnly = true;
            // 
            // columnaSincronziado
            // 
            columnaSincronziado.HeaderText = "Sincronizado";
            columnaSincronziado.Name = "columnaSincronziado";
            columnaSincronziado.ReadOnly = true;
            // 
            // columnaFechaHoraSincro
            // 
            columnaFechaHoraSincro.HeaderText = "Fecha - Hora sincro";
            columnaFechaHoraSincro.Name = "columnaFechaHoraSincro";
            columnaFechaHoraSincro.ReadOnly = true;
            // 
            // vistaAltaLegajos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "vistaAltaLegajos";
            Size = new Size(1100, 700);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn columnaNombreApellido;
        private DataGridViewTextBoxColumn columnaNroTarjeta;
        private DataGridViewTextBoxColumn columnaDNI;
        private DataGridViewTextBoxColumn columnaEmail;
        private DataGridViewTextBoxColumn columnaSexo;
        private DataGridViewTextBoxColumn columnaEdad;
        private DataGridViewTextBoxColumn columnaEstado;
        private DataGridViewTextBoxColumn columnaSincronziado;
        private DataGridViewTextBoxColumn columnaFechaHoraSincro;
        private Label labelCantPaginas;
        private Button botonAntPaginacion;
        private Button botonSgtPaginacion;
    }
}
