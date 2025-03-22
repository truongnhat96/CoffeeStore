using Database;
using GUI.Security;
using MaterialDesignThemes.Wpf;
using Notifications.Wpf;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotificationManager _notificationManager = new NotificationManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(pwdBox.Password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataTable data = DataProvider.Instance.ExecuteQuery(@"SELECT * FROM ACCOUNT WHERE Username = @Username", new object[] { txtUsername.Text });
            if (data.Rows.Count > 0)
            {
                var passwordHash = data.Rows[0]["Password"].ToString();
                if (PasswordHasher.VerifyPassword(pwdBox.Password, passwordHash))
                {
                    Home home = new Home(new Models.AccountModel(data.Rows[0], true));
                    home.ResetInfor += RefreshInforLogin;
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = "Đăng nhập thành công",
                        Message = "Chào mừng bạn đến với hệ thống quản lý cửa hàng",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(5));
                    this.Hide();
                    home.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Tài khoản không tồn tại?\nVui lòng báo với quản trị viên để được cấp tài khoản mới", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ResetPassword reset = new ResetPassword();
            reset.ShowDialog();
        }

        private void RefreshInforLogin()
        {
            txtUsername.Text = "";
            pwdBox.Password = "";
            txtPasswordVisible.Text = "";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnTogglePassword_Checked(object sender, RoutedEventArgs e)
        {
            // Khi bật: hiển thị mật khẩu dưới dạng văn bản
            iconToggle.Kind = PackIconKind.EyeOff;
            // Cập nhật nội dung TextBox từ PasswordBox
            txtPasswordVisible.Text = pwdBox.Password;
            pwdBox.Visibility = Visibility.Collapsed;
            txtPasswordVisible.Visibility = Visibility.Visible;
        }

        private void btnTogglePassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Khi tắt: hiển thị mật khẩu ẩn
            iconToggle.Kind = PackIconKind.Eye;
            // Đồng bộ lại mật khẩu từ TextBox về PasswordBox
            pwdBox.Password = txtPasswordVisible.Text;
            txtPasswordVisible.Visibility = Visibility.Collapsed;
            pwdBox.Visibility = Visibility.Visible;
        }
    }
}
