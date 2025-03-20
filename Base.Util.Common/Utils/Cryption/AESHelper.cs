using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// AES 加密辅助类
    /// SymmetricAlgorithm  对称性
    /// 对称性可逆加密
    /// </summary>
    public class AESHelper
    {
        /// <summary>
        /// 默认共享密钥
        /// </summary>
        private static string defaultKey = "weaversys";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密文本</param>
        /// <returns>密文</returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, defaultKey);
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text">待加密文本</param> 
        /// <param name="shareKey">共享密钥</param> 
        /// <returns>密文</returns> 
        public static string Encrypt(string text, string shareKey)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.ASCII.GetBytes(HashHelper.MD5Encrypt(shareKey, MD5Length.L32));
            aes.IV = Encoding.ASCII.GetBytes(HashHelper.MD5Encrypt(shareKey, MD5Length.L16));// 向量
            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                StreamWriter sWriter = new StreamWriter(crypStream);
                sWriter.Write(text);
                sWriter.Flush();
                crypStream.FlushFinalBlock();
                memStream.Flush();
                return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text">加密的密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string text)
        {
            return Decrypt(text, defaultKey);
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text">加密的密文</param> 
        /// <param name="shareKey">共享密钥</param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string shareKey)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.ASCII.GetBytes(HashHelper.MD5Encrypt(shareKey, MD5Length.L32));
            aes.IV = Encoding.ASCII.GetBytes(HashHelper.MD5Encrypt(shareKey, MD5Length.L16));// 向量
            byte[] buffer = Convert.FromBase64String(text);

            using (MemoryStream memStream = new MemoryStream())
            {
                CryptoStream crypStream = new CryptoStream(memStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                crypStream.Write(buffer, 0, buffer.Length);
                crypStream.FlushFinalBlock();
                return EncodeingHelper.GetEncoding().GetString(memStream.ToArray());
            }
        }

        /// <summary>
        /// 校验 DES加密 是否一致
        /// </summary>
        /// <param name="encode">加密后的密文</param>
        /// <param name="text">待验证明文</param>
        /// <returns>是否一致</returns>
        public static bool VerifyCryption(string encode, string text)
        {
            if (string.IsNullOrWhiteSpace(encode)) throw new ArgumentNullException(nameof(encode));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            string result = Decrypt(encode);
            return result.Equals(text);
        }

        /// <summary>
        /// 校验 DES加密 是否一致
        /// </summary>
        /// <param name="encode">加密后的密文</param>
        /// <param name="text">待验证明文</param>
        /// <param name="shareKey">共享密钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyCryption(string encode, string text, string shareKey)
        {
            if (string.IsNullOrWhiteSpace(encode)) throw new ArgumentNullException(nameof(encode));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(shareKey)) throw new ArgumentNullException(nameof(shareKey));
            string result = Decrypt(encode, shareKey);
            return result.Equals(text);
        }
    }
}
