namespace DeportNetReconocimiento.GUI
{
    partial class WFConfiguracion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFConfiguracion));
            tituloConfig = new Label();
            label6 = new Label();
            personalizacionTituloLabel = new Label();
            opcionesTituloLabel = new Label();
            propertyGrid1 = new PropertyGrid();
            guardarCambiosButton = new Button();
            ComboBoxAperturaMolinete = new ComboBox();
            BotonOcultarConfig = new Button();
            PanelConfigAdminsitrador = new Panel();
            comboBoxNroLector = new ComboBox();
            label3 = new Label();
            botonEditarCredenciales = new Button();
            label2 = new Label();
            BotonAbrirFileDialog = new Button();
            TextBoxRutaExe = new TextBox();
            label1 = new Label();
            LabelAdmin = new Label();
            TextBoxAdmin = new TextBox();
            BotonIngresarAdmin = new Button();
            button1 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            panel2 = new Panel();
            botonModoOffline = new Button();
            PanelConfigAdminsitrador.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tituloConfig
            // 
            tituloConfig.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline);
            tituloConfig.Location = new Point(2, 25);
            tituloConfig.Name = "tituloConfig";
            tituloConfig.Size = new Size(1003, 45);
            tituloConfig.TabIndex = 0;
            tituloConfig.Text = "Configuraciones dispositivo reconocimiento";
            tituloConfig.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(21, 44);
            label6.Name = "label6";
            label6.Size = new Size(139, 21);
            label6.TabIndex = 9;
            label6.Text = "Apertura molinete:";
            label6.Visible = false;
            // 
            // personalizacionTituloLabel
            // 
            personalizacionTituloLabel.AutoSize = true;
            personalizacionTituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline);
            personalizacionTituloLabel.Location = new Point(12, 90);
            personalizacionTituloLabel.Name = "personalizacionTituloLabel";
            personalizacionTituloLabel.Size = new Size(131, 21);
            personalizacionTituloLabel.TabIndex = 12;
            personalizacionTituloLabel.Text = "Personalizacion";
            // 
            // opcionesTituloLabel
            // 
            opcionesTituloLabel.AutoSize = true;
            opcionesTituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline);
            opcionesTituloLabel.Location = new Point(21, 9);
            opcionesTituloLabel.Name = "opcionesTituloLabel";
            opcionesTituloLabel.Size = new Size(81, 21);
            opcionesTituloLabel.TabIndex = 13;
            opcionesTituloLabel.Text = "Opciones";
            // 
            // propertyGrid1
            // 
            propertyGrid1.AllowDrop = true;
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.Location = new Point(0, 0);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(639, 508);
            propertyGrid1.TabIndex = 15;
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
            propertyGrid1.DragDrop += PropertyGrid1_DragDrop;
            propertyGrid1.DragEnter += PropertyGrid1_DragEnter;
            propertyGrid1.DragLeave += PropertyGrid1_DragLeave;
            // 
            // guardarCambiosButton
            // 
            guardarCambiosButton.BackColor = Color.OliveDrab;
            guardarCambiosButton.Cursor = Cursors.Hand;
            guardarCambiosButton.FlatAppearance.BorderSize = 0;
            guardarCambiosButton.FlatStyle = FlatStyle.Flat;
            guardarCambiosButton.ForeColor = Color.White;
            guardarCambiosButton.Location = new Point(22, 643);
            guardarCambiosButton.Name = "guardarCambiosButton";
            guardarCambiosButton.Size = new Size(171, 36);
            guardarCambiosButton.TabIndex = 16;
            guardarCambiosButton.Text = "Guardar Cambios";
            guardarCambiosButton.UseVisualStyleBackColor = false;
            guardarCambiosButton.Click += GuardarCambiosButton_Click;
            // 
            // ComboBoxAperturaMolinete
            // 
            ComboBoxAperturaMolinete.BackColor = Color.WhiteSmoke;
            ComboBoxAperturaMolinete.FormattingEnabled = true;
            ComboBoxAperturaMolinete.Items.AddRange(new object[] { ".exe", "Hikvision", "Ninguno" });
            ComboBoxAperturaMolinete.Location = new Point(171, 46);
            ComboBoxAperturaMolinete.Name = "ComboBoxAperturaMolinete";
            ComboBoxAperturaMolinete.Size = new Size(121, 23);
            ComboBoxAperturaMolinete.TabIndex = 18;
            ComboBoxAperturaMolinete.Visible = false;
            // 
            // BotonOcultarConfig
            // 
            BotonOcultarConfig.BackColor = Color.WhiteSmoke;
            BotonOcultarConfig.Cursor = Cursors.Hand;
            BotonOcultarConfig.FlatAppearance.BorderSize = 0;
            BotonOcultarConfig.FlatStyle = FlatStyle.Flat;
            BotonOcultarConfig.Location = new Point(21, 262);
            BotonOcultarConfig.Name = "BotonOcultarConfig";
            BotonOcultarConfig.Size = new Size(121, 23);
            BotonOcultarConfig.TabIndex = 19;
            BotonOcultarConfig.Text = "Guardar/Ocultar";
            BotonOcultarConfig.UseVisualStyleBackColor = false;
            BotonOcultarConfig.Click += BotonOcultarConfig_Click;
            // 
            // PanelConfigAdminsitrador
            // 
            PanelConfigAdminsitrador.BackColor = Color.IndianRed;
            PanelConfigAdminsitrador.Controls.Add(botonModoOffline);
            PanelConfigAdminsitrador.Controls.Add(comboBoxNroLector);
            PanelConfigAdminsitrador.Controls.Add(label3);
            PanelConfigAdminsitrador.Controls.Add(botonEditarCredenciales);
            PanelConfigAdminsitrador.Controls.Add(label2);
            PanelConfigAdminsitrador.Controls.Add(BotonAbrirFileDialog);
            PanelConfigAdminsitrador.Controls.Add(TextBoxRutaExe);
            PanelConfigAdminsitrador.Controls.Add(label1);
            PanelConfigAdminsitrador.Controls.Add(BotonOcultarConfig);
            PanelConfigAdminsitrador.Controls.Add(ComboBoxAperturaMolinete);
            PanelConfigAdminsitrador.Controls.Add(opcionesTituloLabel);
            PanelConfigAdminsitrador.Controls.Add(label6);
            PanelConfigAdminsitrador.Location = new Point(18, 112);
            PanelConfigAdminsitrador.Name = "PanelConfigAdminsitrador";
            PanelConfigAdminsitrador.Size = new Size(318, 308);
            PanelConfigAdminsitrador.TabIndex = 20;
            PanelConfigAdminsitrador.Visible = false;
            // 
            // comboBoxNroLector
            // 
            comboBoxNroLector.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxNroLector.FormattingEnabled = true;
            comboBoxNroLector.Items.AddRange(new object[] { "Lector 1", "Lector 2" });
            comboBoxNroLector.Location = new Point(90, 168);
            comboBoxNroLector.Name = "comboBoxNroLector";
            comboBoxNroLector.Size = new Size(121, 23);
            comboBoxNroLector.TabIndex = 31;
            comboBoxNroLector.SelectedIndexChanged += comboBoxNroLector_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(21, 166);
            label3.Name = "label3";
            label3.Size = new Size(60, 21);
            label3.TabIndex = 30;
            label3.Text = "Lector: ";
            label3.Click += label3_Click;
            // 
            // botonEditarCredenciales
            // 
            botonEditarCredenciales.BackColor = Color.Gainsboro;
            botonEditarCredenciales.Cursor = Cursors.Hand;
            botonEditarCredenciales.FlatAppearance.BorderSize = 0;
            botonEditarCredenciales.FlatStyle = FlatStyle.Flat;
            botonEditarCredenciales.Location = new Point(171, 127);
            botonEditarCredenciales.Margin = new Padding(0);
            botonEditarCredenciales.Name = "botonEditarCredenciales";
            botonEditarCredenciales.Size = new Size(75, 23);
            botonEditarCredenciales.TabIndex = 29;
            botonEditarCredenciales.Text = "Editar";
            botonEditarCredenciales.UseVisualStyleBackColor = false;
            botonEditarCredenciales.Click += botonEditarCredenciales_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(21, 127);
            label2.Name = "label2";
            label2.Size = new Size(142, 21);
            label2.TabIndex = 28;
            label2.Text = "Editar credenciales:";
            label2.Click += label2_Click;
            // 
            // BotonAbrirFileDialog
            // 
            BotonAbrirFileDialog.BackColor = Color.Gainsboro;
            BotonAbrirFileDialog.Cursor = Cursors.Hand;
            BotonAbrirFileDialog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BotonAbrirFileDialog.Location = new Point(263, 90);
            BotonAbrirFileDialog.Name = "BotonAbrirFileDialog";
            BotonAbrirFileDialog.Size = new Size(31, 24);
            BotonAbrirFileDialog.TabIndex = 22;
            BotonAbrirFileDialog.Text = "•••";
            BotonAbrirFileDialog.TextAlign = ContentAlignment.TopCenter;
            BotonAbrirFileDialog.UseVisualStyleBackColor = false;
            BotonAbrirFileDialog.Click += button1_Click;
            // 
            // TextBoxRutaExe
            // 
            TextBoxRutaExe.BackColor = Color.WhiteSmoke;
            TextBoxRutaExe.Location = new Point(106, 90);
            TextBoxRutaExe.Name = "TextBoxRutaExe";
            TextBoxRutaExe.Size = new Size(151, 23);
            TextBoxRutaExe.TabIndex = 21;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(21, 88);
            label1.Name = "label1";
            label1.Size = new Size(79, 21);
            label1.TabIndex = 20;
            label1.Text = "Ruta .exe: ";
            // 
            // LabelAdmin
            // 
            LabelAdmin.AutoSize = true;
            LabelAdmin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelAdmin.Location = new Point(26, 39);
            LabelAdmin.Name = "LabelAdmin";
            LabelAdmin.Size = new Size(110, 21);
            LabelAdmin.TabIndex = 21;
            LabelAdmin.Text = "Administrador";
            // 
            // TextBoxAdmin
            // 
            TextBoxAdmin.Cursor = Cursors.IBeam;
            TextBoxAdmin.Location = new Point(142, 39);
            TextBoxAdmin.Name = "TextBoxAdmin";
            TextBoxAdmin.Size = new Size(111, 23);
            TextBoxAdmin.TabIndex = 22;
            TextBoxAdmin.UseSystemPasswordChar = true;
            // 
            // BotonIngresarAdmin
            // 
            BotonIngresarAdmin.BackColor = Color.WhiteSmoke;
            BotonIngresarAdmin.Cursor = Cursors.Hand;
            BotonIngresarAdmin.FlatAppearance.BorderSize = 0;
            BotonIngresarAdmin.FlatStyle = FlatStyle.Flat;
            BotonIngresarAdmin.Location = new Point(196, 68);
            BotonIngresarAdmin.Name = "BotonIngresarAdmin";
            BotonIngresarAdmin.Size = new Size(57, 23);
            BotonIngresarAdmin.TabIndex = 23;
            BotonIngresarAdmin.Text = "OK";
            BotonIngresarAdmin.UseVisualStyleBackColor = false;
            BotonIngresarAdmin.Click += BotonIngresarAdmin_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Gainsboro;
            button1.Cursor = Cursors.Hand;
            button1.Image = Properties.Resources.eye;
            button1.Location = new Point(259, 40);
            button1.Name = "button1";
            button1.Size = new Size(35, 22);
            button1.TabIndex = 24;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.Controls.Add(panel1, 1, 0);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Location = new Point(12, 114);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(993, 514);
            tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            panel1.Controls.Add(LabelAdmin);
            panel1.Controls.Add(PanelConfigAdminsitrador);
            panel1.Controls.Add(BotonIngresarAdmin);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(TextBoxAdmin);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(648, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(342, 508);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(propertyGrid1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(639, 508);
            panel2.TabIndex = 1;
            // 
            // botonModoOffline
            // 
            botonModoOffline.BackColor = Color.WhiteSmoke;
            botonModoOffline.Cursor = Cursors.Hand;
            botonModoOffline.FlatAppearance.BorderSize = 0;
            botonModoOffline.FlatStyle = FlatStyle.Flat;
            botonModoOffline.Location = new Point(21, 212);
            botonModoOffline.Name = "botonModoOffline";
            botonModoOffline.Size = new Size(174, 23);
            botonModoOffline.TabIndex = 32;
            botonModoOffline.Text = "Activar modo offline";
            botonModoOffline.UseVisualStyleBackColor = false;
            botonModoOffline.Click += button2_Click;
            // 
            // WFConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkGray;
            ClientSize = new Size(1008, 687);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(guardarCambiosButton);
            Controls.Add(personalizacionTituloLabel);
            Controls.Add(tituloConfig);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(1024, 726);
            MinimizeBox = false;
            MinimumSize = new Size(1024, 726);
            Name = "WFConfiguracion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuracion Dispositivo";
            FormClosing += WFConfiguracion_FormClosing;
            DragEnter += PropertyGrid1_DragEnter;
            DragLeave += PropertyGrid1_DragLeave;
            PanelConfigAdminsitrador.ResumeLayout(false);
            PanelConfigAdminsitrador.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloConfig;
        private Label label6;
        private Label alertaPorcentajeLabel;
        private Label personalizacionTituloLabel;
        private Label opcionesTituloLabel;
        private PropertyGrid propertyGrid1;
        private Button guardarCambiosButton;
        private ComboBox ComboBoxAperturaMolinete;
        private Button BotonOcultarConfig;
        private Panel PanelConfigAdminsitrador;
        private Label LabelAdmin;
        private TextBox TextBoxAdmin;
        private Button BotonIngresarAdmin;
        private Label label1;
        private TextBox TextBoxRutaExe;
        private Button BotonAbrirFileDialog;
        private Button button1;
        private Label label2;
        private Button botonEditarCredenciales;
        private Label label3;
        private ComboBox comboBoxNroLector;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Panel panel2;
        private Button botonModoOffline;
    }
}