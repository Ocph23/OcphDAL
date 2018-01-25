using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DAL.AspNet.SimpleAuth.Mysql;
using DAL.AspNet.SimpleAuth.Mysql.Models;

namespace DAL.AspNet.SimpleAuth.Mysql
{
    public class AuthContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public AuthContext()
        {

            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public AuthContext(string constring)
        {

            this.ConnectionString = constring;
        }

        public IRepository<userroles> UserRoles{ get { return new Repository<userroles>(this); } }
        public IRepository<users> Users { get { return new Repository<users>(this); } }
        public IRepository<roles> Roles { get { return new Repository<roles>(this); } }
    
        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
