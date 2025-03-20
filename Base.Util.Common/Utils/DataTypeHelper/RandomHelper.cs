using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Util.Common.Utils.DataTypeHelper
{
    /// <summary>
    /// 随机数 辅助类#
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        public static RandomHelper Instance => new RandomHelper();

        /// <summary>
        /// 设置最小值
        /// </summary>
        private int Minimum { set; get; } = 0;
        /// <summary>
        /// 设置最大值
        /// </summary>
        private int Maximal { set; get; } = 1000;
        /// <summary>
        /// 设置随机长度
        /// </summary>
        private int RandomLength { set; get; } = 5;

        private string RandomString { set; get; } = "0123456789ABCDEFGHIJKMLNOPQRSTUVWXYZ";

        private readonly Random Random = new Random(DateTime.Now.Second);

        private DateTime StartDate { set; get; } = new DateTime(1970, 1, 1);
        private DateTime EndDate { set; get; } = DateTime.Now;

        public RandomHelper()
        {
        }

        /// <summary>
        /// 指定随机字符串 
        /// </summary>
        /// <param name="str"></param>
        public RandomHelper(string str)
        {
            RandomString = str;
        }

        /// <summary>
        /// 指定随机最小值和最大值
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RandomHelper(int min, int max)
        {
            Minimum = min;
            Maximal = max;
        }

        /// <summary>
        /// 指定随机起始时间和结束时间
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public RandomHelper(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        #region 字符串

        /// <summary>
        /// 产生随机字符
        /// </summary>
        /// <returns>字符串</returns>
        public string GetRandomString()
        {
            return GetRandomString(RandomLength);
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="stringLength">随机字符串长度</param>
        /// <returns>返回随机字符串</returns>
        public string GetRandomString(int stringLength)
        {
            string returnValue = string.Empty;
            for (int i = 0; i < stringLength; i++)
            {
                int r = Random.Next(0, RandomString.Length);
                returnValue += RandomString[r];
            }
            return returnValue;
        }

        #endregion

        #region 整型

        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <returns>随机数</returns>
        public int GetRandomInt()
        {
            return Random.Next(Minimum, Maximal);
        }

        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="minNumber">最小值</param>
        /// <param name="maxNumber">最大值</param>
        /// <returns>随机数</returns>
        public int GetRandomInt(int minNumber, int maxNumber)
        {
            return Random.Next(minNumber, maxNumber);
        }

        #endregion

        #region 浮点

        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="s">小数保留位数</param>
        /// <returns>随机数</returns>
        public double GetRandomDouble(int s = 5)
        {
            return GetRandomDouble(Minimum, Maximal, s);
        }

        /// <summary>
        /// 产生随机数
        /// </summary>
        /// <param name="minNumber">最小值</param>
        /// <param name="maxNumber">最大值</param>
        /// <param name="s">小数保留位数</param>
        /// <returns>随机数</returns>
        public double GetRandomDouble(int minNumber, int maxNumber, int s = 5)
        {
            if (maxNumber - 1 < minNumber) maxNumber = minNumber + 1;
            return Random.Next(minNumber, maxNumber - 1) + Random.NextDouble().ToFixed(s);
        }

        #endregion

        #region 时间

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <returns>间隔日期之间的 随机日期</returns>
        public DateTime GetRandomTime()
        {
            return GetRandomTime(StartDate, EndDate);
        }

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            TimeSpan ts = new TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > int.MaxValue)
            {
                iTotalSecontds = int.MaxValue;
            }
            else if (dTotalSecontds < int.MinValue)
            {
                iTotalSecontds = int.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }

            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= int.MinValue)
                maxValue = int.MinValue + 1;

            int i = Random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        #endregion

        #region 数组

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="list">需要随机排序的列表</param>
        public void GetRandomArray<T>(ref List<T> list)
        {
            T[] arr = list.ToArray();
            GetRandomArray(arr);
            list = arr.ToList();
        }

        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public void GetRandomArray<T>(T[] arr)
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换

            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;

            //开始交换
            for (int i = 0; i < count; i++)
            {
                //生成两个随机数位置
                int targetIndex1 = GetRandomInt(0, arr.Length);
                int targetIndex2 = GetRandomInt(0, arr.Length);

                //定义临时变量
                T temp;

                //交换两个随机数位置的值
                temp = arr[targetIndex1];
                arr[targetIndex1] = arr[targetIndex2];
                arr[targetIndex2] = temp;
            }
        }

        /// <summary>
        /// 获取随机基准值
        /// </summary>
        /// <returns></returns>
        internal int GetRandomSeed()
        {
            return DateTime.Now.Second;
        }

        #endregion

    }
}
