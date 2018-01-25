using DAL.ExpressionHandler;
using DAL.QueryBuilder;
using DAL.Repository;
using DAL.SQLites.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DContext
{
    public class SQLiteDataTable<T>:IDataTable<T> where T: class
    {
        public IDbContext datacontext { get; set; }

        public EntityInfo Entity { get; set; }
        public SQLiteDataTable(IDbContext dbContext)
        {
            this.datacontext = dbContext;
            this.Entity = new EntityInfo(typeof(T));
        }

        public bool Insert(T t)
        {
            try
            {
                IDbCommand cmd = datacontext.Connection.CreateCommand();
                cmd.Parameters.Clear();
                var iq = new InsertQuery(Entity);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = iq.GetQuerywithParameter(t);
                this.SetParameter(ref cmd, t);
                var result = (Int32)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (SQLiteException ex)
            {
                
                throw new System.Exception(ex.Message);
            }
        }

        public bool Delete(System.Linq.Expressions.Expression<Func<T, bool>> Predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(System.Linq.Expressions.Expression<Func<T, dynamic>> fieldUpdate, System.Linq.Expressions.Expression<Func<T, bool>> whereClause, object source)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Select(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            List<T> list = new List<T>();
            StringBuilder sb = new StringBuilder();

            sb.Append("(Select * From ").Append(Entity.TableName).Append(" Where ");

            sb.Append(new WhereTranslator().Translate(expression));

            sb.Append(")");
            IDbCommand cmd = datacontext.Connection.CreateCommand();
           
            cmd.CommandType = CommandType.Text;
            string text =this.CleanCommandtext(sb.ToString());
            cmd.CommandText = text;
            IDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                
                list = new MappingColumnSQLite(Entity).MappingWithoutInclud<T>(dr);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {

                dr.Close();
            }
            return list.AsQueryable(); 
        }

        private string CleanCommandtext(string p)
        {
            string result = string.Empty;


            var q = p.Trim().Substring(0, 1);

            if (q == "(")
            {
                var qq = p.Remove(0, 1);
                var a = qq.Length;
                result = qq;
            }

            var c = result.Substring(result.Length - 1, 1);
            if (c == ")")
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result;
        }

        public IQueryable<T> SelectAll()
        {
            List<T> list = new List<T>();
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * From ").Append(Entity.TableName);
            IDataReader dr = null;
            try
            {
                IDbCommand cmd = datacontext.Connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sb.ToString();
                dr = cmd.ExecuteReader();
                list = new MappingColumnSQLite(Entity).MappingWithoutInclud<T>(dr);
            }
            catch (Exception ex)
            {
                var result = ex;
                throw new Exception(ex.Message);

            }
            finally
            {
                dr.Close();
            }
            return list.AsQueryable<T>();
        }

        public IQueryable<T> Select(System.Linq.Expressions.Expression<Func<T, dynamic>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Includ( System.Linq.Expressions.Expression<Func<T, dynamic>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ExecuteStoreProcedureQuery(string storeProcedure)
        {
            throw new NotImplementedException();
        }

        public object ExecuteStoreProcedureNonQuery(string storeProcedure)
        {
            throw new NotImplementedException();
        }

        public int GetLastID(T t)
        {
            int result = 0;
            try
            {
                IDbCommand cmd = datacontext.Connection.CreateCommand();
                cmd.Parameters.Clear();
                var iq = new InsertQuery(Entity);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = iq.GetQuerywithParameter(t) + "; select last_insert_rowid();";
                iq.SetParameter(ref cmd, t);
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SQLiteException ex)
            {
                throw new System.Exception(ex.Message);
            }

            return result;
        }

        public object GetLastItem()
        {
            throw new NotImplementedException();
        }


        private void SetParameter(ref System.Data.IDbCommand cmd, object obj)
        {
            EntityInfo ent = new EntityInfo(obj.GetType());
            foreach (PropertyInfo p in ent.DbTableProperty)
            {
                cmd.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ent.GetAttributDbColumn(p)), CommonDAL.GetParameterValue(p, p.GetValue(obj))));
            }

        }

        public object ExecuteNonQuery(string query)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteQuery(string Query)
        {
            throw new NotImplementedException();
        }

    }
}
