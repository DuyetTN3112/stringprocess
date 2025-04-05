using System.Windows.Forms;

namespace StringProcessingApp.WinFormsUI
{
    partial class AdvancedEncodingForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.NumericUpDown numericShift;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnShowInputCodes;
        private System.Windows.Forms.Button btnShowOutputCodes;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Panel panelStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.numericShift = new System.Windows.Forms.NumericUpDown();
            this.lblOutput = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnShowInputCodes = new System.Windows.Forms.Button();
            this.btnShowOutputCodes = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.panelStatus = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericShift)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Advanced Encoding";

            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(20, 70);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(137, 17);
            this.lblInput.TabIndex = 1;
            this.lblInput.Text = "Input String (A-Z):";

            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(20, 90);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(400, 100);
            this.txtInput.TabIndex = 2;
            this.txtInput.MaxLength = 40;
            this.txtInput.CharacterCasing = CharacterCasing.Upper;

            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(20, 200);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(103, 17);
            this.lblShift.TabIndex = 3;
            this.lblShift.Text = "Shift Value (-25 to 25):";

            // 
            // numericShift
            // 
            this.numericShift.Location = new System.Drawing.Point(20, 220);
            this.numericShift.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            -2147483648});
            this.numericShift.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericShift.Name = "numericShift";
            this.numericShift.Size = new System.Drawing.Size(120, 22);
            this.numericShift.TabIndex = 4;

            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(450, 70);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(52, 17);
            this.lblOutput.TabIndex = 5;
            this.lblOutput.Text = "Output:";

            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(450, 90);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(400, 100);
            this.txtOutput.TabIndex = 6;

            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(10, 10);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(120, 40);
            this.btnEncode.TabIndex = 7;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;

            // 
            // btnShowInputCodes
            // 
            this.btnShowInputCodes.Location = new System.Drawing.Point(140, 10);
            this.btnShowInputCodes.Name = "btnShowInputCodes";
            this.btnShowInputCodes.Size = new System.Drawing.Size(150, 40);
            this.btnShowInputCodes.TabIndex = 8;
            this.btnShowInputCodes.Text = "Show Input Codes";
            this.btnShowInputCodes.UseVisualStyleBackColor = true;

            // 
            // btnShowOutputCodes
            // 
            this.btnShowOutputCodes.Location = new System.Drawing.Point(300, 10);
            this.btnShowOutputCodes.Name = "btnShowOutputCodes";
            this.btnShowOutputCodes.Size = new System.Drawing.Size(150, 40);
            this.btnShowOutputCodes.TabIndex = 9;
            this.btnShowOutputCodes.Text = "Show Output Codes";
            this.btnShowOutputCodes.UseVisualStyleBackColor = true;

            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(460, 10);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(120, 40);
            this.btnSort.TabIndex = 10;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(590, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;

            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(720, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(120, 40);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(48, 17);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Ready";

            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblInput);
            this.panelMain.Controls.Add(this.txtInput);
            this.panelMain.Controls.Add(this.lblShift);
            this.panelMain.Controls.Add(this.numericShift);
            this.panelMain.Controls.Add(this.lblOutput);
            this.panelMain.Controls.Add(this.txtOutput);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(900, 300);
            this.panelMain.TabIndex = 14;

            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnEncode);
            this.panelButtons.Controls.Add(this.btnShowInputCodes);
            this.panelButtons.Controls.Add(this.btnShowOutputCodes);
            this.panelButtons.Controls.Add(this.btnSort);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 300);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(900, 60);
            this.panelButtons.TabIndex = 15;

            // 
            // panelStatus
            // 
            this.panelStatus.Controls.Add(this.lblStatus);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(0, 360);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(900, 40);
            this.panelStatus.TabIndex = 16;

            // 
            // AdvancedEncodingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 400);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelStatus);
            this.Name = "AdvancedEncodingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advanced Encoding";
            ((System.ComponentModel.ISupportInitialize)(this.numericShift)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}