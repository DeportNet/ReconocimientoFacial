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
            panel1.Location = new Point(0, 197);
            panel1.Name = "panel1";
            panel1.Size = new Size(484, 64);
            panel1.TabIndex = 0;
            // 
            // BotonNo
            // 
            BotonNo.Location = new Point(313, 12);
            BotonNo.Name = "BotonNo";
            BotonNo.Size = new Size(100, 40);
            BotonNo.TabIndex = 1;
            BotonNo.Text = "No";
            BotonNo.UseVisualStyleBackColor = true;
            BotonNo.Click += BotonNo_Click;
            // 
            // botonSi
            // 
            botonSi.Location = new Point(85, 12);
            botonSi.Name = "botonSi";
            botonSi.Size = new Size(100, 40);
            botonSi.TabIndex = 0;
            botonSi.Text = "Sí";
            botonSi.UseVisualStyleBackColor = true;
            botonSi.Click += BotonSi_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(216, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 2;
            label1.Text = "Atencion!";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.Window;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.DetectUrls = false;
            richTextBox1.Location = new Point(18, 33);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            richTextBox1.Size = new Size(454, 158);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.UseWaitCursor = true;
            // 
            // HTMLMessageBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 261);
            ControlBox = false;
            Controls.Add(richTextBox1);
            Controls.Add(label1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "HTMLMessageBox";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pregunta";
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