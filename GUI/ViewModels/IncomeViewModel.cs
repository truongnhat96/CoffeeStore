using Database;
using GUI.Models;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Controls;

namespace GUI.ViewModels
{
    public class IncomeViewModel : INotifyPropertyChanged
    {
        private SeriesCollection _seriesCollection;
        private int[] _years;
        private string[] _labels;
        private int _selectedYear;
        private Func<double, string> _formatter;
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { _seriesCollection = value; OnPropertyChanged(); } }

        public string[] Labels { get => _labels; set { _labels = value; OnPropertyChanged(); } }

        public Func<double, string> Formatter { get => _formatter; set { _formatter = value; OnPropertyChanged(); } }

        public int[] Years { get => _years; set { _years = value; OnPropertyChanged(); } }

        public int SelectedYear 
        { 
            get => _selectedYear; 
            set 
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();

                    if (_selectedYear != 0)
                    {
                        LoadDataChart(_selectedYear);
                    }
                    else
                    {
                        SeriesCollection = null;
                    }
                }
            } 
        }

        public IncomeViewModel() 
        {
            var query = DataProvider.Instance.ExecuteQuery("SELECT DISTINCT YEAR(TimeCheckOut) AS YEARS FROM BILL WHERE Status = 1");
            Years = query.AsEnumerable().Select(x => x.Field<int>("YEARS")).ToArray();
            this.SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N0");
        }

        private void LoadDataChart(int year)
        {
            var data = DataProvider.Instance.ExecuteQuery("EXECUTE SP_GetTotalIncomeByMonth @year", new object[] { year });
            ChartValues<double> values = data.AsEnumerable().Select(x => x.Field<double>("TotalIncome")).ToList().AsChartValues();
            Labels = data.AsEnumerable().Select(x => x.Field<int>("Month")).Select(x => "Tháng " + x.ToString()).ToArray();

            SeriesCollection.Clear();
            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Doanh thu",
                Values = values
            });

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
