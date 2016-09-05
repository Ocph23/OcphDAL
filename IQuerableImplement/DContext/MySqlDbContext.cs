using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DContext
{
    public class MySqlDbContext:IDbConnection, IDbContext
    {
        private string _ConnectionString;
        private MySqlConnection _Connection;

        public MySqlDbContext(string constring)
        {
            this.ConnectionString = constring;
            this._Connection = new MySqlConnection(_ConnectionString);
            
        }
       
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            this.Open();
            return _Connection.BeginTransaction(il);
        }

        public IDbTransaction BeginTransaction()
        {
            this.Open();

            return _Connection.BeginTransaction();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            if (this.State != ConnectionState.Closed)
                _Connection.Close();
        }

        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get { return _Connection.ConnectionTimeout; }
        }

        public IDbCommand CreateCommand()
        {
            this.Open();
            return _Connection.CreateCommand();
        }

        public string Database
        {
            get { return _Connection.Database; }
        }

        public void Open()
        {
            if (this.State != ConnectionState.Open)
                _Connection.Open();
        }

        public ConnectionState State
        {
            get { return _Connection.State; }
        }

        public void Dispose()
        {
            this.Close();
        }

        public IDbConnection Connection
        {
            get { return _Connection; }
        }
    }
}
