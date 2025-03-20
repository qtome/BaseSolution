using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Base.Util.Core8.Cores.Base
{
    /// <summary>
    /// MVC 启动服务
    /// </summary>
    public static class MVCCore
    {
        /// <summary>
        /// MVC 服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddMVCCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        /// <summary>
        /// MVC 服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="globalFliters">全局过滤器</param>
        /// <param name="scopeFliters">指定过滤器</param>
        public static void AddMVCCore(this IServiceCollection services, List<Type> globalFliters, List<Type> scopeFliters)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (scopeFliters != null && scopeFliters.Any())
            {
                foreach (var fliter in scopeFliters)
                {
                    services.AddScoped(fliter);
                }
            }
            services.AddMvc(options =>
            {
                if (globalFliters != null && globalFliters.Any())
                {
                    foreach (var fliter in globalFliters)
                    {
                        options.Filters.Add(fliter);
                    }
                }
                options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

    }
}
