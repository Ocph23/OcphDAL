using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ocph.DAL.Provider.SQLite
{
    public class SQLiteDbConnection : IDbConnection
    {
        private SQLiteConnection _Connection;
        private SQLiteCommand _Command;
        private SQLiteTransaction _Transaction;
        public SQLiteDbConnection() { }
        public SQLiteDbConnection(string constr)
        {
            this.ConnectionString = constr;
            _Connection = new SQLiteConnection(constr);
        }

        private SQLiteCommand Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        private SQLiteTransaction Transaction
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
                if (_Connection == null)
                    _Connection = new SQLiteConnection(value);
                else
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
            if (_Command == null)
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
            catch (SQLiteException Ex)
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
            return new SQLiteParameter(paramaterName, value);
        }

    }
}
