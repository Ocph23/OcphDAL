using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AspNet.SimpleAuth.Mysql.Models
{


    [TableName("roles")]
     public class roles:BaseNotifyProperty
    {
        private int idRole;
        private string name;
      
        [PrimaryKey("IdRole")]
        [DbColumn("IdRole")]
        public int IdUser
        {
            get { return idRole; }
            set { SetProperty(ref idRole, value); }
        }


        [DbColumn("Name")]
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

    }




}
