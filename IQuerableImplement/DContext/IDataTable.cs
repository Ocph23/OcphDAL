using DAL.QueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DContext
{
    public interface IDataTable<T>
    {

        IDbContext datacontext{get;set;}
        EntityInfo Entity{get;set;}

        object ExecuteNonQuery(string query);

        IDataReader ExecuteQuery(string Query);

        bool Insert(T t);


        bool Delete(Expression<Func<T, bool>> Predicate);
        

        bool Update(Expression<Func<T, dynamic>> fieldUpdate, Expression<Func<T, bool>> whereClause, object source);


        IQueryable<T> Select(Expression<Func<T, bool>> expression);

        IQueryable<T> SelectAll();
        

        IQueryable<T> Select(Expression<Func<T, dynamic>> expression);


        IQueryable<T> Includ(IQueryable<T> query, Expression<Func<T, dynamic>> expression, IDbConnection dataconetxt);

        IQueryable<T> ExecuteStoreProcedureQuery(string storeProcedure);

        object ExecuteStoreProcedureNonQuery(string storeProcedure);


        int GetLastID(T t);

        object GetLastItem();
       

    }
}
