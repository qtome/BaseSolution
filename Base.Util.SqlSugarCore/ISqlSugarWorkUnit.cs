using SqlSugar;

namespace Base.Util.SqlSugarCore
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface ISqlSugarWorkUnit
    {
        /// <summary>
        /// 获取SqlSugar中心
        /// </summary>
        /// <returns></returns>
        ISqlSugarClient GetSqlSugarClient();

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
