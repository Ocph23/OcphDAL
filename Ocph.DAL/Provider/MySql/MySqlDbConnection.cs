using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ocph.DAL.Provider.MySql
{
    public class MySqlDbConnection:IDbConnection
    {
        private MySqlConnection _Connection = new MySqlConnection();
        private MySqlCommand _Command;
        private MySqlTransaction _Transaction;
        public MySqlDbConnection() { }
        public MySqlDbConnection(string constr)
        {
            this.ConnectionString = constr;
        }

        private MySqlCommand Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        private MySqlTransaction Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }


        public void Dispose()
        {
            if (_Command != null)
                _Command = null;

            if (_Connection.State == ConnectionState.Open)
                _Connection.Close();

            if (_Transaction != null)
                _Transaction = null;
        }

        //Connection

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            _Transaction = _Connection.BeginTransaction(il);
            return _Transaction;
        }

        public IDbTransaction BeginTransaction()
        {
            this.Open();

            if (_Transaction == null)
                _Transaction = _Connection.BeginTransaction();
            if (_Command == null)
                _Command = _Connection.CreateCommand();
            _Command.Transaction = _Transaction;
            return _Transaction;
        }

        public void ChangeDatabase(string databaseName)
        {
            _Connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            _Connection.Close();
        }

        public string ConnectionString
        {
            get
            {
                return _Connection.ConnectionString;
            }
            set
            {
                _Connection.ConnectionString = value;
            }
        }

        public int ConnectionTimeout
        {
            get { return _Connection.ConnectionTimeout; }
        }

        public IDbCommand CreateCommand()
        {
            this.Open();
            _Command = _Connection.CreateCommand();
            return _Command;
        }

        public string Database
        {
            get { return _Connection.Database; }
        }

        public void Open()
        {
            try
            {
                if (this.State != ConnectionState.Open)
                    _Connection.Open();
            }
            catch (MySqlException Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public ConnectionState State
        {
            get { return _Connection.State; }
        }



        public IDbDataParameter CreateParameter(string paramaterName, object value)
        {
            return new MySqlParameter(paramaterName, value);
        }


        public void ClearParameter()
        {
            if(Command!=null && Command.Parameters!=null)
            {
                Command.Parameters.Clear();
            }
        }

    }
}
