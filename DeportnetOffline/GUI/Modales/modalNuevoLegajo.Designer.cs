namespace DeportnetOffline.GUI.Modales
{
    partial class ModalNuevoLegajo
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
            buttonGuardarLegajo = new Button();
            label5 = new Label();
            panel2 = new Panel();
            labelNroTarjetaError = new Label();
            labelPisoDepartamentoError = new Label();
            labelDireccionError = new Label();
            labelTelefonoError = new Label();
            labelEmailError = new Label();
            labelFechaNacimientoError = new Label();
            labelErrorApellido = new Label();
            labelErrorNombre = new Label();
            textBoxEmail = new TextBox();
            textBoxNombre = new TextBox();
            textBoxTelefono = new TextBox();
            label4 = new Label();
            comboBoxGenero = new ComboBox();
            label3 = new Label();
            textBoxDireccion = new TextBox();
            textBoxApellido = new TextBox();
            textBoxPiso = new TextBox();
            label7 = new Label();
            label10 = new Label();
            label6 = new Label();
            textBoxNroTarjeta = new TextBox();
            dateTimePickerFechaNacimiento = new DateTimePicker();
            label1 = new Label();
            label9 = new Label();
            label2 = new Label();
            label8 = new Label();
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
            panel1.Size = new Size(730, 594);
            panel1.TabIndex = 0;
            // 
            // buttonGuardarLegajo
            // 
            buttonGuardarLegajo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonGuardarLegajo.Location = new Point(250, 524);
            buttonGuardarLegajo.Name = "buttonGuardarLegajo";
            buttonGuardarLegajo.Size = new Size(250, 35);
            buttonGuardarLegajo.TabIndex = 19;
            buttonGuardarLegajo.Text = "Guardar nuevo legajo";
            buttonGuardarLegajo.UseVisualStyleBackColor = true;
            buttonGuardarLegajo.Click += buttonGuardarLegajo_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(238, 23);
            label5.Name = "label5";
            label5.Size = new Size(294, 45);
            label5.TabIndex = 21;
            label5.Text = "Crear nuevo legajo";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLight;
            panel2.Controls.Add(labelNroTarjetaError);
            panel2.Controls.Add(labelPisoDepartamentoError);
            panel2.Controls.Add(labelDireccionError);
            panel2.Controls.Add(labelTelefonoError);
            panel2.Controls.Add(labelEmailError);
            panel2.Controls.Add(labelFechaNacimientoError);
            panel2.Controls.Add(labelErrorApellido);
            panel2.Controls.Add(labelErrorNombre);
            panel2.Controls.Add(textBoxEmail);
            panel2.Controls.Add(textBoxNombre);
            panel2.Controls.Add(textBoxTelefono);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(comboBoxGenero);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(textBoxDireccion);
            panel2.Controls.Add(textBoxApellido);
            panel2.Controls.Add(textBoxPiso);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(textBoxNroTarjeta);
            panel2.Controls.Add(dateTimePickerFechaNacimiento);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label8);
            panel2.Location = new Point(51, 71);
            panel2.Name = "panel2";
            panel2.Size = new Size(620, 436);
            panel2.TabIndex = 20;
            // 
            // labelNroTarjetaError
            // 
            labelNroTarjetaError.AutoSize = true;
            labelNroTarjetaError.ForeColor = Color.Red;
            labelNroTarjetaError.Location = new Point(35, 390);
            labelNroTarjetaError.Name = "labelNroTarjetaError";
            labelNroTarjetaError.Size = new Size(145, 15);
            labelNroTarjetaError.TabIndex = 26;
            labelNroTarjetaError.Text = "Solo se permiten números";
            labelNroTarjetaError.Visible = false;
            // 
            // labelPisoDepartamentoError
            // 
            labelPisoDepartamentoError.AutoSize = true;
            labelPisoDepartamentoError.ForeColor = Color.Red;
            labelPisoDepartamentoError.Location = new Point(343, 312);
            labelPisoDepartamentoError.Name = "labelPisoDepartamentoError";
            labelPisoDepartamentoError.Size = new Size(203, 15);
            labelPisoDepartamentoError.TabIndex = 25;
            labelPisoDepartamentoError.Text = "No se permiten caracteres especiales ";
            labelPisoDepartamentoError.Visible = false;
            // 
            // labelDireccionError
            // 
            labelDireccionError.AutoSize = true;
            labelDireccionError.ForeColor = Color.Red;
            labelDireccionError.Location = new Point(35, 311);
            labelDireccionError.Name = "labelDireccionError";
            labelDireccionError.Size = new Size(200, 15);
            labelDireccionError.TabIndex = 24;
            labelDireccionError.Text = "No se permiten caracteres especiales";
            labelDireccionError.Visible = false;
            // 
            // labelTelefonoError
            // 
            labelTelefonoError.AutoSize = true;
            labelTelefonoError.ForeColor = Color.Red;
            labelTelefonoError.Location = new Point(344, 232);
            labelTelefonoError.Name = "labelTelefonoError";
            labelTelefonoError.Size = new Size(145, 15);
            labelTelefonoError.TabIndex = 23;
            labelTelefonoError.Text = "Solo se permiten números";
            labelTelefonoError.Visible = false;
            // 
            // labelEmailError
            // 
            labelEmailError.AutoSize = true;
            labelEmailError.ForeColor = Color.Red;
            labelEmailError.Location = new Point(35, 233);
            labelEmailError.Name = "labelEmailError";
            labelEmailError.Size = new Size(262, 15);
            labelEmailError.TabIndex = 22;
            labelEmailError.Text = "Ingrese un correo valido (ej: correo@gmail.com)";
            labelEmailError.Visible = false;
            // 
            // labelFechaNacimientoError
            // 
            labelFechaNacimientoError.AutoSize = true;
            labelFechaNacimientoError.ForeColor = Color.Red;
            labelFechaNacimientoError.Location = new Point(343, 158);
            labelFechaNacimientoError.Name = "labelFechaNacimientoError";
            labelFechaNacimientoError.Size = new Size(174, 15);
            labelFechaNacimientoError.TabIndex = 21;
            labelFechaNacimientoError.Text = "La fecha debe ser anterior a hoy";
            labelFechaNacimientoError.Visible = false;
            // 
            // labelErrorApellido
            // 
            labelErrorApellido.AutoSize = true;
            labelErrorApellido.BackColor = Color.Transparent;
            labelErrorApellido.ForeColor = Color.Red;
            labelErrorApellido.Location = new Point(343, 78);
            labelErrorApellido.Name = "labelErrorApellido";
            labelErrorApellido.Size = new Size(183, 15);
            labelErrorApellido.TabIndex = 20;
            labelErrorApellido.Text = "Solo se permiten letras y espacios";
            labelErrorApellido.Visible = false;
            // 
            // labelErrorNombre
            // 
            labelErrorNombre.AutoSize = true;
            labelErrorNombre.BackColor = Color.Transparent;
            labelErrorNombre.ForeColor = Color.Red;
            labelErrorNombre.Location = new Point(35, 78);
            labelErrorNombre.Name = "labelErrorNombre";
            labelErrorNombre.Size = new Size(183, 15);
            labelErrorNombre.TabIndex = 19;
            labelErrorNombre.Text = "Solo se permiten letras y espacios";
            labelErrorNombre.Visible = false;
            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxEmail.Location = new Point(32, 201);
            textBoxEmail.MaxLength = 100;
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.Size = new Size(250, 29);
            textBoxEmail.TabIndex = 16;
            textBoxEmail.Enter += textBoxEmail_Enter;
            textBoxEmail.Leave += textBoxEmail_Leave;
            textBoxEmail.Validating += textBoxEmail_Validating;
            // 
            // textBoxNombre
            // 
            textBoxNombre.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNombre.Location = new Point(32, 46);
            textBoxNombre.MaxLength = 100;
            textBoxNombre.Name = "textBoxNombre";
            textBoxNombre.Size = new Size(250, 29);
            textBoxNombre.TabIndex = 18;
            textBoxNombre.Enter += textBoxNombre_Enter;
            textBoxNombre.Leave += textBoxNombre_Leave;
            textBoxNombre.Validating += textBoxNombre_Validating;
            // 
            // textBoxTelefono
            // 
            textBoxTelefono.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxTelefono.Location = new Point(340, 201);
            textBoxTelefono.MaxLength = 50;
            textBoxTelefono.Name = "textBoxTelefono";
            textBoxTelefono.Size = new Size(250, 29);
            textBoxTelefono.TabIndex = 15;
            textBoxTelefono.Enter += textBoxTelefono_Enter;
            textBoxTelefono.Leave += textBoxTelefono_Leave;
            textBoxTelefono.Validating += textBoxTelefono_Validating;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(340, 175);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 3;
            label4.Text = "Telefono";
            // 
            // comboBoxGenero
            // 
            comboBoxGenero.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxGenero.FormattingEnabled = true;
            comboBoxGenero.Items.AddRange(new object[] { "m", "f" });
            comboBoxGenero.Location = new Point(32, 127);
            comboBoxGenero.Name = "comboBoxGenero";
            comboBoxGenero.Size = new Size(100, 29);
            comboBoxGenero.TabIndex = 10;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(32, 175);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 2;
            label3.Text = "Email*";
            // 
            // textBoxDireccion
            // 
            textBoxDireccion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxDireccion.Location = new Point(32, 280);
            textBoxDireccion.MaxLength = 100;
            textBoxDireccion.Name = "textBoxDireccion";
            textBoxDireccion.Size = new Size(250, 29);
            textBoxDireccion.TabIndex = 14;
            textBoxDireccion.Enter += textBoxDireccion_Enter;
            textBoxDireccion.Leave += textBoxDireccion_Leave;
            textBoxDireccion.Validating += textBoxDireccion_Validating;
            // 
            // textBoxApellido
            // 
            textBoxApellido.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxApellido.Location = new Point(340, 46);
            textBoxApellido.MaxLength = 100;
            textBoxApellido.Name = "textBoxApellido";
            textBoxApellido.Size = new Size(250, 29);
            textBoxApellido.TabIndex = 17;
            textBoxApellido.Enter += textBoxApellido_Enter;
            textBoxApellido.Leave += textBoxApellido_Leave;
            textBoxApellido.Validating += textBoxApellido_Validating;
            // 
            // textBoxPiso
            // 
            textBoxPiso.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxPiso.Location = new Point(340, 280);
            textBoxPiso.MaxLength = 5;
            textBoxPiso.Name = "textBoxPiso";
            textBoxPiso.Size = new Size(100, 29);
            textBoxPiso.TabIndex = 13;
            textBoxPiso.Enter += textBoxDireccionConPiso_Enter;
            textBoxPiso.Leave += textBoxDireccionConPiso_Leave;
            textBoxPiso.Validating += textBoxPiso_Validating;
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(340, 254);
            label7.Name = "label7";
            label7.Size = new Size(153, 23);
            label7.TabIndex = 6;
            label7.Text = "Piso / Departamento";
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(32, 103);
            label10.Name = "label10";
            label10.Size = new Size(100, 23);
            label10.TabIndex = 9;
            label10.Text = "Genero*";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(32, 254);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 5;
            label6.Text = "Dirección";
            // 
            // textBoxNroTarjeta
            // 
            textBoxNroTarjeta.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxNroTarjeta.Location = new Point(32, 358);
            textBoxNroTarjeta.MaxLength = 11;
            textBoxNroTarjeta.Name = "textBoxNroTarjeta";
            textBoxNroTarjeta.Size = new Size(141, 29);
            textBoxNroTarjeta.TabIndex = 12;
            textBoxNroTarjeta.Enter += textBoxNroTarjeta_Enter;
            textBoxNroTarjeta.Leave += textBoxNroTarjeta_Leave;
            textBoxNroTarjeta.Validating += textBoxNroTarjeta_Validating;
            // 
            // dateTimePickerFechaNacimiento
            // 
            dateTimePickerFechaNacimiento.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dateTimePickerFechaNacimiento.Format = DateTimePickerFormat.Short;
            dateTimePickerFechaNacimiento.Location = new Point(340, 127);
            dateTimePickerFechaNacimiento.MaxDate = new DateTime(2209, 12, 31, 0, 0, 0, 0);
            dateTimePickerFechaNacimiento.MinDate = new DateTime(1900, 12, 31, 0, 0, 0, 0);
            dateTimePickerFechaNacimiento.Name = "dateTimePickerFechaNacimiento";
            dateTimePickerFechaNacimiento.Size = new Size(141, 29);
            dateTimePickerFechaNacimiento.TabIndex = 11;
            dateTimePickerFechaNacimiento.Validating += dateTimePickerFechaNacimiento_Validating;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(32, 20);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 0;
            label1.Text = "Nombre*";
            // 
            // label9
            // 
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(32, 332);
            label9.Name = "label9";
            label9.Size = new Size(141, 23);
            label9.TabIndex = 8;
            label9.Text = "Numero de tarjeta*";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(340, 20);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 1;
            label2.Text = "Apellido*";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(340, 103);
            label8.Name = "label8";
            label8.Size = new Size(177, 23);
            label8.TabIndex = 7;
            label8.Text = "Fecha de nacimiento*";
            // 
            // ModalNuevoLegajo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(730, 594);
            Controls.Add(panel1);
            Name = "ModalNuevoLegajo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nuevo Legajo";
            Load += modalNuevoLegajo_Load;
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
        private TextBox textBoxPiso;
        private TextBox textBoxNroTarjeta;
        private DateTimePicker dateTimePickerFechaNacimiento;
        private ComboBox comboBoxGenero;
        private Panel panel2;
        private Button buttonGuardarLegajo;
        private Label label5;
        private Label labelErrorNombre;
        private Label labelErrorApellido;
        private Label labelEmailError;
        private Label labelFechaNacimientoError;
        private Label labelNroTarjetaError;
        private Label labelPisoDepartamentoError;
        private Label labelDireccionError;
        private Label labelTelefonoError;
    }
}