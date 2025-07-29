using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Configuration.Cores
{
    /// <summary>
    /// 基础服务注入
    /// </summary>
    internal static class BaseServerCore
    {
        /// <summary>
        /// 基础服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddBaseServerCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthenticationServerSdkCore();
        }

        /// <summary>
        /// 权限服务sdk 注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthenticationServerSdkCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


        }

    }
}
