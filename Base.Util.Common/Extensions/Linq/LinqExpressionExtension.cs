using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Base.Util.Common.Extensions.Linq
{
    /// <summary>
    /// LINQ表达式扩展#
    /// </summary>
    public static class LinqExpressionExtension
    {
        /// <summary>
        /// Lambda表达式拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        /// <summary>
        /// and扩展
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, bool>> And<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3, T4, bool>> first, Expression<Func<T1, T2, T3, T4, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        public static Expression<Func<T1, T2, T3, bool>> And<T1, T2, T3>(this Expression<Func<T1, T2, T3, bool>> first, Expression<Func<T1, T2, T3, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        public static Expression<Func<T1, T2, bool>> And<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// or扩展
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static Expression<Func<T1, T2, T3, T4, bool>> Or<T1, T2, T3, T4>(this Expression<Func<T1, T2, T3, T4, bool>> first, Expression<Func<T1, T2, T3, T4, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        public static Expression<Func<T1, T2, T3, bool>> Or<T1, T2, T3>(this Expression<Func<T1, T2, T3, bool>> first, Expression<Func<T1, T2, T3, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        public static Expression<Func<T1, T2, bool>> Or<T1, T2>(this Expression<Func<T1, T2, bool>> first, Expression<Func<T1, T2, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
    }
}
