using Base.Util.Core8.Utils;
using Base.Util.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core8.Cores.Extensions
{
    /// <summary>
    /// Redis 启动服务
    /// </summary>
    public static class RedisCore
    {
        /// <summary>
        /// Redis 注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddRedisCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton(x =>
            {
                //配置信息
                string pass = AppSetting.GetSection("Startup.Redis.Pass");
                string customKey = AppSetting.GetSection("Startup.Redis.CustomKey");
                string connStr = AppSetting.GetSection("Startup.Redis.ConnectionString");
                if (string.IsNullOrEmpty(connStr)) throw new ArgumentNullException($@"Redis连接字符串参数为空：{nameof(connStr)}");
                var result = new RedisConfig(pass, customKey, connStr);
                return result;
            });
        }
    }
}
