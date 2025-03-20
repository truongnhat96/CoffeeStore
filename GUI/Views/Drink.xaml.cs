using Database;
using GUI.Models;
using GUI.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace GUI.Views
{
    /// <summary>
    /// Interaction logic for Drink.xaml
    /// </summary>
    public partial class Drink : Window
    {
        public ObservableCollection<CategoryModel> Categories { get; set; }

        public Drink()
        {
            InitializeComponent();

            // Khởi tạo danh sách đồ uống
            LoadDrinks();
        }


        private void LoadDrinks()
        {
            DataContext = new DrinkViewModel();
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string appFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Uploads");


                    // Tạo thư mục nếu chưa tồn tại
                    if (!Directory.Exists(appFolder))
                    {
                        Directory.CreateDirectory(appFolder);
                    }

                    string fileName = Path.GetFileName(openFileDialog.FileName);
                    string destFilePath = Path.Combine(appFolder, fileName);

                    // Copy ảnh vào thư mục ứng dụng
                    File.Copy(openFileDialog.FileName, destFilePath, true);

                    txtImageUrl.Text = Path.Combine("Images", "Uploads", fileName);

                    MessageBox.Show("Ảnh đã được tải lên thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            int categoryID = (cbCategory.SelectedItem as CategoryModel).Id;
            string name = txtName.Text;
            string imagePath = txtImageUrl.Text.Contains(appFolder) ? txtImageUrl.Text.Replace(appFolder, string.Empty) : txtImageUrl.Text;
            var price = numPrice.Value;
            DataProvider.Instance.ExecuteNonQuery(@"INSERT INTO DRINKS
                                                   VALUES ( @NAME, @CATEGORYID, @PRICE, @IMAGE )"
            , new object[] { name, categoryID, price, imagePath });
            MessageBox.Show("Thêm đồ uống thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDrinks();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            DataProvider.Instance.ExecuteNonQuery("EXECUTE SP_DeleteDrink @ID, @NAME", new object[] { id, name });
            MessageBox.Show("Xóa đồ uống thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDrinks();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            int categoryID = (cbCategory.SelectedItem as CategoryModel).Id;
            int id = int.Parse(txtId.Text);
            string imagePath = txtImageUrl.Text.Contains(appFolder) ? txtImageUrl.Text.Replace(appFolder, string.Empty) : txtImageUrl.Text;
            var price = numPrice.Value;
            DataProvider.Instance.ExecuteNonQuery(@"UPDATE DRINKS SET Name = @NAME, DrinkCategoryID = @CATEGORYID, Price = @PRICE, ImagePath = @IMAGE WHERE ID = @ID", new object[] { txtName.Text, categoryID, price, imagePath, id });
            MessageBox.Show("Cập nhật đồ uống thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            LoadDrinks();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            LoadDrinks();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                DataContext = new DrinkViewModel(txtSearch.Text);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
