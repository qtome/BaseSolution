using Base.Util.Common.Utils.DataTypeHelper;

namespace Base.Util.Common.Models.WebApi
{
    /// <summary>
    /// 服务层响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceMessageModel<T> : ServiceMessageModel
    {
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T result { get; set; }

        /// <summary>
        /// 返回成功消息体
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Success(string info)
        {
            return Message((int)HttpStateCode.成功, true, info, default);
        }
        /// <summary>
        /// 返回成功消息体
        /// </summary>
        /// <param name="result">数据</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Success(T result, string info)
        {
            return Message((int)HttpStateCode.成功, true, info, result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Failure(int code, string info)
        {
            return Message(code, false, info, default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Failure(int code, string info, T result)
        {
            return Message(code, false, info, result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        private static ServiceMessageModel<T> Failure(HttpStateCode code, string info)
        {
            return Message((int)code, false, GetErrorMessage(code, info), default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        private static ServiceMessageModel<T> Failure(HttpStateCode code, string info, T result)
        {
            return Message((int)code, false, GetErrorMessage(code, info), result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Failure(SystemErrorCode code, string info)
        {
            return Message((int)HttpStateCode.服务器内部错误, false, GetErrorMessage(code, info), default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Failure(SystemErrorCode code, string info, T result)
        {
            return Message((int)HttpStateCode.服务器内部错误, false, GetErrorMessage(code, info), result);
        }

        /// <summary>
        /// 返回服务消息体
        /// </summary>
        /// <param name="code">状态码（HttpStateCode 状态码 || SystemErrorCode 系统错误码）</param>
        /// <param name="success">失败/成功</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel<T> Message(int code, bool success, string info, T result)
        {
            return new ServiceMessageModel<T>() { code = code, success = success, info = info, result = result };
        }
    }

    /// <summary>
    /// 服务层响应实体
    /// </summary>
    public class ServiceMessageModel
    {
        /// <summary>
        /// http状态码
        /// </summary>
        public int code { get; set; } = (int)HttpStateCode.成功;
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool success { get; set; } = false;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string info { get; set; } = "";
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public object result { get; set; } = null;

        /// <summary>
        /// 系统错误代码
        /// </summary>
        internal SystemErrorCode SystemCode { get; set; } = SystemErrorCode.系统错误;
        /// <summary>
        /// http状态码
        /// </summary>
        internal HttpStateCode HttpCode { get; set; } = HttpStateCode.服务器内部错误;

        /// <summary>
        /// 获取对应错误码
        /// </summary>
        /// <returns></returns>
        public int GetErrorCode() => (int)SystemCode;

        /// <summary>
        /// 返回成功消息体
        /// </summary>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel Success(string info)
        {
            return Message((int)HttpStateCode.成功, true, info, default);
        }
        /// <summary>
        /// 返回成功消息体
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel Success(string info, object result)
        {
            return Message((int)HttpStateCode.成功, true, info, result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(int code, string info)
        {
            return Message(code, false, info, default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(int code, string info, object result)
        {
            return Message(code, false, info, result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(HttpStateCode code, string info)
        {
            return Message((int)code, false, GetErrorMessage(code, info), default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">HttpStateCode 状态码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(HttpStateCode code, string info, object result)
        {
            return Message((int)code, false, GetErrorMessage(code, info), result);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(SystemErrorCode code, string info)
        {
            return Message(code, false, GetErrorMessage(code, info), default);
        }
        /// <summary>
        /// 返回失败消息体
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel Failure(SystemErrorCode code, string info, object result)
        {
            return Message((int)HttpStateCode.服务器内部错误, false, GetErrorMessage(code, info), result);
        }

        /// <summary>
        /// 返回服务消息体
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="success">失败/成功</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        public static ServiceMessageModel Message(int code, bool success, string info, object result)
        {
            return new ServiceMessageModel() { code = code, success = success, info = info, result = result };
        }

        /// <summary>
        /// 返回服务消息体
        /// </summary>
        /// <param name="code">HttpStateCode HTTP状态码</param>
        /// <param name="success">失败/成功</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        internal static ServiceMessageModel Message(HttpStateCode code, bool success, string info, object result)
        {
            return new ServiceMessageModel()
            {
                code = (int)code,
                success = success,
                info = info,
                result = result,
                HttpCode = code,
            };
        }

        /// <summary>
        /// 返回服务消息体
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="success">失败/成功</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        internal static ServiceMessageModel Message(SystemErrorCode code, bool success, string info, object result)
        {
            return new ServiceMessageModel()
            {
                code = (int)HttpStateCode.服务器内部错误,
                success = success,
                info = info,
                result = result,
                SystemCode = code,
            };
        }

        /// <summary>
        /// 获取SystemErrorCode 错误描述
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static string GetErrorMessage(SystemErrorCode code, string info)
        {
            if (!string.IsNullOrWhiteSpace(info)) return info;
            return code.GetEnumDesc();
        }

        /// <summary>
        /// 获取HttpStateCode 错误描述
        /// </summary>
        /// <param name="code">HttpStateCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        public static string GetErrorMessage(HttpStateCode code, string info)
        {
            if (!string.IsNullOrWhiteSpace(info)) return info;
            return code.GetEnumDesc();
        }
    }
}
