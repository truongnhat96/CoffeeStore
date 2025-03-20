using Database;
using GUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class HistoryImportViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProductModel> _productsImported;

        public ObservableCollection<ProductModel> ProductsImported { get => _productsImported; set { _productsImported = value; OnPropertyChanged(); } }

        public HistoryImportViewModel()
        {
            ProductsImported = new ObservableCollection<ProductModel>();
            var data = DataProvider.Instance.ExecuteQuery("SELECT ID, ProductName, Count, Unit, Price, TimeImport, DisplayName FROM History");
            foreach (DataRow item in data.Rows)
            {
                ProductsImported.Add(new ProductModel(item, true));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
