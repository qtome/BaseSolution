using System.ComponentModel;

namespace Base.Util.Common.Models.WebApi
{
    /// <summary>
    /// 请求方法枚举
    /// </summary>
    public enum HttpRequestMethodOption
    {
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        GET = 1,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        POST = 2,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        PUT = 3,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        DELETE = 4,
        /// <summary>
        /// 
        /// </summary>
        HEAD = 5,
        /// <summary>
        /// 
        /// </summary>
        OPTIONS = 6,
        /// <summary>
        /// 
        /// </summary>
        PATCH = 7,
        /// <summary>
        /// 
        /// </summary>
        MERGE = 8,
        /// <summary>
        /// 
        /// </summary>
        COPY = 9
    }
}
