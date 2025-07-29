using Base.Util.Common.Models.WebApi;
using Base.Util.Configuration.Filters.Base;
using Base.Util.Redis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Base.Util.Configuration.Filters.Global
{
    /// <summary>
    /// 全局异常处理
    /// </summary>
    public class GlobalExceptionFilterAttribute : BaseFilterAttribute, IExceptionFilter
    {
        private ILogger<GlobalExceptionFilterAttribute> _logger = null;
        private IModelMetadataProvider _modelMetadataProvider = null;
        private readonly IWebHostEnvironment _env;
        private readonly RedisManager _redis;

        public GlobalExceptionFilterAttribute(
            ILogger<GlobalExceptionFilterAttribute> logger
            , RedisConfig redisConfig
            , IModelMetadataProvider modelMetadataProvider
            , IWebHostEnvironment env)
        {
            _logger = logger;
            _modelMetadataProvider = modelMetadataProvider;
            _env = env;
            _redis = new RedisManager(redisConfig, RedisConst.SystemErrorRedisDbNum);
        }

        /// <summary>
        /// 发生异常之后触发到这儿
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            try
            {
                if (!context.ExceptionHandled) //异常是否被处理过
                {
                    //在这里是不允许发生异常
                    _logger.LogCritical(context.Exception, context.Exception.Message);

                    //在这里判断  是否是访问页面
                    if (IsViewRequest(context.HttpContext.Request))
                    {
                        //跳转到异常页面
                        ViewResult result = new ViewResult { ViewName = "~/Views/Shared/Error.cshtml" };
                        result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                        result.ViewData.Add("Exception", context.Exception);

                        context.Result = result; //断路器---只要对Result赋值--就不继续往后了；
                    }
                    else
                    {
                        ServiceMessageModel apiResult = GetExceptionServiceMessageModel(context.Exception, context);
                        context.Result = new JsonResult(apiResult);
                    }

                    context.ExceptionHandled = true; //标记当前抛出的异常已经被处理过了
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);
                context.ExceptionHandled = true; //标记当前抛出的异常已经被处理过了
                ServiceMessageModel apiResult = GetExceptionServiceMessageModel(ex, context);
                context.Result = new JsonResult(apiResult);
            }
            DeleteTraceInfo(context.HttpContext.TraceIdentifier);
        }

        /// <summary>
        /// 根据异常返回接口消息体
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="context"></param>
        private ServiceMessageModel GetExceptionServiceMessageModel(Exception ex, ExceptionContext context)
        {
            ServiceException serviceException = ServiceException.GetServiceException(ex);
            ServiceMessageModel serviceMessageModel = serviceException == null ? Failed(SystemErrorCode.系统错误, "服务发生错误，请联系管理员")
                                                                               : serviceException.ServiceMessageModel;
            ServiceErrorTraceModel traceModel = GetErrorTraceModel(context.HttpContext, serviceMessageModel, ex);
            _redis.InsertLog(traceModel, RedisConst.SystemErrorRedisKey);
            serviceMessageModel.result = traceModel.GetViewModel();
            return serviceMessageModel;
        }
    }
}
