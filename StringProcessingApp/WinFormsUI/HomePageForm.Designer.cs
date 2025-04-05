using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringProcessingApp.Models;
using StringProcessingApp.Services;

namespace StringProcessingApp.WinFormsUI
{
    public partial class HomePageForm : Form
    {
        private User _currentUser;



        private void UpdateUI()
        {
            // Update user information display
            lblWelcome.Text = $"Welcome, {_currentUser.name} {_currentUser.last_name}!";

            // Show admin-specific buttons if user is admin
            btnManageUsers.Visible = AuthService.IsAdmin();
        }



        private void btnAdvancedEncryption_Click(object sender, EventArgs e)
        {
            AdvancedEncodingForm advancedEncoding = new AdvancedEncodingForm();
            advancedEncoding.ShowDialog();
        }

        private void btnASCIITable_Click(object sender, EventArgs e)
        {
            ASCIIForm asciiForm = new ASCIIForm();
            asciiForm.ShowDialog();
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            MessageListForm messageList = new MessageListForm();
            messageList.ShowDialog();
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            UserManagementForm userManagement = new UserManagementForm();
            userManagement.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?",
                "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AuthService.Logout();
                this.Close();
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to permanently delete your account? This action cannot be undone.",
                "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var userRepository = ServiceProvider.Instance.UserRepository;
                    userRepository.DeleteUser(_currentUser.id);
                    AuthService.Logout();
                    MessageBox.Show("Your account has been permanently deleted.",
                        "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting your account: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnBasicEncryption = new System.Windows.Forms.Button();
            this.btnAdvancedEncryption = new System.Windows.Forms.Button();
            this.btnASCIITable = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnDeleteAccount = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(131, 31);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome!";

            // 
            // btnAdvancedEncryption
            // 
            this.btnAdvancedEncryption.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdvancedEncryption.Location = new System.Drawing.Point(350, 100);
            this.btnAdvancedEncryption.Name = "btnAdvancedEncryption";
            this.btnAdvancedEncryption.Size = new System.Drawing.Size(200, 60);
            this.btnAdvancedEncryption.TabIndex = 2;
            this.btnAdvancedEncryption.Text = "Advanced Encryption";
            this.btnAdvancedEncryption.UseVisualStyleBackColor = true;
            this.btnAdvancedEncryption.Click += new System.EventHandler(this.btnAdvancedEncryption_Click);
            // 
            // btnASCIITable
            // 
            this.btnASCIITable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnASCIITable.Location = new System.Drawing.Point(100, 180);
            this.btnASCIITable.Name = "btnASCIITable";
            this.btnASCIITable.Size = new System.Drawing.Size(200, 60);
            this.btnASCIITable.TabIndex = 3;
            this.btnASCIITable.Text = "ASCII Code Table";
            this.btnASCIITable.UseVisualStyleBackColor = true;
            this.btnASCIITable.Click += new System.EventHandler(this.btnASCIITable_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessages.Location = new System.Drawing.Point(350, 180);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(200, 60);
            this.btnMessages.TabIndex = 4;
            this.btnMessages.Text = "Message List";
            this.btnMessages.UseVisualStyleBackColor = true;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // btnManageUsers
            // 
            this.btnManageUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageUsers.Location = new System.Drawing.Point(225, 260);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Size = new System.Drawing.Size(200, 60);
            this.btnManageUsers.TabIndex = 5;
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Visible = false; // Only visible for admins
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(100, 340);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 40);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnDeleteAccount
            // 
            this.btnDeleteAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteAccount.ForeColor = System.Drawing.Color.Red;
            this.btnDeleteAccount.Location = new System.Drawing.Point(350, 340);
            this.btnDeleteAccount.Name = "btnDeleteAccount";
            this.btnDeleteAccount.Size = new System.Drawing.Size(200, 40);
            this.btnDeleteAccount.TabIndex = 7;
            this.btnDeleteAccount.Text = "Delete Account";
            this.btnDeleteAccount.UseVisualStyleBackColor = true;
            this.btnDeleteAccount.Click += new System.EventHandler(this.btnDeleteAccount_Click);
            // 
            // HomePageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 420);
            this.Controls.Add(this.btnDeleteAccount);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnManageUsers);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnASCIITable);
            this.Controls.Add(this.btnAdvancedEncryption);
            this.Controls.Add(this.btnBasicEncryption);
            this.Controls.Add(this.lblWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "HomePageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "String Processing App - Home";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomePageForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnBasicEncryption;
        private System.Windows.Forms.Button btnAdvancedEncryption;
        private System.Windows.Forms.Button btnASCIITable;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnDeleteAccount;

        private void HomePageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If the form is closing but user is still logged in, confirm logout
            if (AuthService.IsLoggedIn())
            {
                // Don't show confirmation if we're already logging out
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to exit the application?",
                        "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        AuthService.Logout();
                    }
                }
            }
        }
    }
}
