using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class IncomeModel
    {
        private double _totalIncome;
        private int _month;

        public IncomeModel(DataRow row)
        {
            this._totalIncome = (double)row["TotalIncome"];
            this.Month = (int)row["Month"];
        }
        public double TotalIncome { get => _totalIncome; set => _totalIncome = value; }
        public int Month { get => _month; set => _month = value; }
    }
}
