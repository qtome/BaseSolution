using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Util.Repository.SqlSugars.Abstraction
{
    /// <summary>
    /// SqlSugar工作单元
    /// </summary>
    public interface ISqlSugarWorkUnit
    {
        /// <summary>
        /// 获取ISqlSugarClient
        /// </summary>
        /// <returns></returns>
        object GetClient();

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
