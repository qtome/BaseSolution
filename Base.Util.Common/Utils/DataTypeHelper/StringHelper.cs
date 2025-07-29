using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Base.Util.Common.Utils.DataTypeHelper
{
    /// <summary>
    /// string 辅助类
    /// </summary>
    public static class StringHelper
    {
        #region 属性操作

        /// <summary>
        /// 获取类的排序用字段
        /// </summary>
        /// <param name="propertyName">需排序字段</param>
        /// <param name="defaultPropertyName">默认排序字段</param>
        /// <returns></returns>
        public static string GetSortPropertyName<TEntity>(string propertyName, string defaultPropertyName)
            where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                var memberProp = typeof(TEntity).GetProperties().FirstOrDefault(ff => ff.Name.ToLower().Equals(propertyName.ToLower()));
                if (memberProp != null)
                {
                    return memberProp.Name;
                }
            }
            return defaultPropertyName;
        }

        #endregion

        #region 字符串操作

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string str, char separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string str, string? separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string str, char separator, params int[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string str, string? separator, params int[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strArr"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string[] strArr, char separator, params string[] newStrs)
        {
            List<string> strs = strArr.ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strArr"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string[] strArr, string? separator, params string[] newStrs)
        {
            List<string> strs = strArr.ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strArr"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string[] strArr, char separator, params int[] newStrs)
        {
            List<string> strs = strArr.ToList();
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strArr"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this string[] strArr, string? separator, params int[] newStrs)
        {
            List<string> strs = strArr.ToList();
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this List<string> strs, char separator, params string[] newStrs)
        {
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this List<string> strs, string? separator, params string[] newStrs)
        {
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this List<string> strs, char separator, params int[] newStrs)
        {
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重拼接
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string Combine(this List<string> strs, string? separator, params int[] newStrs)
        {
            foreach (var newStr in newStrs)
            {
                strs.Add(newStr.ToString());
            }
            return string.Join(separator, strs.Distinct());
        }

        /// <summary>
        /// 按规则添加新的string集合并去重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static List<string> CombineList(this string str, char separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return strs.Distinct().ToList();
        }

        /// <summary>
        /// 按规则添加新的string集合并去重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static List<string> CombineList(this string str, string? separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return strs.Distinct().ToList();
        }

        /// <summary>
        /// 按规则添加新的string集合并去重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string[] CombineArray(this string str, char separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return strs.Distinct().ToArray();
        }

        /// <summary>
        /// 按规则添加新的string集合并去重
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="newStrs">字符串集合</param>
        /// <returns></returns>
        public static string[] CombineArray(this string str, string? separator, params string[] newStrs)
        {
            if (string.IsNullOrWhiteSpace(str)) str = string.Empty;
            List<string> strs = str.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var newStr in newStrs)
            {
                if (!string.IsNullOrWhiteSpace(newStr))
                {
                    strs.Add(newStr);
                }
            }
            return strs.Distinct().ToArray();
        }

        /// <summary>
        /// 删除最后结尾的一个特定字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string DelLastString(this string str, char separator)
        {
            return str.Substring(0, str.LastIndexOf(separator));
        }

        /// <summary>
        /// 删除最后结尾的一个特定字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string DelLastString(this string str, string? separator)
        {
            return str.Substring(0, str.LastIndexOf(separator));
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if (str != null && str != "")
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(this string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }


        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        /// <param name="fixStr">补位字符</param>
        /// <returns></returns>
        public static string PadStart(this string inputString, int limitedLength = 9, string fixStr = "0")
        {
            //补足0的字符串
            string temp = "";
            //补足0
            for (int i = 0; i < limitedLength - inputString.Length; i++)
            {
                temp += fixStr;
            }
            //连接text
            temp += inputString;
            //返回补足0的字符串
            return temp;
        }

        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的后面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="inputString">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        /// <param name="fixStr">补位字符</param>
        /// <returns></returns>
        public static string PadEnd(this string inputString, int limitedLength = 9, string fixStr = "0")
        {
            //补足0的字符串
            string temp = "";
            //补足0
            for (int i = 0; i < limitedLength - inputString.Length; i++)
            {
                temp += fixStr;
            }
            //连接text
            inputString += temp;
            //返回补足0的字符串
            return inputString;
        }

        /// <summary>
        /// 乱序排列字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns></returns>
        public static string GetRandomArrayByStr(this string str)
        {
            char[] arr = str.ToCharArray();
            RandomHelper ra = new RandomHelper();
            ra.GetRandomArray(arr);
            return string.Join("", arr);
        }

        /// <summary>
        /// 字符串脱敏
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <param name="startIndex">脱敏起始字符串位数</param>
        /// <param name="num">脱敏字符串位数</param>
        /// <returns>脱敏后的字符串</returns>
        public static string ToSensitive(this string str, int startIndex, int num)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            if (startIndex < 0) throw new ArgumentOutOfRangeException("参数startIndex不能小于0!");
            if (num <= 0) throw new ArgumentOutOfRangeException("参数num不能小于等于0!");
            if (str.Length <= startIndex + num) throw new ArgumentOutOfRangeException("字符串长度不足!");

            string begin = str.Substring(0, startIndex);
            string end = str.Substring(startIndex + num);
            string middle = string.Empty;
            for (int i = 0; i < num; i++)
            {
                middle += "*";
            }
            string result = string.Empty;
            result = begin + middle + end;
            return result;
        }


        #endregion

        #region 转义操作

        /// <summary>
        /// 去除转义或特殊字符
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns></returns>
        public static string ToNormalString(this string input)
        {
            // 定义要移除的特殊字符模式
            string pattern = @"[\r\n\t]";
            // 使用正则表达式进行替换操作
            string output = Regex.Replace(input, pattern, "");
            return output;
        }

        #endregion

        #region 全角半角

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        #endregion

        #region SQL过滤

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary> 
        /// 检查过滤设定的危险字符
        /// </summary> 
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        public static bool SqlFilter(string word, string InText)
        {
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if (InText.ToLower().IndexOf(i + " ") > -1 || InText.ToLower().IndexOf(" " + i) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region HTML转行成TEXT

        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region 验证

        /// <summary>
        /// 获取正确的Id，如果不是正整数，返回0
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>返回正确的整数ID，失败返回0</returns>
        public static int StrToId(string _value)
        {
            if (IsNumberId(_value))
                return int.Parse(_value);
            else
                return 0;
        }

        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }

        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }

        #endregion
    }
}
