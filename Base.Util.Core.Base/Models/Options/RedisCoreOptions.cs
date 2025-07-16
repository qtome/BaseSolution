namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// Redis注入配置项
    /// </summary>
    public class RedisCoreOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "127.0.0.1:6379";

        /// <summary>
        /// 密码
        /// </summary>
        public string Pass { get; set; } = string.Empty;
        /// <summary>
        /// 默认键值
        /// </summary>
        public string CustomKey { get; set; } = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RedisCoreOptions()
        {
           
        }

    }
}
