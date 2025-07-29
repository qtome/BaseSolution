using Base.Util.Common.Models.WebApi;
using Base.Util.Common.Utils.DataTypeHelper;
using Base.Util.Configuration.Filters.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Base.Util.Configuration.Filters.Global
{
    /// <summary>
    /// 全局结果过滤器
    /// </summary>
    public class GlobalResultFilterAttribute : BaseFilterAttribute, IResultFilter
    {

        private IModelMetadataProvider _modelMetadataProvider = null;

        public GlobalResultFilterAttribute(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// 渲染视图之前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            //根据实际需求进行具体实现
            if (context.Result is ObjectResult)
            {
                ObjectResult objectResult = context.Result as ObjectResult;
                if (!objectResult.StatusCode.HasValue)
                {
                    if (objectResult.Value.GetType().Name.StartsWith(typeof(ServiceMessageModel).Name))
                    {   // 是 新返回消息体 的时候 直接返回
                        return;
                    }
                    if (objectResult.StatusCode == (int)HttpStateCode.未授权)
                    {
                        context.Result = new ObjectResult(Failed(HttpStateCode.未授权));
                    }
                    else
                    {
                        if (objectResult.Value == null)
                        {
                            context.Result = new ObjectResult(Failed(HttpStateCode.未找到));
                        }
                        else
                        {
                            context.Result = new ObjectResult(Success(objectResult.Value, HttpStateCode.成功.ToString()));
                        }
                    }
                }
                else
                {
                    if (context.Result.GetType().Equals(typeof(BadRequestObjectResult)))
                    {
                        BadRequestObjectResult badRequestObjectResult = (BadRequestObjectResult)context.Result;
                        context.Result = new ObjectResult(Failed(badRequestObjectResult.Value, HttpStateCode.错误请求));
                        return;
                    }
                    else
                    {
                        context.Result = new ObjectResult(Failed(objectResult.Value, objectResult.StatusCode.Value));
                    }
                }
            }
            else if (context.Result is JsonResult)
            {
                JsonResult jsonResult = (JsonResult)context.Result;
                if (jsonResult.Value.GetType().Name.StartsWith(typeof(ServiceMessageModel).Name))
                {   // 是 新返回消息体 的时候 直接返回
                    return;
                }
                context.Result = new ObjectResult(Success(jsonResult.Value, HttpStateCode.成功.ToString()));
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(Failed(HttpStateCode.未找到));
            }
            else if (context.Result is ContentResult)
            {
                ContentResult contentResult = (ContentResult)context.Result;
                context.Result = new ObjectResult(Success(contentResult.Content, HttpStateCode.成功.ToString()));
            }
            else if (context.Result is StatusCodeResult)
            {
                StatusCodeResult statusCodeResult = (StatusCodeResult)context.Result;
                context.Result = new ObjectResult(Message(statusCodeResult.StatusCode, typeof(HttpStateCode).GetEnumValue(statusCodeResult.StatusCode)));
            }
            else if (context.Result is ViewResult)
            {
                //do nothing
            }
        }

        /// <summary>
        /// 渲染视图之后执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            DeleteTraceInfo(context.HttpContext.TraceIdentifier);
        }
    }
}
