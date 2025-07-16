using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务 -- 函数相关
    /// </summary>
    public partial class SqlSugarRepository<TEntity>
    {
        public int Count()
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAsync()
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Count(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Count(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.CountAsync(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
                return query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
                return query.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TResult Max<TResult>(string maxField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Max<TResult>(maxField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TResult> MaxAsync<TResult>(string maxField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.MaxAsync<TResult>(maxField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Max<TResult>(string maxField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Max<TResult>(maxField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MaxAsync<TResult>(string maxField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.MaxAsync<TResult>(maxField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Max<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.MaxAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Max<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.MaxAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Min<TResult>(string minField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Min<TResult>(minField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TResult> MinAsync<TResult>(string minField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.MinAsync<TResult>(minField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Min<TResult>(string minField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Min<TResult>(minField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MinAsync<TResult>(string minField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.MinAsync<TResult>(minField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Min<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.MinAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Min<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Min<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.MinAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Sum<TResult>(string sumField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Sum<TResult>(sumField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TResult> SumAsync<TResult>(string sumField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.SumAsync<TResult>(sumField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Sum<TResult>(string sumField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Sum<TResult>(sumField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> SumAsync<TResult>(string sumField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.SumAsync<TResult>(sumField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Sum<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.SumAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Sum<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Sum<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> SumAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.SumAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Avg<TResult>(string avgField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Avg<TResult>(avgField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TResult> AvgAsync<TResult>(string avgField)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.AvgAsync<TResult>(avgField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Avg<TResult>(string avgField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Avg<TResult>(avgField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> AvgAsync<TResult>(string avgField, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.AvgAsync<TResult>(avgField);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return query.Avg<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable();
                return await query.AvgAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TResult Avg<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return query.Avg<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TResult> AvgAsync<TResult>(Expression<Func<TEntity, TResult>> expression, Expression<Func<TEntity, bool>> whereExpression)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryable().Where(whereExpression);
                return await query.AvgAsync<TResult>(expression);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
