using System;
using System.Linq.Expressions;

namespace Base.Util.Repository.SqlSugarCore.Abstraction
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface ISqlSugarRepository<TEntity>
    {
        /// <summary>
        /// 下一次 inner 联表查询
        /// </summary>
        /// <typeparam name="TEntity2"></typeparam>
        /// <param name="joinExpression"></param>
        ISqlSugarRepository<TEntity> InnerJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> joinExpression);

        /// <summary>
        /// 下一次 inner 联表查询
        /// </summary>
        /// <typeparam name="TEntity2"></typeparam>
        /// <typeparam name="TEntity3"></typeparam>
        /// <param name="joinExpression"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> InnerJoin<TEntity2, TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> joinExpression);
    }
}
