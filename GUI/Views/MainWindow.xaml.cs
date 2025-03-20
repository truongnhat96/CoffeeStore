using Database;
using GUI.Security;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(pwdBox.Password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(@"SELECT * FROM ACCOUNT WHERE Username = @Username", new object[] { txtUsername.Text });
            var passwordHash = data.Rows[0]["Password"].ToString();
            if (data.Rows.Count > 0 && PasswordHasher.VerifyPassword(pwdBox.Password, passwordHash))
            {
                Home home = new Home(new Models.AccountModel(data.Rows[0], true));
                home.ResetInfor += RefreshInforLogin;
                this.Hide();
                home.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
