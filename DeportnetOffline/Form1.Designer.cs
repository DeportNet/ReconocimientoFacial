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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            botonAltaLegajos = new Button();
            botonCobros = new Button();
            botonAccesos = new Button();
            botonSocios = new Button();
            panelContenido = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
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
            panel1.Controls.Add(tableLayoutPanel2);
            panel1.Location = new Point(0, 692);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1350, 37);
            panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.Controls.Add(botonAltaLegajos, 3, 0);
            tableLayoutPanel2.Controls.Add(botonCobros, 2, 0);
            tableLayoutPanel2.Controls.Add(botonAccesos, 1, 0);
            tableLayoutPanel2.Controls.Add(botonSocios, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1350, 37);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // botonAltaLegajos
            // 
            botonAltaLegajos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            botonAltaLegajos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonAltaLegajos.BackColor = SystemColors.ButtonFace;
            botonAltaLegajos.Location = new Point(1011, 0);
            botonAltaLegajos.Margin = new Padding(0);
            botonAltaLegajos.Name = "botonAltaLegajos";
            botonAltaLegajos.Size = new Size(339, 37);
            botonAltaLegajos.TabIndex = 4;
            botonAltaLegajos.Text = "Ver alta de legajos";
            botonAltaLegajos.UseVisualStyleBackColor = false;
            botonAltaLegajos.Click += botonAltaLegajos_Click;
            // 
            // botonCobros
            // 
            botonCobros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            botonCobros.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonCobros.BackColor = SystemColors.ButtonFace;
            botonCobros.Location = new Point(674, 0);
            botonCobros.Margin = new Padding(0);
            botonCobros.Name = "botonCobros";
            botonCobros.Size = new Size(337, 37);
            botonCobros.TabIndex = 3;
            botonCobros.Text = "Ver cobros offline";
            botonCobros.UseVisualStyleBackColor = false;
            botonCobros.Click += botonCobros_Click;
            // 
            // botonAccesos
            // 
            botonAccesos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            botonAccesos.BackColor = SystemColors.ButtonFace;
            botonAccesos.Location = new Point(337, 0);
            botonAccesos.Margin = new Padding(0);
            botonAccesos.Name = "botonAccesos";
            botonAccesos.Size = new Size(337, 37);
            botonAccesos.TabIndex = 2;
            botonAccesos.Text = "Ver accesos offline";
            botonAccesos.UseVisualStyleBackColor = false;
            botonAccesos.Click += botonAccesos_Click;
            // 
            // botonSocios
            // 
            botonSocios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            botonSocios.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonSocios.BackColor = SystemColors.ButtonFace;
            botonSocios.Location = new Point(0, 0);
            botonSocios.Margin = new Padding(0);
            botonSocios.Name = "botonSocios";
            botonSocios.Size = new Size(337, 37);
            botonSocios.TabIndex = 1;
            botonSocios.Text = "Busqueda de legajos";
            botonSocios.UseVisualStyleBackColor = false;
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Modo Offline";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
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
        private Button botonAltaLegajos;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
