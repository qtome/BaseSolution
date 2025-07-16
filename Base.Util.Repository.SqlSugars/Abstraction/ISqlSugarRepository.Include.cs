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
    {
        /// <summary>
        /// 下一次查询需要关联查询的属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="include"></param>
        ISqlSugarRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 下一次查询需要关联查询的属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="include"></param>
        ISqlSugarRepository<TEntity> Include<TProperty>(Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 下一次查询需要关联查询的属性（二级）
        /// </summary>
        /// <typeparam name="TProperty1"></typeparam>
        /// <typeparam name="TProperty2"></typeparam>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, TProperty1>> include1, Expression<Func<TProperty1, TProperty2>> include2);
        /// <summary>
        /// 下一次查询需要关联查询的属性（二级）
        /// </summary>
        /// <typeparam name="TProperty1"></typeparam>
        /// <typeparam name="TProperty2"></typeparam>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, TProperty1>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2);
        /// <summary>
        /// 下一次查询需要关联查询的属性（二级）
        /// </summary>
        /// <typeparam name="TProperty1"></typeparam>
        /// <typeparam name="TProperty2"></typeparam>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, TProperty2>> include2);
        /// <summary>
        /// 下一次查询需要关联查询的属性（二级）
        /// </summary>
        /// <typeparam name="TProperty1"></typeparam>
        /// <typeparam name="TProperty2"></typeparam>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2);
        /// <summary>
        /// 下一次查询需要关联查询的属性（三级）
        /// </summary>
        /// <typeparam name="TProperty1"></typeparam>
        /// <typeparam name="TProperty2"></typeparam>
        /// <typeparam name="TProperty3"></typeparam>
        /// <param name="include1"></param>
        /// <param name="include2"></param>
        /// <param name="include3"></param>
        /// <returns></returns>
        ISqlSugarRepository<TEntity> Include<TProperty1, TProperty2, TProperty3>(Expression<Func<TEntity, List<TProperty1>>> include1
            , Expression<Func<TProperty1, List<TProperty2>>> include2
            , Expression<Func<TProperty2, List<TProperty3>>> include3);

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);


        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);



        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);


        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, TProperty>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, TProperty>> include);

        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        TEntity FirstOrDefaultInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);


        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 查找数据（异步）
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultIncludeAsync<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);



        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> QueryInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);


        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, Expression<Func<TEntity, List<TProperty>>> include);
        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="orderByField">排序字段</param>
        /// <param name="isDesc">是否是倒叙</param>
        /// <param name="thenByFields">再排序字段,倒叙true 正序false</param>
        /// <param name="pageIndex">分页数</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="include">包含关联表表达式</param>
        /// <returns></returns>
        (IQueryable<TEntity>, int) QueryPageListInclude<TProperty>(Expression<Func<TEntity, bool>> expression, string orderByField, bool isDesc, Dictionary<string, bool> thenByFields, int pageIndex, int pageSize, Expression<Func<TEntity, List<TProperty>>> include);
    }
}
