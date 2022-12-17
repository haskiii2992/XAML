using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAML
{
    class User
    {
        public int id { get; set; }

        private string login, email, pass, adminMode;

        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public string AdminMode
        {
            get { return adminMode; }
            set { adminMode = value; }
        }


        public User() { }

        public User(string login, string email, string pass, string adminMode)
        {
            this.login = login;
            this.email = email;
            this.pass = pass;
            this.adminMode = adminMode;
        }

    }
}
