using Database;
using GUI.MailHelper;
using GUI.Models;
using GUI.Security;
using MaterialDesignThemes.Wpf;
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for PasswordChange.xaml
    /// </summary>
    public partial class PasswordChange : Window
    {
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
            if (string.IsNullOrEmpty(pwdCurrent.Password) ||
                string.IsNullOrEmpty(pwdNew.Password) ||
                string.IsNullOrEmpty(pwdConfirm.Password) ||
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
            if (pwdNew.Password != pwdConfirm.Password)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu không khớp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            try
            {
                var mail = new MailSender();
                mail.Send(account.Email, "MÃ XÁC THỰC EMAIL ĐỔI MẬT KHẨU", HtmlHelper.ContentConfirm(verificationCode.ToString()));
                MessageBox.Show("Mã xác nhận đã được gửi đến hòm thư của bạn.\nVui lòng kiểm tra!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi gửi mã xác nhận.\n" + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                btnGetVerificationCode.IsEnabled = true;
            }
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
