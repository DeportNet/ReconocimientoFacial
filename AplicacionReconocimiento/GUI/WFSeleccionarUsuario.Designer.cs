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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFSeleccionarUsuario));
            label1 = new Label();
            comboBox1 = new ComboBox();
            panel1 = new Panel();
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
            label1.Location = new Point(62, 97);
            label1.Name = "label1";
            label1.Size = new Size(226, 20);
            label1.TabIndex = 0;
            label1.Text = "Listado de usuarios de {sucursal}:";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Prueba" });
            comboBox1.Location = new Point(382, 94);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(185, 28);
            comboBox1.TabIndex = 1;
            comboBox1.Text = "Seleccione...";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = SystemColors.ControlLight;
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(62, 158);
            panel1.Name = "panel1";
            panel1.Size = new Size(505, 115);
            panel1.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(341, 23);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(152, 23);
            textBox1.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.ForeColor = Color.FromArgb(33, 33, 33);
            label3.Location = new Point(25, 74);
            label3.Name = "label3";
            label3.Size = new Size(161, 20);
            label3.TabIndex = 2;
            label3.Text = "¿Olvidó su contraseña?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(192, 78);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(213, 15);
            linkLabel1.TabIndex = 1;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Ingrese sin usuario momentáneamente";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.FromArgb(33, 33, 33);
            label2.Location = new Point(22, 26);
            label2.Name = "label2";
            label2.Size = new Size(313, 20);
            label2.TabIndex = 0;
            label2.Text = "Ingrese la contraseña de {Nombre} {Apellido}:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.LimeGreen;
            button1.Cursor = Cursors.Hand;
            button1.Enabled = false;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(33, 33, 33);
            button1.Location = new Point(229, 305);
            button1.Name = "button1";
            button1.Size = new Size(198, 41);
            button1.TabIndex = 3;
            button1.Text = "Seleccionar";
            button1.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(33, 33, 33);
            label4.Location = new Point(219, 23);
            label4.Name = "label4";
            label4.Size = new Size(226, 30);
            label4.TabIndex = 4;
            label4.Text = "Seleccion de Usuario";
            // 
            // WFSeleccionarUsuario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(647, 370);
            ControlBox = false;
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "WFSeleccionarUsuario";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
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
    }
}