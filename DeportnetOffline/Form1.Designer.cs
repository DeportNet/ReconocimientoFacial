namespace DeportnetOffline
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            button1 = new Button();
            botonCobros = new Button();
            botonAccesos = new Button();
            botonSocios = new Button();
            panelContenido = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panelContenido, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 95F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1350, 729);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(botonCobros);
            panel1.Controls.Add(botonAccesos);
            panel1.Controls.Add(botonSocios);
            panel1.Location = new Point(0, 692);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1350, 37);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(1001, 3);
            button1.Name = "button1";
            button1.Size = new Size(300, 31);
            button1.TabIndex = 4;
            button1.Text = "Ver alta de legajos";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // botonCobros
            // 
            botonCobros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            botonCobros.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonCobros.Location = new Point(684, 3);
            botonCobros.Name = "botonCobros";
            botonCobros.Size = new Size(300, 31);
            botonCobros.TabIndex = 3;
            botonCobros.Text = "Ver cobros offline";
            botonCobros.UseVisualStyleBackColor = true;
            botonCobros.Click += botonCobros_Click;
            // 
            // botonAccesos
            // 
            botonAccesos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            botonAccesos.Location = new Point(358, 3);
            botonAccesos.Name = "botonAccesos";
            botonAccesos.Size = new Size(300, 31);
            botonAccesos.TabIndex = 2;
            botonAccesos.Text = "Ver accesos offline";
            botonAccesos.UseVisualStyleBackColor = true;
            botonAccesos.Click += botonAccesos_Click;
            // 
            // botonSocios
            // 
            botonSocios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            botonSocios.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonSocios.Location = new Point(38, 3);
            botonSocios.Name = "botonSocios";
            botonSocios.Size = new Size(300, 31);
            botonSocios.TabIndex = 1;
            botonSocios.Text = "Busqueda de legajos";
            botonSocios.UseVisualStyleBackColor = true;
            botonSocios.Click += botonSocios_Click;
            // 
            // panelContenido
            // 
            panelContenido.AutoSize = true;
            panelContenido.BackColor = Color.WhiteSmoke;
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 0);
            panelContenido.Margin = new Padding(0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(1350, 692);
            panelContenido.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1350, 729);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panelContenido;
        private Panel panel1;
        private Button botonCobros;
        private Button botonAccesos;
        private Button botonSocios;
        private Button button1;
    }
}
