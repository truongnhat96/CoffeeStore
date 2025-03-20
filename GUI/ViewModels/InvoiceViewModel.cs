using Database;
using GUI.Models;
using GUI.ViewModels.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.ViewModels
{
    public class InvoiceViewModel : INotifyPropertyChanged
    {
        private int _selectedPage;
        public int SelectedPage
        {
            get => _selectedPage;
            set { _selectedPage = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand SelectPageCommand { get; }

        private DateTime? _fromDate;
        public DateTime? FromDate
        {
            get => _fromDate;
            set { _fromDate = value; OnPropertyChanged(); }
        }

        private DateTime? _toDate;
        public DateTime? ToDate
        {
            get => _toDate;
            set { _toDate = value; OnPropertyChanged(); }
        }

        public ObservableCollection<int> PageNumbers { get; set; }

        private ObservableCollection<InvoiceModel> _invoiceList;
        public ObservableCollection<InvoiceModel> InvoiceList
        {
            get => _invoiceList;
            set
            {
                _invoiceList = value;
                OnPropertyChanged();
            }
        }

        private double _totalRevenue;
        public double TotalRevenue
        {
            get => _totalRevenue;
            set { _totalRevenue = value; OnPropertyChanged(); }
        }


        public InvoiceViewModel()
        {
            // Khởi tạo data tạm thời
            InvoiceList = new ObservableCollection<InvoiceModel>();

            DateTime now = DateTime.Now;
            FromDate = new DateTime(now.Year, now.Month, 1);
            ToDate = FromDate.Value.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);

            // Giả sử có 5 trang phân trang
            PageNumbers = new ObservableCollection<int>();

            // Trang mặc định là 1
            SelectedPage = 1;

            // Khởi tạo các command
            SelectPageCommand = new RelayCommand(OnSelectPage);
            SearchCommand = new RelayCommand(OnSearch);

            // Load dữ liệu ban đầu
            OnSearch(null);
        }

        private void OnSelectPage(object parameter)
        {
            if (FromDate == null || ToDate == null)
                return;

            if (int.TryParse(parameter.ToString(), out int page))
            {
                SelectedPage = page;
                InvoiceList.Clear();
                // Load dữ liệu của trang được chọn
                var bills = DataProvider.Instance.ExecuteQuery("EXECUTE SP_SearchBill @IN, @OUT, @PAGE", new object[] { FromDate.Value, ToDate.Value, page });
                foreach (DataRow item in bills.Rows)
                {
                    InvoiceList.Add(new InvoiceModel(item));
                }
            }
        }

        private void OnSearch(object value)
        {
            if (FromDate == null || ToDate == null)
                return;

            InvoiceList.Clear();

            var bills = DataProvider.Instance.ExecuteQuery("EXECUTE SP_SearchBill @IN, @OUT, @PAGE", new object[] { FromDate.Value, ToDate.Value, 1 });
            foreach (DataRow item in bills.Rows)
            {
                InvoiceList.Add(new InvoiceModel(item));
            }

            int allrows = (int)DataProvider.Instance.ExecuteScalar(@"SELECT COUNT(*) FROM BILL WHERE BILL.Status = 1 AND TimeCheckOut >= @In AND TimeCheckOut <= @Out", new object[] { FromDate.Value, ToDate.Value });
            int totalpages = allrows / 10;
            if (allrows % 10 != 0) ++allrows;

            PageNumbers.Clear();
            for (int i = 1; i <= totalpages; i++)
            {
                PageNumbers.Add(i);
            }
            UpdateTotalRevenue();
            SelectedPage = 1;
        }

        private void UpdateTotalRevenue()
        {
            TotalRevenue = (double)DataProvider.Instance.ExecuteScalar(@"SELECT SUM(TotalPrice) FROM BILL WHERE BILL.Status = 1 AND TimeCheckOut >= @In AND TimeCheckOut <= @Out", new object[] { FromDate.Value, ToDate.Value });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
