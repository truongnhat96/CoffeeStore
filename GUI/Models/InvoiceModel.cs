using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class InvoiceModel
    {
        public InvoiceModel() { }
        private int iD;
        private string tableName;
        private DateTime? timeCheckIn;
        private DateTime? timeCheckOut;
        private int discount;
        private double totalprice;

        public InvoiceModel(int iD, string tableName, DateTime? timeCheckIn, DateTime? timeCheckOut, int discount, double totalprice)
        {
            this.iD = iD;
            this.tableName = tableName;
            this.timeCheckIn = timeCheckIn;
            this.timeCheckOut = timeCheckOut;
            this.discount = discount;
            this.totalprice = totalprice;
        }

        public InvoiceModel(DataRow row)
        {
            iD = (int)row["ID"];
            tableName = row["TableNumber"].ToString();
            timeCheckIn = (DateTime?)row["TimeCheckIn"];
            timeCheckOut = (DateTime?)row["TimeCheckOut"];
            discount = (int)row["Discount"];
            totalprice = (double)row["TotalPrice"];
        }

        public int ID { get => iD; set => iD = value; }
        public DateTime? TimeCheckIn { get => timeCheckIn; set => timeCheckIn = value; }
        public DateTime? TimeCheckOut { get => timeCheckOut; set => timeCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
        public double Totalprice { get => totalprice; set => totalprice = value; }
        public string TableName { get => tableName; set => tableName = value; }
    }
}
