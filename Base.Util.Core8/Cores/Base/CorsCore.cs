using Base.Util.Core8.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core8.Cores.Base
{
    /// <summary>
    /// 跨域配置 启动服务
    /// </summary>
    public static class CorsCore
    {
        /// <summary>
        /// 跨域配置 注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 跨域配置访问服务注入
            services.AddCors(cors =>
            {
                if (Convert.ToBoolean(AppSetting.GetSection("Startup.Cors.EnableAllIPs")))
                {
                    cors.AddPolicy(AppSetting.GetSection("Startup.Cors.PolicyName"),
                        policy =>
                        {
                            policy.WithOrigins(AppSetting.GetSection("Startup.Cors.IPs").Split(','))
                                  .AllowAnyHeader()//Ensures that the policy allows any header.
                                  .AllowAnyMethod();
                        });
                }
                else
                {
                    //允许任意跨域请求
                    cors.AddPolicy(AppSetting.GetSection("Startup.Cors.PolicyName"),
                        policy =>
                        {
                            policy.SetIsOriginAllowed((host) => true)
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials();
                        });
                }
            });
        }

        /// <summary>
        /// 跨域配置 中间件
        /// </summary>
        /// <param name="app"></param>
        public static void UseCorsCore(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseCors(AppSetting.GetSection("Startup.Cors.PolicyName"));
        }
    }
}
