using System.Collections.Generic;
using System.Data;

namespace Base.Util.Repository.SqlSugarCore.Models
{
    /// <summary>
    /// 存储过程类
    /// </summary>
    public class SqlSugarStoredProcedureDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private SqlSugarStoredProcedureDto()
        {
            Parameters = new List<SqlSugarSqlSugarStoredProcedureParameter>();
        }

        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string ProcedureName { get; set; }
        /// <summary>
        /// 参数集合
        /// </summary>
        public List<SqlSugarSqlSugarStoredProcedureParameter> Parameters { get; set; }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string name, object value)
        {
            Parameters.Add(SqlSugarSqlSugarStoredProcedureParameter.CreateInput(name, value));
        }

        /// <summary>
        /// 添加输出参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public void AddOutputParameter(string name, object value)
        {
            Parameters.Add(SqlSugarSqlSugarStoredProcedureParameter.CreateOutput(name, value));
        }

        /// <summary>
        /// 添加输入输出参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public void AddIntputOutputParameter(string name, object value)
        {
            Parameters.Add(SqlSugarSqlSugarStoredProcedureParameter.CreateInputOutput(name, value));
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="name">存储过程名称</param>
        /// <returns></returns>
        public static SqlSugarStoredProcedureDto Create(string name)
        {
            return new SqlSugarStoredProcedureDto()
            {
                ProcedureName = name,
            };
        }
    }

    /// <summary>
    /// 存储过程参数
    /// </summary>
    public class SqlSugarSqlSugarStoredProcedureParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private SqlSugarSqlSugarStoredProcedureParameter()
        {
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// 是否是输出参数
        /// </summary>
        public bool IsOutput => Direction == ParameterDirection.Output || Direction == ParameterDirection.InputOutput;

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static SqlSugarSqlSugarStoredProcedureParameter CreateInput(string name, object value)
        {
            return new SqlSugarSqlSugarStoredProcedureParameter()
            {
                ParameterName = name,
                Value = value,
                Direction = ParameterDirection.Input,
            };
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static SqlSugarSqlSugarStoredProcedureParameter CreateOutput(string name, object value)
        {
            return new SqlSugarSqlSugarStoredProcedureParameter()
            {
                ParameterName = name,
                Value = value,
                Direction = ParameterDirection.Output,
            };
        }

        /// <summary>
        /// 创建输出参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        public static SqlSugarSqlSugarStoredProcedureParameter CreateInputOutput(string name, object value)
        {
            return new SqlSugarSqlSugarStoredProcedureParameter()
            {
                ParameterName = name,
                Value = value,
                Direction = ParameterDirection.InputOutput,
            };
        }

    }
}
