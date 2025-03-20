using Base.Util.Repository.SqlSugarCore.Models;
using System.Collections.Generic;
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
        /// 执行存储过程
        /// </summary>
        /// <param name="procedure">存储过程信息</param>
        /// <returns></returns>
        List<T> StoreProcedure<T>(StoredProcedureDto procedure) where T : new();
        /// <summary>
        /// 执行存储过程（异步）
        /// </summary>
        /// <param name="procedure">存储过程信息</param>
        /// <returns></returns>
        Task<List<T>> StoreProcedureAsync<T>(StoredProcedureDto procedure) where T : new();
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        bool CommitTransaction();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTransaction();

    }
}
