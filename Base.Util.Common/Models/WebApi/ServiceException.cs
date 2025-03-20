using System;

namespace Base.Util.Common.Models.WebApi
{
    /// <summary>
    /// API 异常
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// 服务层响应实体
        /// </summary>
        public ServiceMessageModel ServiceMessageModel { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException() : base()
        {
            ServiceMessageModel = ServiceMessageModel.Failure(HttpStateCode.服务器内部错误, "服务器异常！");
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException(string message)
            : base(message)
        {
            ServiceMessageModel = ServiceMessageModel.Failure((int)HttpStateCode.服务器内部错误, message);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
            ServiceMessageModel = ServiceMessageModel.Failure((int)HttpStateCode.服务器内部错误, message);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException(ServiceMessageModel result)
            : base(result.info)
        {
            ServiceMessageModel = result;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException(ServiceMessageModel result, Exception innerException)
            : base(result.info, innerException)
        {
            ServiceMessageModel = result;
        }


        /// <summary>
        /// 获取最初异常
        /// </summary>
        /// <returns></returns>
        public override Exception GetBaseException()
        {
            return base.GetBaseException();
        }

        /// <summary>
        /// 获取最初的服务异常
        /// </summary>
        /// <param name="ex">异常基类</param>
        /// <returns>ServiceException</returns>
        public static ServiceException GetServiceException(Exception ex)
        {
            if (ex.GetType().Equals(typeof(ServiceException))) return (ServiceException)ex;
            if (ex.InnerException == null) return new ServiceException(ex.Message, ex);
            return GetServiceException(ex.InnerException);
        }
    }
}
