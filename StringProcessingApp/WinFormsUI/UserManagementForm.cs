using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using StringProcessingApp.Models;
using StringProcessingApp.Services;
using StringProcessingApp.Exceptions;
using StringProcessingApp.Interfaces;

namespace StringProcessingApp.WinFormsUI
{
    public partial class UserManagementForm : Form
    {
        private readonly IUserRepository _userRepository;
        private User _selectedUser;

        // Phân trang với 20 dòng mỗi trang
        private List<User> _allUsers;
        private int _currentPage = 1;
        private const int _pageSize = 22;
        private int _totalPages = 1;

        public UserManagementForm()
        {
            InitializeComponent();
            _userRepository = ServiceProvider.Instance.UserRepository;

            // Tối ưu hiển thị DataGridView
            dataGridViewUsers.BorderStyle = BorderStyle.None;
            dataGridViewUsers.BackgroundColor = SystemColors.Control;
            dataGridViewUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewUsers.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;

            LoadUsers();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AdjustDataGridViewColumns();
        }

        private void AdjustDataGridViewColumns()
        {
            dataGridViewUsers.Dock = DockStyle.None;
            dataGridViewUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in dataGridViewUsers.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustDataGridViewColumns();
        }

        private void LoadUsers()
        {
            try
            {
                _allUsers = _userRepository.GetAllUsers().ToList();
                UpdatePagination();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void UpdatePagination()
        {
            if (_allUsers == null)
            {
                _allUsers = new List<User>();
            }

            _totalPages = (_allUsers.Count + _pageSize - 1) / _pageSize;

            if (_currentPage < 1)
                _currentPage = 1;
            if (_currentPage > _totalPages && _totalPages > 0)
                _currentPage = _totalPages;

            lblPageInfo.Text = $"{_currentPage} / {_totalPages}";

            var usersToDisplay = _allUsers
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .Select(u => new
                {
                    u.id,
                    u.name,
                    u.last_name,
                    u.email,
                    u.phone_number,
                    u.codename,
                    u.role,
                    u.created_at
                })
                .ToList();
            dataGridViewUsers.DataSource = usersToDisplay;

            btnFirst.Enabled = btnPrevious.Enabled = (_currentPage > 1);
            btnNext.Enabled = btnLast.Enabled = (_currentPage < _totalPages);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Please select a user to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _selectedUser.name = txtName.Text;
                _selectedUser.last_name = txtLastName.Text;
                _selectedUser.email = txtEmail.Text;
                _selectedUser.phone_number = txtPhone.Text;
                _selectedUser.codename = txtCodename.Text;
                _selectedUser.role = chkIsAdmin.Checked ? "Admin" : "User";

                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    _selectedUser.password = txtPassword.Text;
                }

                _userRepository.UpdateUser(_selectedUser);
                ErrorHandling.DisplaySuccess("User updated successfully!");
                ClearFields();
                LoadUsers();
                _selectedUser = null;
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ErrorHandling.ConfirmAction("Are you sure you want to delete this user?"))
            {
                try
                {
                    _userRepository.DeleteUser(_selectedUser.id);
                    ErrorHandling.DisplaySuccess("User deleted successfully!");
                    ClearFields();
                    LoadUsers();
                    _selectedUser = null;
                }
                catch (Exception ex)
                {
                    ErrorHandling.HandleException(ex);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text;
                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadUsers();
                    return;
                }

                _allUsers = _userRepository.SearchUsers(searchTerm).ToList();
                _currentPage = 1;
                UpdatePagination();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
        }

        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewUsers.SelectedRows[0];
                int userId = (int)selectedRow.Cells["id"].Value;

                _selectedUser = _allUsers.FirstOrDefault(u => u.id == userId);
                if (_selectedUser != null)
                {
                    txtName.Text = _selectedUser.name;
                    txtLastName.Text = _selectedUser.last_name;
                    txtEmail.Text = _selectedUser.email;
                    txtPhone.Text = _selectedUser.phone_number;
                    txtCodename.Text = _selectedUser.codename;
                    txtPassword.Text = "";
                    chkIsAdmin.Checked = _selectedUser.role == "Admin";
                }
            }
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtCodename.Text = "";
            txtPassword.Text = "";
            chkIsAdmin.Checked = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            _selectedUser = null;
            dataGridViewUsers.ClearSelection();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            UpdatePagination();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdatePagination();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                UpdatePagination();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            _currentPage = _totalPages;
            UpdatePagination();
        }
    }
}