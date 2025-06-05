using System;
using System.Collections.Generic;

namespace Base.Util.Redis
{
    /// <summary>
    ///  Redis扩展类
    /// </summary>
    public static class RedisExtension
    {
        /// <summary>
        /// 插入系统错误日志
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="item"></param>
        public static void InsertSystemErrorLog(this RedisManager redis, object item)
        {
            try
            {
                redis.InsertLog(item, RedisConst.SystemErrorRedisKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 插入日志
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="item"></param>
        /// <param name="key"></param>
        public static void InsertLog(this RedisManager redis, object item, string key = "")
        {
            key = $@"{key}{DateTime.Now.ToString("yyyy:MM:dd")}";
            try
            {
                redis.ListLeftPush(key, item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="redis"></param>
        /// <param name="date"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetLogList<T>(this RedisManager redis, DateTime? date, string key = "")
        {
            if (!date.HasValue) date = DateTime.Now;
            try
            {
                key = $@"{key}{date.Value.ToString("yyyy:MM:dd")}";
                var list = redis.ListRange<T>(key);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
