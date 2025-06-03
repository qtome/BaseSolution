using Base.Util.Repository.SqlSugarCore.Abstraction;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务 -- 联表相关
    /// </summary>
    public partial class SqlSugarRepository<TEntity>
    {
        public ISqlSugarRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, List<TProperty>>> include)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, TProperty1>> include1, Expression<Func<TProperty1, TProperty2>> include2)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include1, include2);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, TProperty1>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include1, include2);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, TProperty2>> include2)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include1, include2);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include1, include2);
            return this;
        }

        public ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2, TProperty3>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2
            , Expression<Func<TProperty2, List<TProperty3>>> include3)
        {
            if (this.Queryable == null) Queryable = _baseContext.Queryable<TEntity>();
            Queryable.Includes(include1, include2, include3);
            return this;
        }


        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return FirstOrDefaultInclude(a => true, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, orderByField, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, orderByField, isDesc, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return query.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(a => true, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, orderByField, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, orderByField, isDesc, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return await query.FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(a => true, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(a => true, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, thenByFields, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return query.ToPageList(pageIndex, pageSize).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(a => true, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(a => true, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                int total = 0;
                var pageList = query.ToPageList(pageIndex, pageSize, ref total);
                return (pageList.AsQueryable(), total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取查询集合（涉包含）
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="orderByField"></param>
        /// <param name="isDesc"></param>
        /// <param name="thenByFields"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        private ISugarQueryable<TEntity> GetQueryableEntities<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include)
        {
            ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
            if (!string.IsNullOrWhiteSpace(orderByField))
            {
                var orderByString = GetOrderFiledString(orderByField, isDesc);
                if (thenByFields != null)
                {
                    foreach (var item in thenByFields)
                    {
                        orderByString += $", {GetOrderFiledString(item.Key, item.Value)}";
                    }
                }
                query = query.OrderBy(orderByString);
            }
            return query;
        }


        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return FirstOrDefaultInclude(a => true, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, orderByField, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return FirstOrDefaultInclude(expression, orderByField, isDesc, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return query.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(a => true, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, null, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, orderByField, false, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return await FirstOrDefaultIncludeAsync(expression, orderByField, isDesc, null, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return await query.FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(a => true, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(a => true, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, false, thenByFields, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryInclude(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                return query.ToPageList(pageIndex, pageSize).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(a => true, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(a => true, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, null, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, null, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, false, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, null, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, null, pageIndex, pageSize, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                return QueryPageListInclude(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue, include);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields, include);
                int total = 0;
                var pageList = query.ToPageList(pageIndex, pageSize, ref total);
                return (pageList.AsQueryable(), total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取查询集合（涉包含）
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="orderByField"></param>
        /// <param name="isDesc"></param>
        /// <param name="thenByFields"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        private ISugarQueryable<TEntity> GetQueryableEntities<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include)
        {
            ISugarQueryable<TEntity> query = GetQueryEntities(expression, include);
            if (!string.IsNullOrWhiteSpace(orderByField))
            {
                var orderByString = GetOrderFiledString(orderByField, isDesc);
                if (thenByFields != null)
                {
                    foreach (var item in thenByFields)
                    {
                        orderByString += $", {GetOrderFiledString(item.Key, item.Value)}";
                    }
                }
                query = query.OrderBy(orderByString);
            }
            return query;
        }

        /// <summary>
        /// 获取数据源集合（过滤）
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        private ISugarQueryable<TEntity> GetQueryEntities<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include)
        {
            ISugarQueryable<TEntity> query = GetQueryable().Includes(include).Where(expression);
            return query;
        }

        /// <summary>
        /// 获取数据源集合（过滤）
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        private ISugarQueryable<TEntity> GetQueryEntities<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include)
        {
            ISugarQueryable<TEntity> query = GetQueryable().Includes(include).Where(expression);
            return query;
        }

    }
}
