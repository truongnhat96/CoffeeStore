using Database;
using GUI.MailHelper;
using GUI.Models;
using GUI.Security;
using MaterialDesignThemes.Wpf;
using Notifications.Wpf;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
        private readonly NotificationManager _notificationManager = new NotificationManager();
        private AccountModel account;
        private int verificationCode = new Random().Next(121212, 989898);
        public PasswordChange(AccountModel account)
        {
            InitializeComponent();
            this.account = account;
        }

        private void btnToggleNewPassword_Checked(object sender, RoutedEventArgs e)
        {
            // Khi bật toggle: hiển thị mật khẩu dạng văn bản
            txtNewVisible.Text = pwdNew.Password;
            txtNewVisible.Visibility = Visibility.Visible;
            pwdNew.Visibility = Visibility.Collapsed;
            iconNewPassword.Kind = PackIconKind.EyeOffOutline;
        }

        private void btnToggleNewPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            // Khi tắt toggle: hiển thị lại PasswordBox
            pwdNew.Password = txtNewVisible.Text;
            pwdNew.Visibility = Visibility.Visible;
            txtNewVisible.Visibility = Visibility.Collapsed;
            iconNewPassword.Kind = PackIconKind.EyeOutline;
        }

        // Xử lý nút toggle cho Xác nhận mật khẩu
        private void btnToggleConfirmPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtConfirmVisible.Text = pwdConfirm.Password;
            txtConfirmVisible.Visibility = Visibility.Visible;
            pwdConfirm.Visibility = Visibility.Collapsed;
            iconConfirmPassword.Kind = PackIconKind.EyeOffOutline;
        }

        private void btnToggleConfirmPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pwdConfirm.Password = txtConfirmVisible.Text;
            pwdConfirm.Visibility = Visibility.Visible;
            txtConfirmVisible.Visibility = Visibility.Collapsed;
            iconConfirmPassword.Kind = PackIconKind.EyeOutline;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra các ô nhập liệu không được để trống
            if (((string.IsNullOrEmpty(pwdCurrent.Password) || string.IsNullOrEmpty(pwdNew.Password) || string.IsNullOrEmpty(pwdConfirm.Password)) && (string.IsNullOrEmpty(txtCurrentVisible.Text) || string.IsNullOrEmpty(txtNewVisible.Text) || string.IsNullOrEmpty(txtConfirmVisible.Text))) ||
                string.IsNullOrEmpty(txtVerificationCode.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataTable data = DataProvider.Instance.ExecuteQuery(@"SELECT * FROM ACCOUNT WHERE Username = @Username", new object[] { account.Username });
            var passwordHash = data.Rows[0]["Password"].ToString();
            if(!PasswordHasher.VerifyPassword(pwdCurrent.Password, passwordHash))
            {
                MessageBox.Show("Mật khẩu hiện tại không chính xác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Kiểm tra mật khẩu mới và xác nhận có khớp nhau hay không
            if ((pwdNew.Password != pwdConfirm.Password) && (txtConfirmVisible.Text != txtNewVisible.Text))
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string pass = !string.IsNullOrEmpty(txtNewVisible.Text) ? txtNewVisible.Text : pwdNew.Password;
            if (InputPasswordRequired(pass) == false)
            {
                MessageBox.Show("Mật khẩu mới không đủ mạnh.\nMật khẩu phải chứa ít nhất 6 ký tự, 1 chữ hoa, 1 chữ thường, 1 số và 1 ký tự đặc biệt.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tiến hành cập nhật mật khẩu (bạn có thể thêm logic xác nhận mã, kiểm tra mật khẩu hiện tại,...)
            if (txtVerificationCode.Text != verificationCode.ToString())
            {
                MessageBox.Show("Mã xác nhận không chính xác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataProvider.Instance.ExecuteNonQuery("UPDATE ACCOUNT SET Password = @Password WHERE Username = @Username", new object[] { PasswordHasher.Hash(pwdNew.Password), account.Username });
            MessageBox.Show("Mật khẩu đã được cập nhật thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGetVerificationCode_Click(object sender, RoutedEventArgs e)
        {
            btnGetVerificationCode.IsEnabled = false;
            if(account.Email.Contains("@gmail.com") == false)
            {
                MessageBox.Show("Email đăng ký không hợp lệ hoặc không được hỗ trợ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                btnGetVerificationCode.IsEnabled = true;
                return;
            }
            try
            {
                var mail = new MailSender();
                mail.Send(account.Email, "MÃ XÁC THỰC EMAIL ĐỔI MẬT KHẨU", HtmlHelper.ContentConfirm(verificationCode.ToString()));
                MessageBox.Show("Mã xác nhận đã được gửi đến hòm thư của bạn.\nVui lòng kiểm tra!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                _notificationManager.Show(new NotificationContent
                {
                    Title = "Thông báo",
                    Message = "Có lỗi khi gửi Email\nVui lòng kiểm tra kết nối internet và thử lại",
                    Type = NotificationType.Error
                }, expirationTime: TimeSpan.FromSeconds(10));
                btnGetVerificationCode.IsEnabled = true;
            }
        }

        private bool InputPasswordRequired(string password)
        {
            if(password.Length < 6)
            {
                return false;
            }
            if(!password.Any(char.IsUpper))
            {
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                return false;
            }
            if (!password.Contains('@'))
            {
                return false;
            }
            return true;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnToggleCurrentPassword_Checked(object sender, RoutedEventArgs e)
        {
            txtCurrentVisible.Text = pwdCurrent.Password;
            txtCurrentVisible.Visibility = Visibility.Visible;
            pwdCurrent.Visibility = Visibility.Collapsed;
            iconCurrentPassword.Kind = PackIconKind.EyeOffOutline;
        }

        private void btnToggleCurrentPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            pwdCurrent.Password = txtCurrentVisible.Text;
            pwdCurrent.Visibility = Visibility.Visible;
            txtCurrentVisible.Visibility = Visibility.Collapsed;
            iconCurrentPassword.Kind = PackIconKind.EyeOutline;
        }
    }
}
