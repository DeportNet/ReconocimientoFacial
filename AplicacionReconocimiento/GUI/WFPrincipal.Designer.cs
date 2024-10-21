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
            imagenDeportnet = new PictureBox();
            pictureBox1 = new PictureBox();
            ApellidoLabel = new Label();
            NombreLabel = new Label();
            ActividadLabel = new Label();
            ClasesRestantesLabel = new Label();
            MensajeLabel = new Label();
            ValorApellidoLabel = new Label();
            valorNombreLabel = new Label();
            ValorActividadLabel = new Label();
            ValorClasesRestantesLabel = new Label();
            ValorMensajeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // imagenDeportnet
            // 
            imagenDeportnet.Image = Properties.Resources.logo_deportnet_1;
            imagenDeportnet.Location = new Point(230, 30);
            imagenDeportnet.Name = "imagenDeportnet";
            imagenDeportnet.Size = new Size(370, 70);
            imagenDeportnet.SizeMode = PictureBoxSizeMode.StretchImage;
            imagenDeportnet.TabIndex = 1;
            imagenDeportnet.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.Location = new Point(76, 122);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(185, 200);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // ApellidoLabel
            // 
            ApellidoLabel.AutoSize = true;
            ApellidoLabel.ForeColor = SystemColors.Info;
            ApellidoLabel.Location = new Point(344, 142);
            ApellidoLabel.Name = "ApellidoLabel";
            ApellidoLabel.Size = new Size(54, 15);
            ApellidoLabel.TabIndex = 12;
            ApellidoLabel.Text = "Apellido:";
            ApellidoLabel.Click += ApellidoLabel_Click;
            // 
            // NombreLabel
            // 
            NombreLabel.AutoSize = true;
            NombreLabel.ForeColor = SystemColors.Info;
            NombreLabel.Location = new Point(344, 182);
            NombreLabel.Name = "NombreLabel";
            NombreLabel.Size = new Size(54, 15);
            NombreLabel.TabIndex = 13;
            NombreLabel.Text = "Nombre:";
            NombreLabel.Click += NombreLabel_Click;
            // 
            // ActividadLabel
            // 
            ActividadLabel.AutoSize = true;
            ActividadLabel.ForeColor = SystemColors.Info;
            ActividadLabel.Location = new Point(344, 223);
            ActividadLabel.Name = "ActividadLabel";
            ActividadLabel.Size = new Size(60, 15);
            ActividadLabel.TabIndex = 14;
            ActividadLabel.Text = "Actividad:";
            ActividadLabel.Click += ActividadLabel_Click;
            // 
            // ClasesRestantesLabel
            // 
            ClasesRestantesLabel.AutoSize = true;
            ClasesRestantesLabel.ForeColor = SystemColors.Info;
            ClasesRestantesLabel.Location = new Point(311, 262);
            ClasesRestantesLabel.Name = "ClasesRestantesLabel";
            ClasesRestantesLabel.Size = new Size(93, 15);
            ClasesRestantesLabel.TabIndex = 15;
            ClasesRestantesLabel.Text = "Clases restantes:";
            ClasesRestantesLabel.Click += ClasesRestantesLabel_Click;
            // 
            // MensajeLabel
            // 
            MensajeLabel.AutoSize = true;
            MensajeLabel.ForeColor = SystemColors.Info;
            MensajeLabel.Location = new Point(344, 307);
            MensajeLabel.Name = "MensajeLabel";
            MensajeLabel.Size = new Size(54, 15);
            MensajeLabel.TabIndex = 16;
            MensajeLabel.Text = "Mensaje:";
            // 
            // ValorApellidoLabel
            // 
            ValorApellidoLabel.AutoSize = true;
            ValorApellidoLabel.ForeColor = SystemColors.Info;
            ValorApellidoLabel.Location = new Point(432, 142);
            ValorApellidoLabel.Name = "ValorApellidoLabel";
            ValorApellidoLabel.Size = new Size(88, 15);
            ValorApellidoLabel.TabIndex = 17;
            ValorApellidoLabel.Text = "Apellido Aqui...";
            // 
            // valorNombreLabel
            // 
            valorNombreLabel.AutoSize = true;
            valorNombreLabel.ForeColor = SystemColors.Info;
            valorNombreLabel.Location = new Point(432, 182);
            valorNombreLabel.Name = "valorNombreLabel";
            valorNombreLabel.Size = new Size(88, 15);
            valorNombreLabel.TabIndex = 18;
            valorNombreLabel.Text = "Nombre Aqui...";
            // 
            // ValorActividadLabel
            // 
            ValorActividadLabel.AutoSize = true;
            ValorActividadLabel.ForeColor = SystemColors.Info;
            ValorActividadLabel.Location = new Point(432, 223);
            ValorActividadLabel.Name = "ValorActividadLabel";
            ValorActividadLabel.Size = new Size(94, 15);
            ValorActividadLabel.TabIndex = 19;
            ValorActividadLabel.Text = "Actividad Aqui...";
            // 
            // ValorClasesRestantesLabel
            // 
            ValorClasesRestantesLabel.AutoSize = true;
            ValorClasesRestantesLabel.ForeColor = SystemColors.Info;
            ValorClasesRestantesLabel.Location = new Point(432, 262);
            ValorClasesRestantesLabel.Name = "ValorClasesRestantesLabel";
            ValorClasesRestantesLabel.Size = new Size(130, 15);
            ValorClasesRestantesLabel.TabIndex = 20;
            ValorClasesRestantesLabel.Text = "Clases Restantes Aqui...";
            // 
            // ValorMensajeLabel
            // 
            ValorMensajeLabel.AutoSize = true;
            ValorMensajeLabel.ForeColor = SystemColors.Info;
            ValorMensajeLabel.Location = new Point(432, 307);
            ValorMensajeLabel.Name = "ValorMensajeLabel";
            ValorMensajeLabel.Size = new Size(88, 15);
            ValorMensajeLabel.TabIndex = 21;
            ValorMensajeLabel.Text = "Mensaje Aqui...";
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(800, 450);
            Controls.Add(ValorMensajeLabel);
            Controls.Add(ValorClasesRestantesLabel);
            Controls.Add(ValorActividadLabel);
            Controls.Add(valorNombreLabel);
            Controls.Add(ValorApellidoLabel);
            Controls.Add(MensajeLabel);
            Controls.Add(ClasesRestantesLabel);
            Controls.Add(ActividadLabel);
            Controls.Add(NombreLabel);
            Controls.Add(ApellidoLabel);
            Controls.Add(imagenDeportnet);
            Controls.Add(pictureBox1);
            Name = "WFPrincipal";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imagenDeportnet;
        private PictureBox pictureBox1;
        private Label ApellidoLabel;
        private Label NombreLabel;
        private Label ActividadLabel;
        private Label ClasesRestantesLabel;
        private Label MensajeLabel;
        private Label ValorApellidoLabel;
        private Label valorNombreLabel;
        private Label ValorActividadLabel;
        private Label ValorClasesRestantesLabel;
        private Label ValorMensajeLabel;
    }
}