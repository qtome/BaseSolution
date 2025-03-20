using Base.Util.Common.Models.WebApi;

namespace Base.Util.Common.BaseClass.Attributes
{
    /// <summary>
    /// 基础消息基类
    /// </summary>
    public class BaseServiceMessage
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
        /// <param name="info">信息</param>
        /// <param name="data">数据</param>
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
    }
}
