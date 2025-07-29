using Base.Util.Repository.SqlSugars.Abstraction;
using Base.Util.Repository.SqlSugars.Implementation;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Base.Util.Core.SqlSugars.Cores
{
    /// <summary>
    /// SqlSugar注入
    /// </summary>
    public static class SqlSugarCore
    {
        /// <summary>
        /// SqlSugar注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configs"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddSqlSugarCore(this IServiceCollection services, List<ConnectionConfig> configs)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

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


    }
}
