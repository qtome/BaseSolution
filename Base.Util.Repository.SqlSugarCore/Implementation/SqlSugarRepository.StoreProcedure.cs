using Base.Util.Repository.SqlSugarCore.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.Util.Repository.SqlSugarCore.Implementation
{
    /// <summary>
    /// 基类服务 -- 联表相关
    /// </summary>
    public partial class SqlSugarRepository<TEntity>
    {
        public void BeginTransaction()
        {
            _workUnit.BeginTransaction();
        }

        public bool CommitTransaction()
        {
            return _workUnit.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _workUnit.RollbackTransaction();
        }


        public List<T> StoreProcedure<T>(SqlSugarStoredProcedureDto procedure) where T : new()
        {
            try
            {
                var paras = new List<SugarParameter>();
                bool isDm = IsDmDataBase();
                string procedureName = isDm ? procedure.ProcedureName.ToUpper() : procedure.ProcedureName;
                foreach (var item in procedure.Parameters)
                {
                    string parameterName = item.ParameterName;
                    if (isDm && parameterName.StartsWith("@")) parameterName = parameterName.Substring(1);
                    var para = new SugarParameter(parameterName, item.Value);
                    if (item.IsOutput) para.Direction = item.Direction;
                    paras.Add(para);
                }
                var dt = _baseContext.Ado.UseStoredProcedure().GetDataTable(procedureName, paras);
                foreach (var item in procedure.Parameters.Where(ww => ww.IsOutput))
                {
                    string parameterName = item.ParameterName;
                    if (isDm && parameterName.StartsWith("@")) parameterName = parameterName.Substring(1);
                    var para = paras.FirstOrDefault(ff => ff.ParameterName.Equals(parameterName));
                    if (para == null) continue;
                    item.Value = para.Value;
                }
                var result = DataTableToList<T>(dt);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> StoreProcedureAsync<T>(SqlSugarStoredProcedureDto procedure) where T : new()
        {
            try
            {
                var paras = new List<SugarParameter>();
                foreach (var item in procedure.Parameters)
                {
                    var para = new SugarParameter(item.ParameterName, item.Value);
                    if (item.IsOutput) para.Direction = item.Direction;
                    paras.Add(para);
                }
                var dt = await _baseContext.Ado.UseStoredProcedure().GetDataTableAsync(procedure.ProcedureName, paras);
                foreach (var item in procedure.Parameters.Where(ww => ww.IsOutput))
                {
                    var para = paras.FirstOrDefault(ff => ff.ParameterName.Equals(item.ParameterName));
                    if (para == null) continue;
                    item.Value = para.Value;
                }
                var result = DataTableToList<T>(dt);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断是否是达梦数据库
        /// </summary>
        private bool IsDmDataBase()
        {
            return _baseContext.CurrentConnectionConfig.DbType.Equals(SqlSugar.DbType.Dm);
        }

        /// <summary>
        /// DataTable所有数据转换成实体类列表
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>返回实体类列表</returns>
        private List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return new List<T>();
                }

                // 实例化实体类和列表

                List<T> list = new List<T>();

                // 获取所有列
                DataColumnCollection columns = dt.Columns;

                var type = typeof(T);
                foreach (DataRow dr in dt.Rows)
                {
                    T t = new T();
                    if (type.IsClass)
                    {
                        // 获得实体类的所有公共属性
                        PropertyInfo[] propertys = t.GetType().GetProperties();

                        //循环比对且赋值
                        foreach (PropertyInfo p in propertys)
                        {
                            string name = p.Name;
                            // 检查DataTable是否包含此列
                            if (columns.Contains(name))
                            {
                                if (!p.CanWrite) continue;

                                object value = dr[name];

                                if (value != DBNull.Value)
                                {
                                    //if (value is int || value is float || value is decimal || value is double)
                                    //{
                                    //    p.SetValue(t, value.ToString(), null);
                                    //}
                                    //else
                                    //{
                                    //    p.SetValue(t, value, null);
                                    //}

                                    p.SetValue(t, value, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataColumn column in columns)
                        {
                            if (column.DataType.Equals(type))
                            {
                                t = (T)dr[column.ColumnName];
                                break;
                            }
                        }
                    }
                    list.Add(t);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
