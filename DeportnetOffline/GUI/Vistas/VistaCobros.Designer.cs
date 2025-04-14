namespace DeportnetOffline
{
    partial class VistaCobros
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
            columnaSocio = new DataGridViewTextBoxColumn();
            columnaServicioArticulo = new DataGridViewTextBoxColumn();
            columnaMonto = new DataGridViewTextBoxColumn();
            columnaFechaHora = new DataGridViewTextBoxColumn();
            columanSincronizado = new DataGridViewTextBoxColumn();
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
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(1106, 706);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1106, 120);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI Semibold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0);
            label1.Margin = new Padding(0);
            label1.Name = "label1";
            label1.Size = new Size(1106, 120);
            label1.TabIndex = 0;
            label1.Text = "Cobros offline";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(labelCantPaginas);
            panel2.Controls.Add(botonAntPaginacion);
            panel2.Controls.Add(botonSgtPaginacion);
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 120);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1106, 586);
            panel2.TabIndex = 1;
            // 
            // labelCantPaginas
            // 
            labelCantPaginas.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelCantPaginas.AutoSize = true;
            labelCantPaginas.Location = new Point(793, 524);
            labelCantPaginas.Name = "labelCantPaginas";
            labelCantPaginas.Size = new Size(37, 15);
            labelCantPaginas.TabIndex = 9;
            labelCantPaginas.Text = "------";
            // 
            // botonAntPaginacion
            // 
            botonAntPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAntPaginacion.Location = new Point(751, 520);
            botonAntPaginacion.Name = "botonAntPaginacion";
            botonAntPaginacion.Size = new Size(36, 23);
            botonAntPaginacion.TabIndex = 8;
            botonAntPaginacion.Text = "<--";
            botonAntPaginacion.UseVisualStyleBackColor = true;
            // 
            // botonSgtPaginacion
            // 
            botonSgtPaginacion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonSgtPaginacion.Location = new Point(885, 520);
            botonSgtPaginacion.Name = "botonSgtPaginacion";
            botonSgtPaginacion.Size = new Size(38, 23);
            botonSgtPaginacion.TabIndex = 7;
            botonSgtPaginacion.Text = "-->";
            botonSgtPaginacion.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { columnaSocio, columnaServicioArticulo, columnaMonto, columnaFechaHora, columanSincronizado, columnaFechaHoraSincro });
            dataGridView1.Location = new Point(154, 3);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Size = new Size(831, 505);
            dataGridView1.TabIndex = 0;
            // 
            // columnaSocio
            // 
            columnaSocio.HeaderText = "Socio";
            columnaSocio.Name = "columnaSocio";
            columnaSocio.ReadOnly = true;
            // 
            // columnaServicioArticulo
            // 
            columnaServicioArticulo.HeaderText = "Servicio / Artículo ";
            columnaServicioArticulo.Name = "columnaServicioArticulo";
            columnaServicioArticulo.ReadOnly = true;
            // 
            // columnaMonto
            // 
            columnaMonto.HeaderText = "Monto";
            columnaMonto.Name = "columnaMonto";
            columnaMonto.ReadOnly = true;
            // 
            // columnaFechaHora
            // 
            columnaFechaHora.HeaderText = "Fecha - Hora";
            columnaFechaHora.Name = "columnaFechaHora";
            columnaFechaHora.ReadOnly = true;
            // 
            // columanSincronizado
            // 
            columanSincronizado.HeaderText = "Sincronizado";
            columanSincronizado.Name = "columanSincronizado";
            columanSincronizado.ReadOnly = true;
            // 
            // columnaFechaHoraSincro
            // 
            columnaFechaHoraSincro.HeaderText = "Fecha - Hora sincro";
            columnaFechaHoraSincro.Name = "columnaFechaHoraSincro";
            columnaFechaHoraSincro.ReadOnly = true;
            // 
            // VisataCobros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(0);
            Name = "VisataCobros";
            Size = new Size(1106, 706);
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
        private DataGridViewTextBoxColumn columnaSocio;
        private DataGridViewTextBoxColumn columnaServicioArticulo;
        private DataGridViewTextBoxColumn columnaMonto;
        private DataGridViewTextBoxColumn columnaFechaHora;
        private DataGridViewTextBoxColumn columanSincronizado;
        private DataGridViewTextBoxColumn columnaFechaHoraSincro;
        private Label labelCantPaginas;
        private Button botonAntPaginacion;
        private Button botonSgtPaginacion;
    }
}
