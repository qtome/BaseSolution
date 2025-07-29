using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Base.Util.Configuration.Statics.AuthInfo
{
    /// <summary>
    /// 基础权限静态类
    /// </summary>
    public class BaseAuthConst
    {
        /// <summary>
        /// 验证信息 头信息 键
        /// </summary>
        public const string headerKey = "appToken";

        #region 密码相关

        /// <summary>
        /// 项目默认用密码
        /// </summary>
        public const string defaultPassword = "a1b2c3@4D";

        #region 密码强度校验

        /// <summary>
        /// 项目密码复杂度正则
        /// 长度在8到20个字符之间
        /// 至少包含一个数字
        /// 至少包含一个小写字母
        /// 至少包含一个大写字母
        /// 至少包含一个特殊字符
        /// </summary>
        public const string defaultPasswordRegex = "^(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*()\\-_=+{}\\[\\]|;:'\",.<>/?]).{8,20}$";

        /// <summary>
        /// 验证密码是否是强密码
        /// defaultPasswordRegex
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static (bool isValid, string errorMessage) ValidateStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "密码不能为空");

            if (password.Length < 8 || password.Length > 20)
                return (false, "密码长度必须在8到20个字符之间");

            var requirements = new List<string>();

            if (!Regex.IsMatch(password, @"[0-9]"))
                requirements.Add("至少一个数字");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                requirements.Add("至少一个大写字母");

            if (!Regex.IsMatch(password, @"[a-z]"))
                requirements.Add("至少一个小写字母");

            if (!Regex.IsMatch(password, @"[!@#$%^&*()\-_=+{}\[\]|;:'"",.<>/?]"))
                requirements.Add("至少一个特殊字符 (!@#$%^&*等)");

            if (Regex.IsMatch(password, @"\s"))
                requirements.Add("不能包含空白字符");

            if (requirements.Count == 0)
                return (true, "密码强度足够");

            return (false, $"密码强度不足，缺少以下要求: {string.Join("，", requirements)}");
        }

        /// <summary>
        /// 判断用户是否超过半年未修改密码
        /// </summary>
        /// <param name="latestChangeDate">最新修改密码时间</param>
        /// <param name="passwordOverMonth">密码强制变更周期</param>
        /// <returns></returns>
        public static bool IsPasswordOverTime(DateTime latestChangeDate, int passwordOverMonth)
        {
            return latestChangeDate <= DateTime.Now.AddMonths(-passwordOverMonth).Date;
        }

        /// <summary>
        /// 使用私钥解密密码
        /// </summary>
        /// <param name="encryptedPassword">加密后的密码</param>
        /// <param name="privateKey">私钥</param>
        /// <returns></returns>
        public static string DecryptWithRsa(string encryptedPassword, string privateKey)
        {
            try
            {
                byte[] privateKeyDer = Convert.FromBase64String(privateKey);

                using (RSA rsa = RSA.Create())
                {
                    rsa.ImportRSAPrivateKey(privateKeyDer, out _);
                    byte[] cipherBytes = Convert.FromBase64String(encryptedPassword);
                    byte[] plainBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.Pkcs1);
                    return Encoding.UTF8.GetString(plainBytes);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

    }
}
