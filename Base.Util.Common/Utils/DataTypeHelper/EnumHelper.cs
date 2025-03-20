using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Base.Util.Common.Utils.DataTypeHelper
{
    /// <summary>
    /// 枚举 辅助类#
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 判断value是否被枚举定义
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefinedEnum(this Type type, object value)
        {
            return Enum.IsDefined(type, value);
        }

        /// <summary>
        /// 判断value是否被枚举定义(描述属性)
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefinedEnumDesc(this Type type, string value)
        {
            return type.GetEnumDictionaryAttr().Any(aa => aa.Value == value.ToString());
        }

        /// <summary>
        /// 获取对应的枚举的枚举长度个数
        /// </summary>
        /// <returns></returns>
        public static int GetLength<TEnum>()
            where TEnum : struct, Enum
        {
            Array array = typeof(TEnum).GetEnumArray();
            return array.Length;
        }

        /// <summary>
        /// 获取对应的枚举的枚举长度个数
        /// </summary>
        /// <returns></returns>
        public static int GetLength(this Type type)
        {
            Array array = type.GetEnumArray();
            return array.Length;
        }

        /// <summary>
        /// 获取对应的枚举的最大值枚举
        /// </summary>
        /// <returns></returns>
        public static TEnum GetMax<TEnum>()
            where TEnum : struct, Enum
        {
            Array array = typeof(TEnum).GetEnumArray();
            return (TEnum)array.GetValue(array.GetUpperBound(0));
        }

        /// <summary>
        /// 获取对应的枚举的最小值枚举
        /// </summary>
        /// <returns></returns>
        public static TEnum GetMin<TEnum>()
            where TEnum : struct, Enum
        {
            Array array = typeof(TEnum).GetEnumArray();
            return (TEnum)array.GetValue(array.GetLowerBound(0));
        }

        /// <summary>
        /// 获取对应的枚举
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetEnum(this Type type, string value)
        {
            if (!type.IsEnum) return null;
            if (Enum.IsDefined(type, value))
            {
                return Enum.Parse(type, value);
            }
            return null;
        }

        /// <summary>
        /// 获取对应的枚举
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetEnum<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value))
            {
                return Enum.Parse(typeof(TEnum), value.ToString());
            }
            return null;
        }

        /// <summary>
        /// 获取枚举对应的int值
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? GetEnumInt(this Type type, string value)
        {
            if (!type.IsEnum) return null;
            object obj = type.GetEnum(value);
            if (obj == null) return null;
            return (int)obj;
        }

        /// <summary>
        /// 获取枚举对应的int值
        /// </summary>
        /// <param name="@enum">typeof(enum)类型</param>
        /// <returns></returns>
        public static int? GetEnumInt<TEnum>(this TEnum @enum)
            where TEnum : struct, Enum
        {
            object obj = @enum.GetEnum();
            if (obj == null) return null;
            return (int)obj;
        }

        /// <summary>
        /// 获取枚举对应的string值
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetEnumValue(this Type type, int value)
        {
            if (!type.IsEnum) return string.Empty;
            return Enum.GetName(type, value);
        }

        /// <summary>
        /// 获取枚举对应的string值
        /// </summary>
        /// <param name="@enum">typeof(enum)类型</param>
        /// <returns></returns>
        public static string? GetEnumValue<TEnum>(this TEnum @enum)
            where TEnum : struct, Enum
        {
            return Enum.GetName(typeof(TEnum), @enum);
        }

        /// <summary>
        /// 获取枚举对应的属性值
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetEnumDesc(this Type type, string value)
        {
            if (!type.IsEnum) return string.Empty;
            // 获取枚举字段。
            FieldInfo fieldInfo = type.GetField(value);
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return type.GetEnum(value).ToString();
        }

        /// <summary>
        /// 获取枚举对应的属性值
        /// </summary>
        /// <param name="type">typeof(enum)类型</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetEnumDesc(this Type type, int value)
        {
            if (!type.IsEnum) return string.Empty;
            // 获取枚举字段。
            FieldInfo fieldInfo = type.GetField(type.GetEnumValue(value));
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return type.GetEnum(type.GetEnumValue(value)).ToString();
        }

        /// <summary>
        /// 获取枚举对应的属性值
        /// </summary>
        /// <param name="@enum">typeof(enum)类型</param>
        /// <returns></returns>
        public static string? GetEnumDesc<TEnum>(this TEnum @enum)
            where TEnum : struct, Enum
        {
            // 获取枚举字段。
            FieldInfo fieldInfo = typeof(TEnum).GetField(@enum.ToString());
            if (fieldInfo != null)
            {
                // 获取描述的属性。
                DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
            return typeof(TEnum).GetEnum(@enum.ToString()).ToString();
        }

        /// <summary>
        /// 获取枚举数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Array GetEnumArray(this Type type)
        {
            if (!type.IsEnum) return null;
            return Enum.GetValues(type);
        }

        /// <summary>
        /// 获取枚举数组
        /// </summary>
        /// <returns></returns>
        public static TEnum[] GetEnumArray<TEnum>()
             where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum)) as TEnum[];
        }

        /// <summary>
        /// 枚举转字典(无需获取描述时使用)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumDictionary(this Type type)
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            if (type.IsEnum)
            {
                foreach (object item in type.GetEnumArray())
                {
                    dic.Add((int)item, item.ToString());
                }
            }
            return dic;
        }

        /// <summary>
        /// 枚举转字典(无需获取描述时使用)
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumDictionary<TEnum>()
             where TEnum : struct, Enum
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            foreach (object item in GetEnumArray<TEnum>())
            {
                dic.Add((int)item, item.ToString());
            }
            return dic;
        }

        /// <summary>
        /// 枚举转字典(需获取描述时使用)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumDictionaryAttr(this Type type)
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            if (type.IsEnum)
            {
                foreach (object item in type.GetEnumArray())
                {
                    dic.Add((int)item, type.GetEnumDesc(item.ToString()));
                }
            }
            return dic;
        }

        /// <summary>
        /// 枚举转字典(无需获取描述时使用)
        /// </summary>
        /// <returns></returns>
        public static IDictionary<int, string> GetEnumDictionaryAttr<TEnum>()
             where TEnum : struct, Enum
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            foreach (object item in GetEnumArray<TEnum>())
            {
                dic.Add((int)item, typeof(TEnum).GetEnumDesc(item.ToString()));
            }
            return dic;
        }
    }
}
