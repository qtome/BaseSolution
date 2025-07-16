using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务 -- 分组相关
    /// </summary>
    public partial class SqlSugarRepository<TEntity>
    {
        public (List<TEntity>, int) GroupByPageList(
            Expression<Func<TEntity, bool>> whereExpression
            , Expression<Func<TEntity, object>> groupByExpression
            , Expression<Func<TEntity, bool>> havingExpression
            , Expression<Func<TEntity, object>> orderByExpression
            , Expression<Func<TEntity, TEntity>> selectExpression
            , int pageIndex, int pageSize
            , bool isDesc = false)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(whereExpression);
                if (groupByExpression != null)
                {
                    query.GroupBy(groupByExpression);
                }
                if (havingExpression != null)
                {
                    query.Having(havingExpression);
                }
                if (orderByExpression != null)
                {
                    if (isDesc)
                    {
                        query.OrderByDescending(orderByExpression);
                    }
                    else
                    {
                        query.OrderBy(orderByExpression);
                    }
                }
                if (selectExpression != null)
                {
                    query.Select(selectExpression);
                }
                int total = 0;
                var pageList = query.ToPageList(pageIndex, pageSize, ref total);
                return (pageList, total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TEntity> GroupBy(
            Expression<Func<TEntity, bool>> whereExpression
            , Expression<Func<TEntity, object>> groupByExpression
            , Expression<Func<TEntity, bool>> havingExpression
            , Expression<Func<TEntity, object>> orderByExpression
            , Expression<Func<TEntity, TEntity>> selectExpression
            , bool isDesc = false)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(whereExpression);
                if (groupByExpression != null)
                {
                    query.GroupBy(groupByExpression);
                }
                if (havingExpression != null)
                {
                    query.Having(havingExpression);
                }
                if (orderByExpression != null)
                {
                    if (isDesc)
                    {
                        query.OrderByDescending(orderByExpression);
                    }
                    else
                    {
                        query.OrderBy(orderByExpression);
                    }
                }
                if (selectExpression != null)
                {
                    query.Select(selectExpression);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
