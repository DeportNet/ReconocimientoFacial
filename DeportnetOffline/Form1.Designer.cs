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
            botonCobros = new Button();
            botonAccesos = new Button();
            botonSocios = new Button();
            pictureBox1 = new PictureBox();
            panelContenido = new Panel();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Controls.Add(panelContenido, 1, 0);
            tableLayoutPanel1.Location = new Point(2, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1344, 728);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BackColor = Color.Red;
            panel1.Controls.Add(botonCobros);
            panel1.Controls.Add(botonAccesos);
            panel1.Controls.Add(botonSocios);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(235, 722);
            panel1.TabIndex = 0;
            // 
            // botonCobros
            // 
            botonCobros.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            botonCobros.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonCobros.Location = new Point(-3, 298);
            botonCobros.Name = "botonCobros";
            botonCobros.Size = new Size(238, 60);
            botonCobros.TabIndex = 3;
            botonCobros.Text = "Cobros";
            botonCobros.UseVisualStyleBackColor = true;
            botonCobros.Click += botonCobros_Click;
            // 
            // botonAccesos
            // 
            botonAccesos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            botonAccesos.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            botonAccesos.Location = new Point(0, 232);
            botonAccesos.Name = "botonAccesos";
            botonAccesos.Size = new Size(232, 60);
            botonAccesos.TabIndex = 2;
            botonAccesos.Text = "Accesos";
            botonAccesos.UseVisualStyleBackColor = true;
            botonAccesos.Click += botonAccesos_Click;
            // 
            // botonSocios
            // 
            botonSocios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            botonSocios.Location = new Point(0, 168);
            botonSocios.Name = "botonSocios";
            botonSocios.Size = new Size(235, 60);
            botonSocios.TabIndex = 1;
            botonSocios.Text = "Socios";
            botonSocios.UseVisualStyleBackColor = true;
            botonSocios.Click += botonSocios_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(232, 104);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelContenido
            // 
            panelContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelContenido.AutoSize = true;
            panelContenido.BackColor = Color.Blue;
            panelContenido.Location = new Point(244, 3);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(1097, 722);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button botonSocios;
        private PictureBox pictureBox1;
        private Panel panelContenido;
        private Button botonCobros;
        private Button botonAccesos;
    }
}
