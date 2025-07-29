using Base.Util.Common.Models.WebApi;
using Base.Util.Common.Utils.DataTypeHelper;
using Base.Util.Configuration.Filters.Base;
using Base.Util.Redis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Base.Util.Configuration.Filters.Global
{
    /// <summary>
    /// 全局行为过滤器
    /// </summary>
    public class ActionFilterAttribute : BaseFilterAttribute, IActionFilter
    {

        private ILogger<ActionFilterAttribute> _logger = null;
        private readonly IWebHostEnvironment _env;
        private IModelMetadataProvider _modelMetadataProvider = null;
        private readonly RedisManager _redis;

        public ActionFilterAttribute(
            ILogger<ActionFilterAttribute> logger
            , RedisConfig redisConfig
            , IWebHostEnvironment env
            , IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _env = env;
            _modelMetadataProvider = modelMetadataProvider;
            _redis = new RedisManager(redisConfig, RedisConst.SystemErrorRedisDbNum);
        }

        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            InsertTraceInfo(context);
            // 表单model验证不通过时，直接抛出异常
            if (!context.ModelState.IsValid)
            {
                List<string> errorMessages = new List<string>();
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
                ServiceMessageModel apiResult = GetServiceMessageModel(errorMessages, context);
                context.Result = new JsonResult(apiResult);
                return;
            }
        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// 返回接口消息体
        /// </summary>
        /// <param name="errorMessages"></param>
        /// <param name="context"></param>
        private ServiceMessageModel GetServiceMessageModel(List<string> errorMessages, ActionExecutingContext context)
        {
            ServiceMessageModel serviceMessageModel = Failed(SystemErrorCode.表单验证失败, errorMessages.Combine("|", new string[] { }));
            ServiceErrorTraceModel traceModel = GetErrorTraceModel(context.HttpContext, serviceMessageModel);
            _redis.InsertLog(traceModel, RedisConst.SystemErrorRedisKey);
            serviceMessageModel.result = traceModel.GetViewModel();
            return serviceMessageModel;
        }
    }
}
