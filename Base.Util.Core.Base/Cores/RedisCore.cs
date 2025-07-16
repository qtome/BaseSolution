using Base.Util.Core.Base.Models.Options;
using Base.Util.Core.Base.Utils;
using Base.Util.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core.Base.Cores
{
    /// <summary>
    /// Redis注入
    /// </summary>
    public static class RedisCore
    {
        /// <summary>
        /// Redis 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddRedisCore(this IServiceCollection services, RedisCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = AppSetting.GetConfiguration<RedisCoreOptions>("Startup.Redis");

            services.AddSingleton(x =>
            {
                var redisConfig = new RedisConfig(options.Pass, options.CustomKey, options.ConnectionString);
                return redisConfig;
            });

        }
    }
}
