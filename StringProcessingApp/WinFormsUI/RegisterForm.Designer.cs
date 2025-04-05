using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using StringProcessingApp.Exceptions;
using StringProcessingApp.Helpers;
using StringProcessingApp.Interfaces;
using StringProcessingApp.Models;
using StringProcessingApp.Services;

namespace StringProcessingApp.WinFormsUI
{
    public partial class RegisterForm : Form
    {
        private readonly IUserRepository _userRepository;



        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear previous error messages
                lblNameError.Text = "";
                lblLastNameError.Text = "";
                lblEmailError.Text = "";
                lblPhoneError.Text = "";
                lblCodenameError.Text = "";
                lblPasswordError.Text = "";
                lblConfirmPasswordError.Text = "";

                // Validate all fields
                ValidateFields();

                // Check if codename or email already exists
                var existingUser = _userRepository.GetUserByCodename(txtCodename.Text);
                if (existingUser != null)
                {
                    lblCodenameError.Text = "Codename already exists";
                    return;
                }

                existingUser = _userRepository.GetUserByEmail(txtEmail.Text);
                if (existingUser != null)
                {
                    lblEmailError.Text = "Email already exists";
                    return;
                }

                // Create and save new user
                User newUser = new User
                {
                    name = txtName.Text,
                    last_name = txtLastName.Text,
                    email = txtEmail.Text,
                    phone_number = txtPhone.Text,
                    codename = txtCodename.Text,
                    password = txtPassword.Text,
                    role = "User" // Default role
                };

                _userRepository.AddUser(newUser);

