using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.Util.Common.Extensions.Linq
{
    /// <summary>
    /// LINQ动态排序扩展#
    /// </summary>
    public static class LinqEnumerablExtension
    {
        /// <summary>
        /// 动态排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName">需排序字段</param>
        /// <param name="isDesc">是否 true降序，false升序，不填，默认true降序</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> query, string propertyName, bool isDesc = true)
        {
            return _OrderBy(query, propertyName, isDesc);
        }

        static IOrderedEnumerable<T> _OrderBy<T>(IEnumerable<T> query, string propertyName, bool isDesc)
        {
            string methodname = isDesc ? "OrderByDescendingInternal" : "OrderByInternal";

            var memberProp = typeof(T).GetProperty(propertyName);

            var method = typeof(LinqEnumerablExtension).GetMethod(methodname)
            .MakeGenericMethod(typeof(T), memberProp.PropertyType);

            return (IOrderedEnumerable<T>)method.Invoke(null, new object[] { query, memberProp });
        }

        public static IOrderedEnumerable<T> OrderByInternal<T, TProp>(IEnumerable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderBy(_GetLamba<T, TProp>(memberProperty).Compile());
        }
        public static IOrderedEnumerable<T> OrderByDescendingInternal<T, TProp>(IEnumerable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderByDescending(_GetLamba<T, TProp>(memberProperty).Compile());
        }
        static Expression<Func<T, TProp>> _GetLamba<T, TProp>(PropertyInfo memberProperty)
        {
            if (memberProperty.PropertyType != typeof(TProp)) throw new Exception();

            var thisArg = Expression.Parameter(typeof(T));
            var lamba = Expression.Lambda<Func<T, TProp>>(Expression.Property(thisArg, memberProperty), thisArg);

            return lamba;
        }


        /// <summary>
        /// 动态排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName">需排序字段</param>
        /// <param name="isDesc">是否 true降序，false升序，不填，默认true降序</param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenByDynamic<T>(this IOrderedEnumerable<T> query, string propertyName, bool isDesc = true)
        {
            return _ThenBy(query, propertyName, isDesc);
        }


        static IOrderedEnumerable<T> _ThenBy<T>(IOrderedEnumerable<T> query, string propertyName, bool isDesc)
        {
            string methodname = isDesc ? "ThenByDescendingInternal" : "ThenByInternal";

            var memberProp = typeof(T).GetProperty(propertyName);

            var method = typeof(LinqEnumerablExtension).GetMethod(methodname)
            .MakeGenericMethod(typeof(T), memberProp.PropertyType);

            return (IOrderedEnumerable<T>)method.Invoke(null, new object[] { query, memberProp });
        }

        public static IOrderedEnumerable<T> ThenByInternal<T, TProp>(IOrderedEnumerable<T> query, PropertyInfo memberProperty)
        {
            return query.ThenBy(_GetLamba<T, TProp>(memberProperty).Compile());
        }
        public static IOrderedEnumerable<T> ThenByDescendingInternal<T, TProp>(IOrderedEnumerable<T> query, PropertyInfo memberProperty)
        {
            return query.ThenByDescending(_GetLamba<T, TProp>(memberProperty).Compile());
        }
    }
}
