namespace DeportNetReconocimiento.GUI
{
    partial class WFPrincipal
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
            pictureBox1 = new PictureBox();
            Apellido = new Label();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            apellidoText = new Label();
            nombreText = new Label();
            clasesRestaintesText = new Label();
            mensajeText = new Label();
            CuadroFotoCliente = new PictureBox();
            actividadText = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CuadroFotoCliente).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo_deportnet_1;
            pictureBox1.Location = new Point(276, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(237, 71);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Apellido
            // 
            Apellido.AutoSize = true;
            Apellido.Location = new Point(304, 252);
            Apellido.Name = "Apellido";
            Apellido.Size = new Size(57, 15);
            Apellido.TabIndex = 1;
            Apellido.Text = "Apellido: ";
            Apellido.Click += apellido_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(304, 281);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 2;
            label2.Text = "Nombre:";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(304, 311);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Actividad:";
            label1.Click += label1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(304, 337);
            label3.Name = "label3";
            label3.Size = new Size(96, 15);
            label3.TabIndex = 4;
            label3.Text = "Clases Restantes:";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(304, 363);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 5;
            label4.Text = "Mensaje:";
            label4.Click += label4_Click;
            // 
            // apellidoText
            // 
            apellidoText.AutoSize = true;
            apellidoText.Location = new Point(437, 252);
            apellidoText.Name = "apellidoText";
            apellidoText.Size = new Size(49, 15);
            apellidoText.TabIndex = 6;
            apellidoText.Text = "apellido";
            apellidoText.Click += label5_Click;
            // 
            // nombreText
            // 
            nombreText.AutoSize = true;
            nombreText.Location = new Point(437, 281);
            nombreText.Name = "nombreText";
            nombreText.Size = new Size(49, 15);
            nombreText.TabIndex = 7;
            nombreText.Text = "nombre";
            nombreText.Click += nombreText_Click;
            // 
            // clasesRestaintesText
            // 
            clasesRestaintesText.AutoSize = true;
            clasesRestaintesText.Location = new Point(437, 337);
            clasesRestaintesText.Name = "clasesRestaintesText";
            clasesRestaintesText.Size = new Size(88, 15);
            clasesRestaintesText.TabIndex = 9;
            clasesRestaintesText.Text = "clases restantes";
            clasesRestaintesText.Click += this.clasesRestaintesText_Click;
            // 
            // mensajeText
            // 
            mensajeText.AutoSize = true;
            mensajeText.Location = new Point(437, 363);
            mensajeText.Name = "mensajeText";
            mensajeText.Size = new Size(51, 15);
            mensajeText.TabIndex = 10;
            mensajeText.Text = "mensaje";
            // 
            // CuadroFotoCliente
            // 
            CuadroFotoCliente.Location = new Point(345, 89);
            CuadroFotoCliente.Name = "CuadroFotoCliente";
            CuadroFotoCliente.Size = new Size(110, 129);
            CuadroFotoCliente.TabIndex = 11;
            CuadroFotoCliente.TabStop = false;
            CuadroFotoCliente.Click += pictureBox2_Click;
            // 
            // actividadText
            // 
            actividadText.AutoSize = true;
            actividadText.Location = new Point(437, 311);
            actividadText.Name = "actividadText";
            actividadText.Size = new Size(55, 15);
            actividadText.TabIndex = 12;
            actividadText.Text = "actividad";
            actividadText.Click += actividadText_Click;
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(800, 450);
            Controls.Add(actividadText);
            Controls.Add(CuadroFotoCliente);
            Controls.Add(mensajeText);
            Controls.Add(clasesRestaintesText);
            Controls.Add(nombreText);
            Controls.Add(apellidoText);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(Apellido);
            Controls.Add(pictureBox1);
            Name = "WFPrincipal";
            Text = "Menu Principal Reconocimiento";
            Load += WFPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)CuadroFotoCliente).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label Apellido;
        private Label label2;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label apellidoText;
        private Label nombreText;
        private Label label8;
        private Label mensajeText;
        private PictureBox CuadroFotoCliente;
        private Label actividadText;
        private Label clasesRestaintesText;
    }
}