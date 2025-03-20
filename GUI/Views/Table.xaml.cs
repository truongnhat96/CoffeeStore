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

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        public Table()
        {
            InitializeComponent();
            LoadTables();
        }

        private void LoadTables()
        {
            DataContext = new TableViewModel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Instance.ExecuteNonQuery(@"INSERT INTO TABLES VALUES ( @TABLENAME, N'Trống')", new object[] { txtName.Text });
            MessageBox.Show("Thêm bàn thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadTables();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_DeleteTable @ID, @TABLENAME", new object[] { int.Parse(txtId.Text), txtName.Text });
            MessageBox.Show("Xóa bàn thành công (bàn đang có khách sẽ không thể bị xóa)", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadTables();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Instance.ExecuteNonQuery("UPDATE TABLES SET TableNumber = @TABLENAME WHERE ID = @ID", new object[] { txtName.Text, int.Parse(txtId.Text) });
            MessageBox.Show("Sửa bàn thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadTables();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            LoadTables();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new TableViewModel(txtSearch.Text);
        }
    }
}
