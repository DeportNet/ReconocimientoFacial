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
            tokenIdentificadorLabel = new Label();
            personalizacionTituloLabel = new Label();
            opcionesTituloLabel = new Label();
            propertyGrid1 = new PropertyGrid();
            guardarCambiosButton = new Button();
            TextBoxToken = new TextBox();
            ComboBoxAperturaMolinete = new ComboBox();
            BotonOcultarConfig = new Button();
            PanelConfigAdminsitrador = new Panel();
            BotonAbrirFileDialog = new Button();
            TextBoxRutaExe = new TextBox();
            label1 = new Label();
            LabelAdmin = new Label();
            TextBoxAdmin = new TextBox();
            BotonIngresarAdmin = new Button();
            PanelConfigAdminsitrador.SuspendLayout();
            SuspendLayout();
            // 
            // tituloConfig
            // 
            tituloConfig.AutoSize = true;
            tituloConfig.Font = new Font("Segoe UI", 24F, FontStyle.Bold | FontStyle.Underline);
            tituloConfig.Location = new Point(267, 20);
            tituloConfig.Name = "tituloConfig";
            tituloConfig.Size = new Size(679, 45);
            tituloConfig.TabIndex = 0;
            tituloConfig.Text = "Configuraciones dispositivo reconocimiento";
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
            // 
            // tokenIdentificadorLabel
            // 
            tokenIdentificadorLabel.AutoSize = true;
            tokenIdentificadorLabel.Font = new Font("Segoe UI", 12F);
            tokenIdentificadorLabel.Location = new Point(21, 135);
            tokenIdentificadorLabel.Name = "tokenIdentificadorLabel";
            tokenIdentificadorLabel.Size = new Size(144, 21);
            tokenIdentificadorLabel.TabIndex = 10;
            tokenIdentificadorLabel.Text = "Token identificador:";
            // 
            // personalizacionTituloLabel
            // 
            personalizacionTituloLabel.AutoSize = true;
            personalizacionTituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline);
            personalizacionTituloLabel.Location = new Point(22, 90);
            personalizacionTituloLabel.Name = "personalizacionTituloLabel";
            personalizacionTituloLabel.Size = new Size(131, 21);
            personalizacionTituloLabel.TabIndex = 12;
            personalizacionTituloLabel.Text = "Personalizacion";
            // 
            // opcionesTituloLabel
            // 
            opcionesTituloLabel.AutoSize = true;
            opcionesTituloLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold | FontStyle.Underline);
            opcionesTituloLabel.Location = new Point(21, 3);
            opcionesTituloLabel.Name = "opcionesTituloLabel";
            opcionesTituloLabel.Size = new Size(81, 21);
            opcionesTituloLabel.TabIndex = 13;
            opcionesTituloLabel.Text = "Opciones";
            // 
            // propertyGrid1
            // 
            propertyGrid1.AllowDrop = true;
            propertyGrid1.Location = new Point(22, 124);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new Size(701, 490);
            propertyGrid1.TabIndex = 15;
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
            propertyGrid1.Click += PropertyGrid1_Click;
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
            // TextBoxToken
            // 
            TextBoxToken.Location = new Point(171, 137);
            TextBoxToken.Name = "TextBoxToken";
            TextBoxToken.Size = new Size(121, 23);
            TextBoxToken.TabIndex = 17;
            // 
            // ComboBoxAperturaMolinete
            // 
            ComboBoxAperturaMolinete.FormattingEnabled = true;
            ComboBoxAperturaMolinete.Items.AddRange(new object[] { ".exe", "Hikvision" });
            ComboBoxAperturaMolinete.Location = new Point(171, 46);
            ComboBoxAperturaMolinete.Name = "ComboBoxAperturaMolinete";
            ComboBoxAperturaMolinete.Size = new Size(121, 23);
            ComboBoxAperturaMolinete.TabIndex = 18;
            // 
            // BotonOcultarConfig
            // 
            BotonOcultarConfig.Location = new Point(171, 183);
            BotonOcultarConfig.Name = "BotonOcultarConfig";
            BotonOcultarConfig.Size = new Size(121, 23);
            BotonOcultarConfig.TabIndex = 19;
            BotonOcultarConfig.Text = "Guardar/Ocultar";
            BotonOcultarConfig.UseVisualStyleBackColor = true;
            BotonOcultarConfig.Click += BotonOcultarConfig_Click;
            // 
            // PanelConfigAdminsitrador
            // 
            PanelConfigAdminsitrador.BackColor = Color.IndianRed;
            PanelConfigAdminsitrador.Controls.Add(BotonAbrirFileDialog);
            PanelConfigAdminsitrador.Controls.Add(TextBoxRutaExe);
            PanelConfigAdminsitrador.Controls.Add(label1);
            PanelConfigAdminsitrador.Controls.Add(BotonOcultarConfig);
            PanelConfigAdminsitrador.Controls.Add(ComboBoxAperturaMolinete);
            PanelConfigAdminsitrador.Controls.Add(TextBoxToken);
            PanelConfigAdminsitrador.Controls.Add(opcionesTituloLabel);
            PanelConfigAdminsitrador.Controls.Add(tokenIdentificadorLabel);
            PanelConfigAdminsitrador.Controls.Add(label6);
            PanelConfigAdminsitrador.Location = new Point(827, 267);
            PanelConfigAdminsitrador.Name = "PanelConfigAdminsitrador";
            PanelConfigAdminsitrador.Size = new Size(330, 222);
            PanelConfigAdminsitrador.TabIndex = 20;
            PanelConfigAdminsitrador.Visible = false;
            PanelConfigAdminsitrador.Paint += PanelConfigAdminsitrador_Paint;
            // 
            // BotonAbrirFileDialog
            // 
            BotonAbrirFileDialog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BotonAbrirFileDialog.Location = new Point(275, 89);
            BotonAbrirFileDialog.Name = "BotonAbrirFileDialog";
            BotonAbrirFileDialog.Size = new Size(31, 24);
            BotonAbrirFileDialog.TabIndex = 22;
            BotonAbrirFileDialog.Text = "•••";
            BotonAbrirFileDialog.TextAlign = ContentAlignment.TopCenter;
            BotonAbrirFileDialog.UseVisualStyleBackColor = true;
            BotonAbrirFileDialog.Click += button1_Click;
            // 
            // TextBoxRutaExe
            // 
            TextBoxRutaExe.Location = new Point(106, 90);
            TextBoxRutaExe.Name = "TextBoxRutaExe";
            TextBoxRutaExe.Size = new Size(163, 23);
            TextBoxRutaExe.TabIndex = 21;
            TextBoxRutaExe.TextChanged += TextBoxRutaExe_TextChanged;
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
            label1.Click += label1_Click_1;
            // 
            // LabelAdmin
            // 
            LabelAdmin.AutoSize = true;
            LabelAdmin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelAdmin.Location = new Point(829, 196);
            LabelAdmin.Name = "LabelAdmin";
            LabelAdmin.Size = new Size(110, 21);
            LabelAdmin.TabIndex = 21;
            LabelAdmin.Text = "Administrador";
            LabelAdmin.Click += label1_Click;
            // 
            // TextBoxAdmin
            // 
            TextBoxAdmin.Location = new Point(945, 194);
            TextBoxAdmin.Name = "TextBoxAdmin";
            TextBoxAdmin.Size = new Size(111, 23);
            TextBoxAdmin.TabIndex = 22;
            // 
            // BotonIngresarAdmin
            // 
            BotonIngresarAdmin.Location = new Point(1062, 194);
            BotonIngresarAdmin.Name = "BotonIngresarAdmin";
            BotonIngresarAdmin.Size = new Size(57, 23);
            BotonIngresarAdmin.TabIndex = 23;
            BotonIngresarAdmin.Text = "OK";
            BotonIngresarAdmin.UseVisualStyleBackColor = true;
            BotonIngresarAdmin.Click += BotonIngresarAdmin_Click;
            // 
            // WFConfiguracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(1177, 691);
            Controls.Add(BotonIngresarAdmin);
            Controls.Add(TextBoxAdmin);
            Controls.Add(LabelAdmin);
            Controls.Add(PanelConfigAdminsitrador);
            Controls.Add(guardarCambiosButton);
            Controls.Add(propertyGrid1);
            Controls.Add(personalizacionTituloLabel);
            Controls.Add(tituloConfig);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "WFConfiguracion";
            Text = "Configuracion Dispositivo";
            FormClosing += WFConfiguracion_FormClosing;
            Load += WFConfiguracion_Load;
            DragEnter += PropertyGrid1_DragEnter;
            DragLeave += PropertyGrid1_DragLeave;
            PanelConfigAdminsitrador.ResumeLayout(false);
            PanelConfigAdminsitrador.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label tituloConfig;
        private Label label6;
        private Label tokenIdentificadorLabel;
        private Label alertaPorcentajeLabel;
        private Label personalizacionTituloLabel;
        private Label opcionesTituloLabel;
        private PropertyGrid propertyGrid1;
        private Button guardarCambiosButton;
        private TextBox TextBoxToken;
        private ComboBox ComboBoxAperturaMolinete;
        private Button BotonOcultarConfig;
        private Panel PanelConfigAdminsitrador;
        private Label LabelAdmin;
        private TextBox TextBoxAdmin;
        private Button BotonIngresarAdmin;
        private Label label1;
        private TextBox TextBoxRutaExe;
        private Button BotonAbrirFileDialog;
    }
}