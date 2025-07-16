namespace Base.Util.Core.Base.Models.Options
{
    /// <summary>
    /// 跨域注入配置
    /// </summary>
    public class CorsCoreOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public bool EnableAllIPs { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public string PolicyName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<string> IPs { get; set; } = new List<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorsCoreOptions() 
        {
        }
    }
}
