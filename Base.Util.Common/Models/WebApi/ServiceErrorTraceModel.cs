using System;
using System.Collections.Generic;

namespace Base.Util.Common.Models.WebApi
{
    /// <summary>
    /// 服务错误追踪类
    /// </summary>
    public class ServiceErrorTraceModel
    {
        /// <summary>
        /// 追踪Id
        /// </summary>
        public string TraceId { get; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime ReportDate { get; } = DateTime.Now;
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; } = (int)HttpStateCode.成功;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string ExceptionMessage { get; set; }
        /// <summary>
        /// 堆栈
        /// </summary>
        public string Stack { get; set; }

        /// <summary>
        /// 追踪Id
        /// </summary>
        public string TraceIdentifier { get; set; }
        /// <summary>
        /// Action参数集合
        /// </summary>
        public IDictionary<string, object?> ActionArguments { get; set; }
        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }
        /// <summary>
        /// 查询参数集合
        /// </summary>
        public IDictionary<string, string> Query { get; set; }
        /// <summary>
        /// 体参数
        /// </summary>
        public object Body { get; set; }
        /// <summary>
        /// Api地址
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Api路由
        /// </summary>
        public IDictionary<string, object?> RouteValues { get; set; }

    }

    /// <summary>
    /// 服务错误追踪类 扩展
    /// </summary>
    public static class ServiceErrorTraceModelExtension
    {
        /// <summary>
        /// 获取前端错误码追踪信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static object GetViewModel(this ServiceErrorTraceModel model)
        {
            return new
            {
                TraceId = model.TraceId,
                Code = model.Code,
            };
        }
    }
}
