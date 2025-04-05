using System.Windows.Forms;
using StringProcessingApp.Services;


namespace StringProcessingApp.WinFormsUI
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            _userRepository = ServiceProvider.Instance.UserRepository;

        }
    }
}
