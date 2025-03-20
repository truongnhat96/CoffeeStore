using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace GUI.Models
{
    public class BillDetailModel : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int quantity;
        private double price;
        private double total;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public BillDetailModel(int id, string name, int count, double price, double total)
        {
            this.id = id;
            this.name = name;
            this.quantity = count;
            this.price = price;
            this.total = total;
        }
        public BillDetailModel(DataRow row)
        {
            this.id = (int)row["BillID"];
            this.name = row["Name"].ToString();
            this.quantity = (int)row["Count"];
            this.price = (double)row["Price"];
            this.total = (double)row["SUM"];
        }
        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        public int Quantity { get => quantity; set { quantity = value; OnPropertyChanged(); } }
        public double Price { get => price; set { price = value; OnPropertyChanged(); } }
        public double Total { get => total; set { total = value; OnPropertyChanged(); } }
    }
}
