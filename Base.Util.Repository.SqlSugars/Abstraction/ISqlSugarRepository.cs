using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Abstraction
{
    /// <summary>
    /// 基类服务接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface ISqlSugarRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="selectExpression">映射表达式</param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Select(Expression<Func<TEntity, TEntity>> selectExpression);
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isDesc">是否是倒序</param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isDesc = false);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        bool Insert(params TEntity[] model);
        /// <summary>
        /// 新增数据（异步）
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        Task<bool> InsertAsync(params TEntity[] model);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        bool Insert(IEnumerable<TEntity> model);
        /// <summary>
        /// 新增数据（异步）
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        Task<bool> InsertAsync(IEnumerable<TEntity> model);


        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        bool Update(params TEntity[] model);
        /// <summary>
        /// 修改数据（异步）
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(params TEntity[] model);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        bool Update(IEnumerable<TEntity> model);
        /// <summary>
        /// 修改数据（异步）
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<TEntity> model);

        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        bool LogicDelete(params TEntity[] model);
        /// <summary>
        /// 逻辑删除数据（异步）
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        Task<bool> LogicDeleteAsync(params TEntity[] model);
        /// <summary>
        /// 逻辑删除数据
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        bool LogicDelete(IEnumerable<TEntity> model);
        /// <summary>
        /// 逻辑删除数据（异步）
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        Task<bool> LogicDeleteAsync(IEnumerable<TEntity> model);

        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        bool Delete(params TEntity[] model);
        /// <summary>
        /// 物理删除数据（异步）
        /// </summary>
        /// <param name="model">实体数组</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(params TEntity[] model);
        /// <summary>
        /// 物理删除数据
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        bool Delete(IEnumerable<TEntity> model);
        /// <summary>
        /// 物理删除数据（异步）
        /// </summary>
        /// <param name="model">实体集合</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<TEntity> model);

        /// <summary>
        /// 检查是否存在实体
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>true 存在  false 不存在</returns>
        bool Any(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>逻辑删除数据 返回 null</returns>
        TEntity Find(object key);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="logicDeleteJudge">是否需要逻辑删除判断</param>
        /// <returns></returns>
        TEntity Find(object key, bool logicDeleteJudge);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>逻辑删除数据 返回 null</returns>
        Task<TEntity> FindAsync(object key);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="logicDeleteJudge">是否需要逻辑删除判断</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object key, bool logicDeleteJudge);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields);


        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize);

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList();
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageList(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize);


    }
}
