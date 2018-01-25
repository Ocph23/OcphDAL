using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AspNet.SimpleAuth.Mysql.Models
{
    [TableName("Users")]
    public  class users:DAL.BaseNotifyProperty
    {
        private int idUser;
        private string email;
        private string userName;
        private string password;

        [PrimaryKey("IdSuer")]
        [DbColumn("idUser")]
        public int IdUser
        {
            get { return idUser; }
            set { SetProperty(ref idUser,value); }
        }


        [DbColumn("Email")]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        [DbColumn("Password")]
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

    }
}
