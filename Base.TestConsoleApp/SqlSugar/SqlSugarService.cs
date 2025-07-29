using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.TestConsoleApp.SqlSugar
{
    internal class SqlSugarService
    {

        /// <summary>
        /// 数据库模型映射
        /// </summary>
        public static void DbFirstShow()
        {
            try
            {
                var dicts = new Dictionary<string, string>()
                {
                    { "BusinessServices_Maintain", "Maintain" },
                };

                // 遍历数据库表
                foreach (var item in dicts)
                {
                    string configStr = @$"Data Source=192.168.1.119;Initial Catalog={item.Key};User ID=sa;Password=weaversys0601A";
                    string folder = item.Value;
                    DbFirstShow(configStr, folder);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="folder">文件夹变量</param>
        private static void DbFirstShow(string connStr, string folder)
        {
            try
            {
                ConnectionConfig connectionConfig = new ConnectionConfig()
                {
                    ConnectionString = connStr,
                    IsAutoCloseConnection = true,
                    DbType = DbType.SqlServer
                };

                // 解决方案文件夹也属于一层
                var basePath = Path.GetFullPath(@$"../../../../Weaversys.BusinessServices.{folder}Api/Entitys");
                using (SqlSugarClient db = new SqlSugarClient(connectionConfig))
                {
                    foreach (var item in db.DbMaintenance.GetTableInfoList())
                    {
                        db.MappingTables.Add(item.Name.Replace("_", ""), item.Name);
                    }

                    string nameSpace = @$"Weaversys.BusinessServices.{folder}Api.Entitys";
                    db.DbFirst
                      .Where(ww => ww.StartsWith("View_FacilityInfo"))
                      .SettingPropertyDescriptionTemplate(ww =>
                      {
                          string desc = "           /// <summary>\r\n           /// {PropertyDescription}\r\n           /// </summary>";
                          return desc;
                      })
                      .IsCreateAttribute()
                      .IsCreateDefaultValue()
                      .StringNullable()
                      .FormatFileName(ww => ww.Replace("_", ""))
                      .CreateClassFile(basePath, nameSpace);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
