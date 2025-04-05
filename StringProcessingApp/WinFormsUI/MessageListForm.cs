using System;
using System.Linq;
using System.Windows.Forms;
using StringProcessingApp.Services;
using StringProcessingApp.Models;

namespace StringProcessingApp.WinFormsUI
{
    public partial class MessageListForm : Form
    {
        private readonly MessageService _messageService;
        private readonly bool _isAdmin;
        private int _currentPage = 1;
        private const int PageSize = 27;
        private int _totalMessages = 0;

        public MessageListForm()
        {
            InitializeComponent();
            _messageService = ServiceProvider.Instance.MessageService;
            _isAdmin = AuthService.IsAdmin();
            ConfigureDataGridView();
            LoadMessages();
            SetupPaginationControls();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewMessages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMessages.BackgroundColor = System.Drawing.Color.LightGray;
            dataGridViewMessages.Dock = DockStyle.Fill;
            dataGridViewMessages.ReadOnly = true;
            dataGridViewMessages.AllowUserToAddRows = false;
            dataGridViewMessages.AllowUserToDeleteRows = false;
            dataGridViewMessages.RowHeadersVisible = false;
        }

        private void SetupPaginationControls()
        {
            btnPrevious.Enabled = _currentPage > 1;
            btnNext.Enabled = (_currentPage * PageSize) < _totalMessages;
            lblPageInfo.Text = $"Page {_currentPage} of {Math.Ceiling((double)_totalMessages / PageSize)}";
        }

        private void LoadMessages()
        {
            try
            {
                var query = _isAdmin
                    ? _messageService.GetAllMessages()
                    : _messageService.GetMessagesByuser_id(AuthService.GetCurrentUser().id);

                _totalMessages = query.Count();

                var messages = query
                    .Skip((_currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dataGridViewMessages.DataSource = messages.Select(m => new
                {
                    Sender = $"{m.user.name} {m.user.last_name}",
                    Codename = m.user.codename,
                    Message = m.message_text,
                    EncodedMessage = m.encoded_text,
                    ShiftValue = m.shift_value,
                    InputCodes = m.input_codes,   // Thêm
                    OutputCodes = m.output_codes, // Thêm
                    SentAt = m.created_at
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
            finally
            {
                SetupPaginationControls();
            }
        }

        private void BtnSortAlphabetically_Click(object sender, EventArgs e)
        {
            try
            {
                var query = _isAdmin
                    ? _messageService.GetMessagesSortedAlphabetically(true)
                    : _messageService.GetMessagesByuser_id(AuthService.GetCurrentUser().id).OrderBy(m => m.message_text);

                _totalMessages = query.Count();

                var messages = query
                    .Skip((_currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dataGridViewMessages.DataSource = messages.Select(m => new
                {
                    Sender = $"{m.user.name} {m.user.last_name}",
                    Codename = m.user.codename,
                    Message = m.message_text,
                    EncodedMessage = m.encoded_text,
                    ShiftValue = m.shift_value,
                    SentAt = m.created_at
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
            finally
            {
                SetupPaginationControls();
            }
        }

        private void BtnSortByDate_Click(object sender, EventArgs e)
        {
            try
            {
                var query = _isAdmin
                    ? _messageService.GetMessagesSortedByDate(true)
                    : _messageService.GetMessagesByuser_id(AuthService.GetCurrentUser().id).OrderBy(m => m.created_at);

                _totalMessages = query.Count();

                var messages = query
                    .Skip((_currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dataGridViewMessages.DataSource = messages.Select(m => new
                {
                    Sender = $"{m.user.name} {m.user.last_name}",
                    Codename = m.user.codename,
                    Message = m.message_text,
                    EncodedMessage = m.encoded_text,
                    ShiftValue = m.shift_value,
                    SentAt = m.created_at
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
            finally
            {
                SetupPaginationControls();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text;
                var query = _messageService.SearchMessages(searchTerm);

                _totalMessages = query.Count();
                _currentPage = 1; // Reset to first page when searching

                var messages = query
                    .Skip((_currentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

                dataGridViewMessages.DataSource = messages.Select(m => new
                {
                    Sender = $"{m.user.name} {m.user.last_name}",
                    Codename = m.user.codename,
                    Message = m.message_text,
                    EncodedMessage = m.encoded_text,
                    ShiftValue = m.shift_value,
                    SentAt = m.created_at
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorHandling.HandleException(ex);
            }
            finally
            {
                SetupPaginationControls();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                LoadMessages();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((_currentPage * PageSize) < _totalMessages)
            {
                _currentPage++;
                LoadMessages();
            }
        }
    }
}