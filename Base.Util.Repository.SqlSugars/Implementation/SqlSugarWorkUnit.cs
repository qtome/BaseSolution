using Base.Util.Repository.SqlSugars.Abstraction;
using SqlSugar;
using System;

namespace Base.Util.Repository.SqlSugars.Implementation
{
    /// <summary>
    /// SqlSugar工作单元
    /// </summary>
    public class SqlSugarWorkUnit : ISqlSugarWorkUnit
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public SqlSugarWorkUnit(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        public object GetClient()
        {
            return _sqlSugarClient;
        }


        public void BeginTransaction()
        {
            if (!HasTransaction())
            {
                _sqlSugarClient.Ado.BeginTran();
            }
        }

        public bool CommitTransaction()
        {
            try
            {
                if (HasTransaction())
                {
                    _sqlSugarClient.Ado.CommitTran();
                }
                return true;
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        public void RollbackTransaction()
        {
            if (HasTransaction())
            {
                _sqlSugarClient.Ado.RollbackTran();
            }
        }

        /// <summary>
        /// 确认当前状态是否开启事务
        /// </summary>
        /// <returns></returns>
        private bool HasTransaction()
        {
            return _sqlSugarClient.Ado.Transaction != null;
        }
    }
}
