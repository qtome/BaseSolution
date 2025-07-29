using Base.Util.Repository.SqlSugarCore.Abstraction;
using Base.Util.Repository.SqlSugars.Abstraction;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class SqlSugarRepository<TEntity>
        : ISqlSugarRepository<TEntity>
        where TEntity : class, new()
    {
        internal readonly ISqlSugarWorkUnit _workUnit;
        internal ISqlSugarClient _sqlSugarClient;
        /// <summary>
        ///通过在子类的构造函数中注入，这里是基类，不用构造函数
        /// </summary>
        protected ISqlSugarClient _baseContext
        {
            get
            {
                if (typeof(TEntity).GetTypeInfo().GetCustomAttributes(typeof(SugarTable), true).FirstOrDefault((x => x.GetType() == typeof(SugarTable))) is SugarTable sugarTable && !string.IsNullOrEmpty(sugarTable.TableDescription))
                {
                    var client = _sqlSugarClient as SqlSugarClient;
                    client.ChangeDatabase(sugarTable.TableDescription.ToLower());
                }
                return _sqlSugarClient;
            }
            set
            {
                _sqlSugarClient = value;
            }
        }

        private ISugarQueryable<TEntity> Queryable { get; set; }
        private Expression<Func<TEntity, TEntity>> SelectExpression { get; set; }
        private List<(Expression<Func<TEntity, object>>, bool)> OrderByExpressions { get; set; }


        public SqlSugarRepository(ISqlSugarWorkUnit workUnit)
        {
            if (workUnit == null) return;
            _workUnit = workUnit;
            var client = workUnit.GetClient();
            if (client.GetType() == typeof(ISqlSugarClient)) _sqlSugarClient = client as ISqlSugarClient;
        }

        public SqlSugarRepository(SqlSugarRepository<TEntity> baseRepository)
        {
            _workUnit = baseRepository._workUnit;
            _sqlSugarClient = baseRepository._sqlSugarClient;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected virtual void Dispose()
        {
            if (_baseContext != null)
            {
                _baseContext.Dispose();
            }
        }

        public ISqlSugarRepository<TEntity> Select(Expression<Func<TEntity, TEntity>> selectExpression)
        {
            if (selectExpression != null)
            {
                this.SelectExpression = selectExpression;
            }
            return this;
        }

        public ISqlSugarRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isDesc = false)
        {
            if (orderByExpression != null)
            {
                if (this.OrderByExpressions == null) this.OrderByExpressions = new List<(Expression<Func<TEntity, object>>, bool)>();
                this.OrderByExpressions.Add((orderByExpression, isDesc));
            }
            return this;
        }

        /// <summary>
        /// 获取查询数据源
        /// </summary>
        private ISugarQueryable<TEntity> GetQueryable()
        {
            var query = GetInternalQueryable();
            if (this.OrderByExpressions != null)
            {
                foreach (var item in this.OrderByExpressions)
                {
                    query = query.OrderBy(item.Item1, item.Item2 == false ? OrderByType.Asc : OrderByType.Desc);
                }
                this.OrderByExpressions = null;
            }
            return query;
        }
        private ISugarQueryable<TEntity> GetInternalQueryable()
        {
            if (this.Queryable == null)
            {
                var result = _baseContext.Queryable<TEntity>();
                if (this.SelectExpression != null)
                {
                    result = result.Select(this.SelectExpression);
                    this.SelectExpression = null;
                }
                return result;
            }
            var query = this.Queryable;
            this.Queryable = null;
            if (this.SelectExpression != null)
            {
                query = query.Select(this.SelectExpression);
                this.SelectExpression = null;
            }
            return query;
        }

        private void SetEntityIdentity(params TEntity[] models)
        {
            try
            {
                var props = typeof(TEntity).GetProperties();
                var prop = props.FirstOrDefault(ff =>
                            ff.CustomAttributes.Any(aa => aa.AttributeType.FullName.Equals("SqlSugar.SugarColumn")
                                                       && aa.NamedArguments.Any(cc => cc.MemberName.Equals("IsIdentity")
                                                       && Convert.ToBoolean(cc.TypedValue.Value.ToString()) == true)));
                if (prop == null) return;
                if (prop.PropertyType != typeof(int)) return;
                int setValue = 0;
                foreach (var entity in models)
                {
                    if (setValue == 0) setValue = Convert.ToInt32(prop.GetValue(entity));
                    prop.SetValue(entity, setValue);
                    setValue++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private System.Reflection.PropertyInfo GetLogicDeleteBoolProperty()
        {
            try
            {
                var props = typeof(TEntity).GetProperties();
                var prop = props.FirstOrDefault(ff => ff.Name.Equals("IsDeleted") || ff.Name.Equals("IsDelete"));
                if (prop != null && prop.PropertyType != typeof(bool)) return null;
                return prop;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LogicDeleteUpdate(params TEntity[] models)
        {
            try
            {
                var prop = GetLogicDeleteBoolProperty();
                if (prop == null) return;
                foreach (var entity in models)
                {
                    prop.SetValue(entity, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual bool Insert(params TEntity[] model)
        {
            try
            {
                var result = new List<bool>();
                foreach (var item in model)
                {
                    var query = _baseContext.Insertable<TEntity>(item);
                    query = query.IgnoreColumns(ignoreNullColumn: true);
                    bool itemResult = query.ExecuteCommandIdentityIntoEntity();
                    result.Add(itemResult);
                }
                return result.All(aa => aa == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> InsertAsync(params TEntity[] model)
        {
            try
            {
                var result = new List<bool>();
                foreach (var item in model)
                {
                    var query = _baseContext.Insertable<TEntity>(item);
                    query = query.IgnoreColumns(ignoreNullColumn: true);
                    bool itemResult = await query.ExecuteCommandIdentityIntoEntityAsync();
                    result.Add(itemResult);
                }
                return result.All(aa => aa == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(IEnumerable<TEntity> model)
        {
            try
            {
                return Insert(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> InsertAsync(IEnumerable<TEntity> model)
        {
            try
            {
                return await InsertAsync(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual bool Update(params TEntity[] model)
        {
            try
            {
                var query = _baseContext.Updateable<TEntity>(model);
                var result = query.ExecuteCommand();
                return result >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> UpdateAsync(params TEntity[] model)
        {
            try
            {
                var query = _baseContext.Updateable<TEntity>(model);
                var result = await query.ExecuteCommandAsync();
                return result >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(IEnumerable<TEntity> model)
        {
            try
            {
                return Update(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateAsync(IEnumerable<TEntity> model)
        {
            try
            {
                return await UpdateAsync(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool LogicDelete(params TEntity[] model)
        {
            try
            {
                LogicDeleteUpdate(model);
                return Update(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> LogicDeleteAsync(params TEntity[] model)
        {
            try
            {
                LogicDeleteUpdate(model);
                return await UpdateAsync(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool LogicDelete(IEnumerable<TEntity> model)
        {
            try
            {
                return LogicDelete(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> LogicDeleteAsync(IEnumerable<TEntity> model)
        {
            try
            {
                return await LogicDeleteAsync(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool Delete(params TEntity[] model)
        {
            try
            {
                var result = _baseContext.Deleteable<TEntity>(model.ToList()).ExecuteCommand();
                return result >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual async Task<bool> DeleteAsync(params TEntity[] model)
        {
            try
            {
                var result = await _baseContext.Deleteable<TEntity>(model.ToList()).ExecuteCommandAsync();
                return result >= 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<TEntity> model)
        {
            try
            {
                return Delete(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteAsync(IEnumerable<TEntity> model)
        {
            try
            {
                return await DeleteAsync(model.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public TEntity Find(object key)
        {
            try
            {
                return Find(key, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity Find(object key, bool logicDeleteJudge)
        {
            try
            {
                var entity = GetQueryable().InSingle(key);
                if (entity != null && logicDeleteJudge == true)
                {
                    var prop = GetLogicDeleteBoolProperty();
                    if (prop != null)
                    {
                        bool logicDelete = Convert.ToBoolean(prop.GetValue(entity));
                        if (logicDelete == true) return null;
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return FirstOrDefault(expression, null, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField)
        {
            try
            {
                return FirstOrDefault(expression, orderByField, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc)
        {
            try
            {
                return FirstOrDefault(expression, orderByField, isDesc, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields);
                return query.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = FirstOrDefault(expression, null, false, null);
                return entity != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TEntity> FindAsync(object key)
        {
            try
            {
                return await FindAsync(key, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> FindAsync(object key, bool logicDeleteJudge)
        {
            try
            {
                var entity = await GetQueryable().InSingleAsync(key);
                if (entity != null && logicDeleteJudge == true)
                {
                    var prop = GetLogicDeleteBoolProperty();
                    if (prop != null)
                    {
                        bool logicDelete = Convert.ToBoolean(prop.GetValue(entity));
                        if (logicDelete == true) return null;
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return await FirstOrDefaultAsync(expression, null, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField)
        {
            try
            {
                return await FirstOrDefaultAsync(expression, orderByField, false, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc)
        {
            try
            {
                return await FirstOrDefaultAsync(expression, orderByField, isDesc, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields);
                return await query.FirstAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<TEntity> Query()
        {
            try
            {
                return Query(a => true, null, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(int pageIndex, int pageSize)
        {
            try
            {
                return Query(a => true, null, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return Query(expression, null, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize)
        {
            try
            {
                return Query(expression, null, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField)
        {
            try
            {
                return Query(expression, orderByField, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize)
        {
            try
            {
                return Query(expression, orderByField, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields)
        {
            try
            {
                return Query(expression, orderByField, false, thenByFields, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize)
        {
            try
            {
                return Query(expression, orderByField, false, thenByFields, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc)
        {
            try
            {
                return Query(expression, orderByField, isDesc, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize)
        {
            try
            {
                return Query(expression, orderByField, isDesc, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields)
        {
            try
            {
                return Query(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields);
                return query.ToPageList(pageIndex, pageSize).AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public (IQueryable<TEntity>, int) QueryPageList()
        {
            try
            {
                return QueryPageList(a => true, null, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(int pageIndex, int pageSize)
        {
            try
            {
                return QueryPageList(a => true, null, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                return QueryPageList(expression, null, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize)
        {
            try
            {
                return QueryPageList(expression, null, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField)
        {
            try
            {
                return QueryPageList(expression, orderByField, false, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize)
        {
            try
            {
                return QueryPageList(expression, orderByField, false, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields)
        {
            try
            {
                return QueryPageList(expression, orderByField, false, thenByFields, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize)
        {
            try
            {
                return QueryPageList(expression, orderByField, false, thenByFields, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc)
        {
            try
            {
                return QueryPageList(expression, orderByField, isDesc, null, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize)
        {
            try
            {
                return QueryPageList(expression, orderByField, isDesc, null, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields)
        {
            try
            {
                return QueryPageList(expression, orderByField, isDesc, thenByFields, 1, int.MaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize)
        {
            try
            {
                ISugarQueryable<TEntity> query = GetQueryableEntities(expression, orderByField, isDesc, thenByFields);
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
        /// 获取查询集合
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderByField"></param>
        /// <param name="isDesc"></param>
        /// <param name="thenByFields"></param>
        /// <returns></returns>
        protected ISugarQueryable<TEntity> GetQueryableEntities(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields)
        {
            ISugarQueryable<TEntity> query = GetQueryEntities(expression);
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
        /// <returns></returns>
        private ISugarQueryable<TEntity> GetQueryEntities(Expression<Func<TEntity, bool>> expression)
        {
            ISugarQueryable<TEntity> query = GetQueryable().Where(expression);
            return query;
        }

        /// <summary>
        /// 获取排序字段
        /// </summary>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        private string GetOrderFiledString(string orderByField, bool isDesc)
        {
            try
            {
                string field = orderByField;
                if (field.Contains("."))
                {   // 说明具有联表修饰前缀
                    var strs = field.Split(".");
                    field = strs[strs.Length - 1];
                }
                //通过类中属性名获取数据库字段名
                var orderByFieldName = _baseContext.EntityMaintenance.GetDbColumnName<TEntity>(field);//防注入
                var orderBy = "asc";
                if (isDesc) orderBy = "desc";
                return (orderByFieldName == field ? orderByField : field) + @$" {orderBy} ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
