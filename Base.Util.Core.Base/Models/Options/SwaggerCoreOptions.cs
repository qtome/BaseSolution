namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// SwaggerCore配置项
    /// </summary>
    public class SwaggerCoreOptions
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "SwaggerTitle";

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; } = "v1";

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "SwaggerName";

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; } = "/swagger/v1/swagger.json";

        /// <summary>
        /// 路由前缀
        /// </summary>
        public string RoutePrefix { get; set; } = "swagger";

        /// <summary>
        /// 说明文件集合
        /// </summary>
        public List<string> XmlFiles { get; set; } = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public SwaggerCoreOptions() 
        {
            
        }

    }
}
