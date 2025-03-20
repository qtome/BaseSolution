using System;

namespace Base.Util.Common.Utils.DataTypeHelper
{
    /// <summary>
    /// 日期 辅助类#
    /// </summary>
    public static partial class DateTimeHelper
    {
        #region 时间戳转换

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string timeStamp)
        {
            if (string.IsNullOrWhiteSpace(timeStamp)) throw new ArgumentNullException(nameof(timeStamp));
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp.PadEnd(17, "0"));//补足到17位
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            return timeStamp.ToString().ToDateTime();
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式(秒)
        /// </summary>
        /// <param name="time"></param>
        /// <returns>单位 秒</returns>
        public static long ToUnixTime(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式（毫秒）
        /// </summary>
        /// <param name="time"></param>
        /// <returns>单位 秒</returns>
        public static long ToUnixTimeMilliSecond(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }

        #endregion


        #region 时间计算类

        /// <summary>
        /// 获取1小时前时间（以5分钟为一结果）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetOneHourBefore()
        {
            DateTime now = DateTime.Now;
            int mins = 0;
            while (mins + 5 <= now.Minute)
            {
                mins += 5;
            }
            DateTime time = now.AddHours(-1);
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, mins, 0);
        }

        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(this int Second)
        {
            decimal mm = Second / (decimal)60;
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="d">时间</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(this DateTime d)
        {
            return DateTime.DaysInMonth(d.Year, d.Month);
        }

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }

        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="d">时间</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(this DateTime? d)
        {
            if (!d.HasValue) return 0;
            return DateTime.DaysInMonth(d.Value.Year, d.Value.Month);
        }


        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            {
            }
            return dateDiff;
        }

        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }

        /// <summary>
        /// 获取两个时间差：返回 年
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static double GetYearsOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return 0;
            int months = GetMonthsOfTwoDate(startDate, endDate);
            double totalyears = months / 12d;
            return double.Parse(string.Format("{0:N2}", totalyears));
        }

        /// <summary>
        /// 获取两个时间差：返回 月
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetMonthsOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return 0;
            int months = (endDate.Value.Year - startDate.Value.Year) * 12 + (endDate.Value.Month - startDate.Value.Month);
            //如果天数还没到，月数要减1
            if (endDate.Value.Day < startDate.Value.Day)
            {
                months--;
            }
            return months;
        }

        /// <summary>
        /// 获取两个时间差：返回 天
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string GetDaysOfTwoDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null) return string.Empty;
            TimeSpan TimSpan = endDate.Value.Subtract(startDate.Value);
            return TimSpan.Days.ToString();
        }

        #endregion


        #region 日期操作类

        /// <summary>
        /// 返回指定格式的日期
        /// </summary>
        /// <param name="dt">年月日分隔符</param>
        /// <param name="dateTimeFormat">yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime dt, string dateTimeFormat = "0")
        {
            return dt.ToString(DateFormat(dateTimeFormat));
        }

        /// <summary>
        /// 返回指定格式的日期
        /// </summary>
        /// <param name="dt">年月日分隔符</param>
        /// <param name="dateTimeFormat">yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string GetFormatDate(this DateTime? dt, string dateTimeFormat = "0")
        {
            if (!dt.HasValue) return null;
            return dt.Value.ToString(DateFormat(dateTimeFormat));
        }

        /// <summary>
        /// 获取日期格式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string DateFormat(string type)
        {
            string format = type switch
            {
                "0" => "yyyy-MM-dd",
                "1" => "yyyy-MM-dd HH:mm:ss",
                "2" => "yyyy/MM/dd",
                "3" => "yyyy年MM月dd日",
                "4" => "MM-dd",
                "5" => "MM/dd",
                "6" => "MM月dd日",
                _ => type,
            };
            return format;
        }

        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
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

            int i = random.Next(Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }

        /// <summary>
        /// 将对象转换成可空日期
        /// </summary>
        /// <param name="dateObj"></param>
        /// <returns></returns>
        public static DateTime? ParseToDateValue(this object dateObj)
        {
            if (dateObj == null) return null;
            DateTime dtValue;
            if (!DateTime.TryParse(dateObj.ToString(), out dtValue)) return null;
            return dtValue;
        }

        /// <summary>
        /// 获取日期对应的季度
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetQuarter(this DateTime input)
        {
            if (input.Month % 3 > 0)
                return input.Month / 3 + 1;
            else
                return input.Month / 3;
        }

        /// <summary>
        /// 获取日期对应的季度
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetQuarter(this DateTime? input)
        {
            if (!input.HasValue) return 0;
            if (input.Value.Month % 3 > 0)
                return input.Value.Month / 3 + 1;
            else
                return input.Value.Month / 3;
        }

        /// <summary>
        /// 获取对应日期第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weekday"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static DateTime GetWeekUpOfDate(this DateTime dt, DayOfWeek weekday, int Number)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
        }

        /// <summary>
        /// 获取对应日期第几周
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weekday"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static DateTime? GetWeekUpOfDate(this DateTime? dt, DayOfWeek weekday, int Number)
        {
            if (!dt.HasValue) return dt;
            int wd1 = (int)weekday;
            int wd2 = (int)dt.Value.DayOfWeek;
            return wd2 == wd1 ? dt.Value.AddDays(7 * Number) : dt.Value.AddDays(7 * Number - wd2 + wd1);
        }

        /// <summary>
        /// 获取日期是当月的第几周
        /// </summary>
        /// <param name="day"></param>
        /// <param name="WeekStart">1表示 周一至周日 为一周 2表示 周日至周六 为一周</param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime day, int WeekStart)
        {
            //WeekStart
            //1表示 周一至周日 为一周
            //2表示 周日至周六 为一周

            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);

            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }

            if (WeekStart == 1)
            {
                return (day.Date.Day + i - 2) / 7 + 1;
            }
            if (WeekStart == 2)
            {
                return (day.Date.Day + i - 1) / 7;

            }
            return 0;
            //错误返回值0
        }

        /// <summary>
        /// 获取日期是当月的第几周
        /// </summary>
        /// <param name="day"></param>
        /// <param name="WeekStart">1表示 周一至周日 为一周 2表示 周日至周六 为一周</param>
        /// <returns></returns>
        public static int WeekOfMonth(this DateTime? day, int WeekStart)
        {
            if (!day.HasValue) return 0;

            //WeekStart
            //1表示 周一至周日 为一周
            //2表示 周日至周六 为一周

            DateTime FirstofMonth;
            FirstofMonth = Convert.ToDateTime(day.Value.Date.Year + "-" + day.Value.Date.Month + "-" + 1);

            int i = (int)FirstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }

            if (WeekStart == 1)
            {
                return (day.Value.Date.Day + i - 2) / 7 + 1;
            }
            if (WeekStart == 2)
            {
                return (day.Value.Date.Day + i - 1) / 7;

            }
            return 0;
            //错误返回值0
        }

        /// <summary>
        /// 获取以0点0分0秒开始的日期
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime GetStartDateTime(this DateTime d)
        {
            return d.Date;
        }

        /// <summary>
        /// 获取以0点0分0秒开始的日期
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime? GetStartDateTime(this DateTime? d)
        {
            if (!d.HasValue) return d;
            return d.Value.Date;
        }

        /// <summary>
        /// 获取以23点59分59秒结束的日期
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this DateTime d)
        {
            return d.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 获取以23点59分59秒结束的日期
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime? GetEndDateTime(this DateTime? d)
        {
            if (!d.HasValue) return d;
            return d.Value.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// 根据不同周期计算出起始日期和结束日期
        /// </summary>
        /// <param name="judgeFrequency"></param>
        /// <returns>起始时间，结束时间</returns>
        public static Tuple<DateTime?, DateTime?> GetDateRange(this DateTime date, DateFrequency judgeFrequency)
        {
            try
            {
                (DateTime? startDate, DateTime? endDate) = CalculateDateRange(date, judgeFrequency);
                return Tuple.Create(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据不同周期计算出起始日期和结束日期
        /// </summary>
        /// <param name="judgeFrequency"></param>
        /// <returns>起始时间，结束时间</returns>
        public static Tuple<DateTime?, DateTime?> GetDateRange(this DateTime? date, DateFrequency judgeFrequency)
        {
            try
            {
                (DateTime? startDate, DateTime? endDate) = date.CalculateDateRange(judgeFrequency);
                return Tuple.Create(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据不同周期计算出起始日期和结束日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="season"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static (DateTime?, DateTime?) GetDateRange(int? year, int? season, int? month)
        {
            if (!year.HasValue) return (null, null);
            try
            {
                DateFrequency judgeFrequency = DateFrequency.年;
                int inMonth = 1;
                if (season.HasValue)
                {
                    judgeFrequency = DateFrequency.季;
                    inMonth = season.Value * 3;
                }
                if (month.HasValue)
                {
                    judgeFrequency = DateFrequency.月;
                    inMonth = month.Value;
                }
                DateTime date = new DateTime(year.Value, inMonth, 1);
                (DateTime? startDate, DateTime? endDate) = CalculateDateRange(date, judgeFrequency);
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 计算出起始日期和结束日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static (DateTime?, DateTime?) GetDateRange(
            DateTime? startDate, DateTime? endDate
            , int? year, int? month)
        {
            try
            {
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    (startDate, endDate) = GetDateRange(year, null, month);
                }
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据年月计算出起始日期和结束日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static (DateTime?, DateTime?) GetDateRangeArray(
            int? year
            , int? month)
        {
            if (!year.HasValue) return (null, null);
            try
            {
                DateTime date = new DateTime(year.Value, 1, 1);
                DateFrequency judgeFrequency = DateFrequency.年;
                if (month.HasValue)
                {
                    date = new DateTime(year.Value, month.Value, 1);
                    judgeFrequency = DateFrequency.月;
                }
                (DateTime? startDate, DateTime? endDate) = CalculateDateRange(date, judgeFrequency);
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 计算出起始日期和结束日期
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static (DateTime?, DateTime?) GetDateRangeArray(
            DateTime? startDate, DateTime? endDate
            , int? year, int? month)
        {

            try
            {
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    (startDate, endDate) = GetDateRangeArray(year, month);
                }
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据不同周期计算出起始日期和结束日期
        /// </summary>
        /// <param name="judgeFrequency"></param>
        /// <returns>起始时间，结束时间</returns>
        public static (DateTime?, DateTime?) GetDateRangeArray(this DateTime date, DateFrequency judgeFrequency)
        {
            try
            {
                (DateTime? startDate, DateTime? endDate) = CalculateDateRange(date, judgeFrequency);
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据不同周期计算出起始日期和结束日期
        /// </summary>
        /// <param name="judgeFrequency"></param>
        /// <returns>起始时间，结束时间</returns>
        public static (DateTime?, DateTime?) GetDateRangeArray(this DateTime? date, DateFrequency judgeFrequency)
        {
            try
            {
                (DateTime? startDate, DateTime? endDate) = date.CalculateDateRange(judgeFrequency);
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 周期计算时间范围
        /// </summary>
        /// <param name="date"></param>
        /// <param name="judgeFrequency"></param>
        /// <returns></returns>
        private static (DateTime?, DateTime?) CalculateDateRange(this DateTime? date, DateFrequency judgeFrequency)
        {
            try
            {
                DateTime? startDate = null, endDate = null;
                if (!date.HasValue) return (startDate, endDate);

                switch (judgeFrequency)
                {
                    case DateFrequency.日:
                        //本日开始时间  
                        startDate = date.Value.Date;
                        //本日结束时间    
                        endDate = startDate.Value.AddDays(1).AddSeconds(-1);
                        break;
                    case DateFrequency.周:
                        //本周第一天时间  
                        startDate = date.Value.AddDays((int)DayOfWeek.Monday - (int)date.Value.DayOfWeek).Date;
                        //本周最后一天时间    
                        endDate = startDate.Value.AddDays(7).AddSeconds(-1);
                        break;
                    case DateFrequency.月:
                        //本月第一天时间  
                        startDate = date.Value.AddDays(1 - date.Value.Day).Date;
                        //本月最后一天时间    
                        endDate = startDate.Value.AddMonths(1).AddSeconds(-1);
                        break;
                    case DateFrequency.季:
                        //本季第一天时间    
                        startDate = date.Value.AddMonths(0 - (date.Value.Month - 1) % 3).AddDays(1 - date.Value.Day).Date;
                        //本季最后一天时间    
                        endDate = startDate.Value.AddMonths(3).AddSeconds(-1);
                        break;
                    case DateFrequency.半年:
                        //半年第一天时间    
                        startDate = date.Value.AddMonths(0 - (date.Value.Month - 1) % 6).AddDays(1 - date.Value.Day).Date;
                        //半年最后一天时间    
                        endDate = startDate.Value.AddMonths(6).AddSeconds(-1);
                        break;
                    case DateFrequency.年:
                        //本年第一天时间    
                        startDate = new DateTime(date.Value.Year, 1, 1);
                        //本年最后一天时间    
                        endDate = startDate.Value.AddYears(1).AddSeconds(-1);
                        break;
                    default:
                        break;
                }
                return (startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    /// <summary>
    /// 时间周期枚举
    /// </summary>
    public enum DateFrequency
    {
        日 = 0,
        周,
        月,
        季,
        半年,
        年,
    }
}
