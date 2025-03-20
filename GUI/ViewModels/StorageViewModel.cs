using Database;
using GUI.Models;
using GUI.ViewModels.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class StorageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> _productList;
        private ProductModel _selectedProduct;
        private double _totalPrice;

        public ObservableCollection<ProductModel> ProductList
        {
            get => _productList;
            set { _productList = value; OnPropertyChanged(); }
        }

        public ProductModel SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(); }
        }

        public double TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(); } }

        public StorageViewModel()
        {
            // Khởi tạo danh sách sản phẩm (demo)
            ProductList = new ObservableCollection<ProductModel>();

            LoadProducts();

            // Gán SelectedProduct mặc định
            SelectedProduct = ProductList.Count > 0 ? ProductList[0] : null;

            // Tính tổng tiền ban đầu
            CalculateTotal();
        }

        private void LoadProducts()
        {
            var data = DataProvider.Instance.ExecuteQuery(@"Select ID, ProductName, Count, Unit, Price, TimeImport from PRODUCT");
            ProductList.Clear();
            foreach (DataRow item in data.Rows)
            {
                ProductList.Add(new ProductModel(item, false));
            }
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            double total = 0;
            foreach (var p in ProductList)
            {
                total += p.Price * p.Quantity;
            }
            TotalPrice = total;
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

 

}
