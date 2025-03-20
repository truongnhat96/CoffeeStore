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
    public class TableViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TableModel> _tables;
        private TableModel _selectedTable;
        public ObservableCollection<TableModel> Tables { get => _tables; set { _tables = value; OnPropertyChanged(); } }

        public TableModel SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (_selectedTable != value)
                {
                    _selectedTable = value;
                    OnPropertyChanged();
                }
            }
        }

        public TableViewModel()
        {
            LoadData();
        }

        public TableViewModel(string keyword)
        {
            LoadData(keyword);
        }

        private void LoadData(string keyword)
        {
            var data = DataProvider.Instance.ExecuteQuery($@"SELECT ID, TableNumber AS [Tên bàn], Status as [Trạng thái] FROM TABLES WHERE TableNumber COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%{keyword}%'");
            Tables = new ObservableCollection<TableModel>();
            foreach (DataRow item in data.Rows)
            {
                Tables.Add(new TableModel(item));
            }
        }

        private void LoadData()
        {
            var data = DataProvider.Instance.ExecuteQuery("EXECUTE SP_TableLoad");
            Tables = new ObservableCollection<TableModel>();
            foreach (DataRow item in data.Rows)
            {
                Tables.Add(new TableModel(item));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
