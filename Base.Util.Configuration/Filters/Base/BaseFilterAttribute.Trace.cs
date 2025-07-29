using Base.Util.Common.Models.WebApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Base.Util.Configuration.Filters.Base
{
    /// <summary>
    /// 基类过滤器 堆栈
    /// </summary>
    public partial class BaseFilterAttribute
    {
        /// <summary>
        /// 获取服务追踪信息类
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        protected ServiceErrorTraceModel GetErrorTraceModel(HttpContext context, ServiceMessageModel model, Exception ex = null)
        {
            var errorTraceModel = FindTraceInfo(context.TraceIdentifier);
            if (errorTraceModel == null) errorTraceModel = GenerateTraceModel(context);
            errorTraceModel.Code = model.GetErrorCode();
            errorTraceModel.Message = model.info;
            errorTraceModel.ExceptionMessage = ex == null ? model.info : ex.GetBaseException().Message;
            return errorTraceModel;
        }

        /// <summary>
        /// 插入追踪信息数据
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns>true or false</returns>
        protected void InsertTraceInfo(ActionExecutingContext context)
        {
            ServiceErrorTraceModel traceModel = GenerateTraceModel(context);
            string key = $@"{_redisKey}:{traceModel.TraceIdentifier}";
            _redis.StringSet(key, traceModel);
        }

        /// <summary>
        /// 获取请求追踪信息
        /// </summary>
        /// <param name="traceId">请求追踪Id</param>
        /// <returns>true or false</returns>
        protected ServiceErrorTraceModel FindTraceInfo(string traceId)
        {
            string key = $@"{_redisKey}:{traceId}";
            if (_redis.KeyExists(key))
            {
                return _redis.StringGet<ServiceErrorTraceModel>(key);
            }
            return null;
        }

        /// <summary>
        /// 移除请求追踪信息
        /// </summary>
        /// <param name="traceId">请求追踪Id</param>
        /// <returns>true or false</returns>
        protected ServiceErrorTraceModel DeleteTraceInfo(string traceId)
        {
            string key = $@"{_redisKey}:{traceId}";
            if (_redis.KeyExists(key))
            {
                var result = _redis.StringGet<ServiceErrorTraceModel>(key);
                _redis.KeyDelete(key);
                return result;
            }
            return null;
        }


        /// <summary>
        /// 生成追踪信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns></returns>
        private ServiceErrorTraceModel GenerateTraceModel(ActionExecutingContext context)
        {
            var traceModel = new ServiceErrorTraceModel()
            {
                ActionArguments = context.ActionArguments,
                TraceIdentifier = context.HttpContext.TraceIdentifier,
                RouteValues = context.HttpContext.Request.RouteValues,
                Headers = context.HttpContext.Request.Headers.ToDictionary(ss => ss.Key, ss => ss.Value.ToString()),
                Query = context.HttpContext.Request.Query.ToDictionary(ss => ss.Key, ss => ss.Value.ToString()),
                Path = context.HttpContext.Request.Path,
            };
            return traceModel;
        }
        /// <summary>
        /// 生成追踪信息
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <returns></returns>
        private ServiceErrorTraceModel GenerateTraceModel(HttpContext context)
        {
            var traceModel = new ServiceErrorTraceModel()
            {
                TraceIdentifier = context.TraceIdentifier,
                RouteValues = context.Request.RouteValues,
                Headers = context.Request.Headers.ToDictionary(ss => ss.Key, ss => ss.Value.ToString()),
                Query = context.Request.Query.ToDictionary(ss => ss.Key, ss => ss.Value.ToString()),
                Path = context.Request.Path,
            };
            return traceModel;
        }
    }
}
