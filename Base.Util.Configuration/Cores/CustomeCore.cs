using Base.Util.Core.Base.Cores;
using Base.Util.Core.SqlSugars.Cores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Configuration.Cores
{
    /// <summary>
    /// 自定义注入
    /// </summary>
    public static class CustomeCore
    {
        /// <summary>
        /// 自定义注入
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddCustomeCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddConfigureOptionCore();

            services.AddRouteEndpointCore();

            services.AddHealthCore();

            services.AddCorsCore();

            services.AddSwaggerCore();

            services.AddSqlSugarCore();

            services.AddRedisCore();

            services.AddAutoMapperCore();

            services.AddMvcConfiguartionCore();

            services.AddBaseServerCore();

        }

        /// <summary>
        /// 自定义注入中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomeCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseRouteEndpointCore();

            app.UseHealthCore();

            app.UseCorsCore();

            app.UseSwaggerCore();
        }

    }
}
