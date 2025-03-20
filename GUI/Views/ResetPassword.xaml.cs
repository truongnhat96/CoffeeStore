using Database;
using GUI.MailHelper;
using GUI.Security;
using System;
using System.Windows;
using System.Windows.Input;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for ResetPassword.xaml
    /// </summary>
    public partial class ResetPassword : Window
    {
        public ResetPassword()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Xác nhận cấp lại mật khẩu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var defaultPassword = "CFSM" + new Random().Next(111, 99999).ToString();
                string passwordHash = PasswordHasher.Hash(defaultPassword);
                try
                {
                    var mail = new MailSender();
                    var query = DataProvider.Instance.ExecuteQuery("SELECT Username FROM ACCOUNT WHERE Email = @EMAIL", new object[] { email });
                    var username = query.Rows[0]["Username"].ToString();
                    mail.Send(email, "Cấp Lại Mật Khẩu", HtmlHelper.Content(username, defaultPassword));
                    DataProvider.Instance.ExecuteNonQuery("UPDATE ACCOUNT SET Password = @PASSWORD WHERE Username = @USERNAME", new object[] { passwordHash, username });
                    MessageBox.Show($"Mật khẩu đã được gửi đến email: {email}\nVui lòng kiểm tra hòm thư và đăng nhập lại vào tài khoản", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể gửi email cấp lại mật khẩu\nVui lòng kiểm tra kết nối mạng và thử lại\n{ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
