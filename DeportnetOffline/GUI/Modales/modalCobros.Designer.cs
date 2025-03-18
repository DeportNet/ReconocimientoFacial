namespace DeportnetOffline
{
    partial class modalCobro
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
            panel1 = new Panel();
            labelNombreApelldioCliente = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            panel2 = new Panel();
            buttonCobrar = new Button();
            panel3 = new Panel();
            label4 = new Label();
            labelVigencia = new Label();
            labelCaracteristicasCobro = new Label();
            labelDescripcion = new Label();
            labelCantidad = new Label();
            labelPrecio = new Label();
            label3 = new Label();
            label2 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(labelNombreApelldioCliente);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 0;
            // 
            // labelNombreApelldioCliente
            // 
            labelNombreApelldioCliente.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            labelNombreApelldioCliente.Location = new Point(151, 54);
            labelNombreApelldioCliente.Name = "labelNombreApelldioCliente";
            labelNombreApelldioCliente.Size = new Size(488, 34);
            labelNombreApelldioCliente.TabIndex = 3;
            labelNombreApelldioCliente.Text = "Nombre y apellido del cliente";
            labelNombreApelldioCliente.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(268, 112);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(229, 29);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(246, 9);
            label1.Name = "label1";
            label1.Size = new Size(281, 45);
            label1.TabIndex = 1;
            label1.Text = "Cobrar servicio a: ";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Gainsboro;
            panel2.Controls.Add(buttonCobrar);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(labelPrecio);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(63, 210);
            panel2.Name = "panel2";
            panel2.Size = new Size(677, 175);
            panel2.TabIndex = 0;
            // 
            // buttonCobrar
            // 
            buttonCobrar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCobrar.Location = new Point(530, 128);
            buttonCobrar.Name = "buttonCobrar";
            buttonCobrar.Size = new Size(120, 35);
            buttonCobrar.TabIndex = 9;
            buttonCobrar.Text = "Cobrar";
            buttonCobrar.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Silver;
            panel3.Controls.Add(label4);
            panel3.Controls.Add(labelVigencia);
            panel3.Controls.Add(labelCaracteristicasCobro);
            panel3.Controls.Add(labelDescripcion);
            panel3.Controls.Add(labelCantidad);
            panel3.Location = new Point(24, 49);
            panel3.Name = "panel3";
            panel3.Size = new Size(626, 67);
            panel3.TabIndex = 8;
            panel3.Paint += panel3_Paint;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(501, 31);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 7;
            label4.Text = "Importe";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelVigencia
            // 
            labelVigencia.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelVigencia.Location = new Point(302, 31);
            labelVigencia.Name = "labelVigencia";
            labelVigencia.Size = new Size(190, 23);
            labelVigencia.TabIndex = 6;
            labelVigencia.Text = "Vigencia";
            labelVigencia.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCaracteristicasCobro
            // 
            labelCaracteristicasCobro.AutoSize = true;
            labelCaracteristicasCobro.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelCaracteristicasCobro.Location = new Point(12, 10);
            labelCaracteristicasCobro.Name = "labelCaracteristicasCobro";
            labelCaracteristicasCobro.Size = new Size(575, 21);
            labelCaracteristicasCobro.TabIndex = 3;
            labelCaracteristicasCobro.Text = "Cantidad    |             Descripción              |                 Vigencia                 |     Importe";
            labelCaracteristicasCobro.Click += labelCaracteristicasCobro_Click;
            // 
            // labelDescripcion
            // 
            labelDescripcion.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDescripcion.Location = new Point(103, 30);
            labelDescripcion.Name = "labelDescripcion";
            labelDescripcion.Size = new Size(186, 23);
            labelDescripcion.TabIndex = 5;
            labelDescripcion.Text = "Descripción";
            labelDescripcion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelCantidad
            // 
            labelCantidad.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelCantidad.Location = new Point(17, 28);
            labelCantidad.Name = "labelCantidad";
            labelCantidad.Size = new Size(67, 23);
            labelCantidad.TabIndex = 4;
            labelCantidad.Text = "cantidad";
            labelCantidad.TextAlign = ContentAlignment.MiddleCenter;
            labelCantidad.Click += labelCantidad_Click;
            // 
            // labelPrecio
            // 
            labelPrecio.AutoSize = true;
            labelPrecio.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPrecio.Location = new Point(88, 12);
            labelPrecio.Name = "labelPrecio";
            labelPrecio.Size = new Size(51, 20);
            labelPrecio.TabIndex = 2;
            labelPrecio.Text = "precio";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(26, 12);
            label3.Name = "label3";
            label3.Size = new Size(56, 20);
            label3.TabIndex = 1;
            label3.Text = "Total:  ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(85, 172);
            label2.Name = "label2";
            label2.Size = new Size(171, 30);
            label2.TabIndex = 0;
            label2.Text = "Detalle del cobro";
            // 
            // modalCobro
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Name = "modalCobro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cobro servicios";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private ComboBox comboBox1;
        private Label labelCaracteristicasCobro;
        private Label labelPrecio;
        private Label label3;
        private Label label2;
        private Label labelCantidad;
        private Label label4;
        private Label labelVigencia;
        private Label labelDescripcion;
        private Panel panel3;
        private Button buttonCobrar;
        private Label labelNombreApelldioCliente;
    }
}