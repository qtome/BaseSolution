using Base.Util.Core8.Cores.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core8.Customs
{

    /// <summary>
    /// 通用服务 启动
    /// </summary>
    public static class CustomCore
    {
        /// <summary>
        /// 自定义服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            // 常用项配置
            services.AddConfigureOptionCore();
            //跨域配置
            services.AddCorsCore();
            // 健康检查配置
            services.AddHealthCore();
            //MVC配置
            services.AddMVCCore();
            // 路由配置
            services.AddRouteEndpointCore();
            //Session配置
            services.AddSessionCore();
            // Swagger配置
            services.AddSwaggerCore();
        }


        /// <summary>
        /// 自定义 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            //路由配置
            app.UseRouteEndpointCore();
            //健康检查
            app.UseHealthCore();
            //跨域配置
            app.UseCorsCore();
            //Session配置
            app.UseSessionCore();
            // Swagger中间件
            app.UseSwaggerCore();
        }
    }
}
