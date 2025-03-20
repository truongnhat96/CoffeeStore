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
    public class StaffViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<AccountModel> _accounts;
        private ObservableCollection<string> _auths;

        private AccountModel _selectedAccount;
        private string _selectedAuth;


        public AccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                if (_selectedAccount != value)
                {
                    _selectedAccount = value;
                    OnPropertyChanged();
                    // Khi chọn drink mới, cập nhật SelectedCategory dựa trên CategoryId của drink đó
                    if (_selectedAccount != null && Auths != null)
                    {
                        SelectedAuth = Auths.FirstOrDefault(a => a == _selectedAccount.Auth);
                    }
                    else
                    {
                        SelectedAuth = null;
                    }
                }
            }
        }

        // Thuộc tính binding cho combobox
        public string SelectedAuth
        {
            get => _selectedAuth;
            set
            {
                if (_selectedAuth != value)
                {
                    _selectedAuth = value;
                    OnPropertyChanged();
                    // Cập nhật thuộc tính của drink hiện hành khi danh mục thay đổi
                    if (SelectedAccount != null && !string.IsNullOrEmpty(_selectedAuth))
                    {
                        SelectedAccount.Auth = _selectedAuth;
                        // Nếu DrinkModel cũng implements INotifyPropertyChanged thì các binding sẽ tự động update
                        OnPropertyChanged(nameof(SelectedAccount));
                    }
                }
            }
        }

        public ObservableCollection<AccountModel> Accounts
        {
            get => _accounts;
            set { _accounts = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Auths
        {
            get => _auths;
            set { _auths = value; OnPropertyChanged(); }
        }

        public StaffViewModel()
        {
            LoadAuth();
            LoadAccounts();
        }

        public StaffViewModel(string keyword)
        {
            LoadAuth();
            SearchingResult(keyword);
        }

        private void SearchingResult(string keyword)
        {
            var drinks = DataProvider.Instance.ExecuteQuery($"SELECT Username, DisplayName, AccountType, DateOfBirth, Email, WorkBegin FROM ACCOUNT WHERE DisplayName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%{keyword}%'");
            Accounts = new ObservableCollection<AccountModel>();
            foreach (DataRow row in drinks.Rows)
            {
                Accounts.Add(new AccountModel(row, false));
            }
            SelectedAccount = Accounts.Count > 0 ? Accounts[0] : null;
        }

        private void LoadAuth()
        {
            Auths = new ObservableCollection<string> { "ADMIN", "STAFF" };
        }

        private void LoadAccounts()
        {
            //thêm trường DrinkCategoryID vào query nếu cần dùng cho binding
            var accounts = DataProvider.Instance.ExecuteQuery("SELECT Username, DisplayName, AccountType, DateOfBirth, Email, WorkBegin FROM ACCOUNT");

            Accounts = new ObservableCollection<AccountModel>();
            foreach (DataRow row in accounts.Rows)
            {
                Accounts.Add(new AccountModel(row, false));
            }
            SelectedAccount = Accounts.Count > 0 ? Accounts[0] : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
