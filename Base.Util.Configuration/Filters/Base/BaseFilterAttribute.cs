using Base.Util.Common.Utils.Cryption;
using Base.Util.Common.Utils.Serialize;
using Base.Util.Core.Base.Utils;
using Base.Util.Redis;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Base.Util.Configuration.Filters.Base
{
    /// <summary>
    /// 基础过滤器
    /// </summary>
    public partial class BaseFilterAttribute : Attribute
    {
        /// <summary>
        /// 业务key
        /// </summary>
        protected string BussinessKey { get; set; } = string.Empty;

        /// <summary>
        /// Redis 管理工具
        /// </summary>
        protected RedisManager _redis { get; }
        /// <summary>
        /// Redis 缓存Key
        /// </summary>
        protected string _redisKey { get; }

        public BaseFilterAttribute()
            : this(RedisConst.DBTransactionDbNum, $@"{RedisConst.DBTransactionRedisKey}{RedisConst.TraceCollectionRedisKey}")
        {
        }

        public BaseFilterAttribute(int dbNum, string key)
        {
            //配置appJson
            string pass = AppSetting.GetConfiguration("Startup.Redis.Pass");
            string customKey = AppSetting.GetConfiguration("Startup.Redis.CustomKey");
            string connStr = AppSetting.GetConfiguration("Startup.Redis.ConnectionString");
            var redisConig = new RedisConfig(pass, customKey, connStr);
            _redis = new RedisManager(redisConig, dbNum);
            _redisKey = key;
        }

        /// <summary>
        /// 检查是否是Ajax调用
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns>true or false</returns>
        protected bool IsAjaxRequest(HttpRequest request)
        {
            var headers = GetRequestHeaderInfos(request);
            string header = GetRequestHeaderInfo(headers, "X-Requested-With");
            string headerSwagger = GetRequestHeaderInfo(headers, "Referer");
            string headerRestSharp = GetRequestHeaderInfo(headers, "User-Agent");
            if (string.IsNullOrWhiteSpace(headerSwagger)) return "XMLHttpRequest".Equals(header) || headerRestSharp.Contains("RestSharp");
            return "XMLHttpRequest".Equals(header) || headerSwagger.Contains("swagger") || headerRestSharp.Contains("RestSharp");
        }

        /// <summary>
        /// 检查是否 swagger调试调用
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns>true or false</returns>
        protected bool IsSwaggerRequest(HttpRequest request)
        {
            string headerSwagger = GetRequestHeaderInfo(request, "Referer");
            return !string.IsNullOrWhiteSpace(headerSwagger) && headerSwagger.Contains("swagger");
        }

        /// <summary>
        /// 检查是否是查看页面
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns>true or false</returns>
        protected bool IsViewRequest(HttpRequest request)
        {
            string path = request.Path.Value;
            return string.IsNullOrWhiteSpace(path) ? false : path.StartsWith("/Pages");
        }

        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns>缓存key</returns>
        protected string GetCacheKey(HttpRequest request)
        {
            string path = request.Path;
            string query = JsonHelper.SerializeObject(request.Query);
            return path + query;
        }

        /// <summary>
        /// 获取缓存key(md5)
        /// </summary>
        /// <param name="data">接口请求</param>
        /// <returns>缓存key</returns>
        protected string GetCacheKey(string data)
        {
            return $@"{BussinessKey}{HashHelper.MD5Encrypt(data)}";
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        protected string GetClientIpAddress(HttpContext context)
        {
            string header = GetRequestHeaderInfo(context.Request, "X-Forwarded-For");
            if (string.IsNullOrWhiteSpace(header)) header = GetRequestHeaderInfo(context.Request, "CF-Connecting-IP");
            if (!string.IsNullOrWhiteSpace(header) && IPAddress.TryParse(header, out IPAddress ip)) return ip.ToString();
            ip = context.Connection.RemoteIpAddress;
            return ip.ToString();
        }

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        protected string GetRequestHeaderInfo(HttpRequest request, string key)
        {
            var headers = GetRequestHeaderInfos(request);
            return GetRequestHeaderInfo(headers, key);
        }

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        protected string GetRequestHeaderInfo(HttpContext context, string key)
        {
            var headers = GetRequestHeaderInfos(context);
            return GetRequestHeaderInfo(headers, key);
        }

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="headers">头信息集合字典</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        protected string GetRequestHeaderInfo(Dictionary<string, string> headers, string key)
        {
            if (headers.ContainsKey(key)) return headers[key];
            if (headers.ContainsKey(key.ToLower())) return headers[key.ToLower()];
            return string.Empty;
        }

        /// <summary>
        /// 获取所有头信息
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns></returns>
        protected Dictionary<string, string> GetRequestHeaderInfos(HttpRequest request)
        {
            var heads = request.Headers.ToDictionary(ss => ss.Key, aa => aa.Value.ToString());
            return heads;
        }

        /// <summary>
        /// 获取所有头信息
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        protected Dictionary<string, string> GetRequestHeaderInfos(HttpContext context)
        {
            return GetRequestHeaderInfos(context.Request);
        }
    }
}
