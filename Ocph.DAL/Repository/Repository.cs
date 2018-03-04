using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data;
using Ocph.DAL.DbContext;

namespace Ocph.DAL.Repository
{
   
    public class Repository<T> :  IRepository<T> where T:class
    {
        public IDataTable<T> DataTables { get; set; }

        public Repository(IDbConnection dataContex) 
        {
            DataTables = Helpers.GetDatatable<T>(dataContex);

        }

        public bool Delete(Expression<Func<T, bool>> Predicate)
        {
            return  DataTables.Delete(Predicate);
        }


        //Update
        public bool Update(Expression<Func<T, dynamic>> fieldUpdate, T source, Expression<Func<T, bool>> whereClause)
        {
            return DataTables.Update(fieldUpdate, whereClause, source);
        }

        public bool Insert(T t)
        {
            return  DataTables.Insert(t);
        }



        public IQueryable<T> Select()
        {
            return DataTables.SelectAll();
        }

        public IQueryable<T> Select(Expression<Func<T, dynamic>> expression)
        {
           return DataTables.Select(expression);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return DataTables.Select(expression);
        }



        public IQueryable<T> ExecuteStoreProcedureQuery(string storeProcedure)
        {
            return DataTables.ExecuteStoreProcedureQuery(storeProcedure);
        }


        public object ExecuteStoreProcedureNonQuery(string storeProcedure)
        {
            return DataTables.ExecuteStoreProcedureNonQuery(storeProcedure);
        }

       

        public int InsertAndGetLastID(T t)
        {
            return DataTables.GetLastID(t);
        }


        public T GetLastItem()
        {
            return (T)DataTables.GetLastItem();
        }
    }
}
