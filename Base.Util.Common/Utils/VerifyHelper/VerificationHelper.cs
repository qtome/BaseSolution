using System;
using System.Text.RegularExpressions;

namespace Base.Util.Common.Utils.VerifyHelper
{
    /// <summary>
    /// 正则验证库 辅助类
    /// </summary>
    public static class VerificationHelper
    {
        /// <summary>
        /// 验证 输入 字符串是否为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        #region 验证输入字符串是否与模式字符串匹配

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            input = input.Trim(); // 去除字符串空格
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// 获取验证信息
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        public static Match GetMatch(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            return match;
        }

        #endregion

        #region 验证IP地址是否合法

        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>
        public static bool IsIP(string ip)
        {
            //模式字符串
            //string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            string pattern = @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$";

            //验证
            return IsMatch(ip, pattern);
        }

        public static bool HasIP(string source)
        {
            //模式字符串
            string pattern = @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region  验证EMail是否合法

        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            //模式字符串
            //string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            string pattern = @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$";

            //验证
            return IsMatch(email, pattern);
        }

        public static bool HasEmail(string source)
        {
            //模式字符串
            string pattern = @"[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 验证数字

        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>        
        public static bool IsInt(string number)
        {
            //模式字符串
            string pattern = @"^[0-9]+[0-9]*$";

            //验证
            return IsMatch(number, pattern);
        }

        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsNumber(string number)
        {
            //模式字符串
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //验证
            return IsMatch(number, pattern);
        }

        /// <summary>
        /// 验证是否是小数
        /// </summary>
        /// <param name="strIn"></param>
        /// <returns></returns>
        public static bool IsValidDecimal(string strIn)
        {
            //模式字符串
            string pattern = @"[0].d{1,2}|[1]";

            //验证
            return IsMatch(strIn, pattern);
        }

        /// <summary>
        /// 是否为数字型
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsDecimal(string strNumber)
        {
            //模式字符串
            string pattern = @"^([0-9])[0-9]*(\.\w*)?$";

            //验证
            return IsMatch(strNumber, pattern);
        }

        #endregion

        #region 验证日期是否合法

        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            //模式字符串
            string pattern = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            StrSource = StrSource.Replace("/", "-");

            //验证
            return IsMatch(StrSource, pattern);
        }

        /// <summary>
        /// 是否为时间型字符串
        /// </summary>
        /// <param name="StrSource">时间字符串(15:00:00)</param>
        /// <returns></returns>
        public static bool IsTime(string StrSource)
        {
            //模式字符串
            string pattern = @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";

            //验证
            return IsMatch(StrSource, pattern);
        }

        /// <summary>
        /// 是否为日期+时间型字符串
        /// </summary>
        /// <param name="StrSource"></param>
        /// <returns></returns>
        public static bool IsDateTime(string StrSource)
        {
            //模式字符串
            string pattern = @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";
            StrSource = StrSource.Replace("/", "-");

            //验证
            return IsMatch(StrSource, pattern);
        }

        #endregion

        #region 验证身份证是否合法

        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id)) return false;
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }
        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
            string[] Wi = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 获取正则验证结果（包含身份证）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Match HasIDCardMatch(string source)
        {
            //模式字符串
            string pattern = @"^\d{15}|\d{18}|\d{17}X$";
            //验证
            return GetMatch(source, pattern);
        }

        #endregion

        #region 验证网址

        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            //模式字符串
            string pattern = @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$";

            //验证
            return IsMatch(source, pattern);
        }

        public static bool HasUrl(string source)
        {
            //模式字符串
            string pattern = @"(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 验证手机号

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobile(string source)
        {
            //模式字符串
            string pattern = @"^1[3456789]\d{9}$";

            //验证
            return IsMatch(source, pattern);
        }

        public static bool HasMobile(string source)
        {
            //模式字符串
            string pattern = @"1[3456789]\d{9}";

            //验证
            return IsMatch(source, pattern);
        }

        /// <summary>
        /// 获取正则验证结果（包含手机号）
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Match HasMobileMatch(string source)
        {
            //模式字符串
            string pattern = @"1[3456789]\d{9}";
            //验证
            return GetMatch(source, pattern);
        }

        #endregion

        #region 是不是中国电话，格式010-85849685

        /// <summary>
        /// 是不是中国电话，格式010-85849685
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsTel(string source)
        {
            //模式字符串
            string pattern = @"^\d{3,4}-?\d{6,8}$";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 看字符串的长度是不是在限定数之间 一个中文为两个字符

        /// <summary>
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="begin">大于等于</param>
        /// <param name="end">小于等于</param>
        /// <returns></returns>
        public static bool IsLengthStr(string source, int begin, int end)
        {
            int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if (length <= begin && length >= end)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 邮政编码 6个数字

        /// <summary>
        /// 邮政编码 6个数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPostCode(string source)
        {
            //模式字符串
            string pattern = @"^\d{6}$";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 中文

        /// <summary>
        /// 中文
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsChinese(string source)
        {
            //模式字符串
            string pattern = @"^[\u4e00-\u9fa5]+$";

            //验证
            return IsMatch(source, pattern);
        }

        public static bool HasChinese(string source)
        {
            //模式字符串
            string pattern = @"[\u4e00-\u9fa5]+";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 验证是不是正常字符 字母，数字，下划线的组合

        /// <summary>
        /// 验证是不是正常字符 字母，数字，下划线的组合
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNormalChar(string source)
        {
            //模式字符串
            string pattern = @"[\w\d_]+";

            //验证
            return IsMatch(source, pattern);
        }

        #endregion

        #region 验证用户名：必须以字母开头，可以包含字母、数字、“_”、“.”，至少5个字符

        /// <summary>
        /// 验证用户名：必须以字母开头，可以包含字母、数字、“_”、“.”，至少5个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckUser(string str)
        {
            //模式字符串
            string pattern = "[a-zA-Z]{1}([a-zA-Z0-9]|[._]){4,19}";

            //验证
            return IsMatch(str, pattern);
        }

        #endregion

        #region 验证后缀名

        public static bool IsValidPostfix(string strIn)
        {
            //模式字符串
            string pattern = @".(?i:gif|jpg)$";

            //验证
            return IsMatch(strIn, pattern);
        }

        #endregion

        #region 验证字符是否在4至12之间

        public static bool IsValidByte(string strIn)
        {
            //模式字符串
            string pattern = @"^[a-z]{4,12}$";

            //验证
            return IsMatch(strIn, pattern);
        }

        #endregion
    }
}
