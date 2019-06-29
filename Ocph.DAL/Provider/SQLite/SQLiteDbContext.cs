using Ocph.DAL.DbContext;
using Ocph.DAL.ExpressionHandler;
using Ocph.DAL.Mapping.SQLite;
using Ocph.DAL.QueryBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ocph.DAL.Provider.SQLite
{
    internal class SQLiteDbContext<T> : IDataTable<T>
    {
        public EntityInfo Entity { get; set; }
        public IDbConnection connection { get; set; }

        public SQLiteDbContext(IDbConnection con)
        {
            this.Entity = new EntityInfo(typeof(T));
            connection = con;
        }

        public bool Insert(T t)
        {
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.Parameters.Clear();
                var iq = new InsertQuery(Entity);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = iq.GetQuerywithParameter(t);
                SetParameter(ref cmd, t);
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
                throw new System.Exception(SQLiteProviderHelper.ErrorHandle(ex));
            }
        }

        public bool Delete(Expression<Func<T, bool>> Predicate)
        {
            bool result = false;
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = new DeleteQuery(Entity).GetQuery(Predicate);

            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            return result;
        }

        public bool Update(Expression<Func<T, dynamic>> fieldUpdate, Expression<Func<T, bool>> whereClause, object source)
        {

            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            bool result = false;
            var Query = new UpdateQuery(Entity,cmd);
            cmd.CommandText = Query.GetQueryWithParameter(fieldUpdate, whereClause, source);
      //      Query.SetParameters(cmd);
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);

            }
            return result;
        }

        public IQueryable<T> Select(Expression<Func<T, bool>> expression)
        {
            List<T> list = new List<T>();
            StringBuilder sb = new StringBuilder();

            sb.Append("Select * From ").Append(Entity.TableName).Append(" Where ");

            sb.Append(new WhereTranslator().Translate(expression));

            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sb.ToString();
            IDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if(dr!=null)
                {

                    var mapping = new MappingColumn(Entity);
                    mapping.ReaderSchema = this.ReadColumnInfo(dr.GetSchemaTable());
                    list = mapping.MappingWithoutInclud<T>(dr);


                }
                else
                {
                    throw new SystemException("Data Tidak Ditemukan");
                }
          
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

        public IQueryable<T> SelectAll()
        {
            List<T> list = new List<T>();
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * From ").Append(Entity.TableName);
            IDataReader dr = null;
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sb.ToString();
                dr = cmd.ExecuteReader();
                var mapping = new MappingColumn(Entity);
                mapping.ReaderSchema = this.ReadColumnInfo(dr.GetSchemaTable());
                list = mapping.MappingWithoutInclud<T>(dr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                dr.Close();
            }
            return list.AsQueryable<T>();
        }

        public IQueryable<T> Select(Expression<Func<T, dynamic>> expression)
        {
            EntityInfo entity = new EntityInfo(typeof(T));
            List<T> list = new List<T>();
            StringBuilder sb = new StringBuilder();
            var job = new CollectPropertyFromExpression().Translate(expression);
            sb.Append("Select ");
            if (job.Count < 1)
                sb.Append(" * ");
            else
            {
                int count = job.Count;
                for (int i = 0; i < count; i++)
                {
                    var att = entity.GetAttributDbColumn(job[i]);

                    sb.Append(string.Format("{0}", att));
                    if (i < count)
                    {
                        sb.Append(", ");
                    }
                }

            }
            string temp = sb.ToString();
            sb.Clear();

            temp = temp.Substring(0, temp.Length - 2);
            sb.Append(temp + " ");
            sb.Append(" From ").Append(Entity.TableName);

            //    sb.Append(new WhereTranslator().Translate(expression));
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sb.ToString();
            IDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader() as SQLiteDataReader;
                var map = new MappingColumn(Entity);
                list = map.MappingWithoutInclud<T>(dr);
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



        public IQueryable<T> Includ(IQueryable<T> query, Expression<Func<T, dynamic>> expression)
        {
            var job = new CollectPropertyFromExpression().Translate(expression);

            foreach (T Item in query)
            {
                foreach (PropertyInfo propertyJOb in job)
                {
                    EntityInfo entityChild = null;
                    if (propertyJOb.PropertyType.GenericTypeArguments.Count() > 0)
                    {
                        entityChild = new EntityInfo(propertyJOb.PropertyType.GenericTypeArguments[0]);
                        string vsql = new InsertQuery(Entity).GetChildInsertQuery(propertyJOb, Item, entityChild);
                        if (vsql != string.Empty)
                        {

                            IDataReader dr = null;
                            try
                            {
                                IDbCommand cmd = connection.CreateCommand();
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = vsql;
                                dr = cmd.ExecuteReader();

                                var propertyproduct = Entity.GetPropertyByPropertyName(propertyJOb.Name);
                                IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(entityChild.GetEntityType()));
                                var map = new MappingColumn(Entity);
                                var resultMapping = (IList)map.MappingWithoutInclud(dr, entityChild.GetEntityType());

                                foreach (var item in resultMapping)
                                {
                                    list.Add(item);
                                }

                                propertyproduct.SetValue(Item, list, null);
                            }
                            catch (Exception ex)
                            {
                                throw new System.Exception(ex.Message);
                            }
                            finally
                            {
                                dr.Close();
                            }
                        }
                    }
                    else
                    {
                        entityChild = new EntityInfo(propertyJOb.ReflectedType);
                    }



                }
            }
            return query;
        }



        public IQueryable<T> ExecuteStoreProcedureQuery(string storeProcedure)
        {
            List<T> list = new List<T>();
            IDataReader dr = null;
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CallEmployee";
                dr = cmd.ExecuteReader();
                list = new MappingColumn(Entity).MappingWithoutInclud<T>(dr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                dr.Close();
            }
            return list.AsQueryable<T>();
        }

        public object ExecuteStoreProcedureNonQuery(string storeProcedure)
        {
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storeProcedure;
                var a = cmd.ExecuteNonQuery();
                return a;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }



        public int GetLastID(T t)
        {
            int result = 0;
            try
            {
                IDbCommand cmd = connection.CreateCommand();
                cmd.Parameters.Clear();
                var iq = new InsertQuery(Entity);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = iq.GetQuerywithParameter(t) + "; select last_insert_rowid();";
                SetParameter(ref cmd, t);
                result = Convert.ToInt32(cmd.ExecuteScalar());

            }
            catch (SQLiteException ex)
            {
                throw new System.Exception(SQLiteProviderHelper.ErrorHandle(ex));
            }

            return result;
        }

        public object GetLastItem()
        {
            List<T> list = new List<T>();
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * From " + Entity.TableName + " Order By " + Entity.GetAttributPrimaryKeyName() + " Desc Limit 1";
            IDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader() as SQLiteDataReader;
                list = new MappingColumn(Entity).MappingWithoutInclud<T>(dr);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {

                dr.Close();
            }
            return list.FirstOrDefault();
        }

        public object ExecuteNonQuery(string query)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            bool result = false;
            cmd.CommandText = query;
            try
            {
                if (cmd.ExecuteNonQuery() > 0)
                    result = true;

            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);

            }
            return result;
        }

        public IDataReader ExecuteQuery(string Query)
        {
            List<T> list = new List<T>();
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Query;
            IDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {

                dr.Close();
            }
            return dr;
        }


        internal void SetParameter(ref System.Data.IDbCommand cmd, object obj)
        {
            EntityInfo ent = new EntityInfo(obj.GetType());
            foreach (PropertyInfo p in ent.DbTableProperty)
            {
                cmd.Parameters.Add(new SQLiteParameter(string.Format("@{0}", ent.GetAttributDbColumn(p)), Helpers.GetParameterValue(p, p.GetValue(obj))));
            }

        }

        private  List<ColumnInfo> ReadColumnInfo(DataTable TableSchema)
        {
            List<ColumnInfo> list = new List<ColumnInfo>();
            foreach (DataRow row in TableSchema.Rows)
            {
                ColumnInfo mr = new ColumnInfo();
                object[] info = row.ItemArray;
                mr.ColumnName = info[0].ToString();
                mr.Ordinal = (Int32)info[1];
                mr.ColumnSize = (Int32)info[2];
                mr.IsUnique = (bool)info[5];
                mr.IsKey = (bool)info[6];
                mr.BaseCatalogName = info[8].ToString();
                mr.TableName = info[11].ToString();
                mr.DataType = (Type)info[12];
                mr.AllowNUll = (bool)info[13];
                mr.ProviderType = (Int32)info[14];
                mr.IsAutoIncrement = (bool)info[17];
                list.Add(mr);
            }
            return list;
        }
    }
}
