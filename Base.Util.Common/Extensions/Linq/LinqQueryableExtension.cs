﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.Util.Common.Extensions.Linq
{
    /// <summary>
    /// LINQ动态排序扩展#
    /// </summary>
    public static class LinqQueryableExtension
    {
        /// <summary>
        /// 动态排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="propertyName">需排序字段</param>
        /// <param name="isDesc">是否 true降序，false升序，不填，默认true降序</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool isDesc = true)
        {
            return _OrderBy(query, propertyName, isDesc);
        }


        static IOrderedQueryable<T> _OrderBy<T>(IQueryable<T> query, string propertyName, bool isDesc)
        {
            string methodname = isDesc ? "OrderByDescendingInternal" : "OrderByInternal";

            var memberProp = typeof(T).GetProperty(propertyName);

            var method = typeof(LinqQueryableExtension).GetMethod(methodname)
            .MakeGenericMethod(typeof(T), memberProp.PropertyType);

            return (IOrderedQueryable<T>)method.Invoke(null, new object[] { query, memberProp });
        }

        public static IOrderedQueryable<T> OrderByInternal<T, TProp>(IQueryable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderBy(_GetLamba<T, TProp>(memberProperty));
        }
        public static IOrderedQueryable<T> OrderByDescendingInternal<T, TProp>(IQueryable<T> query, PropertyInfo memberProperty)
        {
            return query.OrderByDescending(_GetLamba<T, TProp>(memberProperty));
        }
        static Expression<Func<T, TProp>> _GetLamba<T, TProp>(PropertyInfo memberProperty)
        {
            if (memberProperty.PropertyType != typeof(TProp)) throw new Exception();

            var thisArg = Expression.Parameter(typeof(T));
            var lamba = Expression.Lambda<Func<T, TProp>>(Expression.Property(thisArg, memberProperty), thisArg);

            return lamba;
        }
    }

}
