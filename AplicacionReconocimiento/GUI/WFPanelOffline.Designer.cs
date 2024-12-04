namespace DeportNetReconocimiento.GUI
{
    partial class WFPanelOffline
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFPanelOffline));
            pictureBox1 = new PictureBox();
            controlPuertasLabel = new Label();
            AbrirPuerta = new Button();
            CerrarPuerta = new Button();
            MantenerAbiertaPuerta = new Button();
            ManterCerradaPuerta = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.logo_deportnet_1;
            pictureBox1.Location = new Point(63, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(962, 185);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // controlPuertasLabel
            // 
            controlPuertasLabel.AutoSize = true;
            controlPuertasLabel.Font = new Font("Segoe UI", 15F, FontStyle.Underline);
            controlPuertasLabel.Location = new Point(381, 269);
            controlPuertasLabel.Name = "controlPuertasLabel";
            controlPuertasLabel.Size = new Size(175, 28);
            controlPuertasLabel.TabIndex = 1;
            controlPuertasLabel.Text = "Control de puertas";
            controlPuertasLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AbrirPuerta
            // 
            AbrirPuerta.Cursor = Cursors.Hand;
            AbrirPuerta.Location = new Point(313, 315);
            AbrirPuerta.Name = "AbrirPuerta";
            AbrirPuerta.Size = new Size(129, 37);
            AbrirPuerta.TabIndex = 2;
            AbrirPuerta.Text = "Abrir";
            AbrirPuerta.UseVisualStyleBackColor = true;
            AbrirPuerta.Click += AbrirPuerta_Click;
            // 
            // CerrarPuerta
            // 
            CerrarPuerta.Cursor = Cursors.Hand;
            CerrarPuerta.Location = new Point(515, 315);
            CerrarPuerta.Name = "CerrarPuerta";
            CerrarPuerta.Size = new Size(129, 37);
            CerrarPuerta.TabIndex = 3;
            CerrarPuerta.Text = "Cerrar";
            CerrarPuerta.UseVisualStyleBackColor = true;
            CerrarPuerta.Click += CerrarPuerta_Click;
            // 
            // MantenerAbiertaPuerta
            // 
            MantenerAbiertaPuerta.Cursor = Cursors.Hand;
            MantenerAbiertaPuerta.Location = new Point(313, 375);
            MantenerAbiertaPuerta.Name = "MantenerAbiertaPuerta";
            MantenerAbiertaPuerta.Size = new Size(129, 37);
            MantenerAbiertaPuerta.TabIndex = 4;
            MantenerAbiertaPuerta.Text = "Mantener Abierta";
            MantenerAbiertaPuerta.UseVisualStyleBackColor = true;
            MantenerAbiertaPuerta.Click += MantenerAbiertaPuerta_Click;
            // 
            // ManterCerradaPuerta
            // 
            ManterCerradaPuerta.Cursor = Cursors.Hand;
            ManterCerradaPuerta.Location = new Point(515, 375);
            ManterCerradaPuerta.Name = "ManterCerradaPuerta";
            ManterCerradaPuerta.Size = new Size(129, 37);
            ManterCerradaPuerta.TabIndex = 5;
            ManterCerradaPuerta.Text = "Mantener Cerrada";
            ManterCerradaPuerta.UseVisualStyleBackColor = true;
            ManterCerradaPuerta.Click += ManterCerradaPuerta_Click;
            // 
            // WFPanelOffline
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.WindowFrame;
            ClientSize = new Size(964, 502);
            Controls.Add(ManterCerradaPuerta);
            Controls.Add(MantenerAbiertaPuerta);
            Controls.Add(CerrarPuerta);
            Controls.Add(AbrirPuerta);
            Controls.Add(controlPuertasLabel);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WFPanelOffline";
            Text = "Panel Offline";
            Load += WFPanelOffline_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label controlPuertasLabel;
        private Button AbrirPuerta;
        private Button CerrarPuerta;
        private Button MantenerAbiertaPuerta;
        private Button ManterCerradaPuerta;
    }
}