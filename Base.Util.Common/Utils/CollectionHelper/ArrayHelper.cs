using System;
using System.Collections.Generic;

namespace Base.Util.Common.Utils.CollectionHelper
{
    /// <summary>
    /// 数组 辅助类#
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// 查找满足条件的单个元素
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组</param>
        /// <param name="condition">查找条件</param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                //满足条件【调用者指定相应的条件】
                //if(array[i]==5)
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            //泛型的默认值
            return default;
        }

        /// <summary>
        /// 查找满足条件的元素集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            //集合存储满足条件的元素
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                //查找的条件
                //if (array[i]>79)
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            //ToArray将集合转换成数组
            return list.ToArray();
        }

        /// <summary>
        /// 最大值
        /// </summary>
        /// <typeparam name="T">代表的数组的类型 </typeparam>
        /// <typeparam name="Q">比较条件的返回值类型 </typeparam>
        /// <param name="array">要比较的数组</param>
        /// <param name="condition">要比较的方法</param>
        /// <returns></returns>
        public static T GetMax<T, Q>(this T[] array, Func<T, Q> condition)
            where Q : IComparable
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                //比较的条件
                //if (max < array[i])
                if (condition(max).CompareTo(condition(array[i])) < 0)
                {
                    max = array[i];
                }
            }
            return max;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        /// <typeparam name="T">代表的数组的类型 </typeparam>
        /// <typeparam name="Q">比较条件的返回值类型 </typeparam>
        /// <param name="array">要比较的数组</param>
        /// <param name="condition">要比较的方法</param>
        /// <returns></returns>
        public static T GetMin<T, Q>(this T[] array, Func<T, Q> condition)
            where Q : IComparable
        {
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                //比较的条件
                //if (min > array[i])
                if (condition(min).CompareTo(condition(array[i])) > 0)
                {
                    min = array[i];
                }
            }
            return min;
        }

    }
}
