using Database;
using GUI.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;

namespace GUI.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TableModel> _tables;
        private ObservableCollection<BillDetailModel> _billDetails;
        private ObservableCollection<DrinkModel> _drinks;
        private ObservableCollection<CategoryModel> _categories;

        private TableModel _selectedTable;
        private CategoryModel _selectedCategory;

        private string _total;
        private int _discount;
        private double originalTotal;

        public ObservableCollection<TableModel> Tables
        {
            get => _tables;
            set { _tables = value; OnPropertyChanged(nameof(Tables)); }
        }

        public TableModel SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    OnPropertyChanged(nameof(SelectedTable));

                    // Mỗi khi SelectedTable thay đổi, ta load dữ liệu hóa đơn của bàn đó
                    if (_selectedTable != null)
                    {
                        LoadBillDetails(_selectedTable.ID);
                    }
                    else
                    {
                        BillDetails = null;
                    }
                }
            }
        }

        public ObservableCollection<BillDetailModel> BillDetails
        {
            get => _billDetails;
            set { _billDetails = value; OnPropertyChanged(nameof(BillDetails)); }
        }

        public ObservableCollection<DrinkModel> Drinks { get => _drinks; set { _drinks = value; OnPropertyChanged(nameof(Drinks)); } }

        public ObservableCollection<CategoryModel> Categories { get => _categories; set { _categories = value; OnPropertyChanged(nameof(Categories)); } }

        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    if (_selectedCategory != null)
                    {
                        LoadDrinks(_selectedCategory.Id);
                    }
                    else
                    {
                        SelectedCategory = null;
                    }
                }
            }
        }

        public string Total { get => _total; set { _total = value; OnPropertyChanged(nameof(Total)); } }

        public int Discount
        {
            get => _discount;
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged(nameof(Discount));
                    UpdateTotalAfterDiscount();
                }
            }
        }

        public HomeViewModel()
        {
            LoadTables();
            LoadCategory();
        }

        private void LoadCategory()
        {
            var category = DataProvider.Instance.ExecuteQuery("select * from DRINK_CATEGORY");
            var list = new ObservableCollection<CategoryModel>();
            foreach (DataRow item in category.Rows)
            {
                list.Add(new CategoryModel(item));
            }
            Categories = list;
        }

        private void LoadTables()
        {
            // Ví dụ gọi store procedure SP_TableLoad
            var tables = DataProvider.Instance.ExecuteQuery("EXECUTE SP_TableLoad");
            var list = new ObservableCollection<TableModel>();
            foreach (DataRow row in tables.Rows)
            {
                list.Add(new TableModel(row));
            }
            Tables = list;
        }

        private void LoadBillDetails(int tableId)
        {
            // Ví dụ gọi store procedure (bạn thay bằng query thực tế)
            // Giả sử: SP_GetBillDetailsByTable @tableId
            DataTable data = DataProvider.Instance.ExecuteQuery("select BillID, DRINKS.Name, BILL_INFOMATION.Count, Price, BILL_INFOMATION.Count * Price AS SUM from DRINKS join BILL_INFOMATION ON DRINKS.ID = BILL_INFOMATION.DrinkID JOIN BILL ON BILL.ID = BILL_INFOMATION.BillID WHERE BILL.TableID = @ID AND BILL.Status = 0", new object[] { tableId });

            var list = new ObservableCollection<BillDetailModel>();
            double totalPrice = 0;
            foreach (DataRow row in data.Rows)
            {
                var bill = new BillDetailModel(row);
                totalPrice += bill.Total;
                list.Add(bill);
            }
            originalTotal = totalPrice;
            BillDetails = list;
            Total = totalPrice.ToString("C0", new CultureInfo("vi-VN"));
            Discount = 0;
        }

        private void LoadDrinks(int categoryId)
        {
            // _refreshDrink.Invoke();
            var drinks = DataProvider.Instance.ExecuteQuery(@"
                SELECT DRINKS.ID, DRINKS.Name AS [Tên đồ], DRINKS.DrinkCategoryID, 
                       DRINK_CATEGORY.Name AS [Danh mục],
                       Price as [Giá tiền], ImagePath as URL 
                FROM DRINKS 
                JOIN DRINK_CATEGORY ON DRINKS.DrinkCategoryID = DRINK_CATEGORY.ID where DRINK_CATEGORY.ID = @ID", new object[] { categoryId });
            var list = new ObservableCollection<DrinkModel>();
            foreach (DataRow row in drinks.Rows)
            {
                list.Add(new DrinkModel(row));
            }
            Drinks = list;
        }

        private void UpdateTotalAfterDiscount()
        {
            double newTotal = originalTotal * (1 - _discount / 100.0);
            Total = newTotal.ToString("C0", new CultureInfo("vi-VN"));
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
