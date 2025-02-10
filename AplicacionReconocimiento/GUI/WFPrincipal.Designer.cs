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
            timerConexion = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            textoInformacionCliente = new Label();
            timerMinimizar = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)imagenLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuNotifyIcon.SuspendLayout();
            PanelSinConexion.SuspendLayout();
            PanelAlmacenamiento.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // imagenLogo
            // 
            imagenLogo.BackColor = Color.DimGray;
            imagenLogo.Dock = DockStyle.Top;
            imagenLogo.Image = Resources.logo_deportnet_1;
            imagenLogo.Location = new Point(0, 0);
            imagenLogo.Name = "imagenLogo";
            imagenLogo.Size = new Size(1904, 130);
            imagenLogo.SizeMode = PictureBoxSizeMode.Zoom;
            imagenLogo.TabIndex = 1;
            imagenLogo.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.MinimumSize = new Size(420, 450);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(628, 723);
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
            HeaderLabel.ForeColor = Color.Black;
            HeaderLabel.Location = new Point(0, 130);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(1904, 113);
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
            botonPersonalizar.BackColor = Color.WhiteSmoke;
            botonPersonalizar.Cursor = Cursors.Hand;
            botonPersonalizar.FlatAppearance.BorderSize = 0;
            botonPersonalizar.FlatStyle = FlatStyle.Flat;
            botonPersonalizar.ForeColor = Color.Black;
            botonPersonalizar.Location = new Point(1808, 971);
            botonPersonalizar.Name = "botonPersonalizar";
            botonPersonalizar.Size = new Size(84, 23);
            botonPersonalizar.TabIndex = 28;
            botonPersonalizar.Text = "Personalizar";
            botonPersonalizar.UseVisualStyleBackColor = false;
            botonPersonalizar.Click += botonPersonalizar_Click;
            // 
            // PanelSinConexion
            // 
            PanelSinConexion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            PanelSinConexion.BackColor = Color.Black;
            PanelSinConexion.Controls.Add(textoSinCoenxion);
            PanelSinConexion.Location = new Point(1527, 961);
            PanelSinConexion.Name = "PanelSinConexion";
            PanelSinConexion.Size = new Size(275, 36);
            PanelSinConexion.TabIndex = 29;
            PanelSinConexion.Visible = false;
            // 
            // textoSinCoenxion
            // 
            textoSinCoenxion.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textoSinCoenxion.BackColor = Color.Silver;
            textoSinCoenxion.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textoSinCoenxion.ForeColor = Color.Red;
            textoSinCoenxion.Location = new Point(0, 5);
            textoSinCoenxion.Name = "textoSinCoenxion";
            textoSinCoenxion.Size = new Size(275, 31);
            textoSinCoenxion.TabIndex = 0;
            textoSinCoenxion.Text = "No hay conexión a internet";
            textoSinCoenxion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PanelAlmacenamiento
            // 
            PanelAlmacenamiento.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            PanelAlmacenamiento.BackColor = Color.Black;
            PanelAlmacenamiento.Controls.Add(TextoAlmacenamiento);
            PanelAlmacenamiento.Location = new Point(1083, 966);
            PanelAlmacenamiento.Name = "PanelAlmacenamiento";
            PanelAlmacenamiento.Size = new Size(438, 35);
            PanelAlmacenamiento.TabIndex = 30;
            PanelAlmacenamiento.Visible = false;
            // 
            // TextoAlmacenamiento
            // 
            TextoAlmacenamiento.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            TextoAlmacenamiento.BackColor = Color.Silver;
            TextoAlmacenamiento.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TextoAlmacenamiento.ForeColor = Color.Red;
            TextoAlmacenamiento.Location = new Point(0, 0);
            TextoAlmacenamiento.Name = "TextoAlmacenamiento";
            TextoAlmacenamiento.Size = new Size(438, 36);
            TextoAlmacenamiento.TabIndex = 0;
            TextoAlmacenamiento.Text = "Almacenamiento al: 100%    Socios: 1500/1500 ";
            TextoAlmacenamiento.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timerConexion
            // 
            timerConexion.Interval = 20000;
            timerConexion.Tick += VerificarEstadoDispositivoAsync;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67F));
            tableLayoutPanel1.Controls.Add(pictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(textoInformacionCliente, 1, 0);
            tableLayoutPanel1.Location = new Point(0, 243);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1904, 723);
            tableLayoutPanel1.TabIndex = 33;
            // 
            // textoInformacionCliente
            // 
            textoInformacionCliente.FlatStyle = FlatStyle.Flat;
            textoInformacionCliente.Location = new Point(628, 0);
            textoInformacionCliente.Margin = new Padding(0);
            textoInformacionCliente.Name = "textoInformacionCliente";
            textoInformacionCliente.Size = new Size(1276, 723);
            textoInformacionCliente.TabIndex = 12;
            // 
            // WFPrincipal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Silver;
            ClientSize = new Size(1904, 1001);
            Controls.Add(botonPersonalizar);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(PanelAlmacenamiento);
            Controls.Add(HeaderLabel);
            Controls.Add(PanelSinConexion);
            Controls.Add(imagenLogo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1920, 1040);
            MinimumSize = new Size(1364, 766);
            Name = "WFPrincipal";
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pantalla Bienvenida";
            WindowState = FormWindowState.Maximized;
            FormClosing += CerrarFormulario;
            Resize += WFPrincipal_Resize;
            ((System.ComponentModel.ISupportInitialize)imagenLogo).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuNotifyIcon.ResumeLayout(false);
            PanelSinConexion.ResumeLayout(false);
            PanelAlmacenamiento.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.Timer timerConexion;
        private TableLayoutPanel tableLayoutPanel1;
        private Label textoInformacionCliente;
        private System.Windows.Forms.Timer timerMinimizar;
    }
}