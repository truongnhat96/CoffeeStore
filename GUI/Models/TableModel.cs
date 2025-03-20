using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUI.Models
{
    public class TableModel
    {
        private int iD;
        private string tableName;
        private string status;

        public TableModel() { }
        public TableModel(int iD, string tableName, string status)
        {
            this.iD = iD;
            this.tableName = tableName;
            this.status = status;
        }
        public TableModel(DataRow rows)
        {
            iD = (int)rows["ID"];
            tableName = (string)rows["Tên bàn"];
            status = (string)rows["Trạng thái"];

            switch (status)
            {
                case "Có khách":
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#57F204"));
                    break;
                case "Khách đặt trước":
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F66358"));
                    break;
                default:
                    Background = new SolidColorBrush(Colors.White);
                    break;
            }
        }


        public int ID { get => iD; set => iD = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public string Status { get => status; set => status = value; }
        public SolidColorBrush Background { get; set; }
    }
}
