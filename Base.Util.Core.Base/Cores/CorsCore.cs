using Base.Util.Core.Base.Models.Options;
using Base.Util.Core.Base.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core.Base.Cores
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
        /// <param name="setupAction"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddCorsCore(this IServiceCollection services, Action<CorsOptions> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            try
            {
                // 跨域配置访问服务注入
                services.AddCors(setupAction);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 跨域配置 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddCorsCore(this IServiceCollection services, CorsCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = options = AppSetting.GetConfiguration<CorsCoreOptions>("Startup.Cors");

            // 跨域配置访问服务注入
            services.AddCors(cors =>
            {
                if (options.EnableAllIPs)
                {
                    cors.AddPolicy(options.PolicyName,
                        policy =>
                        {
                            policy.WithOrigins(options.IPs.ToArray())
                                  .AllowAnyHeader()//Ensures that the policy allows any header.
                                  .AllowAnyMethod();
                        });
                }
                else
                {
                    //允许任意跨域请求
                    cors.AddPolicy(options.PolicyName,
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
        /// <param name="configurePolicy"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseCorsCore(this IApplicationBuilder app, Action<CorsPolicyBuilder> configurePolicy)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseCors(configurePolicy);
        }

        /// <summary>
        /// 跨域配置 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UseCorsCore(this IApplicationBuilder app, CorsCoreOptions? options = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) options = options = AppSetting.GetConfiguration<CorsCoreOptions>("Startup.Cors");

            app.UseCors(options.PolicyName);
        }

    }
}
