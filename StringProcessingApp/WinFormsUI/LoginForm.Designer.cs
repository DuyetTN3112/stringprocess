using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringProcessingApp.Exceptions;
using StringProcessingApp.Services;

namespace StringProcessingApp.WinFormsUI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;
        private TableLayoutPanel mainLayout;


        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodename.Text) || string.IsNullOrEmpty(txtPassword.Text))
                {
                    lblError.Text = "Codename and password are required.";
                    return;
                }

                var authService = ServiceProvider.Instance.AuthService;
                bool loginSuccess = authService.Login(txtCodename.Text, txtPassword.Text);

                if (loginSuccess)
                {
                    this.Hide();  // Ẩn LoginForm sau khi đăng nhập thành công
                    var homePage = new HomePageForm();
                    homePage.ShowDialog();
                    this.Close(); // Đóng LoginForm sau khi HomePageForm đóng
                }
                else
                {
                    lblError.Text = "Invalid codename or password.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error occurred: " + ex.Message;
            }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();

            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                // Registration was successful, clear fields and show success message
                txtCodename.Clear();
                txtPassword.Clear();
                lblError.Text = "Registration successful! Please login.";
            }

            this.Show();
        }

        private void InitializeComponent()
        {
            // Tạo TableLayoutPanel chính để quản lý layout
            mainLayout = new TableLayoutPanel();

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCodename = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtCodename = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lnkRegister = new System.Windows.Forms.LinkLabel();

            // Cấu hình TableLayoutPanel
            mainLayout.SuspendLayout();
            this.SuspendLayout();

            // 
            // mainLayout
            //
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 3;
            mainLayout.RowCount = 7;

            // Thiết lập tỷ lệ cho các cột (left margin, content, right margin)
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

            // Thiết lập tỷ lệ cho các hàng
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // Khoảng trống trên cùng
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Tiêu đề
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Codename
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Password
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F)); // Nút Login
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F)); // Link Register
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 85F)); // Khoảng trống dưới cùng

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "Account Login";
            mainLayout.Controls.Add(this.lblTitle, 1, 1);

            // 
            // Panel cho Codename (Label + TextBox)
            //
            TableLayoutPanel codenamePanel = new TableLayoutPanel();
            codenamePanel.ColumnCount = 2;
            codenamePanel.RowCount = 1;
            codenamePanel.Dock = DockStyle.Fill;
            codenamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            codenamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // lblCodename
            //
            this.lblCodename.AutoSize = true;
            this.lblCodename.Dock = DockStyle.Fill;
            this.lblCodename.TextAlign = ContentAlignment.MiddleRight;
            this.lblCodename.Text = "Codename:";
            codenamePanel.Controls.Add(this.lblCodename, 0, 0);

            // txtCodename
            //
            this.txtCodename.Dock = DockStyle.Fill;
            this.txtCodename.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            codenamePanel.Controls.Add(this.txtCodename, 1, 0);

            mainLayout.Controls.Add(codenamePanel, 1, 2);

            // 
            // Panel cho Password (Label + TextBox)
            //
            TableLayoutPanel passwordPanel = new TableLayoutPanel();
            passwordPanel.ColumnCount = 2;
            passwordPanel.RowCount = 1;
            passwordPanel.Dock = DockStyle.Fill;
            passwordPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            passwordPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));

            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Dock = DockStyle.Fill;
            this.lblPassword.TextAlign = ContentAlignment.MiddleRight;
            this.lblPassword.Text = "Password:";
            passwordPanel.Controls.Add(this.lblPassword, 0, 0);

            // txtPassword
            //
            this.txtPassword.Dock = DockStyle.Fill;
            this.txtPassword.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtPassword.PasswordChar = '*';
            passwordPanel.Controls.Add(this.txtPassword, 1, 0);

            mainLayout.Controls.Add(passwordPanel, 1, 3);

            // 
            // Panel cho nút Login
            //
            Panel loginButtonPanel = new Panel();
            loginButtonPanel.Dock = DockStyle.Fill;

            // btnLogin
            //
            this.btnLogin.Text = "Login";
            this.btnLogin.Size = new Size(120, 35);
            this.btnLogin.Anchor = AnchorStyles.None;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            loginButtonPanel.Controls.Add(this.btnLogin);

            mainLayout.Controls.Add(loginButtonPanel, 1, 4);

            // 
            // Panel cho Register link và Error label
            //
            TableLayoutPanel registerPanel = new TableLayoutPanel();
            registerPanel.ColumnCount = 1;
            registerPanel.RowCount = 2;
            registerPanel.Dock = DockStyle.Fill;

            // lnkRegister
            //
            this.lnkRegister.AutoSize = true;
            this.lnkRegister.Dock = DockStyle.Top;
            this.lnkRegister.TextAlign = ContentAlignment.MiddleCenter;
            this.lnkRegister.Text = "New user? Register here";
            this.lnkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRegister_LinkClicked);
            registerPanel.Controls.Add(this.lnkRegister, 0, 0);

            // lblError
            //
            this.lblError.AutoSize = false;
            this.lblError.Dock = DockStyle.Fill;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.TextAlign = ContentAlignment.TopCenter;
            registerPanel.Controls.Add(this.lblError, 0, 1);

            mainLayout.Controls.Add(registerPanel, 1, 5);

            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(mainLayout);
            this.MinimumSize = new System.Drawing.Size(400, 300); // Kích thước tối thiểu
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";

            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            this.ResumeLayout(false);
        }

        // Override phương thức OnResize để đảm bảo các phần tử được căn giữa đúng cách
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Đảm bảo nút Login luôn ở giữa panel
            if (btnLogin != null && btnLogin.Parent != null)
            {
                btnLogin.Location = new Point(
                    (btnLogin.Parent.Width - btnLogin.Width) / 2,
                    (btnLogin.Parent.Height - btnLogin.Height) / 2
                );
            }
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCodename;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtCodename;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.LinkLabel lnkRegister;
    }
}