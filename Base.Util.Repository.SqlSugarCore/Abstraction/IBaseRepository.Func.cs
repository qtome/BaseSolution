using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Abstraction
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        int CountInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<int> CountIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        int CountInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// Count 统计行
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<int> CountIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);


        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maxField">MAX 计算字段</param>
        /// <returns></returns>
        TResult Max<TResult>(string maxField);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maxField">MAX 计算字段</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(string maxField);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maxField">MAX 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Max<TResult>(string maxField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maxField">MAX 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(string maxField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">MAX 计算字段表达式</param>
        /// <returns></returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">MAX 计算字段表达式</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">MAX 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Max 取最大值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">MAX 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="minField">Min 计算字段</param>
        /// <returns></returns>
        TResult Min<TResult>(string minField);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="minField">Min 计算字段</param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(string minField);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="minField">Min 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Min<TResult>(string minField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="minField">Min 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(string minField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Min 计算字段表达式</param>
        /// <returns></returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Min 计算字段表达式</param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Min 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Min 取最小值
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Min 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sumField">Sum 计算字段</param>
        /// <returns></returns>
        TResult Sum<TResult>(string sumField);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sumField">Sum 计算字段</param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(string sumField);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sumField">Sum 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Sum<TResult>(string sumField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sumField">Sum 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(string sumField, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Sum 计算字段表达式</param>
        /// <returns></returns>
        TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Sum 计算字段表达式</param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Sum 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// Sum 取求和
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Sum 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);

        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="avgField">Avg 计算字段</param>
        /// <returns></returns>
        TResult Avg<TResult>(string avgField);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="avgField">Avg 计算字段</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(string avgField);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="avgField">Avg 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Avg<TResult>(string avgField, Expression<Func<TEntity, bool>> whereExpression);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="avgField">Avg 计算字段</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(string avgField, Expression<Func<TEntity, bool>> whereExpression);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Avg 计算字段表达式</param>
        /// <returns></returns>
        TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Avg 计算字段表达式</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Avg 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);
        /// <Avgmary>
        /// Avg 取平均
        /// </Avgmary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression">Avg 计算字段表达式</param>
        /// <param name="whereExpression">查询表达式</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression);
    }
}
