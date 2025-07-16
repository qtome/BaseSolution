using Base.Util.Core.Base.Models.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Base.Util.Core.Base.Cores
{
    /// <summary>
    /// 常用配置项 启动服务
    /// </summary>
    public static class ConfigureOptionCore
    {
        /// <summary>
        /// 常用配置项 注入
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <param name="configureOptions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddConfigureOptionCore<TOptions>(this IServiceCollection services, Action<TOptions> configureOptions) where TOptions : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            try
            {
                // 注册全局编码
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            services.Configure(configureOptions);
            try
            {
                // 公共服务
                services.AddOptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return services;
        }

        /// <summary>
        /// 常用配置项 注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddConfigureOptionCore(this IServiceCollection services, ConfigureCoreOptions? options = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (options == null) options = new ConfigureCoreOptions();

            // 注册全局编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //设置接收文件长度的最大值。
            services.Configure<FormOptions>(options.FormOptions);

            services.Configure<ForwardedHeadersOptions>(options.ForwardedHeadersOptions);

            services.Configure<CookiePolicyOptions>(options.CookiePolicyOptions);

            // 公共服务
            services.AddOptions();

        }

    }
}
