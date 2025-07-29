using Base.Util.Configuration.Filters;
using Base.Util.Configuration.Filters.Global;
using Base.Util.Core.Base.Cores;
using Base.Util.Core.Base.Models.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Configuration.Cores
{
    /// <summary>
    /// MVC 启动服务
    /// </summary>
    public static class MvcCore
    {
        /// <summary>
        /// MVC 服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddMvcConfiguartionCore(this IServiceCollection services, MvcCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = new MvcCoreOptions();

            options.GlobalFliters.Add(typeof(GlobalActionFilterAttribute));
            options.GlobalFliters.Add(typeof(GlobalExceptionFilterAttribute));
            options.GlobalFliters.Add(typeof(GlobalResultFilterAttribute));
            options.ScopeFliters.Add(typeof(AuthenticationFilterAttribute));

            services.AddMvcCustomCore(options);
            services.AddHttpContextAccessor();
        }

    }
}
