using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;

namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// 常用配置项
    /// </summary>
    public class ConfigureCoreOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Action<FormOptions> FormOptions { get; set; } = options =>
        {
            //最大10M (小程序10M  PC端3M 前端控制)
            var maxLenght = 1024 * 1024 * 10;
            options.ValueLengthLimit = maxLenght;
            options.MultipartBodyLengthLimit = maxLenght;
            options.MultipartHeadersLengthLimit = maxLenght;
        };

        /// <summary>
        /// 
        /// </summary>
        public Action<ForwardedHeadersOptions> ForwardedHeadersOptions { get; set; } = options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        };

        /// <summary>
        /// 
        /// </summary>
        public Action<CookiePolicyOptions> CookiePolicyOptions { get; set; } = options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => false;//这里要改为false，默认是true，true的时候session无效
            options.MinimumSameSitePolicy = SameSiteMode.None;
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConfigureCoreOptions() 
        {
            
        }
    }
}
