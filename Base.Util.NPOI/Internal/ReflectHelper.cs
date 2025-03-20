using System;

namespace Base.Util.NPOI.Internal
{
    /// <summary>
    /// 内部帮助类#
    /// </summary>
    internal class InternalHelper
    {
        /// <summary>
        /// 验证可空类型，获取基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type VerifyNullableType(Type type)
        {
            //泛型
            if (type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                //Nullable<>
                if (genericTypeDefinition == typeof(Nullable<>)) return Nullable.GetUnderlyingType(type);
            }
            //非泛型
            return type;
        }
    }
}
