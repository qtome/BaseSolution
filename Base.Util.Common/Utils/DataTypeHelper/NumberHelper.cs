using System;
using System.Text;

namespace Base.Util.Common.Utils.DataTypeHelper
{
    /// <summary>
    /// 数字 辅助类
    /// </summary>
    public static class NumberHelper
    {
        #region 转换

        /// <summary>
        /// 将object转换成数字
        /// </summary>
        /// <param name="decimalObj"></param>
        /// <returns></returns>
        public static decimal? ParseToDecimalValue(this object decimalObj)
        {
            if (decimalObj == null) return null;
            decimal decValue;
            if (!decimal.TryParse(decimalObj.ToString(), out decValue)) return null;
            return decValue;
        }

        /// <summary>
        /// 转中文大写数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNumToZHUpperCase(this decimal value)
        {
            string[] numList = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] unitList = { "分", "角", "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };

            decimal money = value;
            if (money == 0)
            {
                return "零元整";
            }

            StringBuilder strMoney = new StringBuilder();
            //只取小数后2位

            string strNum = decimal.Truncate(money * 100).ToString();

            int len = strNum.Length;
            int zero = 0;
            for (int i = 0; i < len; i++)
            {
                int num = int.Parse(strNum.Substring(i, 1));
                int unitNum = len - i - 1;

                if (num == 0)
                {
                    zero++;
                    if (unitNum == 2 || unitNum == 6 || unitNum == 10)
                    {
                        if (unitNum == 2 || zero < 4)
                            strMoney.Append(unitList[unitNum]);
                        zero = 0;
                    }
                }
                else
                {
                    if (zero > 0)
                    {
                        strMoney.Append(numList[0]);
                        zero = 0;
                    }
                    strMoney.Append(numList[num]);
                    strMoney.Append(unitList[unitNum]);
                }
            }
            if (zero > 0)
                strMoney.Append("整");

            return strMoney.ToString();
        }

        /// <summary>
        /// 转中文大写数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertNumToZHUpperCase(this decimal? value)
        {
            if (!value.HasValue) return "";
            return value.Value.ConvertNumToZHUpperCase();
        }

        #endregion

        #region 便捷操作

        /// <summary>
        /// 截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToFixed(this decimal d, int s)
        {
            decimal sp = Convert.ToDecimal(Math.Pow(10, s));
            decimal fix = Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
            string format = "0." + string.Empty.PadEnd(s, "0");
            decimal result = Convert.ToDecimal(fix.ToString(format));
            return result;
        }

        /// <summary>
        /// 截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal? ToFixed(this decimal? d, int s)
        {
            if (!d.HasValue) return d;
            return d.Value.ToFixed(s);
        }

        /// <summary>
        ///  截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToFixed(this double d, int s)
        {
            double sp = Math.Pow(10, s);
            double fix = Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
            string format = "0." + string.Empty.PadEnd(s, "0");
            decimal result = Convert.ToDecimal(fix.ToString(format));
            return result;
        }

        /// <summary>
        ///  截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal? ToFixed(this double? d, int s)
        {
            if (!d.HasValue) return null;
            return d.Value.ToFixed(s);
        }

        /// <summary>
        ///  截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal ToFixed(this float d, int s)
        {
            double sp = Math.Pow(10, s);
            double fix = Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * sp) / sp;
            string format = "0." + string.Empty.PadEnd(s, "0");
            decimal result = Convert.ToDecimal(fix.ToString(format));
            return result;
        }

