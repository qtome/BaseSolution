using Base.Util.Core.Base.Models.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Util.Core.Base.Cores
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
        public static void AddMvcCustomCore(this IServiceCollection services, MvcCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = new MvcCoreOptions();

            foreach (var fliter in options.ScopeFliters)
            {
                services.AddScoped(fliter);
            }
            services.AddMvc(opt =>
            {
                foreach (var fliter in options.GlobalFliters)
                {
                    opt.Filters.Add(fliter);
                }
                opt.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(options.MvcNewtonsoftJsonOptions);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

    }
}
