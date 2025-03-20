using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class ProductModel : INotifyPropertyChanged
    {
        private int _id;
        private string _productName;
        private int _quantity;
        private string _unit;
        private double _price;
        private DateTime _import = DateTime.Now;
        private string _displayName;

        public ProductModel(DataRow row, bool isHistoryImport)
        {
            if (isHistoryImport)
            {
                Id = (int)row["ID"];
                ProductName = row["ProductName"].ToString();
                Quantity = (int)row["Count"];
                Unit = row["Unit"].ToString();
                Price = (double)row["Price"];
                Import = (DateTime)row["TimeImport"];
                DisplayName = row["DisplayName"].ToString();
            }
            else
            {
                Id = (int)row["ID"];
                ProductName = row["ProductName"].ToString();
                Quantity = (int)row["Count"];
                Unit = row["Unit"].ToString();
                Price = (double)row["Price"];
                Import = (DateTime)row["TimeImport"];
            }
        }

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public string Unit
        {
            get => _unit;
            set { _unit = value; OnPropertyChanged(); }
        }

        public double Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public DateTime Import
        {
            get => _import;
            set { _import = value; OnPropertyChanged(); }
        }

        public int Id { get => _id; set { _id = value; OnPropertyChanged(); } }

        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
