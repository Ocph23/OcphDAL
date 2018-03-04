using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Ocph.DAL;
using Ocph.DAL.Provider.MySql;

namespace AppDataBaseCreateModel
{
    public class ContextTest : MySqlDbConnection
    {
       
        public ContextTest(string constr):base(constr) {

            ConnectionString = constr;
        }

        //  public IRepository<IdentityUser> Users { get { return new Repository<IdentityUser>(this); } }

       
    }
}
