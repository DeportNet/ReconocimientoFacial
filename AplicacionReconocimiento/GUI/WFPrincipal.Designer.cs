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
            vtoLabel = new Label();
            actividadLabel = new Label();
            valorFechaVtoLabel = new Label();
            valorClasesRestLabel = new Label();
            mensajeOpcionalLabel = new Label();
            clasesRestLabel = new Label();
            trayReconocimiento = new NotifyIcon(components);
            menuNotifyIcon = new ContextMenuStrip(components);
            abrirToolStripMenuItem = new ToolStripMenuItem();
            cerrarToolStripMenuItem = new ToolStripMenuItem();
            HeaderLabel = new Label();
            valorNombreHeaderLabel = new Label();
            valorMensajeLabel = new Label();
            panel1 = new Panel();
            Abrir = new ToolStripMenuItem();
            Cerrar = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuNotifyIcon.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // imagenDeportnet
            // 
            imagenDeportnet.BackColor = Color.DimGray;
            imagenDeportnet.Image = Resources.logo_deportnet_1;
            imagenDeportnet.Location = new Point(323, 2);
            imagenDeportnet.Name = "imagenDeportnet";
            imagenDeportnet.Size = new Size(1265, 133);
            imagenDeportnet.SizeMode = PictureBoxSizeMode.StretchImage;
            imagenDeportnet.TabIndex = 1;
            imagenDeportnet.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DarkGray;
            pictureBox1.Location = new Point(97, 265);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(493, 518);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // vtoLabel
            // 
            vtoLabel.AutoSize = true;
            vtoLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            vtoLabel.ForeColor = SystemColors.ControlText;
            vtoLabel.Location = new Point(772, 372);
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
            actividadLabel.Location = new Point(772, 297);
            actividadLabel.Name = "actividadLabel";
            actividadLabel.Size = new Size(0, 55);
            actividadLabel.TabIndex = 18;
            actividadLabel.TextAlign = ContentAlignment.MiddleCenter;
            actividadLabel.Click += actividadLabel_Click;
            // 
            // valorFechaVtoLabel
            // 
            valorFechaVtoLabel.AutoSize = true;
            valorFechaVtoLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorFechaVtoLabel.ForeColor = SystemColors.ControlText;
            valorFechaVtoLabel.Location = new Point(1118, 372);
            valorFechaVtoLabel.Name = "valorFechaVtoLabel";
            valorFechaVtoLabel.Size = new Size(0, 55);
            valorFechaVtoLabel.TabIndex = 19;
            valorFechaVtoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // valorClasesRestLabel
            // 
            valorClasesRestLabel.AutoSize = true;
            valorClasesRestLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorClasesRestLabel.ForeColor = SystemColors.ControlText;
            valorClasesRestLabel.Location = new Point(1222, 441);
            valorClasesRestLabel.Name = "valorClasesRestLabel";
            valorClasesRestLabel.Size = new Size(0, 55);
            valorClasesRestLabel.TabIndex = 20;
            valorClasesRestLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // mensajeOpcionalLabel
            // 
            mensajeOpcionalLabel.AutoSize = true;
            mensajeOpcionalLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            mensajeOpcionalLabel.ForeColor = SystemColors.ControlText;
            mensajeOpcionalLabel.Location = new Point(772, 502);
            mensajeOpcionalLabel.Name = "mensajeOpcionalLabel";
            mensajeOpcionalLabel.Size = new Size(234, 55);
            mensajeOpcionalLabel.TabIndex = 21;
            mensajeOpcionalLabel.Text = "Mensaje:";
            mensajeOpcionalLabel.TextAlign = ContentAlignment.MiddleCenter;
            mensajeOpcionalLabel.Click += mensajeOpcionalLabel_Click;
            // 
            // clasesRestLabel
            // 
            clasesRestLabel.AutoSize = true;
            clasesRestLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            clasesRestLabel.ForeColor = SystemColors.ControlText;
            clasesRestLabel.Location = new Point(772, 441);
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
            HeaderLabel.Location = new Point(34, 0);
            HeaderLabel.Name = "HeaderLabel";
            HeaderLabel.Size = new Size(0, 87);
            HeaderLabel.TabIndex = 23;
            HeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            HeaderLabel.Click += HeaderLabel_Click;
            // 
            // valorNombreHeaderLabel
            // 
            valorNombreHeaderLabel.AutoSize = true;
            valorNombreHeaderLabel.BackColor = Color.Transparent;
            valorNombreHeaderLabel.Font = new Font("Arial Rounded MT Bold", 56.25F);
            valorNombreHeaderLabel.ForeColor = Color.FromArgb(192, 0, 0);
            valorNombreHeaderLabel.Location = new Point(971, 0);
            valorNombreHeaderLabel.Name = "valorNombreHeaderLabel";
            valorNombreHeaderLabel.Size = new Size(0, 87);
            valorNombreHeaderLabel.TabIndex = 25;
            valorNombreHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            valorNombreHeaderLabel.Click += valorNombreHeaderLabel_Click;
            // 
            // valorMensajeLabel
            // 
            valorMensajeLabel.AutoSize = true;
            valorMensajeLabel.Font = new Font("Arial Rounded MT Bold", 36F);
            valorMensajeLabel.ForeColor = SystemColors.ControlText;
            valorMensajeLabel.Location = new Point(772, 557);
            valorMensajeLabel.Name = "valorMensajeLabel";
            valorMensajeLabel.Size = new Size(0, 55);
            valorMensajeLabel.TabIndex = 26;
            valorMensajeLabel.TextAlign = ContentAlignment.MiddleCenter;
            valorMensajeLabel.Click += valorMensajeLabel_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkGray;
            panel1.Controls.Add(HeaderLabel);
            panel1.Controls.Add(valorNombreHeaderLabel);
            panel1.Location = new Point(147, 138);
            panel1.Name = "panel1";
            panel1.Size = new Size(1625, 102);
            panel1.TabIndex = 27;
            panel1.Paint += panel1_Paint;
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
            FormClosing += cerrarFormulario;
            Load += WFPrincipal_Load;
            Resize += WFPrincipal_Resize;
            ((System.ComponentModel.ISupportInitialize)imagenDeportnet).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuNotifyIcon.ResumeLayout(false);
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
        private ContextMenuStrip menuNotifyIcon;
        private ToolStripMenuItem Abrir;
        private ToolStripMenuItem Cerrar;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem cerrarToolStripMenuItem;
    }
}