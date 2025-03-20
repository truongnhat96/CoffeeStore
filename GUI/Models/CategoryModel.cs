using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public CategoryModel() { }
        private int id;
        private string name;

        public CategoryModel(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public CategoryModel(DataRow row)
        {
            id = (int)row["ID"];
            name = row["Name"].ToString();
        }
        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
