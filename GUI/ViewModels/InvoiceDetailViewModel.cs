using Database;
using GUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class InvoiceDetailViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BillDetailModel> _bills;

        private string _totalPrice;
        private string _discount;
        private string _clientName;
        private string _tableName;
        private DateTime _created;
        private string iD;

        public ObservableCollection<BillDetailModel> Bills { get => _bills; set { _bills = value; OnPropertyChanged(); } }

        public string TotalPrice { get => _totalPrice; set { _totalPrice = value; OnPropertyChanged(); } }
        public string Discount { get => _discount; set { _discount = value; OnPropertyChanged(); } }
        public string ClientName { get => _clientName; set { _clientName = value;OnPropertyChanged(); } }
        public string TableName { get => _tableName; set { _tableName = value; OnPropertyChanged(); } }

        public DateTime Created { get => _created; set { _created = value; OnPropertyChanged();  } }

        public string ID { get => iD; set { iD = value; OnPropertyChanged(); } }

        public InvoiceDetailViewModel(int tableId, string discount)
        {
            Discount = discount;
            LoadBillDetails(tableId);
            TableName = "Thông Tin Hóa Đơn " + GetTableName(tableId);
            Created = GetDate(tableId);
        }

        private DateTime GetDate(int tableId)
        {
            var data = DataProvider.Instance.ExecuteQuery("SELECT TimeCheckIn From Bill WHERE TableID = @ID", new object[] { tableId });
            return (DateTime)data.Rows[0]["TimeCheckIn"];
        }

        private string GetTableName(int tableId)
        {
            var table = DataProvider.Instance.ExecuteQuery("select TableNumber from TABLES where ID = @ID", new object[] { tableId });
            return table.Rows[0]["TableNumber"].ToString();
        }

        private void LoadBillDetails(int tableId)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select BillID, DRINKS.Name, BILL_INFOMATION.Count, Price, BILL_INFOMATION.Count * Price AS SUM from DRINKS join BILL_INFOMATION ON DRINKS.ID = BILL_INFOMATION.DrinkID JOIN BILL ON BILL.ID = BILL_INFOMATION.BillID WHERE BILL.TableID = @ID AND BILL.Status = 0", new object[] { tableId });
            ID = "00" + data.Rows[0]["BillID"].ToString();
            var list = new ObservableCollection<BillDetailModel>();
            double totalPrice = 0;
            foreach (DataRow row in data.Rows)
            {
                var bill = new BillDetailModel(row);
                totalPrice += bill.Total;
                list.Add(bill);
            }
            Bills = list;
            var dis = Discount.Replace("%", "");
            if (int.TryParse(dis, out int discount))
            {
                totalPrice = totalPrice - (totalPrice * discount / 100);
            }
            TotalPrice = totalPrice.ToString("C0", new CultureInfo("vi-VN"));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
