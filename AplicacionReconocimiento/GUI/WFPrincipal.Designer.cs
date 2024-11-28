using System.Windows.Forms;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFPrincipal));
            imagenDeportnet = new PictureBox();
            pictureBox1 = new PictureBox();
            vtoLabel = new Label();
            actividadLabel = new Label();
            valorFechaVtoLabel = new Label();
            valorClasesRestLabel = new Label();
            mensajeOpcionalLabel = new Label();
            clasesRestLabel = new Label();
            trayReconocimiento = new NotifyIcon(components);
            HeaderLabel = new Label();
            valorNombreHeaderLabel = new Label();
            valorMensajeLabel = new Label();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // imagenDeportnet
            // 
            imagenDeportnet.BackColor = Color.DimGray;
            imagenDeportnet.Image = Properties.Resources.logo_deportnet_1;
            imagenDeportnet.Location = new Point(-1, -1);
            imagenDeportnet.Name = "imagenDeportnet";
            imagenDeportnet.Size = new Size(1265, 133);
            imagenDeportnet.SizeMode = PictureBoxSizeMode.StretchImage;
            imagenDeportnet.TabIndex = 1;
            imagenDeportnet.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox1.Location = new Point(12, 223);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(431, 446);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // vtoLabel
            // 
            vtoLabel.AutoSize = true;
            vtoLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            vtoLabel.ForeColor = SystemColors.ControlText;
            vtoLabel.Location = new Point(449, 313);
            vtoLabel.Name = "vtoLabel";
            vtoLabel.Size = new Size(331, 55);
            vtoLabel.TabIndex = 17;
            vtoLabel.Text = "Vencimiento:";
            vtoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // actividadLabel
            // 
            actividadLabel.AutoSize = true;
            actividadLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            actividadLabel.ForeColor = SystemColors.ControlText;
            actividadLabel.Location = new Point(449, 238);
            actividadLabel.Name = "actividadLabel";
            actividadLabel.Size = new Size(404, 55);
            actividadLabel.TabIndex = 18;
            actividadLabel.Text = "Actividad aqui...";
            actividadLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // valorFechaVtoLabel
            // 
            valorFechaVtoLabel.AutoSize = true;
            valorFechaVtoLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorFechaVtoLabel.ForeColor = SystemColors.ControlText;
            valorFechaVtoLabel.Location = new Point(795, 313);
            valorFechaVtoLabel.Name = "valorFechaVtoLabel";
            valorFechaVtoLabel.Size = new Size(411, 55);
            valorFechaVtoLabel.TabIndex = 19;
            valorFechaVtoLabel.Text = "Fecha vto aqui...";
            valorFechaVtoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // valorClasesRestLabel
            // 
            valorClasesRestLabel.AutoSize = true;
            valorClasesRestLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorClasesRestLabel.ForeColor = SystemColors.ControlText;
            valorClasesRestLabel.Location = new Point(899, 382);
            valorClasesRestLabel.Name = "valorClasesRestLabel";
            valorClasesRestLabel.Size = new Size(155, 55);
            valorClasesRestLabel.TabIndex = 20;
            valorClasesRestLabel.Text = "Nro...";
            valorClasesRestLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mensajeOpcionalLabel
            // 
            mensajeOpcionalLabel.AutoSize = true;
            mensajeOpcionalLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            mensajeOpcionalLabel.ForeColor = SystemColors.ControlText;
            mensajeOpcionalLabel.Location = new Point(449, 443);
            mensajeOpcionalLabel.Name = "mensajeOpcionalLabel";
            mensajeOpcionalLabel.Size = new Size(234, 55);
            mensajeOpcionalLabel.TabIndex = 21;
            mensajeOpcionalLabel.Text = "Mensaje:";
            mensajeOpcionalLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // clasesRestLabel
            // 
            clasesRestLabel.AutoSize = true;
            clasesRestLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            clasesRestLabel.ForeColor = SystemColors.ControlText;
            clasesRestLabel.Location = new Point(449, 382);
            clasesRestLabel.Name = "clasesRestLabel";
            clasesRestLabel.Size = new Size(433, 55);
            clasesRestLabel.TabIndex = 22;
            clasesRestLabel.Text = "Clases restantes:";
            clasesRestLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // trayReconocimiento
            // 
            trayReconocimiento.BalloonTipIcon = ToolTipIcon.Info;
            trayReconocimiento.BalloonTipText = "Deportnet Reconocimiento";
            trayReconocimiento.BalloonTipTitle = "Deportnet Reconocimiento";
            trayReconocimiento.Icon = (Icon)resources.GetObject("trayReconocimiento.Icon");
            trayReconocimiento.Tag = "Deportnet Reconocimiento";
            trayReconocimiento.Text = "Deportnet Reconocimiento";
            trayReconocimiento.Visible = true;
            trayReconocimiento.MouseDoubleClick += trayReconocimiento_MouseDoubleClick;
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.BackColor = Color.Transparent;
            HeaderLabel.Font = new Font("Arial Rounded MT Bold", 56.25F);
            HeaderLabel.ForeColor = Color.FromArgb(192, 0, 0);
            HeaderLabel.Location = new Point(25, 4);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(706, 87);
            HeaderLabel.TabIndex = 23;
            HeaderLabel.Text = "Acceso Denegado";
            HeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // valorNombreHeaderLabel
            // 
            valorNombreHeaderLabel.AutoSize = true;
            valorNombreHeaderLabel.BackColor = Color.Transparent;
            valorNombreHeaderLabel.Font = new Font("Arial Rounded MT Bold", 56.25F);
            valorNombreHeaderLabel.ForeColor = Color.FromArgb(192, 0, 0);
            valorNombreHeaderLabel.Location = new Point(737, 4);
            valorNombreHeaderLabel.Name = "valorNombreHeaderLabel";
            valorNombreHeaderLabel.Size = new Size(388, 87);
            valorNombreHeaderLabel.TabIndex = 25;
            valorNombreHeaderLabel.Text = "{Nombre}";
            valorNombreHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // valorMensajeLabel
            // 
            valorMensajeLabel.AutoSize = true;
            valorMensajeLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorMensajeLabel.ForeColor = SystemColors.ControlText;
            valorMensajeLabel.Location = new Point(449, 498);
            valorMensajeLabel.Name = "valorMensajeLabel";
            valorMensajeLabel.Size = new Size(377, 55);
            valorMensajeLabel.TabIndex = 26;
            valorMensajeLabel.Text = "Mensaje aqui...";
            valorMensajeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGray;
            panel1.Controls.Add(valorNombreHeaderLabel);
            panel1.Controls.Add(HeaderLabel);
            panel1.Location = new Point(-1, 110);
            panel1.Name = "panel1";
            panel1.Size = new Size(1265, 107);
            panel1.TabIndex = 27;
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1904, 1041);
            Controls.Add(panel1);
            Controls.Add(valorMensajeLabel);
            Controls.Add(clasesRestLabel);
            Controls.Add(mensajeOpcionalLabel);
            Controls.Add(valorClasesRestLabel);
            Controls.Add(valorFechaVtoLabel);
            Controls.Add(actividadLabel);
            Controls.Add(vtoLabel);
            Controls.Add(imagenDeportnet);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "WFPrincipal";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pantalla Bienvenida";
            WindowState = FormWindowState.Maximized;
            Load += WFPrincipal_Load;
            Resize += WFPrincipal_Resize;
            FormClosing += cerrarFormulario;
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imagenDeportnet;
        private PictureBox pictureBox1;
        private Label vtoLabel;
        private Label actividadLabel;
        private Label valorFechaVtoLabel;
        private Label valorClasesRestLabel;
        private Label mensajeOpcionalLabel;
        private Label clasesRestLabel;
        private NotifyIcon trayReconocimiento;
        private Label HeaderLabel;
        private Label valorNombreHeaderLabel;
        private Label valorMensajeLabel;
        private Panel panel1;
    }
}