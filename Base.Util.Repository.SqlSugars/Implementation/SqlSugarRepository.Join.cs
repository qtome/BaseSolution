using Base.Util.Repository.SqlSugarCore.Abstraction;
using System;
using System.Linq.Expressions;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务 -- 联表相关
    /// </summary>
    public partial class SqlSugarRepository<TEntity>
    {
        public ISqlSugarRepository<TEntity> InnerJoin<TEntity2>(Expression<Func<TEntity, TEntity2, bool>> joinExpression)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity, TEntity2>(joinExpression);
            Queryable.Select<TEntity>();
            return this;
        }

        public ISqlSugarRepository<TEntity> InnerJoin<TEntity2, TEntity3>(Expression<Func<TEntity, TEntity2, TEntity3, bool>> joinExpression)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity, TEntity2, TEntity3>(joinExpression);
            Queryable.Select<TEntity>();
            return this;
        }


    }
}
