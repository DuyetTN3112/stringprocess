using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StringProcessingApp.WinFormsUI
{
    public partial class ASCIIForm : Form
    {
        private DataTable asciiTable;
        private DataView filteredView;
        private int currentPage = 1;
        private int pageSize = 27;

        public ASCIIForm()
        {
            InitializeComponent();
            LoadASCIITable();
            SetupUI();
        }

        private void SetupUI()
        {
            // Setup event handlers
            comboBoxCategory.SelectedIndexChanged += ComboBoxCategory_SelectedIndexChanged;
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            checkBoxShowControlChars.CheckedChanged += CheckBoxShowControlChars_CheckedChanged;

            // Setup character categories
            comboBoxCategory.Items.Add("All Characters");
            comboBoxCategory.Items.Add("Control Characters (0-31)");
            comboBoxCategory.Items.Add("Printable Characters (32-126)");
            comboBoxCategory.Items.Add("Extended ASCII (128-255)");
            comboBoxCategory.SelectedIndex = 0;

            // Configure DataGridView
            dataGridViewASCII.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewASCII.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewASCII.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewASCII.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewASCII.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridViewASCII.Font, FontStyle.Bold);
        }

        private void LoadASCIITable()
        {
            try
            {
                asciiTable = new DataTable();
                asciiTable.Columns.Add("Character", typeof(string));
                asciiTable.Columns.Add("ASCII Code", typeof(int));
                asciiTable.Columns.Add("Hex", typeof(string));
                asciiTable.Columns.Add("Description", typeof(string));
                asciiTable.Columns.Add("Category", typeof(string));

                for (int i = 0; i <= 255; i++)
                {
                    string charDisplay;
                    string description;
                    string category;

                    if (i < 32)
                    {
                        charDisplay = GetControlCharacterRepresentation(i);
                        description = GetControlCharacterDescription(i);
                        category = "Control";
                    }
                    else if (i == 127)
                    {
                        charDisplay = "DEL";
                        description = "Delete";
                        category = "Control";
                    }
                    else if (i > 127)
                    {
                        charDisplay = ((char)i).ToString();
                        description = "Extended ASCII";
                        category = "Extended";
                    }
                    else
                    {
                        charDisplay = ((char)i).ToString();
                        description = "Printable";
                        category = "Printable";
                    }

                    asciiTable.Rows.Add(charDisplay, i, "0x" + i.ToString("X2"), description, category);
                }

                filteredView = new DataView(asciiTable);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading the ASCII table: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetControlCharacterRepresentation(int asciiCode)
        {
            switch (asciiCode)
            {
                case 0: return "NUL";
                case 1: return "SOH";
                case 2: return "STX";
                case 3: return "ETX";
                case 4: return "EOT";
                case 5: return "ENQ";
                case 6: return "ACK";
                case 7: return "BEL";
                case 8: return "BS";
                case 9: return "HT";
                case 10: return "LF";
                case 11: return "VT";
                case 12: return "FF";
                case 13: return "CR";
                case 14: return "SO";
                case 15: return "SI";
                case 16: return "DLE";
                case 17: return "DC1";
                case 18: return "DC2";
                case 19: return "DC3";
                case 20: return "DC4";
                case 21: return "NAK";
                case 22: return "SYN";
                case 23: return "ETB";
                case 24: return "CAN";
                case 25: return "EM";
                case 26: return "SUB";
                case 27: return "ESC";
                case 28: return "FS";
                case 29: return "GS";
                case 30: return "RS";
                case 31: return "US";
                default: return "";
            }
        }

        private string GetControlCharacterDescription(int asciiCode)
        {
            switch (asciiCode)
            {
                case 0: return "Null";
                case 1: return "Start of Heading";
                case 2: return "Start of Text";
                case 3: return "End of Text";
                case 4: return "End of Transmission";
                case 5: return "Enquiry";
                case 6: return "Acknowledgement";
                case 7: return "Bell";
                case 8: return "Backspace";
                case 9: return "Horizontal Tab";
                case 10: return "Line Feed";
                case 11: return "Vertical Tab";
                case 12: return "Form Feed";
                case 13: return "Carriage Return";
                case 14: return "Shift Out";
                case 15: return "Shift In";
                case 16: return "Data Link Escape";
                case 17: return "Device Control 1 (XON)";
                case 18: return "Device Control 2";
                case 19: return "Device Control 3 (XOFF)";
                case 20: return "Device Control 4";
                case 21: return "Negative Acknowledgement";
                case 22: return "Synchronous Idle";
                case 23: return "End of Transmission Block";
                case 24: return "Cancel";
                case 25: return "End of Medium";
                case 26: return "Substitute";
                case 27: return "Escape";
                case 28: return "File Separator";
                case 29: return "Group Separator";
                case 30: return "Record Separator";
                case 31: return "Unit Separator";
                default: return "";
            }
        }

        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void CheckBoxShowControlChars_CheckedChanged(object sender, EventArgs e)
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                LoadCurrentPage();
            }
        }

        private void BtnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCurrentPage();
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            int totalPages = GetTotalPages();
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCurrentPage();
            }
        }

        private void BtnLastPage_Click(object sender, EventArgs e)
        {
            int totalPages = GetTotalPages();
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                LoadCurrentPage();
            }
        }

        private int GetTotalPages()
        {
            if (filteredView.Count == 0 || pageSize <= 0)
                return 1;

            return (int)Math.Ceiling((double)filteredView.Count / pageSize);
        }

        private void UpdatePaginationControls()
        {
            int totalPages = GetTotalPages();

            lblPageInfo.Text = $"Page {currentPage} of {totalPages}";

            btnFirstPage.Enabled = btnPrevPage.Enabled = (currentPage > 1);
            btnNextPage.Enabled = btnLastPage.Enabled = (currentPage < totalPages);
        }

        private void ApplyFilters()
        {
            try
            {
                string filter = "";

                switch (comboBoxCategory.SelectedIndex)
                {
                    case 1: filter = "([ASCII Code] <= 31) OR ([ASCII Code] = 127)"; break;
                    case 2: filter = "([ASCII Code] >= 32) AND ([ASCII Code] <= 126)"; break;
                    case 3: filter = "[ASCII Code] >= 128"; break;
                }

                if (!string.IsNullOrWhiteSpace(textBoxSearch.Text))
                {
                    string searchTerm = textBoxSearch.Text.Trim();
                    string searchFilter = string.Format(
                        "(Character LIKE '%{0}%') OR ([ASCII Code] = '{0}') OR (Hex LIKE '%{0}%') OR (Description LIKE '%{0}%')",
                        searchTerm.Replace("'", "''"));

                    if (!string.IsNullOrEmpty(filter))
                        filter = "(" + filter + ") AND (" + searchFilter + ")";
                    else
                        filter = searchFilter;
                }

                filteredView.RowFilter = filter;
                LoadCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying filters: " + ex.Message,
                              "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCurrentPage()
        {
            try
            {
                DataTable currentPageTable = asciiTable.Clone();
                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize, filteredView.Count);

                var rows = filteredView.Cast<DataRowView>()
                                     .Skip(startIndex)
                                     .Take(pageSize)
                                     .Select(drv => drv.Row);

                foreach (DataRow row in rows)
                {
                    currentPageTable.ImportRow(row);
                }

                dataGridViewASCII.DataSource = currentPageTable;

                if (filteredView.Count == 0)
                {
                    lblResultCount.Text = "No characters found";
                }
                else
                {
                    lblResultCount.Text = $"Displaying {startIndex + 1}-{endIndex} of {filteredView.Count} characters";
                }

                UpdatePaginationControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading current page: " + ex.Message,
                               "Pagination Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    saveDialog.Title = "Export ASCII Table";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveDialog.FileName))
                        {
                            writer.WriteLine("Character,ASCII Code,Hex,Description,Category");

                            foreach (DataRowView row in filteredView)
                            {
                                writer.WriteLine($"\"{row["Character"]}\",{row["ASCII Code"]},{row["Hex"]},\"{row["Description"]}\",{row["Category"]}");
                            }
                        }

                        MessageBox.Show("ASCII table exported successfully!",
                                      "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message,
                              "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}