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
            imagenLogo = new PictureBox();
            pictureBox1 = new PictureBox();
            trayReconocimiento = new NotifyIcon(components);
            menuNotifyIcon = new ContextMenuStrip(components);
            abrirToolStripMenuItem = new ToolStripMenuItem();
            cerrarToolStripMenuItem = new ToolStripMenuItem();
            HeaderLabel = new Label();
            Abrir = new ToolStripMenuItem();
            Cerrar = new ToolStripMenuItem();
            botonPersonalizar = new Button();
            PanelSinConexion = new Panel();
            textoSinCoenxion = new Label();
            PanelAlmacenamiento = new Panel();
            TextoAlmacenamiento = new Label();
            datosSocio = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)imagenLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuNotifyIcon.SuspendLayout();
            PanelSinConexion.SuspendLayout();
            PanelAlmacenamiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datosSocio).BeginInit();
            SuspendLayout();
            // 
            // imagenLogo
            // 
            imagenLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            imagenLogo.BackColor = Color.DimGray;
            imagenLogo.Image = Resources.logo_deportnet_1;
            imagenLogo.Location = new Point(323, 2);
            imagenLogo.Name = "imagenLogo";
            imagenLogo.Size = new Size(710, 130);
            imagenLogo.SizeMode = PictureBoxSizeMode.Zoom;
            imagenLogo.TabIndex = 1;
            imagenLogo.TabStop = false;
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
            HeaderLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            HeaderLabel.BackColor = Color.DarkGray;
            HeaderLabel.Font = new Font("Arial Rounded MT Bold", 56.25F);
            HeaderLabel.ForeColor = Color.FromArgb(192, 0, 0);
            HeaderLabel.Location = new Point(93, 145);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(1161, 102);
            HeaderLabel.TabIndex = 23;
            HeaderLabel.Text = "Bienvenido a Deportnet!";
            HeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
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
            // PanelSinConexion
            // 
            PanelSinConexion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            PanelSinConexion.BackColor = Color.Black;
            PanelSinConexion.Controls.Add(textoSinCoenxion);
            PanelSinConexion.Location = new Point(1048, 12);
            PanelSinConexion.Name = "PanelSinConexion";
            PanelSinConexion.Size = new Size(275, 80);
            PanelSinConexion.TabIndex = 29;
            PanelSinConexion.Visible = false;
            // 
            // textoSinCoenxion
            // 
            textoSinCoenxion.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textoSinCoenxion.ForeColor = Color.Red;
            textoSinCoenxion.Location = new Point(0, 0);
            textoSinCoenxion.Name = "textoSinCoenxion";
            textoSinCoenxion.Size = new Size(275, 80);
            textoSinCoenxion.TabIndex = 0;
            textoSinCoenxion.Text = "No hay conexión a internet";
            textoSinCoenxion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelAlmacenamiento
            // 
            PanelAlmacenamiento.BackColor = Color.Black;
            PanelAlmacenamiento.Controls.Add(TextoAlmacenamiento);
            PanelAlmacenamiento.Location = new Point(12, 12);
            PanelAlmacenamiento.Name = "PanelAlmacenamiento";
            PanelAlmacenamiento.Size = new Size(275, 80);
            PanelAlmacenamiento.TabIndex = 30;
            PanelAlmacenamiento.Visible = false;
            // 
            // TextoAlmacenamiento
            // 
            TextoAlmacenamiento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TextoAlmacenamiento.ForeColor = Color.Red;
            TextoAlmacenamiento.Location = new Point(0, 0);
            TextoAlmacenamiento.Name = "TextoAlmacenamiento";
            TextoAlmacenamiento.Size = new Size(272, 80);
            TextoAlmacenamiento.TabIndex = 0;
            TextoAlmacenamiento.Text = "Almacenamiento al: 100%    Socios: 1500/1500 ";
            TextoAlmacenamiento.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // webView
            // 
            datosSocio.AllowExternalDrop = true;
            datosSocio.CreationProperties = null;
            datosSocio.DefaultBackgroundColor = Color.White;
            datosSocio.Location = new Point(609, 282);
            datosSocio.Name = "webView";
            datosSocio.Size = new Size(714, 376);
            datosSocio.TabIndex = 31;
            datosSocio.ZoomFactor = 1D;
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Silver;
            ClientSize = new Size(1348, 727);
            Controls.Add(datosSocio);
            Controls.Add(PanelAlmacenamiento);
            Controls.Add(HeaderLabel);
            Controls.Add(PanelSinConexion);
            Controls.Add(botonPersonalizar);
            Controls.Add(imagenLogo);
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
            ((System.ComponentModel.ISupportInitialize)imagenLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuNotifyIcon.ResumeLayout(false);
            PanelSinConexion.ResumeLayout(false);
            PanelAlmacenamiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)datosSocio).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imagenLogo;
        private PictureBox pictureBox1;
        private NotifyIcon trayReconocimiento;
        private Label HeaderLabel;
        private ContextMenuStrip menuNotifyIcon;
        private ToolStripMenuItem Abrir;
        private ToolStripMenuItem Cerrar;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem cerrarToolStripMenuItem;
        private Button botonPersonalizar;
        private Panel PanelSinConexion;
        private Label textoSinCoenxion;
        private Panel PanelAlmacenamiento;
        private Label TextoAlmacenamiento;
        private Microsoft.Web.WebView2.WinForms.WebView2 datosSocio;
    }
}