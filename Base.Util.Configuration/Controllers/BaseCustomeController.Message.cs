using Base.Util.Common.Models.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Util.Configuration.Controllers
{
    /// <summary>
    /// 基类控制器 消息
    /// </summary>
    public partial class BaseCustomeController
    {
        /// <summary>
        /// 处理服务异常
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="ex">服务异常</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> ServiceExceptionHandle<T>(ServiceException ex)
        {
            throw ex;
        }

        /// <summary>
        /// 隐藏服务异常
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="ex">服务异常</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> ServiceExceptionHide<T>(ServiceException ex)
        {
            return Failed<T>(ex);
        }

        /// <summary>
        /// 处理服务异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>
        [NonAction]
        protected ServiceMessageModel<T> ServiceExceptionHandle<T>(SystemErrorCode code, string info)
        {
            throw new ServiceException(Failed<T>(code, info));
        }

        /// <summary>
        /// 隐藏服务异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref="ServiceException"></exception>
        [NonAction]
        protected ServiceMessageModel<T> ServiceExceptionHide<T>(SystemErrorCode code, string info)
        {
            return Failed<T>(code, info);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Success<T>(T data, string info = "成功")
        {
            return ServiceMessageModel<T>.Success(data, info);
        }
        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Success<T>(string info = "成功")
        {
            return ServiceMessageModel<T>.Success(info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="code">错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Failed<T>(int code, string info)
        {
            return ServiceMessageModel<T>.Failure(code, info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="code">错误码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Failed<T>(int code, string info, T result)
        {
            return ServiceMessageModel<T>.Failure(code, info, result);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Failed<T>(SystemErrorCode code, string info)
        {
            return ServiceMessageModel<T>.Failure(code, info);
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <param name="result">数据</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Failed<T>(SystemErrorCode code, string info, T result)
        {
            return ServiceMessageModel<T>.Failure(code, info, result);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="exception">抛出的异常</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel<T> Failed<T>(ServiceException exception)
        {
            return ServiceMessageModel<T>.Failure(exception.ServiceMessageModel.code, exception.ServiceMessageModel.info);
        }

        /// <summary>
        /// 结果
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel Message(SystemErrorCode code, string info)
        {
            return ServiceMessageModel.Failure(code, info);
        }

        /// <summary>
        /// 结果
        /// </summary>
        /// <param name="code">SystemErrorCode 系统错误码</param>
        /// <param name="info">信息</param>
        /// <returns></returns>
        [NonAction]
        protected ServiceMessageModel Message(HttpStateCode code, string info)
        {
            return ServiceMessageModel.Failure(code, info);
        }
    }
}
