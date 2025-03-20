using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core8.Cores.Base
{
    /// <summary>
    /// 路由 启动服务
    /// </summary>
    public static class RouteEndpointCore
    {
        /// <summary>
        /// 路由 服务注入
        /// </summary>
        /// <param name="services">服务注入集合</param>
        /// <returns></returns>
        public static void AddRouteEndpointCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 路由配置
            services.AddRouting(options =>
            {
                // 设置URL为小写
                options.LowercaseUrls = true;
                // 在生成的URL后面添加斜杠
                options.AppendTrailingSlash = true;
            });
        }


        /// <summary>
        /// 路由 中间件
        /// </summary>
        /// <param name="app">应用程序构造器</param>
        /// <returns></returns>
        public static void UseRouteEndpointCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
