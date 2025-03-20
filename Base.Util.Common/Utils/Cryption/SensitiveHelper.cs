using Base.Util.Common.Utils.DataTypeHelper;
using Base.Util.Common.Utils.VerifyHelper;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// 脱敏 辅助类#
    /// </summary>
    public class SensitiveHelper
    {
        /// <summary>
        /// 姓名敏感处理
        /// </summary>
        /// <param name="fullName">姓名</param>
        /// <returns>脱敏后的姓名</returns>
        public static string SetSensitiveName(string fullName)
        {
            if (fullName.IsNullOrEmpty()) return string.Empty;

            string familyName = fullName.Substring(0, 1);
            string end = fullName.Substring(fullName.Length - 1, 1);
            string name = string.Empty;
            //长度为2
            if (fullName.Length <= 2) name = familyName + "*";
            //长度大于2
            else if (fullName.Length >= 3)
            {
                name = familyName.PadRight(fullName.Length - 1, '*') + end;
            }
            return name;
        }

        /// <summary>
        /// 身份证脱敏
        /// </summary>
        /// <param name="idCardNo">身份证号</param>
        /// <returns>脱敏后的身份证号</returns>
        public static string SetSensitiveIdCardNo(string idCardNo)
        {
            if (!VerificationHelper.IsIDCard(idCardNo)) return idCardNo;
            return idCardNo.ToSensitive(6, 8);
        }

        /// <summary>
        /// 电话号码脱敏
        /// </summary>
        /// <param name="phone">电话号码</param>
        /// <returns>脱敏后的电话号码</returns>
        public static string SetSensitivePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;
            if (VerificationHelper.IsMobile(phone)) return SetSensitiveTelephone(phone);
            int start = 2, num = 4;
            if (phone.Contains("-"))
            {
                start += 4;
            }
            return phone.ToSensitive(start, num);
        }

        /// <summary>
        /// 手机号码脱敏
        /// </summary>
        /// <param name="telephone">手机号码</param>
        /// <returns>脱敏后的手机号码</returns>
        public static string SetSensitiveTelephone(string telephone)
        {
            if (string.IsNullOrWhiteSpace(telephone)) return telephone;
            return telephone.ToSensitive(3, 4);
        }
    }
}