        /// <summary>
        ///  截取指定位数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal? ToFixed(this float? d, int s)
        {
            if (!d.HasValue) return null;
            return d.Value.ToFixed(s);
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetPercent(decimal numerator, decimal denominator, int s = 2)
        {
            // 分母 不能为 0
            if ((int)denominator == 0) return 0;
            decimal percent = Math.Round((decimal)numerator / (decimal)denominator * 100, s);
            return percent.ToFixed(s);
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetPercent(double numerator, double denominator, int s = 2)
        {
            // 分母 不能为 0
            if ((int)denominator == 0) return 0;
            decimal percent = Math.Round((decimal)numerator / (decimal)denominator * 100, s);
            return percent.ToFixed(s);
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetPercent(float numerator, float denominator, int s = 2)
        {
            // 分母 不能为 0
            if ((int)denominator == 0) return 0;
            decimal percent = Math.Round((decimal)numerator / (decimal)denominator * 100, s);
            return percent;
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetPercent(int numerator, int denominator, int s = 2)
        {
            // 分母 不能为 0
            if (denominator == 0) return 0;
            decimal percent = Math.Round(numerator / (decimal)denominator * 100, s);
            return percent;
        }

        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetAverage(decimal numerator, decimal denominator, int s = 2)
        {
            return (decimal)(GetPercent(numerator, denominator, s + 2) / 100).ToFixed(s);
        }

        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetAverage(double numerator, double denominator, int s = 2)
        {
            return (GetPercent(numerator, denominator, s + 2) / 100).ToFixed(s);
        }

        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetAverage(int numerator, int denominator, int s = 2)
        {
            return (GetPercent(numerator, denominator, s + 2) / 100).ToFixed(s);
        }

        /// <summary>
        /// 计算平均值
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="s">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetAverage(float numerator, float denominator, int s = 2)
        {
            return (GetPercent(numerator, denominator, s + 2) / 100).ToFixed(s);
        }

        #endregion

        #region 三角计算

        /// <summary>
        /// 根据直角边计算斜边和角度
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static (double, double) CalculateAngle(int height, int width)
        {
            var dis = MathF.Sqrt(height * height + width * width);
            var angle = MathF.Asin(height / dis) / MathF.PI * 180;
            return (dis, angle);
        }

        #endregion

        #region 科学计算

        /// <summary>
        /// 算数表达式
        /// </summary>
        /// <param name="strExpression"></param>
        /// <returns></returns>
        public static string MathExpression(this string strExpression)
        {
            string strTemp = "";
            string strExp = "";
            while (strExpression.IndexOf("(") != -1)
            {
                strTemp = strExpression.Substring(strExpression.LastIndexOf("(") + 1, strExpression.Length - strExpression.LastIndexOf("(") - 1);
                strExp = strTemp.Substring(0, strTemp.IndexOf(")"));
                strExpression = strExpression.Replace("(" + strExp + ")", CalculateExpress(strExp).ToString());
            }
            if (strExpression.IndexOf("+") != -1 || strExpression.IndexOf("-") != -1
            || strExpression.IndexOf("*") != -1 || strExpression.IndexOf("/") != -1)
            {
                strExpression = CalculateExpress(strExpression).ToString();
            }
            return strExpression;
        }

        #region MathExpression

        private static double CalculateExpress(string strExpression)
        {
            string strTemp = "";
            string strTempB = "";
            string strOne = "";
            string strTwo = "";
            double ReplaceValue = 0;
            while (strExpression.IndexOf("+") != -1 || strExpression.IndexOf("-") != -1
            || strExpression.IndexOf("*") != -1 || strExpression.IndexOf("/") != -1)
            {
                if (strExpression.IndexOf("*") != -1)
                {
                    strTemp = strExpression.Substring(strExpression.IndexOf("*") + 1, strExpression.Length - strExpression.IndexOf("*") - 1);
                    strTempB = strExpression.Substring(0, strExpression.IndexOf("*"));
                    strOne = strTempB.Substring(GetPrivorPos(strTempB) + 1, strTempB.Length - GetPrivorPos(strTempB) - 1);

                    strTwo = strTemp.Substring(0, GetNextPos(strTemp));
                    ReplaceValue = Convert.ToDouble(GetExpType(strOne)) * Convert.ToDouble(GetExpType(strTwo));
                    strExpression = strExpression.Replace(strOne + "*" + strTwo, ReplaceValue.ToString());
                }
                else if (strExpression.IndexOf("/") != -1)
                {
                    strTemp = strExpression.Substring(strExpression.IndexOf("/") + 1, strExpression.Length - strExpression.IndexOf("/") - 1);
                    strTempB = strExpression.Substring(0, strExpression.IndexOf("/"));
                    strOne = strTempB.Substring(GetPrivorPos(strTempB) + 1, strTempB.Length - GetPrivorPos(strTempB) - 1);


                    strTwo = strTemp.Substring(0, GetNextPos(strTemp));
                    ReplaceValue = Convert.ToDouble(GetExpType(strOne)) / Convert.ToDouble(GetExpType(strTwo));
                    strExpression = strExpression.Replace(strOne + "/" + strTwo, ReplaceValue.ToString());
                }
                else if (strExpression.IndexOf("+") != -1)
                {
                    strTemp = strExpression.Substring(strExpression.IndexOf("+") + 1, strExpression.Length - strExpression.IndexOf("+") - 1);
                    strTempB = strExpression.Substring(0, strExpression.IndexOf("+"));
                    strOne = strTempB.Substring(GetPrivorPos(strTempB) + 1, strTempB.Length - GetPrivorPos(strTempB) - 1);

                    strTwo = strTemp.Substring(0, GetNextPos(strTemp));
                    ReplaceValue = Convert.ToDouble(GetExpType(strOne)) + Convert.ToDouble(GetExpType(strTwo));
                    strExpression = strExpression.Replace(strOne + "+" + strTwo, ReplaceValue.ToString());
                }
                else if (strExpression.IndexOf("-") != -1)
                {
                    strTemp = strExpression.Substring(strExpression.IndexOf("-") + 1, strExpression.Length - strExpression.IndexOf("-") - 1);
                    strTempB = strExpression.Substring(0, strExpression.IndexOf("-"));
                    strOne = strTempB.Substring(GetPrivorPos(strTempB) + 1, strTempB.Length - GetPrivorPos(strTempB) - 1);


                    strTwo = strTemp.Substring(0, GetNextPos(strTemp));
                    ReplaceValue = Convert.ToDouble(GetExpType(strOne)) - Convert.ToDouble(GetExpType(strTwo));
                    strExpression = strExpression.Replace(strOne + "-" + strTwo, ReplaceValue.ToString());
                }
            }
            return Convert.ToDouble(strExpression);
        }

        private static double CalculateExExpress(string strExpression, EnumFormula ExpressType)
        {
            double retValue = 0;
            switch (ExpressType)
            {
                case EnumFormula.Sin:
                    retValue = Math.Sin(Convert.ToDouble(strExpression));
                    break;
                case EnumFormula.Cos:
                    retValue = Math.Cos(Convert.ToDouble(strExpression));
                    break;
                case EnumFormula.Tan:
                    retValue = Math.Tan(Convert.ToDouble(strExpression));
                    break;
                case EnumFormula.ATan:
                    retValue = Math.Atan(Convert.ToDouble(strExpression));
                    break;
                case EnumFormula.Sqrt:
                    retValue = Math.Sqrt(Convert.ToDouble(strExpression));
                    break;
                case EnumFormula.Pow:
                    retValue = Math.Pow(Convert.ToDouble(strExpression), 2);
                    break;
            }
            if (retValue == 0) return Convert.ToDouble(strExpression);
            return retValue;
        }

        private static int GetNextPos(string strExpression)
        {
            int[] ExpPos = new int[4];
            ExpPos[0] = strExpression.IndexOf("+");
            ExpPos[1] = strExpression.IndexOf("-");
            ExpPos[2] = strExpression.IndexOf("*");
            ExpPos[3] = strExpression.IndexOf("/");
            int tmpMin = strExpression.Length;
            for (int count = 1; count <= ExpPos.Length; count++)
            {
                if (tmpMin > ExpPos[count - 1] && ExpPos[count - 1] != -1)
                {
                    tmpMin = ExpPos[count - 1];
                }
            }
            return tmpMin;
        }

        private static int GetPrivorPos(string strExpression)
        {
            int[] ExpPos = new int[4];
            ExpPos[0] = strExpression.LastIndexOf("+");
            ExpPos[1] = strExpression.LastIndexOf("-");
            ExpPos[2] = strExpression.LastIndexOf("*");
            ExpPos[3] = strExpression.LastIndexOf("/");
            int tmpMax = -1;
            for (int count = 1; count <= ExpPos.Length; count++)
            {
                if (tmpMax < ExpPos[count - 1] && ExpPos[count - 1] != -1)
                {
                    tmpMax = ExpPos[count - 1];
                }
            }
            return tmpMax;

        }

        private static string GetExpType(string strExpression)
        {
            strExpression = strExpression.ToUpper();
            if (strExpression.IndexOf("SIN") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, strExpression.Length - 1 - strExpression.IndexOf("N")), EnumFormula.Sin).ToString();
            }
            if (strExpression.IndexOf("COS") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("S") + 1, strExpression.Length - 1 - strExpression.IndexOf("S")), EnumFormula.Cos).ToString();
            }
            if (strExpression.IndexOf("TAN") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, strExpression.Length - 1 - strExpression.IndexOf("N")), EnumFormula.Tan).ToString();
            }
            if (strExpression.IndexOf("ATAN") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("N") + 1, strExpression.Length - 1 - strExpression.IndexOf("N")), EnumFormula.ATan).ToString();
            }
            if (strExpression.IndexOf("SQRT") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("T") + 1, strExpression.Length - 1 - strExpression.IndexOf("T")), EnumFormula.Sqrt).ToString();
            }
            if (strExpression.IndexOf("POW") != -1)
            {
                return CalculateExExpress(strExpression.Substring(strExpression.IndexOf("W") + 1, strExpression.Length - 1 - strExpression.IndexOf("W")), EnumFormula.Pow).ToString();
            }
            return strExpression;
        }

        #endregion


        #endregion
    }

    /// <summary>
    /// 科学计算
    /// </summary>
    public enum EnumFormula
    {
        /// <summary>
        /// 加法
        /// </summary>
        Add,//加号
        /// <summary>
        /// 减法
        /// </summary>
        Dec,//减号
        /// <summary>
        /// 乘法
        /// </summary>
        Mul,//乘号
        /// <summary>
        /// 除法
        /// </summary>
        Div,//除号
        /// <summary>
        /// 正玄计算
        /// </summary>
        Sin,//正玄
        /// <summary>
        /// 余玄计算
        /// </summary>
        Cos,//余玄
        /// <summary>
        /// 正切计算
        /// </summary>
        Tan,//正切
        /// <summary>
        /// 余切计算
        /// </summary>
        ATan,//余切
        /// <summary>
        /// 平方根计算
        /// </summary>
        Sqrt,//平方根
        /// <summary>
        /// 求幂计算
        /// </summary>
        Pow,//求幂
        /// <summary>
        /// 
        /// </summary>
        None,//无
    }

}
