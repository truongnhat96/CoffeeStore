using Database;
using GUI.MailHelper;
using GUI.Security;
using Notifications.Wpf;
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
        private readonly NotificationManager _notificationManager = new NotificationManager();
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
            if(!email.Contains("@gmail.com"))
            {
                MessageBox.Show("Email không hợp lệ hoặc không được hỗ trợ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Xác nhận cấp lại mật khẩu", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var defaultPassword = "CFSM@" + new Random().Next(111, 99999).ToString();
                string passwordHash = PasswordHasher.Hash(defaultPassword);
                try
                {
                    btnRequest.IsEnabled = false;
                    var mail = new MailSender();
                    var query = DataProvider.Instance.ExecuteQuery("SELECT Username FROM ACCOUNT WHERE Email = @EMAIL", new object[] { email });
                    var username = query.Rows[0]["Username"].ToString();
                    mail.Send(email, "Cấp Lại Mật Khẩu", HtmlHelper.Content(username, defaultPassword));
                    DataProvider.Instance.ExecuteNonQuery("UPDATE ACCOUNT SET Password = @PASSWORD WHERE Username = @USERNAME", new object[] { passwordHash, username });
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = "Thông báo",
                        Message = "Đã gửi email cấp lại mật khẩu\nVui lòng kiểm tra hòm thư của bạn",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(8));
                }
                catch
                {
                    btnRequest.IsEnabled = true;
                    _notificationManager.Show(new NotificationContent
                    {
                        Title = "Thông báo",
                        Message = "Không thể gửi email cấp lại mật khẩu\nEmail không được đăng ký với hệ thống hoặc không có kết nối internet",
                        Type = NotificationType.Error
                    }, expirationTime: TimeSpan.FromSeconds(10));
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
