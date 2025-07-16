using Base.Util.Common.Utils.Serialize;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Base.Util.Core.Base.Extensions
{
    /// <summary>
    /// .NetCore中HttpContext扩展
    /// </summary>
    public static class HttpContextExtension
    {
        #region HttpContext

        #region IP和Url

        /// <summary>
        /// 判断是否是 WebSocket 请求
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static bool IsWebSocketRequest(this HttpContext context)
            => context.WebSockets.IsWebSocketRequest || context.Request.Path == "/ws";

        /// <summary>
        /// 获取本机 IPv4地址
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static string GetLocalIpAddressToIPv4(this HttpContext context)
            => context.Connection.LocalIpAddress?.MapToIPv4()?.ToString();

        /// <summary>
        /// 获取本机 IPv6地址
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static string GetLocalIpAddressToIPv6(this HttpContext context)
            => context.Connection.LocalIpAddress?.MapToIPv6()?.ToString();

        /// <summary>
        /// 获取远程 IPv4地址
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="headerKeys">是否优先取值的头信息键值集合</param>
        /// <returns></returns>
        public static string GetRemoteIpAddressToIPv4(this HttpContext context, params string[] headerKeys)
        {
            foreach (string headerKey in headerKeys)
            {
                string header = context.GetRequestHeaderInfo(headerKey);
                if (!string.IsNullOrEmpty(header))
                {
                    if (IPAddress.TryParse(header, out IPAddress ip))
                    {
                        return ip.ToString();
                    }
                }
            }
            return context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString();
        }

        /// <summary>
        /// 获取远程 IPv6地址
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static string GetRemoteIpAddressToIPv6(this HttpContext context)
            => context.Connection.RemoteIpAddress?.MapToIPv6()?.ToString();

        #endregion

        #region HttpRequest

        /// <summary>
        /// 获取所有头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRequestHeaderInfos(this HttpContext context)
            => context.Request.GetRequestHeaderInfos();
        /// <summary>
        /// 获取指定请求头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetRequestHeaderInfo(this HttpContext context, string key)
            => context.Request.GetRequestHeaderInfo(key);
        /// <summary>
        /// 设置请求头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void AddRequestHeaderInfo(this HttpContext context, string key, string value)
            => context.Request.AddRequestHeaderInfo(key, value);

        /// <summary>
        /// 获取完整请求地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestUrlAddress(this HttpContext context)
            => context.Request.GetRequestUrlAddress();

        /// <summary>
        /// 获取来源地址
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="refererHeaderKey">请求头信息来源键值</param>
        /// <returns></returns>
        public static string GetRefererUrlAddress(this HttpContext context, string refererHeaderKey = "Referer")
            => context.Request.GetRefererUrlAddress(refererHeaderKey);

        #endregion

        #region HttpResponse

        /// <summary>
        /// 获取所有响应头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetResponseHeaderInfos(this HttpContext context)
            => context.Response.GetResponseHeaderInfos();
        /// <summary>
        /// 获取指定响应头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetResponseHeaderInfo(this HttpContext context, string key)
            => context.Response.GetResponseHeaderInfo(key);
        /// <summary>
        /// 设置响应头信息
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void AddResponseHeaderInfo(this HttpContext context, string key, string value)
            => context.Response.AddResponseHeaderInfo(key, value);

        #endregion

        #region Session

        /// <summary>
        /// 清除所有Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <returns></returns>
        public static void RemoveSession(this HttpContext context)
            => context.Session.Clear();
        /// <summary>
        /// 清除指定Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static void RemoveSession(this HttpContext context, string key)
            => context.Session.Remove(key);

        /// <summary>
        /// 存储指定Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <param name="value">对象</param>
        /// <returns></returns>
        public static void SetSession(this HttpContext context, string key, object value)
            => context.SetSession(key, JsonHelper.SerializeObject(value));
        /// <summary>
        /// 存储指定Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetSession(this HttpContext context, string key, string value)
            => context.Session.SetString(key, value);

        /// <summary>
        /// 获取指定Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static T GetSession<T>(this HttpContext context, string key)
            => JsonHelper.DeserializeObject<T>(context.GetSession(key));
        /// <summary>
        /// 获取指定Session
        /// </summary>
        /// <param name="context">http请求上下文</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetSession(this HttpContext context, string key)
            => context.Session.GetString(key);

        #endregion

        #endregion


        #region HttpRequest

        /// <summary>
        /// 获取所有请求头信息
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRequestHeaderInfos(this HttpRequest request)
            => request.Headers.ToDictionary(ss => ss.Key, aa => aa.Value.ToString());

        /// <summary>
        /// 获取指定请求头信息
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetRequestHeaderInfo(this HttpRequest request, string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            var headers = request.GetRequestHeaderInfos();
            if (headers.ContainsKey(key)) return headers[key];
            if (headers.ContainsKey(key.ToLower())) return headers[key.ToLower()];
            return string.Empty;
        }

        /// <summary>
        /// 设置请求头信息
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void AddRequestHeaderInfo(this HttpRequest request, string key, string value)
        {
            if (request.Headers.ContainsKey(key)) request.Headers.Add(key, value);
        }

        /// <summary>
        /// 获取完整请求地址
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <returns></returns>
        public static string GetRequestUrlAddress(this HttpRequest request)
        {
            return new StringBuilder()
                    .Append(request.Scheme)
                    .Append("://")
                    .Append(request.Host.Value)
                    .Append(request.PathBase)
                    .Append(request.Path)
                    .Append(request.QueryString)
                    .ToString();
        }

        /// <summary>
        /// 获取来源地址
        /// </summary>
        /// <param name="request">接口请求</param>
        /// <param name="refererHeaderKey">请求头信息来源键值</param>
        /// <returns></returns>
        public static string GetRefererUrlAddress(this HttpRequest request, string refererHeaderKey = "Referer")
            => request.GetRequestHeaderInfo(refererHeaderKey);

        #endregion


        #region HttpResponse

        /// <summary>
        /// 获取所有响应头信息
        /// </summary>
        /// <param name="response">接口响应</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetResponseHeaderInfos(this HttpResponse response)
            => response.Headers.ToDictionary(ss => ss.Key, aa => aa.Value.ToString());

        /// <summary>
        /// 获取指定响应头信息
        /// </summary>
        /// <param name="response">接口响应</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public static string GetResponseHeaderInfo(this HttpResponse response, string key)
        {
            if (string.IsNullOrEmpty(key)) return string.Empty;
            var headers = response.GetResponseHeaderInfos();
            if (headers.ContainsKey(key)) return headers[key];
            if (headers.ContainsKey(key.ToLower())) return headers[key.ToLower()];
            return string.Empty;
        }

        /// <summary>
        /// 设置响应头信息
        /// </summary>
        /// <param name="response">接口响应</param>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void AddResponseHeaderInfo(this HttpResponse response, string key, string value)
        {
            if (response.Headers.ContainsKey(key)) response.Headers.Add(key, value);
        }

        #endregion
    }

}
