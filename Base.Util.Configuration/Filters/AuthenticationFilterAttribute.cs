using Base.Util.Common.Models.WebApi;
using Base.Util.Common.Utils.Serialize;
using Base.Util.Configuration.Filters.Base;
using Base.Util.Configuration.Statics.AuthInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Base.Util.Configuration.Filters
{
    /// <summary>
    /// 验证数据权限过滤器（简易：验证有效权限数据）
    /// </summary>
    public class AuthenticationFilterAttribute : BaseFilterAttribute, IActionFilter
    {
        private ILogger<AuthenticationFilterAttribute> _logger = null;
        private readonly IWebHostEnvironment _env;
        private IModelMetadataProvider _modelMetadataProvider = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="env"></param>
        /// <param name="modelMetadataProvider"></param>
        public AuthenticationFilterAttribute(ILogger<AuthenticationFilterAttribute> logger
            , IWebHostEnvironment env
            , IModelMetadataProvider modelMetadataProvider)
        {
            _logger = logger;
            _env = env;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// 方法执行前 验证是否包含 指定验证信息
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(item => item.GetType() == typeof(NoAuthenticationFilterAttribute)))
            {   //如果标记的有特殊的记号，就避开检查；
                return;
            }

            if (IsSwaggerRequest(context.HttpContext.Request))
            {   //如果是开发swagger调试使用测试数据
                var headers = GetRequestHeaderInfos(context.HttpContext);
                if (!headers.ContainsKey(BaseAuthConst.headerKey))
                {
                    context.HttpContext.Request.Headers.Add(BaseAuthConst.headerKey, JsonHelper.SerializeObject("令牌信息"));
                }
            }
            string tokenJson = context.HttpContext.Request.Headers[BaseAuthConst.headerKey];
            if (string.IsNullOrWhiteSpace(tokenJson))
            {
                context.Result = new ObjectResult(Failed(HttpStateCode.未授权, "没有令牌信息！"));
                return;
            }

            //TokenResult token = JsonHelper.DeserializeObject<TokenResult>(tokenJson);
            //if (token == null)
            //{
            //    context.Result = new ObjectResult(Failed(HttpStateCode.未授权, "令牌信息格式不正确！"));
            //    return;
            //}
            //try
            //{   //检查并自动token续期
            //    BasePermissionCacheService.Instance.CheckAndRenewalPermissionCache(token);
            //}
            //catch (ServiceException ex)
            //{
            //    context.Result = new ObjectResult(Failed(ex.ServiceMessageModel.code, ex.ServiceMessageModel.info));
            //    return;
            //}
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
