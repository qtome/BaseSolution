using System;
using System.IO;
using System.Security.Cryptography;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// HASH 加密辅助类#
    /// MD5 SHA1 SHA256 SHA384 SHA512
    /// HashAlgorithm   哈希散列
    /// 不可逆加密
    /// </summary>
    public class HashHelper
    {
        #region 通用

        /// <summary>
        /// 根据类型返回指定 哈希散列
        /// </summary>
        /// <param name="option">哈希散列类型</param>
        /// <returns>哈希散列</returns>
        public static HashAlgorithm GetHashAlgorithm(HashOption option = HashOption.MD5)
        {
            return option switch
            {
                HashOption.MD5 => new MD5CryptoServiceProvider(),
                HashOption.SHA1 => new SHA1CryptoServiceProvider(),
                HashOption.SHA256 => new SHA256CryptoServiceProvider(),
                HashOption.SHA384 => new SHA384CryptoServiceProvider(),
                HashOption.SHA512 => new SHA512CryptoServiceProvider(),
                _ => new MD5CryptoServiceProvider(),
            };
        }

        #endregion

        #region MD5

        /// <summary>
        /// MD5加密,和动网上的16/32位MD5加密结果相同,
        /// 使用的UTF8编码
        /// </summary>
        /// <param name="source">待加密字串</param>
        /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
        /// <returns>摘要文</returns>
        public static string MD5Encrypt(string source, MD5Length length = MD5Length.L32)//默认参数
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string str_md5_out = string.Empty;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes_md5_in = EncodeingHelper.GetEncoding().GetBytes(source);//这里需要区别编码的
                byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);

                str_md5_out = length == MD5Length.L32
                    ? BitConverter.ToString(bytes_md5_out)
                    : BitConverter.ToString(bytes_md5_out, 4, 8);

                str_md5_out = str_md5_out.Replace("-", "");
            }
            return str_md5_out;
        }

        /// <summary>
        /// 校验 MD5加密 是否一致
        /// </summary>
        /// <param name="md5Source">加密后的MD5</param>
        /// <param name="source">待验证字串</param>
        /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
        /// <returns>是否一致</returns>
        public static bool VerifyMD5Cryption(string md5Source, string source, MD5Length length = MD5Length.L32)//默认参数
        {
            if (string.IsNullOrWhiteSpace(md5Source)) throw new ArgumentNullException(nameof(md5Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string str_md5_out = MD5Encrypt(source, length);
            return str_md5_out.Equals(md5Source);
        }

        /// <summary>
        /// 获取文件的MD5摘要
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>摘要文</returns>
        public static string MD5AbstractFile(string fileName)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Open))
            {
                return MD5AbstractFile(file);
            }
        }

        /// <summary>
        /// 根据stream获取文件摘要
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <returns>摘要文</returns>
        public static string MD5AbstractFile(Stream stream)
        {
            string str_md5_out = string.Empty;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes_md5_out = md5.ComputeHash(stream);

                str_md5_out = BitConverter.ToString(bytes_md5_out);

                str_md5_out = str_md5_out.Replace("-", "");
            }
            return str_md5_out;
        }

        /// <summary>
        /// 校验 文件摘要 是否一致
        /// </summary>
        /// <param name="md5Source">加密后的MD5</param>
        /// <param name="fileName">文件流</param>
        /// <returns>是否一致</returns>
        public static bool VerifyMD5AbstractFile(string md5Source, string fileName)
        {
            if (string.IsNullOrWhiteSpace(md5Source)) throw new ArgumentNullException(nameof(md5Source));
            if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException(nameof(fileName));
            string str_md5_out = MD5AbstractFile(fileName);
            return str_md5_out.Equals(md5Source);
        }

        /// <summary>
        /// 校验 文件摘要 是否一致
        /// </summary>
        /// <param name="md5Source">加密后的MD5</param>
        /// <param name="stream">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifyMD5AbstractFile(string md5Source, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(md5Source)) throw new ArgumentNullException(nameof(md5Source));
            string str_md5_out = MD5AbstractFile(stream);
            return str_md5_out.Equals(md5Source);
        }

        #region HMAC

        /// <summary>
        /// MD5加密,和动网上的16/32位MD5加密结果相同,
        /// 带key的DNA加密
        /// </summary>
        /// <param name="source">待加密字串</param>
        /// <param name="key">加密KEY</param>
        /// <returns>摘要文</returns>
        public static string HMACMD5Encrypt(string source, string key)
        {
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            byte[] secrectKey = EncodeingHelper.GetEncoding().GetBytes(key);//这里需要区别编码的
            string str_md5_out = string.Empty;
            using (HMACMD5 md5 = new HMACMD5(secrectKey))
            {
                byte[] bytes_md5_in = EncodeingHelper.GetEncoding().GetBytes(source);
                byte[] bytes_md5_out = md5.ComputeHash(bytes_md5_in);
                str_md5_out = BitConverter.ToString(bytes_md5_out);
                str_md5_out = str_md5_out.Replace("-", "");
            }
            return str_md5_out;
        }

        /// <summary>
        /// 校验 HMACMD5加密 是否一致
        /// </summary>
        /// <param name="md5Source">加密后的MD5</param>
        /// <param name="source">待验证字串</param>
        /// <param name="key">加密KEY</param>
        /// <returns>是否一致</returns>
        public static bool VerifyHMACMD5Cryption(string md5Source, string source, string key)
        {
            if (string.IsNullOrWhiteSpace(md5Source)) throw new ArgumentNullException(nameof(md5Source));
            if (string.IsNullOrWhiteSpace(source)) throw new ArgumentNullException(nameof(source));
            string str_md5_out = HMACMD5Encrypt(source, key);
            return str_md5_out.Equals(md5Source);
        }

        #endregion


        /// <summary>
        /// 生成MD5摘要文
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>byte[]</returns>
        public static byte[] GetMD5(string content)
        {
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentNullException(nameof(content));
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes_md5_in = EncodeingHelper.GetEncoding().GetBytes(content);//这里需要区别编码的
                return md5.ComputeHash(bytes_md5_in);
            }
        }

        #endregion

        #region SHA1

        /// <summary>
        /// SHA1 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <returns>摘要文</returns>
        public static string SHA1Encrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            string str_sha1_out = string.Empty;
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] bytes_sha1_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
                str_sha1_out = BitConverter.ToString(bytes_sha1_out);
                str_sha1_out = str_sha1_out.Replace("-", "");
            }
            return str_sha1_out;
        }

        /// <summary>
        /// 校验 SHA1加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifySHA1Cryption(string encryptText, string str)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = SHA1Encrypt(str);
            return result.Equals(encryptText);
        }

        #region HMAC

        /// <summary>
        /// HMACSHA1 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <param name="key">加密密钥</param>
        /// <returns>摘要文</returns>
        public static string HMACSHA1Encrypt(string str, string key)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            byte[] secrectKey = EncodeingHelper.GetEncoding().GetBytes(key);
            string str_hamc_out = string.Empty;
            using (HMACSHA1 hmac = new HMACSHA1(secrectKey))
            {
                hmac.Initialize();

                byte[] bytes_hmac_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_hamc_out = hmac.ComputeHash(bytes_hmac_in);

                str_hamc_out = BitConverter.ToString(bytes_hamc_out);
                str_hamc_out = str_hamc_out.Replace("-", "");
            }
            return str_hamc_out;
        }

        /// <summary>
        /// 校验 HMACSHA1加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyHMACSHA1Cryption(string encryptText, string str, string key)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = HMACSHA1Encrypt(str, key);
            return result.Equals(encryptText);
        }

        #endregion

        #endregion

        #region SHA256

        /// <summary>
        /// SHA256 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <returns>摘要文</returns>
        public static string SHA256Encrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            string str_sha256_out = string.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes_sha256_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_sha256_out = sha256.ComputeHash(bytes_sha256_in);
                str_sha256_out = BitConverter.ToString(bytes_sha256_out);
                str_sha256_out = str_sha256_out.Replace("-", "");
            }
            return str_sha256_out;
        }

        /// <summary>
        /// 校验 SHA256加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifySHA256Cryption(string encryptText, string str)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = SHA256Encrypt(str);
            return result.Equals(encryptText);
        }

        #region HMAC

        /// <summary>
        /// HMACSHA256 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <param name="key">加密密钥</param>
        /// <returns>摘要文</returns>
        public static string HMACSHA256Encrypt(string str, string key)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            byte[] secrectKey = EncodeingHelper.GetEncoding().GetBytes(key);
            string str_hamc_out = string.Empty;
            using (HMACSHA256 hmac = new HMACSHA256(secrectKey))
            {
                hmac.Initialize();

                byte[] bytes_hmac_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_hamc_out = hmac.ComputeHash(bytes_hmac_in);

                str_hamc_out = BitConverter.ToString(bytes_hamc_out);
                str_hamc_out = str_hamc_out.Replace("-", "");
            }
            return str_hamc_out;
        }

        /// <summary>
        /// 校验 HMACSHA256加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyHMACSHA256Cryption(string encryptText, string str, string key)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = HMACSHA256Encrypt(str, key);
            return result.Equals(encryptText);
        }

        #endregion

        #endregion

        #region SHA384

        /// <summary>
        /// SHA384 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <returns>摘要文</returns>
        public static string SHA384Encrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            string str_sha384_out = string.Empty;
            using (SHA384 sha384 = SHA384.Create())
            {
                byte[] bytes_sha384_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_sha384_out = sha384.ComputeHash(bytes_sha384_in);
                str_sha384_out = BitConverter.ToString(bytes_sha384_out);
                str_sha384_out = str_sha384_out.Replace("-", "");
            }
            return str_sha384_out;
        }

        /// <summary>
        /// 校验 SHA384加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifySHA384Cryption(string encryptText, string str)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = SHA384Encrypt(str);
            return result.Equals(encryptText);
        }

        #region HMAC

        /// <summary>
        /// HMACSHA384 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <param name="key">加密密钥</param>
        /// <returns>摘要文</returns>
        public static string HMACSHA384Encrypt(string str, string key)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            byte[] secrectKey = EncodeingHelper.GetEncoding().GetBytes(key);
            string str_hamc_out = string.Empty;
            using (HMACSHA384 hmac = new HMACSHA384(secrectKey))
            {
                hmac.Initialize();

                byte[] bytes_hmac_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_hamc_out = hmac.ComputeHash(bytes_hmac_in);

                str_hamc_out = BitConverter.ToString(bytes_hamc_out);
                str_hamc_out = str_hamc_out.Replace("-", "");
            }
            return str_hamc_out;
        }

        /// <summary>
        /// 校验 HMACSHA384加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyHMACSHA384Cryption(string encryptText, string str, string key)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = HMACSHA384Encrypt(str, key);
            return result.Equals(encryptText);
        }

        #endregion

        #endregion

        #region SHA512

        /// <summary>
        /// SHA512 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <returns>摘要文</returns>
        public static string SHA512Encrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));

            string str_sha512_out = string.Empty;
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes_sha512_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_sha512_out = sha512.ComputeHash(bytes_sha512_in);
                str_sha512_out = BitConverter.ToString(bytes_sha512_out);
                str_sha512_out = str_sha512_out.Replace("-", "");
            }
            return str_sha512_out;
        }

        /// <summary>
        /// 校验 SHA512加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <returns>是否一致</returns>
        public static bool VerifySHA512Cryption(string encryptText, string str)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = SHA512Encrypt(str);
            return result.Equals(encryptText);
        }

        #region HMAC

        /// <summary>
        /// HMACSHA512 方式 加密
        /// </summary>
        /// <param name="str">待加密文本</param>
        /// <param name="key">加密密钥</param>
        /// <returns>摘要文</returns>
        public static string HMACSHA512Encrypt(string str, string key)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            byte[] secrectKey = EncodeingHelper.GetEncoding().GetBytes(key);
            string str_hamc_out = string.Empty;
            using (HMACSHA512 hmac = new HMACSHA512(secrectKey))
            {
                hmac.Initialize();

                byte[] bytes_hmac_in = EncodeingHelper.GetEncoding().GetBytes(str);
                byte[] bytes_hamc_out = hmac.ComputeHash(bytes_hmac_in);

                str_hamc_out = BitConverter.ToString(bytes_hamc_out);
                str_hamc_out = str_hamc_out.Replace("-", "");
            }
            return str_hamc_out;
        }

        /// <summary>
        /// 校验 HMACSHA512加密 是否一致
        /// </summary>
        /// <param name="encryptText">加密后的文本</param>
        /// <param name="str">待验证字串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>是否一致</returns>
        public static bool VerifyHMACSHA512Cryption(string encryptText, string str, string key)
        {
            if (string.IsNullOrWhiteSpace(encryptText)) throw new ArgumentNullException(nameof(encryptText));
            if (string.IsNullOrWhiteSpace(str)) throw new ArgumentNullException(nameof(str));
            string result = HMACSHA512Encrypt(str, key);
            return result.Equals(encryptText);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 16/32位MD5
    /// </summary>
    public enum MD5Length
    {
        /// <summary>
        /// 16位MD5
        /// </summary>
        L16 = 16,
        /// <summary>
        /// 32位MD5
        /// </summary>
        L32 = 32
    }

    /// <summary>
    /// 哈希类型
    /// </summary>
    public enum HashOption
    {
        /// <summary>
        /// MD5
        /// </summary>
        MD5 = 0,
        /// <summary>
        /// SHA1
        /// </summary>
        SHA1,
        /// <summary>
        /// SHA256
        /// </summary>
        SHA256,
        /// <summary>
        /// SHA384
        /// </summary>
        SHA384,
        /// <summary>
        /// SHA512
        /// </summary>
        SHA512,
    }

}
