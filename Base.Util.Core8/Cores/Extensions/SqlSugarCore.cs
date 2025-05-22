using Base.Util.Core8.Utils;
using Base.Util.SqlSugarCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Base.Util.Core8.Cores.Extensions
{
    /// <summary>
    /// SqlSugar服务 启动
    /// </summary>
    public static class SqlSugarCore
    {
        /// <summary>
        /// SqlSugar服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddSqlSugarCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var configs = AppSetting.GetSections<ConnectionConfig>("Startup.SqlSugarConnections");

            if (configs.Any())
            {
                // 把多个连接对象注入服务，这里必须采用Scope，因为有事务操作
                services.AddScoped<ISqlSugarClient>(o =>
                {
                    // 连接字符串
                    var listConfig = new List<ConnectionConfig>();
                    foreach (var config in configs)
                    {
                        var configModel = new ConnectionConfig()
                        {
                            ConnectionString = config.ConnectionString,
                            DbType = config.DbType,
                            IsAutoCloseConnection = true,
                            AopEvents = new AopEvents()
                            {
                                OnLogExecuting = (sql, pars) =>
                                {
#if DEBUG
                                    Console.WriteLine(sql);// 打印SQL语句
#endif
                                }
                            }
                        };
                        if (config.ConfigId != null) configModel.ConfigId = Convert.ToString(config.ConfigId).ToLower();
                        listConfig.Add(configModel);
                    }
                    var sqlClient = new SqlSugarClient(listConfig);
                    return sqlClient;
                });
                services.AddScoped<ISqlSugarWorkUnit, SqlSugarWorkUnit>();
            }
        }


        /// <summary>
        /// SqlSugar服务 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseSqlSugarCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

        }
    }
}
