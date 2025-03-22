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
    public class DrinkViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DrinkModel> _drinks;
        private ObservableCollection<CategoryModel> _categories;

        private DrinkModel _selectedDrink;
        private CategoryModel _selectedCategory;


        public DrinkModel SelectedDrink
        {
            get => _selectedDrink;
            set
            {
                if (_selectedDrink != value)
                {
                    _selectedDrink = value;
                    OnPropertyChanged();
                    // Khi chọn drink mới, cập nhật SelectedCategory dựa trên CategoryId của drink đó
                    if (_selectedDrink != null && Categories != null)
                    {
                        SelectedCategory = Categories.FirstOrDefault(c => c.Id == _selectedDrink.CategoryID);
                    }
                    else
                    {
                        SelectedCategory = null;
                    }
                }
            }
        }

        // Thuộc tính binding cho combobox
        public CategoryModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();
                    // Cập nhật thuộc tính của drink hiện hành khi danh mục thay đổi
                    if (SelectedDrink != null && _selectedCategory != null)
                    {
                        SelectedDrink.CategoryID = _selectedCategory.Id;
                        SelectedDrink.CategoryName = _selectedCategory.Name;
                    }
                }
            }
        }

        public ObservableCollection<DrinkModel> Drinks
        {
            get => _drinks;
            set { _drinks = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CategoryModel> Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged(); }
        }

        public DrinkViewModel()
        {
            LoadCategory();
            LoadDrinks();
        }

        public DrinkViewModel(string keyword)
        {
            LoadCategory();
            SearchingResult(keyword);
        }

        private void SearchingResult(string keyword)
        {
            var drinks = DataProvider.Instance.ExecuteQuery($@"
                SELECT DRINKS.ID, DRINKS.Name AS [Tên đồ], DRINKS.DrinkCategoryID, 
                       DRINK_CATEGORY.Name AS [Danh mục],
                       Price as [Giá tiền], ImagePath as URL 
                FROM DRINKS 
                JOIN DRINK_CATEGORY ON DRINKS.DrinkCategoryID = DRINK_CATEGORY.ID WHERE DRINKS.Name COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%{keyword}%'");
            Drinks = new ObservableCollection<DrinkModel>();
            foreach (DataRow row in drinks.Rows)
            {
                Drinks.Add(new DrinkModel(row));
            }
        }

        private void LoadCategory()
        {
            var categories = DataProvider.Instance.ExecuteQuery("SELECT * FROM DRINK_CATEGORY");
            Categories = new ObservableCollection<CategoryModel>();
            foreach (DataRow row in categories.Rows)
            {
                Categories.Add(new CategoryModel(row));
            }
        }

        private void LoadDrinks()
        {
            //thêm trường DrinkCategoryID vào query nếu cần dùng cho binding
            var drinks = DataProvider.Instance.ExecuteQuery(@"
                SELECT DRINKS.ID, DRINKS.Name AS [Tên đồ], DRINKS.DrinkCategoryID, 
                       DRINK_CATEGORY.Name AS [Danh mục],
                       Price as [Giá tiền], ImagePath as URL 
                FROM DRINKS 
                JOIN DRINK_CATEGORY ON DRINKS.DrinkCategoryID = DRINK_CATEGORY.ID");
            Drinks = new ObservableCollection<DrinkModel>();
            foreach (DataRow row in drinks.Rows)
            {
                Drinks.Add(new DrinkModel(row));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
