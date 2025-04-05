using System;
using System.Drawing;
using System.Windows.Forms;
using StringProcessingApp.Models;
using StringProcessingApp.Services;
using StringProcessingApp.Exceptions;
using StringProcessingApp.Helpers;

namespace StringProcessingApp.WinFormsUI
{
    public partial class AdvancedEncodingForm : Form
    {
        private readonly MessageService _messageService;
        private readonly User _currentUser;

        public AdvancedEncodingForm()
        {
            InitializeComponent();
            _messageService = ServiceProvider.Instance.MessageService;
            _currentUser = AuthService.GetCurrentUser();
            SetupForm();
        }

        private void SetupForm()
        {
            // Set form properties
            this.Text = "Advanced Encoding";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Set up event handlers
            btnEncode.Click += BtnEncode_Click;
            btnShowInputCodes.Click += BtnShowInputCodes_Click;
            btnShowOutputCodes.Click += BtnShowOutputCodes_Click;
            btnSort.Click += BtnSort_Click;
            btnSave.Click += BtnSave_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void BtnEncode_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();

                var processor = new StringProcessing(txtInput.Text, (int)numericShift.Value);
                txtOutput.Text = processor.Encode();

                lblStatus.Text = "Message encoded successfully!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (InvalidInputException ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void BtnShowInputCodes_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputString();

                var processor = new StringProcessing(txtInput.Text, 0);
                var codes = processor.InputCode();
                txtOutput.Text = string.Join(" ", codes);

                lblStatus.Text = "Input ASCII codes displayed!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (InvalidInputException ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void BtnShowOutputCodes_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();

                var processor = new StringProcessing(txtInput.Text, (int)numericShift.Value);
                var codes = processor.OutputCode();
                txtOutput.Text = string.Join(" ", codes);

                lblStatus.Text = "Output ASCII codes displayed!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (InvalidInputException ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputString();

                var processor = new StringProcessing(txtInput.Text, 0);
                txtOutput.Text = processor.Sort();

                lblStatus.Text = "Input string sorted alphabetically!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (InvalidInputException ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateInputs();

                _messageService.SaveEncodedMessage(
                    _currentUser,
                    txtInput.Text,
                    (int)numericShift.Value);

                lblStatus.Text = "Message saved successfully!";
                lblStatus.ForeColor = Color.Green;
            }
            catch (InvalidInputException ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
            numericShift.Value = 0;
            lblStatus.Text = "Ready";
            lblStatus.ForeColor = SystemColors.ControlText;
        }

        private void ValidateInputs()
        {
            ValidateInputString();
            Validator.ValidateShiftValue(numericShift.Value.ToString());
        }

        private void ValidateInputString()
        {
            if (string.IsNullOrEmpty(txtInput.Text))
            {
                throw new InvalidInputException("Input string cannot be empty.");
            }
            Validator.ValidateInputString(txtInput.Text);
        }
    }
}