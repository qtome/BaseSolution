using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Base.Util.Repository.SqlSugarCore.Abstraction
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// 分组 分页方法
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="groupByExpression"></param>
        /// <param name="havingExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        (List<TEntity>, int) GroupByPageList(
            Expression<Func<TEntity, bool>> whereExpression
            , Expression<Func<TEntity, object>> groupByExpression
            , Expression<Func<TEntity, bool>> havingExpression
            , Expression<Func<TEntity, object>> orderByExpression
            , Expression<Func<TEntity, TEntity>> selectExpression
            , int pageIndex, int pageSize
            , bool isDesc = false);

        /// <summary>
        /// 分组 分页方法
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="groupByExpression"></param>
        /// <param name="havingExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="selectExpression"></param>
        /// <returns></returns>
        List<TEntity> GroupBy(
            Expression<Func<TEntity, bool>> whereExpression
            , Expression<Func<TEntity, object>> groupByExpression
            , Expression<Func<TEntity, bool>> havingExpression
            , Expression<Func<TEntity, object>> orderByExpression
            , Expression<Func<TEntity, TEntity>> selectExpression
            , bool isDesc = false);
    }
}
