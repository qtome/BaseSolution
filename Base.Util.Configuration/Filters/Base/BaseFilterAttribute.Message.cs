using Base.Util.Common.Models.WebApi;

namespace Base.Util.Configuration.Filters.Base
{
    /// <summary>
    /// 基类过滤器 消息
    /// </summary>
    public  partial class BaseFilterAttribute
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Success(string info = "成功")
        {
            return ServiceMessageModel.Success(info);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Success(object data, string info = "成功")
        {
            return ServiceMessageModel.Success(info, data);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(int code = 500, string info = "失败")
        {
            return ServiceMessageModel.Failure(code, info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="result">数据</param>
        /// <param name="code">错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(object result, int code = 500, string info = "失败")
        {
            return ServiceMessageModel.Failure(code, info, result);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(HttpStateCode code, string info = "")
        {
            return ServiceMessageModel.Failure(code, info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="result">数据</param>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(object result, HttpStateCode code, string info = "")
        {
            return ServiceMessageModel.Failure(code, info, result);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(SystemErrorCode code, string info = "")
        {
            return ServiceMessageModel.Failure(code, info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="result">数据</param>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(object result, SystemErrorCode code, string info = "")
        {
            return ServiceMessageModel.Failure(code, info, result);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="exception">抛出的异常</param>
        /// <returns></returns>
        protected ServiceMessageModel Failed(ServiceException exception)
        {
            return ServiceMessageModel.Failure(exception.ServiceMessageModel.code, exception.ServiceMessageModel.info);
        }

        /// <summary>
        /// 结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        protected ServiceMessageModel Message(int code, string info)
        {
            return ServiceMessageModel.Message(code, code.Equals((int)HttpStateCode.成功), info, null);
        }
    }
}
