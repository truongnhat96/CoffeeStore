using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    public class AccountModel
    {
        private string username;
        private string password;
        private string displayname;
        private string auth;
        private DateTime dateOfBirth;
        private string email;
        private DateTime workBegin;

        public AccountModel(string username, string displayname, string accountType, string password = null)
        {
            this.username = username;
            this.password = password;
            this.displayname = displayname;
            this.auth = accountType;
        }

        public AccountModel(DataRow row, bool isLogin)
        {
            if (isLogin)
            {
                username = row["Username"].ToString();
                password = row["Password"].ToString();
                displayname = row["DisplayName"].ToString();
                auth = row["AccountType"].ToString();
                dateOfBirth = (DateTime)row["DateOfBirth"];
                email = row["Email"].ToString();
                workBegin = (DateTime)row["WorkBegin"];
            }
            else
            {
                username = row["Username"].ToString();
                displayname = row["DisplayName"].ToString();
                auth = row["AccountType"].ToString();
                dateOfBirth = (DateTime)row["DateOfBirth"];
                email = row["Email"].ToString();
                workBegin = (DateTime)row["WorkBegin"];
            }

        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Displayname { get => displayname; set => displayname = value; }
        public string Auth { get => auth; set => auth = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Email { get => email; set => email = value; }
        public DateTime WorkBegin { get => workBegin; set => workBegin = value; }
    }
}

