using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Base.Util.Common.Utils.Serialize
{
    /// <summary>
    /// Json 辅助类#
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 序列化 默认参数
        /// </summary>
        private static JsonSerializerSettings settingsSerialize = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
        };

        /// <summary>
        /// 反序列化 默认参数
        /// </summary>
        private static JsonSerializerSettings settingsDeserialize = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            Formatting = Formatting.Indented
        };

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value, settingsSerialize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 序列化(默认配置)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObjectDefault(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonStr, settingsDeserialize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 反序列化(默认配置)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T DeserializeObjectDefault<T>(string jsonStr)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 序列化object 转换为指定类
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ObjectToClass<T>(object value)
        {
            try
            {
                return DeserializeObject<T>(SerializeObject(value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region 解析JSON字符串生成对象实体

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        #endregion

        #region 解析JSON数组生成对象实体集合

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        #endregion

        #region 反序列化JSON到给定的匿名对象

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        #endregion
    }
}
