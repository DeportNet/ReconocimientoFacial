namespace DeportnetOffline.GUI.Modales
{
    partial class modalNuevoLegajo
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            comboBoxGenero = new ComboBox();
            dateTimePickerFechaNacimiento = new DateTimePicker();
            textBoxNroTarjeta = new TextBox();
            textBoxDireccionConPiso = new TextBox();
            textBoxDireccion = new TextBox();
            textBoxTelefono = new TextBox();
            textBoxEmail = new TextBox();
            textBoxApellido = new TextBox();
            textBoxNombre = new TextBox();
            buttonGuardarLegajo = new Button();
            panel2 = new Panel();
            label5 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonGuardarLegajo);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(652, 745);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(92, 18);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Nombre";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(92, 85);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Apellido";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(92, 143);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(92, 201);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            label4.Text = "Telefono";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(92, 259);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 5;
            label6.Text = "Dirección";
            label6.Click += label6_Click;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(92, 433);
            label7.Name = "label7";
            label7.Size = new Size(153, 23);
            label7.TabIndex = 6;
            label7.Text = "Piso / Departamento";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(92, 375);
            label8.Name = "label8";
            label8.Size = new Size(153, 23);
            label8.TabIndex = 7;
            label8.Text = "Fecha de nacimiento";
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(92, 317);
            label9.Name = "label9";
            label9.Size = new Size(141, 23);
            label9.TabIndex = 8;
            label9.Text = "Numero de tarjeta";
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(92, 491);
            label10.Name = "label10";
            label10.Size = new Size(100, 23);
            label10.TabIndex = 9;
            label10.Text = "Genero";
            label10.Click += label10_Click;
            // 
            // comboBoxGenero
            // 
            comboBoxGenero.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxGenero.FormattingEnabled = true;
            comboBoxGenero.Items.AddRange(new object[] { "", "m", "f" });
            comboBoxGenero.Location = new Point(92, 517);
            comboBoxGenero.Name = "comboBoxGenero";
            comboBoxGenero.Size = new Size(100, 29);
            comboBoxGenero.TabIndex = 10;
            comboBoxGenero.Text = "m";
            // 
            // dateTimePickerFechaNacimiento
            // 
            dateTimePickerFechaNacimiento.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePickerFechaNacimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaNacimiento.Location = new Point(92, 401);
            dateTimePickerFechaNacimiento.Name = "dateTimePickerFechaNacimiento";
            dateTimePickerFechaNacimiento.Size = new Size(141, 29);
            dateTimePickerFechaNacimiento.TabIndex = 11;
            // 
            // textBoxNroTarjeta
            // 
            textBoxNroTarjeta.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNroTarjeta.Location = new Point(92, 343);
            textBoxNroTarjeta.Name = "textBoxNroTarjeta";
            textBoxNroTarjeta.Size = new Size(141, 29);
            textBoxNroTarjeta.TabIndex = 12;
            textBoxNroTarjeta.Text = "46012735";
            textBoxNroTarjeta.TextChanged += textBoxNroTarjeta_TextChanged;
            // 
            // textBoxDireccionConPiso
            // 
            textBoxDireccionConPiso.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxDireccionConPiso.Location = new Point(92, 459);
            textBoxDireccionConPiso.Name = "textBoxDireccionConPiso";
            textBoxDireccionConPiso.Size = new Size(100, 29);
            textBoxDireccionConPiso.TabIndex = 13;
            textBoxDireccionConPiso.Text = "3C";
            // 
            // textBoxDireccion
            // 
            textBoxDireccion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxDireccion.Location = new Point(92, 285);
            textBoxDireccion.Name = "textBoxDireccion";
            textBoxDireccion.Size = new Size(250, 29);
            textBoxDireccion.TabIndex = 14;
            textBoxDireccion.Text = "Jose ingenieros 1254";
            // 
            // textBoxTelefono
            // 
            textBoxTelefono.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxTelefono.Location = new Point(92, 227);
            textBoxTelefono.Name = "textBoxTelefono";
            textBoxTelefono.Size = new Size(250, 29);
            textBoxTelefono.TabIndex = 15;
            textBoxTelefono.Text = "+542235929828";
            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxEmail.Location = new Point(92, 169);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(250, 29);
            textBoxEmail.TabIndex = 16;
            textBoxEmail.Text = "facundoprocelli@gmail.com";
            // 
            // textBoxApellido
            // 
            textBoxApellido.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxApellido.Location = new Point(92, 111);
            textBoxApellido.Name = "textBoxApellido";
            textBoxApellido.Size = new Size(250, 29);
            textBoxApellido.TabIndex = 17;
            textBoxApellido.Text = "Procelli Santalla";
            // 
            // textBoxNombre
            // 
            textBoxNombre.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNombre.Location = new Point(92, 44);
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(250, 29);
            textBoxNombre.TabIndex = 18;
            textBoxNombre.Text = "Facundo José";
            textBoxNombre.TextChanged += textBox7_TextChanged;
            // 
            // buttonGuardarLegajo
            // 
            buttonGuardarLegajo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGuardarLegajo.Location = new Point(192, 671);
            buttonGuardarLegajo.Name = "buttonGuardarLegajo";
            buttonGuardarLegajo.Size = new Size(250, 35);
            buttonGuardarLegajo.TabIndex = 19;
            buttonGuardarLegajo.Text = "Guardar nuevo legajo";
            buttonGuardarLegajo.UseVisualStyleBackColor = true;
            buttonGuardarLegajo.Click += buttonGuardarLegajo_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightGray;
            panel2.Controls.Add(textBoxEmail);
            panel2.Controls.Add(textBoxNombre);
            panel2.Controls.Add(textBoxTelefono);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(comboBoxGenero);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBoxDireccion);
            panel2.Controls.Add(textBoxApellido);
            panel2.Controls.Add(textBoxDireccionConPiso);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(textBoxNroTarjeta);
            panel2.Controls.Add(dateTimePickerFechaNacimiento);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label8);
            panel2.Location = new Point(109, 73);
            panel2.Name = "panel2";
            panel2.Size = new Size(416, 576);
            panel2.TabIndex = 20;
            panel2.Paint += panel2_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(177, 25);
            label5.Name = "label5";
            label5.Size = new Size(294, 45);
            label5.TabIndex = 21;
            label5.Text = "Crear nuevo legajo";
            // 
            // modalNuevoLegajo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(652, 745);
            Controls.Add(panel1);
            Name = "modalNuevoLegajo";
            Text = "Nuevo Legajo";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label7;
        private Label label6;
        private Label label4;
        private Label label3;
        private Label label9;
        private Label label8;
        private Label label10;
        private TextBox textBoxNombre;
        private TextBox textBoxApellido;
        private TextBox textBoxEmail;
        private TextBox textBoxTelefono;
        private TextBox textBoxDireccion;
        private TextBox textBoxDireccionConPiso;
        private TextBox textBoxNroTarjeta;
        private DateTimePicker dateTimePickerFechaNacimiento;
        private ComboBox comboBoxGenero;
        private Panel panel2;
        private Button buttonGuardarLegajo;
        private Label label5;
    }
}