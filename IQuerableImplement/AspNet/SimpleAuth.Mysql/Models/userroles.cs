using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AspNet.SimpleAuth.Mysql.Models
{

    [TableName("userroles")]
    public class userroles:DAL.BaseNotifyProperty
    {
        private int iduser;
        private int idrole;
        [DbColumn("IdUser")]
        public int IdUser
        {
            get { return iduser; }
            set { SetProperty(ref iduser, value);  }
        }

        [DbColumn("IdUser")]
        public int IdRole   
        {
            get { return idrole; }
            set { SetProperty(ref idrole, value); }
        }

    }
}
