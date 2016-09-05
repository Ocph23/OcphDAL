using DAL.DContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extentions
{
    public static class Extentionss
    {

        public static T Include<T>(this IQueryable<T> query, Expression<Func<T, dynamic>> expression, MySqlContextConnection dataconetxt )where T:class
        {
            throw new NotImplementedException();
        }


        public static object LastID(this object[] query)
        {
            object ret=query[1];
            Type t = ret.GetType();

            if (t.Name == "Int32")
                ret= (Int32)ret;
            if (t.Name == "String")
                ret= ret.ToString();
            return ret;
        }

        public static bool IsInsert(this object[] query)
        {
            if ((Int32)query[0] > 0)
                return true;
            else
                return false;
        }

/*
        public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer,
       IQueryable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter,
       TInner, TResult> resultSelector)
        {

            throw new NotImplementedException();
        }

        public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer,
         IQueryable<TInner> inner, Func<TOuter,
         TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector,
         IEqualityComparer<TKey> comparer)
        {
            throw new NotImplementedException();
        }
        */
    }
}
