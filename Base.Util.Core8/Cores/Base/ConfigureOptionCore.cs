using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Base.Util.Core8.Cores.Base
{
    /// <summary>
    /// 常用配置项 启动服务
    /// </summary>
    public static class ConfigureOptionCore
    {
        /// <summary>
        /// 常用配置项 注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddConfigureOptionCore(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 注册全局编码
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //设置接收文件长度的最大值。
            services.Configure<FormOptions>(x =>
            {
                //最大10M (小程序10M  PC端3M 前端控制)
                var maxLenght = 1024 * 1024 * 10;
                x.ValueLengthLimit = maxLenght;
                x.MultipartBodyLengthLimit = maxLenght;
                x.MultipartHeadersLengthLimit = maxLenght;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;//这里要改为false，默认是true，true的时候session无效
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 公共服务
            services.AddOptions();
        }
    }
}
