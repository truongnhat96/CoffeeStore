using Database;
using GUI.Models;
using GUI.Views;
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
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CategoryModel> categories;
        private CategoryModel selectedCategory;

        public CategoryModel SelectedCategory { get => selectedCategory; set { selectedCategory = value; OnPropertyChanged(); } }
        public CategoryViewModel() 
        {
            LoadCategory();
        }

        public CategoryViewModel(string keyword)
        {
            LoadCategory(keyword);
        }

        private void LoadCategory(string keyword)
        {
            var category = DataProvider.Instance.ExecuteQuery($"SELECT ID, NAME FROM DRINK_CATEGORY where Name COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%{keyword}%'");
            var list = new ObservableCollection<CategoryModel>();
            foreach (DataRow item in category.Rows)
            {
                list.Add(new CategoryModel(item));
            }
            Categories = list;
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

        public ObservableCollection<CategoryModel> Categories { get => categories; set { categories = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
