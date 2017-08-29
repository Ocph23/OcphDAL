using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Extentions;
using System.Data;




namespace DAL.Repository
{
    public interface IRepository<T>  where T:class 
    {
        bool Delete(Expression<Func<T, bool>> Predicate);

        bool Update(Expression<Func<T, dynamic>> FieldsNameToUpdate, T SourceValue, Expression<Func<T, bool>> where);

        bool Insert(T t);

        int InsertAndGetLastID(T t);
        IQueryable<T> Inclued(Expression<Func<T, dynamic>> expression);
        IQueryable<T> Select();

        IQueryable<T> Select(Expression<Func<T, dynamic>> expression);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        IQueryable<T> ExecuteStoreProcedureQuery(string storeProcedure);

        object ExecuteStoreProcedureNonQuery(string storeProcedure);

        T GetLastItem();
    }
}
