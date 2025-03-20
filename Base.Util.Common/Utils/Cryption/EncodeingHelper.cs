using System.Text;

namespace Base.Util.Common.Utils.Cryption
{
    /// <summary>
    /// 编码格式类型
    /// </summary>
    public class EncodeingHelper
    {
        /// <summary>
        /// 注册全局编码
        /// .NET Core 编码支持不全 需要注册
        /// </summary>
        /// <param name="provider"></param>
        public static void EncodingRegisterProvider(EncodingProvider provider = null)
        {
            // 注册全局编码
            if (provider != null)
            {
                Encoding.RegisterProvider(provider);
            }
        }

        /// <summary>
        /// 默认使用UTF8 编码方式
        /// </summary>
        /// <param name="options">编码方式类型</param>
        /// <returns>Encoding</returns>
        public static Encoding GetEncoding(EncodeingOptions options = EncodeingOptions.UTF8)
        {
            if (options == EncodeingOptions.GB2312) EncodingRegisterProvider();
            return options switch
            {
                EncodeingOptions.UTF8 => Encoding.UTF8,
                EncodeingOptions.Unicode => Encoding.Unicode,
                EncodeingOptions.GB2312 => Encoding.GetEncoding(936),//编码格式 GB2312 支持中文
                EncodeingOptions.Default => Encoding.Default,
                EncodeingOptions.ASCII => Encoding.ASCII,
                EncodeingOptions.BigEndianUnicode => Encoding.BigEndianUnicode,
                EncodeingOptions.UTF7 => Encoding.UTF7,
                EncodeingOptions.UTF32 => Encoding.UTF32,
                _ => Encoding.UTF8,
            };
        }
    }

    public enum EncodeingOptions
    {
        /// <summary>
        /// UTF8 编码方式
        /// </summary>
        UTF8 = 1,
        /// <summary>
        /// Unicode 编码方式
        /// </summary>
        Unicode,
        /// <summary>
        /// GB2312 编码方式 支持中文
        /// </summary>
        GB2312,
        /// <summary>
        /// Default 编码方式
        /// </summary>
        Default,
        /// <summary>
        /// ASCII 编码方式
        /// </summary>
        ASCII,
        /// <summary>
        /// BigEndianUnicode 编码方式
        /// </summary>
        BigEndianUnicode,
        /// <summary>
        /// Latin1 编码方式
        /// </summary>
        Latin1,
        /// <summary>
        /// UTF7 编码方式
        /// </summary>
        UTF7,
        /// <summary>
        /// UTF32 编码方式
        /// </summary>
        UTF32,
    }
}
