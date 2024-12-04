using DeportNetReconocimiento.Properties;
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
            actividadLabel = new Label();
            valorFechaVtoLabel = new Label();
            valorClasesRestLabel = new Label();
            trayReconocimiento = new NotifyIcon(components);
            menuNotifyIcon = new ContextMenuStrip(components);
            abrirToolStripMenuItem = new ToolStripMenuItem();
            cerrarToolStripMenuItem = new ToolStripMenuItem();
            HeaderLabel = new Label();
            valorMensajeLabel = new Label();
            fondoMensajeAcceso = new Panel();
            Abrir = new ToolStripMenuItem();
            Cerrar = new ToolStripMenuItem();
            botonPersonalizar = new Button();
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuNotifyIcon.SuspendLayout();
            fondoMensajeAcceso.SuspendLayout();
            SuspendLayout();
            // 
            // imagenDeportnet
            // 
            imagenDeportnet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            imagenDeportnet.BackColor = Color.DimGray;
            imagenDeportnet.Image = Resources.logo_deportnet_1;
            imagenDeportnet.Location = new Point(323, 2);
            imagenDeportnet.Name = "imagenDeportnet";
            imagenDeportnet.Size = new Size(709, 133);
            imagenDeportnet.SizeMode = PictureBoxSizeMode.StretchImage;
            imagenDeportnet.TabIndex = 1;
            imagenDeportnet.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox1.Location = new Point(132, 258);
            pictureBox1.MaximumSize = new Size(480, 500);
            pictureBox1.MinimumSize = new Size(420, 450);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(420, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // actividadLabel
            // 
            actividadLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            actividadLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            actividadLabel.ForeColor = SystemColors.ControlText;
            actividadLabel.Location = new Point(647, 308);
            actividadLabel.Name = "actividadLabel";
            actividadLabel.Size = new Size(689, 55);
            actividadLabel.TabIndex = 18;
            actividadLabel.Text = "Actividad Label";
            actividadLabel.Click += actividadLabel_Click;
            // 
            // valorFechaVtoLabel
            // 
            valorFechaVtoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            valorFechaVtoLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorFechaVtoLabel.ForeColor = SystemColors.ControlText;
            valorFechaVtoLabel.Location = new Point(647, 389);
            valorFechaVtoLabel.Name = "valorFechaVtoLabel";
            valorFechaVtoLabel.Size = new Size(689, 55);
            valorFechaVtoLabel.TabIndex = 19;
            valorFechaVtoLabel.Text = "Valor Fecha vto label";
            // 
            // valorClasesRestLabel
            // 
            valorClasesRestLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            valorClasesRestLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorClasesRestLabel.ForeColor = SystemColors.ControlText;
            valorClasesRestLabel.Location = new Point(647, 459);
            valorClasesRestLabel.Name = "valorClasesRestLabel";
            valorClasesRestLabel.Size = new Size(689, 55);
            valorClasesRestLabel.TabIndex = 20;
            valorClasesRestLabel.Text = "Clases restantes label";
            valorClasesRestLabel.Click += valorClasesRestLabel_Click;
            // 
            // trayReconocimiento
            // 
            trayReconocimiento.BalloonTipIcon = ToolTipIcon.Info;
            trayReconocimiento.BalloonTipText = "Deportnet Reconocimiento";
            trayReconocimiento.BalloonTipTitle = "Deportnet Reconocimiento";
            trayReconocimiento.ContextMenuStrip = menuNotifyIcon;
            trayReconocimiento.Icon = (Icon)resources.GetObject("trayReconocimiento.Icon");
            trayReconocimiento.Tag = "Deportnet Reconocimiento";
            trayReconocimiento.Text = "Deportnet Reconocimiento";
            trayReconocimiento.Visible = true;
            trayReconocimiento.MouseClick += trayReconocimiento_MouseClick;
            // 
            // menuNotifyIcon
            // 
            menuNotifyIcon.Items.AddRange(new ToolStripItem[] { abrirToolStripMenuItem, cerrarToolStripMenuItem });
            menuNotifyIcon.Name = "menuNotifyIcon";
            menuNotifyIcon.Size = new Size(107, 48);
            // 
            // abrirToolStripMenuItem
            // 
            abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            abrirToolStripMenuItem.Size = new Size(106, 22);
            abrirToolStripMenuItem.Text = "Abrir";
            abrirToolStripMenuItem.Click += ClickAbrirMenuNotifyIcon;
            // 
            // cerrarToolStripMenuItem
            // 
            cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            cerrarToolStripMenuItem.Size = new Size(106, 22);
            cerrarToolStripMenuItem.Text = "Cerrar";
            cerrarToolStripMenuItem.Click += ClickCerrarMenuNotifyIcon;
            // 
            // HeaderLabel
            // 
            HeaderLabel.AutoSize = true;
            HeaderLabel.BackColor = Color.Transparent;
            HeaderLabel.Font = new Font("Arial Rounded MT Bold", 56.25F);
            HeaderLabel.ForeColor = Color.FromArgb(192, 0, 0);
            HeaderLabel.Location = new Point(48, 7);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(505, 87);
            HeaderLabel.TabIndex = 23;
            HeaderLabel.Text = "Header label";
            HeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            HeaderLabel.Click += HeaderLabel_Click;
            // 
            // valorMensajeLabel
            // 
            valorMensajeLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            valorMensajeLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorMensajeLabel.ForeColor = SystemColors.ControlText;
            valorMensajeLabel.Location = new Point(647, 542);
            valorMensajeLabel.Name = "valorMensajeLabel";
            valorMensajeLabel.Size = new Size(689, 147);
            valorMensajeLabel.TabIndex = 26;
            valorMensajeLabel.Text = "Valor mensaje label";
            valorMensajeLabel.Click += valorMensajeLabel_Click;
            // 
            // fondoMensajeAcceso
            // 
            fondoMensajeAcceso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            fondoMensajeAcceso.AutoSize = true;
            fondoMensajeAcceso.BackColor = Color.DarkGray;
            fondoMensajeAcceso.Controls.Add(HeaderLabel);
            fondoMensajeAcceso.Location = new Point(84, 138);
            fondoMensajeAcceso.Name = "fondoMensajeAcceso";
            fondoMensajeAcceso.Size = new Size(1161, 102);
            fondoMensajeAcceso.TabIndex = 27;
            fondoMensajeAcceso.Paint += panel1_Paint;
            // 
            // Abrir
            // 
            Abrir.Name = "Abrir";
            Abrir.Size = new Size(180, 22);
            Abrir.Text = "toolStripMenuItem1";
            // 
            // Cerrar
            // 
            Cerrar.Name = "Cerrar";
            Cerrar.Size = new Size(180, 22);
            Cerrar.Text = "toolStripMenuItem2";
            // 
            // botonPersonalizar
            // 
            botonPersonalizar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonPersonalizar.Location = new Point(1252, 692);
            botonPersonalizar.Name = "botonPersonalizar";
            botonPersonalizar.Size = new Size(84, 23);
            botonPersonalizar.TabIndex = 28;
            botonPersonalizar.Text = "Personalizar";
            botonPersonalizar.UseVisualStyleBackColor = true;
            botonPersonalizar.Click += botonPersonalizar_Click;
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Silver;
            ClientSize = new Size(1348, 727);
            Controls.Add(botonPersonalizar);
            Controls.Add(fondoMensajeAcceso);
            Controls.Add(valorMensajeLabel);
            Controls.Add(valorClasesRestLabel);
            Controls.Add(valorFechaVtoLabel);
            Controls.Add(actividadLabel);
            Controls.Add(imagenDeportnet);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1920, 1080);
            MinimumSize = new Size(1364, 766);
            Name = "WFPrincipal";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pantalla Bienvenida";
            WindowState = FormWindowState.Maximized;
            FormClosing += cerrarFormulario;
            Load += WFPrincipal_Load;
            Resize += WFPrincipal_Resize;
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuNotifyIcon.ResumeLayout(false);
            fondoMensajeAcceso.ResumeLayout(false);
            fondoMensajeAcceso.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox imagenDeportnet;
        private PictureBox pictureBox1;
        private Label actividadLabel;
        private Label valorFechaVtoLabel;
        private Label valorClasesRestLabel;
        private NotifyIcon trayReconocimiento;
        private Label HeaderLabel;
        private Label valorMensajeLabel;
        private Panel fondoMensajeAcceso;
        private ContextMenuStrip menuNotifyIcon;
        private ToolStripMenuItem Abrir;
        private ToolStripMenuItem Cerrar;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        private Button botonPersonalizar;
    }
}