                MessageBox.Show("Registration successful! You can now login with your codename and password.",
                    "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateFields()
        {
            // Validate name
            if (string.IsNullOrEmpty(txtName.Text))
            {
                lblNameError.Text = "Name cannot be empty";
                throw new InvalidInputException("Name cannot be empty");
            }

            // Validate last name
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                lblLastNameError.Text = "Last name cannot be empty";
                throw new InvalidInputException("Last name cannot be empty");
            }

            // Validate email
            try
            {
                Validator.ValidateEmail(txtEmail.Text);
            }
            catch (InvalidInputException ex)
            {
                lblEmailError.Text = ex.Message;
                throw;
            }

            // Validate phone number
            try
            {
                Validator.ValidatePhoneNumber(txtPhone.Text);
            }
            catch (InvalidInputException ex)
            {
                lblPhoneError.Text = ex.Message;
                throw;
            }

            // Validate codename
            try
            {
                Validator.ValidateCodename(txtCodename.Text);
            }
            catch (InvalidInputException ex)
            {
                lblCodenameError.Text = ex.Message;
                throw;
            }

            // Validate password
            try
            {
                Validator.ValidatePassword(txtPassword.Text, txtName.Text, txtLastName.Text,
                    txtEmail.Text, txtPhone.Text, txtCodename.Text);
            }
            catch (InvalidInputException ex)
            {
                lblPasswordError.Text = ex.Message;
                throw;
            }

            // Validate confirm password
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblConfirmPasswordError.Text = "Passwords do not match";
                throw new InvalidInputException("Passwords do not match");
            }
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            lblLastName = new Label();
            lblEmail = new Label();
            lblPhone = new Label();
            lblCodename = new Label();
            lblPassword = new Label();
            lblConfirmPassword = new Label();
            txtName = new TextBox();
            txtLastName = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            txtCodename = new TextBox();
            txtPassword = new TextBox();
            txtConfirmPassword = new TextBox();
            btnRegister = new Button();
            btnCancel = new Button();
            lblNameError = new Label();
            lblLastNameError = new Label();
            lblEmailError = new Label();
            lblPhoneError = new Label();
            lblCodenameError = new Label();
            lblPasswordError = new Label();
            lblConfirmPasswordError = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(584, 77);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(286, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Account Registration";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(267, 166);
            lblName.Name = "lblName";
            lblName.Size = new Size(52, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name:";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(747, 166);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(82, 20);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Last Name:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(270, 242);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 3;
            lblEmail.Text = "Email:";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(736, 242);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(111, 20);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Phone Number:";
            // 
            // lblCodename
            // 
            lblCodename.AutoSize = true;
            lblCodename.Location = new Point(253, 322);
            lblCodename.Name = "lblCodename";
            lblCodename.Size = new Size(84, 20);
            lblCodename.TabIndex = 5;
            lblCodename.Text = "Codename:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(756, 322);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password:";
            // 
            // lblConfirmPassword
            // 
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(727, 406);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(130, 20);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Confirm Password:";
            // 
            // txtName
            // 
            txtName.Location = new Point(415, 159);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(250, 27);
            txtName.TabIndex = 8;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(882, 159);
            txtLastName.Margin = new Padding(3, 4, 3, 4);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(250, 27);
            txtLastName.TabIndex = 9;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(415, 235);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(250, 27);
            txtEmail.TabIndex = 10;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(882, 235);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(250, 27);
            txtPhone.TabIndex = 11;
            // 
            // txtCodename
            // 
            txtCodename.Location = new Point(415, 315);
            txtCodename.Margin = new Padding(3, 4, 3, 4);
            txtCodename.Name = "txtCodename";
            txtCodename.Size = new Size(250, 27);
            txtCodename.TabIndex = 12;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(882, 315);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(250, 27);
            txtPassword.TabIndex = 13;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(882, 403);
            txtConfirmPassword.Margin = new Padding(3, 4, 3, 4);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.PasswordChar = '*';
            txtConfirmPassword.Size = new Size(250, 27);
            txtConfirmPassword.TabIndex = 14;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(456, 485);
            btnRegister.Margin = new Padding(3, 4, 3, 4);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(100, 38);
            btnRegister.TabIndex = 15;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(873, 485);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 38);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblNameError
            // 
            lblNameError.AutoSize = true;
            lblNameError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNameError.ForeColor = Color.Red;
            lblNameError.Location = new Point(190, 115);
            lblNameError.Name = "lblNameError";
            lblNameError.Size = new Size(0, 15);
            lblNameError.TabIndex = 17;
            // 
            // lblLastNameError
            // 
            lblLastNameError.AutoSize = true;
            lblLastNameError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLastNameError.ForeColor = Color.Red;
            lblLastNameError.Location = new Point(190, 165);
            lblLastNameError.Name = "lblLastNameError";
            lblLastNameError.Size = new Size(0, 15);
            lblLastNameError.TabIndex = 18;
            // 
            // lblEmailError
            // 
            lblEmailError.AutoSize = true;
            lblEmailError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEmailError.ForeColor = Color.Red;
            lblEmailError.Location = new Point(190, 215);
            lblEmailError.Name = "lblEmailError";
            lblEmailError.Size = new Size(0, 15);
            lblEmailError.TabIndex = 19;
            // 
            // lblPhoneError
            // 
            lblPhoneError.AutoSize = true;
            lblPhoneError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPhoneError.ForeColor = Color.Red;
            lblPhoneError.Location = new Point(190, 265);
            lblPhoneError.Name = "lblPhoneError";
            lblPhoneError.Size = new Size(0, 15);
            lblPhoneError.TabIndex = 20;
            // 
            // lblCodenameError
            // 
            lblCodenameError.AutoSize = true;
            lblCodenameError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCodenameError.ForeColor = Color.Red;
            lblCodenameError.Location = new Point(190, 315);
            lblCodenameError.Name = "lblCodenameError";
            lblCodenameError.Size = new Size(0, 15);
            lblCodenameError.TabIndex = 21;
            // 
            // lblPasswordError
            // 
            lblPasswordError.AutoSize = true;
            lblPasswordError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPasswordError.ForeColor = Color.Red;
            lblPasswordError.Location = new Point(190, 365);
            lblPasswordError.Name = "lblPasswordError";
            lblPasswordError.Size = new Size(0, 15);
            lblPasswordError.TabIndex = 22;
            // 
            // lblConfirmPasswordError
            // 
            lblConfirmPasswordError.AutoSize = true;
            lblConfirmPasswordError.Font = new Font("Microsoft Sans Serif", 7F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblConfirmPasswordError.ForeColor = Color.Red;
            lblConfirmPasswordError.Location = new Point(190, 415);
            lblConfirmPasswordError.Name = "lblConfirmPasswordError";
            lblConfirmPasswordError.Size = new Size(0, 15);
            lblConfirmPasswordError.TabIndex = 23;
            // 
            // RegisterForm
            // 
            AcceptButton = btnRegister;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(1478, 860);
            Controls.Add(lblConfirmPasswordError);
            Controls.Add(lblPasswordError);
            Controls.Add(lblCodenameError);
            Controls.Add(lblPhoneError);
            Controls.Add(lblEmailError);
            Controls.Add(lblLastNameError);
            Controls.Add(lblNameError);
            Controls.Add(btnCancel);
            Controls.Add(btnRegister);
            Controls.Add(txtConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(txtCodename);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(txtLastName);
            Controls.Add(txtName);
            Controls.Add(lblConfirmPassword);
            Controls.Add(lblPassword);
            Controls.Add(lblCodename);
            Controls.Add(lblPhone);
            Controls.Add(lblEmail);
            Controls.Add(lblLastName);
            Controls.Add(lblName);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCodename;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtCodename;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNameError;
        private System.Windows.Forms.Label lblLastNameError;
        private System.Windows.Forms.Label lblEmailError;
        private System.Windows.Forms.Label lblPhoneError;
        private System.Windows.Forms.Label lblCodenameError;
        private System.Windows.Forms.Label lblPasswordError;
        private System.Windows.Forms.Label lblConfirmPasswordError;
    }
}
