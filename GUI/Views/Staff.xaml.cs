using Database;
using GUI.MailHelper;
using GUI.Models;
using GUI.Security;
using GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for Staff.xaml
    /// </summary>
    public partial class Staff : Window
    {
        private AccountModel account;
        public Staff(AccountModel account)
        {
            InitializeComponent();
            this.account = account;
            LoadData();
        }

        private void LoadData()
        {
            DataContext = new StaffViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string displayname = txtDisplayName.Text;
            var defaultPassword = "Cafe" + new Random().Next(1111, 9999).ToString();
            string passwordHash = PasswordHasher.Hash(defaultPassword);
            string accounttype = cbAuth.SelectedItem.ToString();
            DateTime birth = dpBirth.SelectedDate.Value;
            string email = txtEmail.Text;
            DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_AddAccount @USERNAME, @DISPLAYNAME, @ACCOUNTTYPE, @BIRTH, @EMAIL, @PASSWORD", new object[] { username, displayname, accounttype, birth, email, passwordHash });
            MessageBox.Show($"Tạo tài khoản thành công! Mật khẩu của bạn là {defaultPassword}\nVui lòng đăng nhập và đặt lại mật khẩu mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(account.Username == txtUsername.Text)
            {
                MessageBox.Show("Không thể xóa tài khoản của chính mình", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?\nKhông thể khôi phục lại tài khoản sau khi xóa", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string username = txtUsername.Text;
                string displayname = txtDisplayName.Text;
                DataProvider.Instance.ExecuteNonQuery("EXEC SP_DeleteAccount @USERNAME, @DISPLAYNAME", new object[] { username, displayname });
                MessageBox.Show("Xóa tài khoản thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string currentUsername = (dtgAccounts.SelectedItem as AccountModel).Username;
            string username = txtUsername.Text;
            string displayname = txtDisplayName.Text;
            string accounttype = cbAuth.SelectedItem.ToString();
            DateTime birth = dpBirth.SelectedDate.Value;
            string email = txtEmail.Text;
            DateTime begin = dpBeginWork.SelectedDate.Value;
            DataProvider.Instance.ExecuteNonQuery("UPDATE ACCOUNT SET Username = @USERNAME, Displayname = @DISPLAYNAME, AccountType = @ACCOUNTTYPE, DateOfBirth = @DATE, Email = @EMAIL, WorkBegin = @BEGIN WHERE Username = @CURUSERNAME", new object[] { username, displayname, accounttype, birth, email, begin, currentUsername });
            MessageBox.Show("Cập nhật thông tin tài khoản thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new StaffViewModel(txtSearch.Text);
        }
    }
}
