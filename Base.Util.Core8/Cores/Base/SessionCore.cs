using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core8.Cores.Base
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
        public static void AddSessionCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromHours(8);
                opt.IOTimeout = TimeSpan.FromHours(8);
                opt.Cookie.HttpOnly = true;
                opt.Cookie.IsEssential = true;
                opt.Cookie.Name = "CookieCrossSession";
            });

            services.AddHttpContextAccessor();

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
        }

        /// <summary>
        /// Session 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseSessionCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSession();

            app.UseCookiePolicy();
        }
    }
}
