using Base.Util.Configuration.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Configuration.Cores
{
    /// <summary>
    /// AutoMapper 启动服务
    /// </summary>
    internal static class AutoMapperCore
    {
        /// <summary>
        /// AutoMapper 服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperCommonProfileConfig));
            AutoMapperCommonProfileConfig.RegisterMappings();
        }
    }
}
