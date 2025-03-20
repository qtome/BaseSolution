using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// BASE64 加密辅助类#
    /// 可逆加密
    /// </summary>
    public class Base64Helper
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <param name="encode">加密采用的编码方式</param>
        /// <returns>密文</returns>
        public static string Encrypt(string source, Encoding encode)
        {
            string encode_out = string.Empty;
            byte[] bytes = encode.GetBytes(source);
            try
            {
                encode_out = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode_out = source;
            }
            return encode_out;
            //return Convert.ToBase64String(encoding.GetBytes(source));

        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string source)
        {
            return Encrypt(source, EncodeingHelper.GetEncoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>明文</returns>
        public static string Decrypt(string result, Encoding encode)
        {
            if (result.Length % 4 != 0) throw new FormatException("包含不正确的BASE64编码");
            if (!Regex.IsMatch(result, "^[A-Z0-9/+=]*$", RegexOptions.IgnoreCase)) throw new FormatException("包含不正确的BASE64编码");

            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
            //return encoding.GetString(Convert.FromBase64String(result));
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string result)
        {
            return Decrypt(result, EncodeingHelper.GetEncoding());
        }

        /// <summary>
        /// 校验 加密后的密文 是否一致
        /// </summary>
        /// <param name="base64Source">加密后的Base64</param>
        /// <param name="source">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifyEncryption(string source, string base64Source)
        {
            if (string.IsNullOrWhiteSpace(base64Source)) throw new ArgumentNullException(nameof(base64Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string result = Encrypt(source);
            return result.Equals(base64Source);
        }

        /// <summary>
        /// 校验 加密后的密文 是否一致
        /// </summary>
        /// <param name="base64Source">加密后的Base64</param>
        /// <param name="source">待验证字串</param>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>是否一致</returns>
        public static bool VerifyEncryption(string source, string base64Source, Encoding encode)
        {
            if (string.IsNullOrWhiteSpace(base64Source)) throw new ArgumentNullException(nameof(base64Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string result = Encrypt(source, encode);
            return result.Equals(base64Source);
        }

        /// <summary>
        /// 校验 Base64解密结果 是否一致
        /// </summary>
        /// <param name="base64Source">加密后的Base64</param>
        /// <param name="source">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifyDecryption(string base64Source, string source)
        {
            if (string.IsNullOrWhiteSpace(base64Source)) throw new ArgumentNullException(nameof(base64Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string result = Decrypt(base64Source);
            return result.Equals(source);
        }

        /// <summary>
        /// 校验 Base64解密结果 是否一致
        /// </summary>
        /// <param name="base64Source">加密后的Base64</param>
        /// <param name="source">待验证字串</param>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>是否一致</returns>
        public static bool VerifyDecryption(string base64Source, string source, Encoding encode)
        {
            if (string.IsNullOrWhiteSpace(base64Source)) throw new ArgumentNullException(nameof(base64Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string result = Decrypt(base64Source, encode);
            return result.Equals(source);
        }
    }
}
