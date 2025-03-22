using Database;
using GUI.Models;
using GUI.ViewModels;
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
    /// Interaction logic for Storage.xaml
    /// </summary>
    public partial class Storage : Window
    {
        private AccountModel account;
        public Storage(AccountModel account)
        {
            InitializeComponent();
            this.account = account;
            LoadData();
            UserAuthorization();
        }

        private void UserAuthorization()
        {
            btnEdit.IsEnabled = btnDelete.IsEnabled = btnHistory.IsEnabled = account.Auth == "ADMIN";
        }

        private void LoadData()
        {
            this.DataContext = new StorageViewModel();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(tbID.Text);
            var productname = txtProductName.Text;
            var quantity = numQuantity.Value;
            var unit = txtUnit.Text;
            var price = numPrice.Value;
            var dateimport = dpImport.SelectedDate.Value;
            DataProvider.Instance.ExecuteNonQuery("UPDATE PRODUCT SET ProductName = @NAME, Count = @CNT, Unit = @UNIT, Price = @PRICE, TimeImport = @TIME WHERE ID = @ID", new object[] { productname, quantity, unit, price, dateimport, id });
            MessageBox.Show("Sửa thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = int.Parse(tbID.Text);
                DataProvider.Instance.ExecuteNonQuery("EXEC SP_DeleteProduct @ID", new object[] { id });
                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryImport historyImport = new HistoryImport();
            historyImport.ShowDialog();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var productname = txtProductName.Text;
            var quantity = numQuantity.Value;
            var unit = txtUnit.Text;
            var price = numPrice.Value;
            var userid = account.Username;
            DataProvider.Instance.ExecuteNonQuery("SP_AddProduct @NAME, @CNT, @UNIT, @PRICE, @TIME, @USERID", new object[] { productname, quantity, unit, price, DateTime.Now, userid });
            MessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
