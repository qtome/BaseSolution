using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// RSA 加密辅助类#
    /// AsymmetricAlgorithm 非对称性
    /// 非对称性可逆加密
    /// </summary>
    public class RSAHelper
    {

        #region 加解密

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string str)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            using (StreamReader streamReader = new StreamReader("PublicKey.xml")) // 读取运行目录下的PublicKey.xml
            {
                rsa.FromXmlString(streamReader.ReadToEnd()); // 将公匙载入进RSA实例中
            }
            byte[] buffer = EncodeingHelper.GetEncoding().GetBytes(str); // 将明文转换为byte[]

            // 加密后的数据就是一个byte[] 数组,可以以 文件的形式保存 或 别的形式(网上很多教程,使用Base64进行编码化保存)
            byte[] EncryptBuffer = rsa.Encrypt(buffer, false); // 进行加密

            string EncryptBase64 = Convert.ToBase64String(EncryptBuffer); // 如果使用base64进行明文化，在解密时 需要再次将base64 转换为byte[]
            return EncryptBase64;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encode">已加密的密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encode)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            using (StreamReader streamReader = new StreamReader("PrivateKey.xml")) // 读取运行目录下的PrivateKey.xml
            {
                rsa.FromXmlString(streamReader.ReadToEnd()); // 将私匙载入进RSA实例中
            }

            byte[] c = Convert.FromBase64String(encode);

            // 解密后得到一个byte[] 数组
            byte[] DecryptBuffer = rsa.Decrypt(c, false); // 进行解密
            string str = EncodeingHelper.GetEncoding().GetString(DecryptBuffer); // 将byte[]转换为明文

            return str;
        }

        /// <summary>
        /// 校验 RSA加密 是否一致
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
        /// 加密
        /// </summary>
        /// <param name="content">需要加密的明文</param>
        /// <param name="publicKey">加密公钥</param>
        /// <returns>密文</returns>
        public static string Encrypt(string content, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey); // 将公匙载入进RSA实例中
            byte[] DataToEncrypt = EncodeingHelper.GetEncoding().GetBytes(content);
            byte[] resultBytes = rsa.Encrypt(DataToEncrypt, false);
            return Convert.ToBase64String(resultBytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encode">已加密的密文</param>
        /// <param name="privateKey">解密私钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encode, string privateKey)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(encode);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] resultBytes = rsa.Decrypt(dataToDecrypt, false);
            return EncodeingHelper.GetEncoding().GetString(resultBytes);
        }

        /// <summary>
        /// 校验 RSA加密 是否一致
        /// </summary>
        /// <param name="encode">加密后的密文</param>
        /// <param name="text">待验证明文</param>
        /// <param name="privateKey">解密私钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyCryption(string encode, string text, string privateKey)
        {
            if (string.IsNullOrWhiteSpace(encode)) throw new ArgumentNullException(nameof(encode));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));
            if (string.IsNullOrWhiteSpace(privateKey)) throw new ArgumentNullException(nameof(privateKey));
            string result = Decrypt(encode, privateKey);
            return result.Equals(text);
        }

        #endregion

        #region 密钥

        /// <summary>
        /// 获取加密所使用的key，RSA算法是一种非对称密码算法，所谓非对称，就是指该算法需要一对密钥，使用其中一个加密，则需要用另一个才能解密。
        /// </summary>
        /// <returns>(公钥,私钥)</returns>
        public static (string publicKey, string privateKey) GetRSAKeyPair()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string PublicKey = rsa.ToXmlString(false); // 获取公匙，用于加密
            string PrivateKey = rsa.ToXmlString(true); // 获取公匙和私匙，用于解密

            //Console.WriteLine("PublicKey is {0}", PublicKey);        // 输出公匙
            //Console.WriteLine("PrivateKey is {0}", PrivateKey);     // 输出密匙
            //// 密匙中含有公匙，公匙是根据密匙进行计算得来的。

            using (StreamWriter streamWriter = new StreamWriter("PublicKey.xml"))
            {
                streamWriter.Write(PublicKey);// 将公匙保存到运行目录下的PublicKey
            }
            using (StreamWriter streamWriter = new StreamWriter("PrivateKey.xml"))
            {
                streamWriter.Write(PrivateKey); // 将公匙&私匙保存到运行目录下的PrivateKey
            }
            return (PublicKey, PrivateKey);
        }

        /// <summary>
        /// 通过解密私钥 重新获取 加密公钥
        /// </summary>
        /// <param name="privateKey">解密私钥</param>
        /// <returns>公钥</returns>
        public static string GetRSAPublicKey(string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return rsa.ToXmlString(false); // 获取公匙，用于加密
        }

        /// <summary>
        /// 校验 RSA密钥对 是否一致
        /// </summary>
        /// <param name="publicKey">加密公钥</param>
        /// <param name="privateKey">解密私钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyKeyPairs(string publicKey, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            string PublicKey = rsa.ToXmlString(false); // 获取公匙，用于加密
            return publicKey.Equals(PublicKey);
        }

        #endregion

        #region 数字签名

        /// <summary>
        /// 生成数字签名
        /// </summary>
        /// <param name="originalText">原文</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="option">摘要类型</param>
        /// <returns></returns>
        public static string GenSign(string originalText, string privateKey, HashOption option = HashOption.MD5)
        {
            byte[] byteData = EncodeingHelper.GetEncoding().GetBytes(originalText);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            //默认使用MD5进行摘要算法，生成签名
            byteData = provider.SignData(byteData, HashHelper.GetHashAlgorithm(option));
            return Convert.ToBase64String(byteData);
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="originalText">原文</param>
        /// <param name="signedData">签名内容</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="option">摘要类型</param>
        /// <returns></returns>
        public static bool VerifySign(string originalText, string signedData, string publicKey, HashOption option = HashOption.MD5)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(publicKey);
            byte[] byteData = EncodeingHelper.GetEncoding().GetBytes(originalText);
            byte[] signData = Convert.FromBase64String(signedData);
            return provider.VerifyData(byteData, HashHelper.GetHashAlgorithm(option), signData);
        }

        #endregion

    }
}
