using System.Windows.Forms;
using StringProcessingApp.Services;

namespace StringProcessingApp.WinFormsUI
{
    public partial class HomePageForm : Form
    {
        public HomePageForm()
        {
            InitializeComponent();
            _currentUser = AuthService.GetCurrentUser();
            UpdateUI();
        }

    }
}
