using System.Windows.Forms;
using StringProcessingApp.Services;


namespace StringProcessingApp.WinFormsUI
{
    public partial class LoginForm : Form
    {
        // Thêm DoubleBuffered để giảm nhấp nháy
        public LoginForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // Thêm dòng này
            this.DoubleBuffered = true;
            _authService = ServiceProvider.Instance.AuthService;

            // Tắt các hiệu ứng không cần thiết
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);
        }
    }
}
