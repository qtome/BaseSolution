using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;

namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// SessionCore配置项
    /// </summary>
    public class SessionCoreOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Action<SessionOptions> SessionOptions { get; set; } = options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(8);
            options.IOTimeout = TimeSpan.FromHours(8);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "CookieCrossSession";
        };

        /// <summary>
        /// 
        /// </summary>
        public Action<AntiforgeryOptions> AntiforgeryOptions { get; set; } = options =>
        {
            options.HeaderName = "X-CSRF-TOKEN";
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public SessionCoreOptions() 
        {
            
        }
    }
}
