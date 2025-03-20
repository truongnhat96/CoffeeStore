using Database;
using GUI.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for UserInfor.xaml
    /// </summary>
    public partial class UserInfor : Window
    {
        private AccountModel account;
        private Action<string> _updateInfo;
        public UserInfor(AccountModel account)
        {
            InitializeComponent();
            this.account = account;
            LoadData();
        }

        private void LoadData()
        {
            txtUsername.Text = account.Username;
            txtDisplayName.Text = account.Displayname;
            txtEmail.Text = account.Email;
            txtRole.Text = account.Auth;
        }

        private bool _isChanged = false;

        public AccountModel Account { get => account; set => account = value; }
        public Action<string> UpdateInfo { get => _updateInfo; set => _updateInfo = value; }

        // Sự kiện thay đổi văn bản trên các TextBox
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isChanged = true;
            btnUpdate.IsEnabled = true;
        }

        // Xử lý sự kiện khi người dùng nhấn nút cập nhật
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Thực hiện logic cập nhật dữ liệu ở đây (ví dụ: lưu vào cơ sở dữ liệu)
            DataProvider.Instance.ExecuteNonQuery("update ACCOUNT set DisplayName = @NEWNAME, Email = @EMAIL where Username = @USERNAME", new object[] { txtDisplayName.Text, txtEmail.Text, txtUsername.Text });
            MessageBox.Show("Thông tin đã được cập nhật!", "Cập nhật", MessageBoxButton.OK, MessageBoxImage.Information);
            _updateInfo.Invoke(txtDisplayName.Text);
            // Sau khi cập nhật thành công, bạn có thể đặt lại trạng thái
            _isChanged = false;
            btnUpdate.IsEnabled = false;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
