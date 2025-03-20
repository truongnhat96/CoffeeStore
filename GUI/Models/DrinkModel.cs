using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.Models
{
    public class DrinkModel : INotifyPropertyChanged
    {
        public DrinkModel() { }

        private int id;
        private string name;
        private int categoryID;
        private string categoryName;
        private double price;
        private string imagePath;

        public DrinkModel(int id, string name, int categoryID, double price)
        {
            this.id = id;
            this.name = name;
            this.categoryID = categoryID;
            this.price = price;
        }

        public DrinkModel(DataRow row)
        {
            this.id = (int)row["ID"];
            this.name = row["Tên đồ"].ToString();
            this.price = (double)row["Giá tiền"];
            this.categoryID = (int)row["DrinkCategoryID"];
            this.imagePath = !string.IsNullOrEmpty(row["URL"].ToString()) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, row["URL"].ToString()) : string.Empty;
            this.categoryName = row["Danh mục"].ToString();
        }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public int CategoryID { get => categoryID; set { categoryID = value; OnPropertyChanged(); } }
        public double Price { get => price; set { price = value; OnPropertyChanged(); } }
        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string ImagePath { get => imagePath; set { imagePath = value; OnPropertyChanged(); } }
        public string CategoryName { get => categoryName; set { categoryName = value; OnPropertyChanged(); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
