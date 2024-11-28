namespace DeportNetReconocimiento
{
    partial class WFRgistrarDispositivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFRgistrarDispositivo));
            panel1 = new Panel();
            label1 = new Label();
            btnCancel = new Button();
            btnAdd = new Button();
            textBoxPassword = new TextBox();
            label5 = new Label();
            label3 = new Label();
            textBoxPort = new TextBox();
            textBoxUserName = new TextBox();
            label4 = new Label();
            label2 = new Label();
            textBoxDeviceAddress = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(553, 82);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Consolas", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(71, 19);
            label1.Name = "label1";
            label1.Size = new Size(417, 41);
            label1.TabIndex = 2;
            label1.Text = "Registrar Dispositivo";
            // 
            // btnCancel
            // 
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(314, 253);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 29);
            btnCancel.TabIndex = 63;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Location = new Point(131, 253);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(103, 29);
            btnAdd.TabIndex = 62;
            btnAdd.Text = "Login";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPassword.Location = new Point(225, 198);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(250, 22);
            textBoxPassword.TabIndex = 60;
            textBoxPassword.Text = "hik12345";
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Consolas", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.DimGray;
            label5.Location = new Point(131, 203);
            label5.Name = "label5";
            label5.Size = new Size(88, 17);
            label5.TabIndex = 61;
            label5.Text = "Contraseña";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Consolas", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DimGray;
            label3.Location = new Point(163, 168);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 59;
            label3.Text = "Puerto";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(225, 167);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(250, 22);
            textBoxPort.TabIndex = 58;
            textBoxPort.Text = "8000";
            // 
            // textBoxUserName
            // 
            textBoxUserName.Location = new Point(225, 135);
            textBoxUserName.Name = "textBoxUserName";
            textBoxUserName.Size = new Size(250, 22);
            textBoxUserName.TabIndex = 56;
            textBoxUserName.Text = "admin";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Consolas", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.DimGray;
            label4.Location = new Point(99, 136);
            label4.Name = "label4";
            label4.Size = new Size(120, 17);
            label4.TabIndex = 57;
            label4.Text = "Nombre Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Consolas", 10.5F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.DimGray;
            label2.Location = new Point(19, 105);
            label2.Name = "label2";
            label2.Size = new Size(200, 17);
            label2.TabIndex = 55;
            label2.Text = "Direccion IP Dispositivo";
            // 
            // textBoxDeviceAddress
            // 
            textBoxDeviceAddress.Location = new Point(225, 104);
            textBoxDeviceAddress.Name = "textBoxDeviceAddress";
            textBoxDeviceAddress.Size = new Size(250, 22);
            textBoxDeviceAddress.TabIndex = 54;
            textBoxDeviceAddress.Text = "10.21.80.42";
            // 
            // WFRgistrarDispositivo
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 321);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(textBoxPassword);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(textBoxPort);
            Controls.Add(textBoxUserName);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBoxDeviceAddress);
            Controls.Add(panel1);
            Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "WFRgistrarDispositivo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrar Dispositivo";
            FormClosing += cerrarFormulario;
            Load += WFRgistrarDispositivo_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDeviceAddress;
    }
}