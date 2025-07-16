using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// Mvc注入配置项
    /// </summary>
    public class MvcCoreOptions
    {
        /// <summary>
        /// 全局过滤器
        /// </summary>
        public List<Type> GlobalFliters = new List<Type>();

        /// <summary>
        /// 指定过滤器
        /// </summary>
        public List<Type> ScopeFliters = new List<Type>();

        public Action<MvcNewtonsoftJsonOptions> MvcNewtonsoftJsonOptions { get; set; } = options =>
        {
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        };

        /// <summary>
        /// 构造函数
        /// </summary>
        public MvcCoreOptions() 
        {
            
        }

    }
}
