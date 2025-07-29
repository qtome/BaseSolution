using Base.Util.Common.BaseClass.Attributes;
using Base.Util.Common.Enums.Common;
using Base.Util.Common.Utils.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using System.Net;

namespace Base.Util.Configuration.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>
    public partial class BaseCustomeController : Controller
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseCustomeController()
        {
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected string GetClientIpAddress()
        {
            string header = GetRequestHeaderInfo("X-Forwarded-For");
            if (string.IsNullOrWhiteSpace(header)) header = GetRequestHeaderInfo("CF-Connecting-IP");
            if (!string.IsNullOrWhiteSpace(header) && IPAddress.TryParse(header, out IPAddress ip)) return ip.ToString();
            ip = HttpContext.Connection.RemoteIpAddress;
            return ip.ToString();
        }

        /// <summary>
        /// 获取头信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [NonAction]
        protected string GetRequestHeaderInfo(string key)
        {
            var headers = GetRequestHeaderInfo();
            if (headers.ContainsKey(key)) return headers[key];
            if (headers.ContainsKey(key.ToLower())) return headers[key.ToLower()];
            return string.Empty;
        }

        /// <summary>
        /// 获取所有头信息
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected Dictionary<string, string> GetRequestHeaderInfo()
        {
            var heads = HttpContext.Request.Headers.ToDictionary(ss => ss.Key, aa => aa.Value.ToString());
            return heads;
        }

        /// <summary>
        /// 检查是否 swagger调试调用
        /// </summary>
        /// <returns>true or false</returns>
        protected bool IsSwaggerRequest()
        {
            string headerSwagger = GetRequestHeaderInfo("Referer");
            return !string.IsNullOrWhiteSpace(headerSwagger) && headerSwagger.Contains("swagger");
        }

        /// <summary>
        /// 添加文件下载头信息
        /// 前端需要文件名的情况需要暴露出特定的头星系
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected virtual void AddResponseFileHeader()
        {   // Access-Control-Expose-Headers 代表需要暴露的头信息
            // Content-Disposition 记录着文件下载接口中的文件名
            this.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
        }

        /// <summary>
        /// 获取数据来源
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected string GetDataSource()
        {
            var methodBase = ReflectHelper.GetMethodByFrame(3);
            var attributes = methodBase.GetCustomAttributes(false);
            if (attributes != null && attributes.Any())
            {
                var attribute = attributes.FirstOrDefault(ff => ff.GetType() == typeof(DataSourceAttribute));
                if (attribute != null)
                {
                    return (attribute as DataSourceAttribute)?.GetDataSource();
                }
            }
            string url = GetRequestHeaderInfo("Referer");
            if (string.IsNullOrWhiteSpace(url)) url = GetRequestHeaderInfo("User-Agent");
            if (!string.IsNullOrWhiteSpace(url))
            {
                if (url.Contains("servicewechat.com")) return DataSourceOption.微信小程序提交.ToString();
                return DataSourceOption.PC提交.ToString();
            }
            return DataSourceOption.未知.ToString();
        }

        /// <summary>
        /// 返回下载文件内容
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected virtual FileContentResult GetFileResult(IWorkbook workbook, string fileName)
        {   // 转成二进制从接口导出
            byte[] buffer = new byte[1024 * 2];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }
            AddResponseFileHeader();
            return File(buffer, "application/msword", fileName);
        }

    }
}
