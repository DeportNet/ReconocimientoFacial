namespace DeportNetReconocimiento.GUI
{
    partial class WFSeleccionarUsuario
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
            label1 = new Label();
            comboBox1 = new ComboBox();
            panel1 = new Panel();
            mensajeErrorLabel = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            linkLabel1 = new LinkLabel();
            label2 = new Label();
            button1 = new Button();
            label4 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.FromArgb(33, 33, 33);
            label1.Location = new Point(71, 129);
            label1.Name = "label1";
            label1.Size = new Size(442, 25);
            label1.TabIndex = 0;
            label1.Text = "Seleccione un usuario para ingresar a {nombreSuc}";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(558, 125);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(269, 33);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(mensajeErrorLabel);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(71, 207);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(757, 153);
            panel1.TabIndex = 2;
            panel1.Visible = false;
            // 
            // mensajeErrorLabel
            // 
            mensajeErrorLabel.AutoSize = true;
            mensajeErrorLabel.ForeColor = Color.Red;
            mensajeErrorLabel.Location = new Point(578, 71);
            mensajeErrorLabel.Name = "mensajeErrorLabel";
            mensajeErrorLabel.Size = new Size(153, 20);
            mensajeErrorLabel.TabIndex = 4;
            mensajeErrorLabel.Text = "Contraseña Incorrecta";
            mensajeErrorLabel.Visible = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(453, 36);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(266, 27);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.ForeColor = Color.FromArgb(33, 33, 33);
            label3.Location = new Point(29, 99);
            label3.Name = "label3";
            label3.Size = new Size(207, 25);
            label3.TabIndex = 2;
            label3.Text = "¿Olvidó su contraseña?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(242, 104);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(265, 20);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Ingrese sin usuario momentáneamente";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.FromArgb(33, 33, 33);
            label2.Location = new Point(25, 35);
            label2.Name = "label2";
            label2.Size = new Size(395, 25);
            label2.TabIndex = 0;
            label2.Text = "Ingrese la contraseña de {Nombre} {Apellido}:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.LimeGreen;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(33, 33, 33);
            button1.Location = new Point(262, 407);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(406, 55);
            button1.TabIndex = 3;
            button1.Text = "Seleccionar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(33, 33, 33);
            label4.Location = new Point(345, 36);
            label4.Name = "label4";
            label4.Size = new Size(282, 37);
            label4.TabIndex = 4;
            label4.Text = "Seleccion de Usuario";
            // 
            // WFSeleccionarUsuario
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            ClientSize = new Size(919, 493);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "WFSeleccionarUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Load += WFSeleccionarUsuario_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Panel panel1;
        private TextBox textBox1;
        private Label label3;
        private LinkLabel linkLabel1;
        private Label label2;
        private Button button1;
        private Label label4;
        private Label mensajeErrorLabel;
    }
}