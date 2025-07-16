using Base.Util.Core.Base.Models.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core.Base.Cores
{
    /// <summary>
    /// Session 启动服务
    /// </summary>
    public static class SessionCore
    {
        /// <summary>
        /// Session 服务注入(通用配置)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddCommonSessionCore(this IServiceCollection services, SessionCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = new SessionCoreOptions();

            services.AddSession(options.SessionOptions);

            services.AddHttpContextAccessor();

            services.AddAntiforgery(options.AntiforgeryOptions);
        }

        /// <summary>
        /// Session 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCommonSessionCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSession();

            app.UseCookiePolicy();
        }
    }
}
