namespace DeportNetReconocimiento.GUI
{
    partial class HTMLMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HTMLMessageBox));
            panel1 = new Panel();
            BotonNo = new Button();
            botonSi = new Button();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(BotonNo);
            panel1.Controls.Add(botonSi);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 352);
            panel1.Name = "panel1";
            panel1.Size = new Size(870, 48);
            panel1.TabIndex = 0;
            // 
            // BotonNo
            // 
            BotonNo.BackColor = Color.Salmon;
            BotonNo.CausesValidation = false;
            BotonNo.Cursor = Cursors.Hand;
            BotonNo.FlatStyle = FlatStyle.Flat;
            BotonNo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BotonNo.Location = new Point(512, 3);
            BotonNo.Name = "BotonNo";
            BotonNo.Size = new Size(174, 40);
            BotonNo.TabIndex = 0;
            BotonNo.TabStop = false;
            BotonNo.Text = "No";
            BotonNo.UseVisualStyleBackColor = false;
            BotonNo.Click += BotonNo_Click;
            // 
            // botonSi
            // 
            botonSi.BackColor = Color.DarkSeaGreen;
            botonSi.CausesValidation = false;
            botonSi.Cursor = Cursors.Hand;
            botonSi.FlatStyle = FlatStyle.Flat;
            botonSi.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            botonSi.Location = new Point(208, 3);
            botonSi.Name = "botonSi";
            botonSi.Size = new Size(174, 40);
            botonSi.TabIndex = 0;
            botonSi.TabStop = false;
            botonSi.Text = "Sí";
            botonSi.UseVisualStyleBackColor = false;
            botonSi.Click += BotonSi_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(381, 5);
            label1.Name = "label1";
            label1.Size = new Size(125, 32);
            label1.TabIndex = 2;
            label1.Text = "Atención!";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.Window;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.DetectUrls = false;
            richTextBox1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox1.Location = new Point(0, 40);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            richTextBox1.Size = new Size(870, 309);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // HTMLMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 400);
            ControlBox = false;
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(450, 260);
            Name = "HTMLMessageBox";
            StartPosition = FormStartPosition.Manual;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button BotonNo;
        private Button botonSi;
        private Label label1;
        private RichTextBox richTextBox1;
    }
}