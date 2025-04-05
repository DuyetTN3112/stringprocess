using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringProcessingApp.Exceptions;

namespace StringProcessingApp.WinFormsUI
{
    public partial class ErrorHandling : Form

    {
        public static void HandleException(Exception ex, Control control = null)
        {
            if (ex is InvalidInputException)
            {
                MessageBox.Show(ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control?.Focus();
            }
            else
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log the error here if needed
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ErrorHandling
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Name = "ErrorHandling";
            this.Text = "Error Handling";
            this.ResumeLayout(false);
        }
        public static void DisplaySuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool ConfirmAction(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}