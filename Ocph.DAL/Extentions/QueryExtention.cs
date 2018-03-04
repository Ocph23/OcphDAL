using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ocph.DAL
{
    public static class QueryExtention
    {

        public static IQueryable<T> Includes<T, TProperty>
            (this IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class
        {
            var prov = source.Provider;
            var type = path.GetType();
            return source;
        }



        public static object LastID(this object[] query)
        {
            
            object ret = query[1];
            Type t = ret.GetType();

            if (t.Name == "Int32")
                ret = (Int32)ret;
            if (t.Name == "String")
                ret = ret.ToString();
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
