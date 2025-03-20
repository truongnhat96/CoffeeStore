using Database;
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
using System.Xml.Linq;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : Window
    {
        public Category()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DataContext = new CategoryViewModel();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Instance.ExecuteNonQuery("UPDATE DRINK_CATEGORY SET Name = @NAME WHERE ID = @ID", new object[] { txtName.Text, Convert.ToInt32(txtId.Text) });
            MessageBox.Show("Sửa danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Instance.ExecuteNonQuery(@"INSERT INTO DRINK_CATEGORY VALUES ( @NAME )", new object[] { txtName.Text });
            MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadData();

        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CategoryViewModel(txtSearch.Text);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này không?\nToàn bộ món thuộc danh mục này cũng sẽ bị xóa!", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DataProvider.Instance.ExecuteNonQuery(@"EXECUTE SP_DeleteCategory @ID, @NAME", new object[] { Convert.ToInt32(txtId.Text), txtName.Text });
                MessageBox.Show("Xóa danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData();
            }
        }
    }
}
