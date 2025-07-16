using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core.Base.Cores
{
    /// <summary>
    /// 健康检查 启动服务
    /// </summary>
    public static class HealthCore
    {
        /// <summary>
        /// 健康检查 注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddHealthCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecks();
        }

        /// <summary>
        /// 健康检查 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseHealthCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
            });
        }

        /// <summary>
        /// 健康检查 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configure"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseHealthCore(this IApplicationBuilder app, Action<IEndpointRouteBuilder> configure)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseEndpoints(configure);
        }

    }
}
