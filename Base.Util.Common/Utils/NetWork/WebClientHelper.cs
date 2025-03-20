using System;
using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Base.Util.Common.Utils.NetWork
{
    /// <summary>    
    /// 上传数据参数    
    /// </summary>    
    public class UploadEventArgs : EventArgs
    {
        int bytesSent;
        int totalBytes;

        /// <summary>       
        /// 已发送的字节数    
        /// </summary>    
        public int BytesSent
        {
            get { return bytesSent; }
            set { bytesSent = value; }
        }

        /// <summary>    
        /// 总字节数    
        /// </summary>    
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }
    }
    /// <summary>    
    /// 下载数据参数    
    /// </summary>    
    public class DownloadEventArgs : EventArgs
    {
        int bytesReceived;
        int totalBytes;
        byte[] receivedData;

        /// <summary>    
        /// 已接收的字节数    
        /// </summary>    
        public int BytesReceived
        {
            get { return bytesReceived; }
            set { bytesReceived = value; }
        }

        /// <summary>    
        /// 总字节数    
        /// </summary>    
        public int TotalBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }

        /// <summary>    
        /// 当前缓冲区接收的数据    
        /// </summary>    
        public byte[] ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }
    }

    /// <summary>
    /// 网络客户端帮助类
    /// </summary>
    public class WebClientHelper
    {
        Encoding encoding = Encoding.Default;
        string respHtml = "";
        WebProxy proxy;
        static CookieContainer cc;
        WebHeaderCollection requestHeaders;
        WebHeaderCollection responseHeaders;
        int bufferSize = 15240;
        public event EventHandler<UploadEventArgs> UploadProgressChanged;
        public event EventHandler<DownloadEventArgs> DownloadProgressChanged;
        static WebClientHelper()
        {
            LoadCookiesFromDisk();
        }

        /// <summary>    
        /// 创建WebClient的实例    
        /// </summary>    
        public WebClientHelper()
        {
            requestHeaders = new WebHeaderCollection();
            responseHeaders = new WebHeaderCollection();
        }

        /// <summary>    
        /// 设置发送和接收的数据缓冲大小    
        /// </summary>    
        public int BufferSize
        {
            get { return bufferSize; }
            set { bufferSize = value; }
        }

        /// <summary>    
        /// 获取响应头集合    
        /// </summary>    
        public WebHeaderCollection ResponseHeaders
        {
            get { return responseHeaders; }
        }

        /// <summary>    
        /// 获取请求头集合    
        /// </summary>    
        public WebHeaderCollection RequestHeaders
        {
            get { return requestHeaders; }
        }

        /// <summary>    
        /// 获取或设置代理    
        /// </summary>    
        public WebProxy Proxy
        {
            get { return proxy; }
            set { proxy = value; }
        }

        /// <summary>    
        /// 获取或设置请求与响应的文本编码方式    
        /// </summary>    
        public Encoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        /// <summary>    
        /// 获取或设置响应的html代码    
        /// </summary>    
        public string RespHtml
        {
            get { return respHtml; }
            set { respHtml = value; }
        }

        /// <summary>    
        /// 获取或设置与请求关联的Cookie容器    
        /// </summary>    
        public CookieContainer CookieContainer
        {
            get { return cc; }
            set { cc = value; }
        }

        /// <summary>    
        ///  获取网页源代码    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <returns></returns>    
        public string GetHtml(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        /// <summary>    
        /// 下载文件    
        /// </summary>    
        /// <param name="url">文件URL地址</param>    
        /// <param name="filename">文件保存完整路径</param>    
        public void DownloadFile(string url, string filename)
        {
            FileStream fs = null;
            try
            {
                HttpWebRequest request = CreateRequest(url, "GET");
                byte[] data = GetData(request);
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                fs.Write(data, 0, data.Length);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>    
        /// 从指定URL下载数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <returns></returns>    
        public byte[] GetData(string url)
        {
            HttpWebRequest request = CreateRequest(url, "GET");
            return GetData(request);
        }

        /// <summary>    
        /// 向指定URL发送文本数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="postData">urlencode编码的文本数据</param>    
        /// <returns></returns>    
        public string Post(string url, string postData)
        {
            byte[] data = encoding.GetBytes(postData);
            return Post(url, data);
        }

        /// <summary>    
        /// 向指定URL发送字节数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="postData">发送的字节数组</param>    
        /// <returns></returns>    
        public string Post(string url, byte[] postData)
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.KeepAlive = true;
            PostData(request, postData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        /// <summary>    
        /// 向指定网址发送mulitpart编码的数据    
        /// </summary>    
        /// <param name="url">网址</param>    
        /// <param name="mulitpartForm">mulitpart form data</param>    
        /// <returns></returns>    
        public string Post(string url, MultipartForm mulitpartForm)
        {
            HttpWebRequest request = CreateRequest(url, "POST");
            request.ContentType = mulitpartForm.ContentType;
            request.ContentLength = mulitpartForm.FormData.Length;
            request.KeepAlive = true;
            PostData(request, mulitpartForm.FormData);
            respHtml = encoding.GetString(GetData(request));
            return respHtml;
        }

        /// <summary>    
        /// 读取请求返回的数据    
        /// </summary>    
        /// <param name="request">请求对象</param>    
        /// <returns></returns>    
        private byte[] GetData(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            responseHeaders = response.Headers;
            //SaveCookiesToDisk();

            DownloadEventArgs args = new DownloadEventArgs();
            if (responseHeaders[HttpResponseHeader.ContentLength] != null)
                args.TotalBytes = Convert.ToInt32(responseHeaders[HttpResponseHeader.ContentLength]);

            MemoryStream ms = new MemoryStream();
            int count = 0;
            byte[] buf = new byte[bufferSize];
            while ((count = stream.Read(buf, 0, buf.Length)) > 0)
            {
                ms.Write(buf, 0, count);
                if (DownloadProgressChanged != null)
                {
                    args.BytesReceived += count;
                    args.ReceivedData = new byte[count];
                    Array.Copy(buf, args.ReceivedData, count);
                    DownloadProgressChanged(this, args);
                }
            }
            stream.Close();
            //解压    
            if (ResponseHeaders[HttpResponseHeader.ContentEncoding] != null)
            {
                MemoryStream msTemp = new MemoryStream();
                count = 0;
                buf = new byte[100];
                switch (ResponseHeaders[HttpResponseHeader.ContentEncoding].ToLower())
                {
                    case "gzip":
                        GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                        while ((count = gzip.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        return msTemp.ToArray();
                    case "deflate":
                        DeflateStream deflate = new DeflateStream(ms, CompressionMode.Decompress);
                        while ((count = deflate.Read(buf, 0, buf.Length)) > 0)
                        {
                            msTemp.Write(buf, 0, count);
                        }
                        return msTemp.ToArray();
                    default:
                        break;
                }
            }
            return ms.ToArray();
        }

        /// <summary>    
        /// 发送请求数据    
        /// </summary>    
        /// <param name="request">请求对象</param>    
        /// <param name="postData">请求发送的字节数组</param>    
        private void PostData(HttpWebRequest request, byte[] postData)
        {
            int offset = 0;
            int sendBufferSize = bufferSize;
            int remainBytes = 0;
            Stream stream = request.GetRequestStream();
            UploadEventArgs args = new UploadEventArgs();
            args.TotalBytes = postData.Length;
            while ((remainBytes = postData.Length - offset) > 0)
            {
                if (sendBufferSize > remainBytes) sendBufferSize = remainBytes;
                stream.Write(postData, offset, sendBufferSize);
                offset += sendBufferSize;
                if (UploadProgressChanged != null)
                {
                    args.BytesSent = offset;
                    UploadProgressChanged(this, args);
                }
            }
            stream.Close();
        }

        /// <summary>    
        /// 创建HTTP请求    
        /// </summary>    
        /// <param name="url">URL地址</param>    
        /// <returns></returns>    
        private HttpWebRequest CreateRequest(string url, string method)
        {
            Uri uri = new Uri(url);

            if (uri.Scheme == "https")
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

            // Set a default policy level for the "http:" and "https" schemes.    
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Revalidate);
            HttpWebRequest.DefaultCachePolicy = policy;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = false;
            request.AllowWriteStreamBuffering = false;
            request.Method = method;
            if (proxy != null)
                request.Proxy = proxy;
            request.CookieContainer = cc;
            foreach (string key in requestHeaders.Keys)
            {
                request.Headers.Add(key, requestHeaders[key]);
            }
            requestHeaders.Clear();
            return request;
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>    
        /// 将Cookie保存到磁盘    
        /// </summary>    
        private static void SaveCookiesToDisk()
        {
            string cookieFile = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\webclient.cookie";
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formater.Serialize(fs, cc);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>    
        /// 从磁盘加载Cookie    
        /// </summary>    
        private static void LoadCookiesFromDisk()
        {
            cc = new CookieContainer();
            string cookieFile = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) + "\\webclient.cookie";
            if (!File.Exists(cookieFile))
                return;
            FileStream fs = null;
            try
            {
                fs = new FileStream(cookieFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formater = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                cc = (CookieContainer)formater.Deserialize(fs);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }

    /// <summary>    
    /// 对文件和文本数据进行Multipart形式的编码    
    /// </summary>    
    public class MultipartForm
    {
        #region 自定义变量
        private Encoding encoding;
        private MemoryStream ms;
        private string boundary;
        private byte[] formData;
        #endregion

        #region 获取编码后的字节数组
        /// <summary>    
        /// 获取编码后的字节数组    
        /// </summary>    
        public byte[] FormData
        {
            get
            {
                if (formData == null)
                {
                    byte[] buffer = encoding.GetBytes("--" + boundary + "--\r\n");
                    ms.Write(buffer, 0, buffer.Length);
                    formData = ms.ToArray();
                }
                return formData;
            }
        }
        #endregion

        #region 获取此编码内容的类型
        /// <summary>    
        /// 获取此编码内容的类型    
        /// </summary>    
        public string ContentType
        {
            get { return string.Format("multipart/form-data; boundary={0}", boundary); }
        }
        #endregion

        #region 获取或设置对字符串采用的编码类型
        /// <summary>    
        /// 获取或设置对字符串采用的编码类型    
        /// </summary>    
        public Encoding StringEncoding
        {
            set { encoding = value; }
            get { return encoding; }
        }
        #endregion

        #region 实例化
        /// <summary>    
        /// 实例化    
        /// </summary>    
        public MultipartForm()
        {
            boundary = string.Format("--{0}--", Guid.NewGuid());
            ms = new MemoryStream();
            encoding = Encoding.Default;
        }
        #endregion

        #region 添加一个文件  
        /// <summary>    
        /// 添加一个文件    
        /// </summary>    
        /// <param name="name">文件域名称</param>    
        /// <param name="filename">文件的完整路径</param>    
        public void AddFlie(string name, string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("尝试添加不存在的文件。", filename);
            FileStream fs = null;
            byte[] fileData = { };
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, fileData.Length);
                AddFlie(name, Path.GetFileName(filename), fileData, fileData.Length);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>    
        /// 添加一个文件    
        /// </summary>    
        /// <param name="name">文件域名称</param>    
        /// <param name="filename">文件名</param>    
        /// <param name="fileData">文件二进制数据</param>    
        /// <param name="dataLength">二进制数据大小</param>    
        public void AddFlie(string name, string filename, byte[] fileData, int dataLength)
        {
            if (dataLength <= 0 || dataLength > fileData.Length)
            {
                dataLength = fileData.Length;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}\r\n", boundary);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\n", name, filename);
            sb.AppendFormat("Content-Type: {0}\r\n", GetContentType(filename));
            sb.Append("\r\n");
            byte[] buf = encoding.GetBytes(sb.ToString());
            ms.Write(buf, 0, buf.Length);
            ms.Write(fileData, 0, dataLength);
            byte[] crlf = encoding.GetBytes("\r\n");
            ms.Write(crlf, 0, crlf.Length);
        }
        #endregion

        #region 添加字符串 
        /// <summary>    
        /// 添加字符串    
        /// </summary>    
        /// <param name="name">文本域名称</param>    
        /// <param name="value">文本值</param>    
        public void AddString(string name, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("--{0}\r\n", boundary);
            sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", name);
            sb.Append("\r\n");
            sb.AppendFormat("{0}\r\n", value);
            byte[] buf = encoding.GetBytes(sb.ToString());
            ms.Write(buf, 0, buf.Length);
        }
        #endregion

        #region 从注册表获取文件类型 

        /// <summary>    
        /// 从注册表获取文件类型    
        /// </summary>    
        /// <param name="filename">包含扩展名的文件名</param>    
        /// <returns>如：application/stream</returns>    
        private string GetContentType(string filename)
        {
            string contentType = "application/stream";
            try
            {
                string ext = Path.GetExtension(filename);
                if (MimeMapping.ContainsKey(ext))
                {
                    contentType = MimeMapping[ext].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contentType;
        }

        #endregion

        public static Hashtable MimeMapping = new Hashtable()
        {
            {".323", "text/h323"},
            {".asx", "video/x-ms-asf"},
            {".acx", "application/internet-property-stream"},
            {".ai", "application/postscript"},
            {".aif", "audio/x-aiff"},
            {".aiff", "audio/aiff"},
            {".axs", "application/olescript"},
            {".aifc", "audio/aiff"},
            {".asr", "video/x-ms-asf"},
            {".avi", "video/x-msvideo"},
            {".asf", "video/x-ms-asf"},
            {".au", "audio/basic"},
            {".application", "application/x-ms-application"},
            {".bin", "application/octet-stream"},
            {".bas", "text/plain"},
            {".bcpio", "application/x-bcpio"},
            {".bmp", "image/bmp"},
            {".cdf", "application/x-cdf"},
            {".cat", "application/vndms-pkiseccat"},
            {".crt", "application/x-x509-ca-cert"},
            {".c", "text/plain"},
            {".css", "text/css"},
            {".cer", "application/x-x509-ca-cert"},
            {".crl", "application/pkix-crl"},
            {".cmx", "image/x-cmx"},
            {".csh", "application/x-csh"},
            {".cod", "image/cis-cod"},
            {".cpio", "application/x-cpio"},
            {".clp", "application/x-msclip"},
            {".crd", "application/x-mscardfile"},
            {".deploy", "application/octet-stream"},
            {".dll", "application/x-msdownload"},
            {".dot", "application/msword"},
            {".doc", "application/msword"},
            {".dvi", "application/x-dvi"},
            {".dir", "application/x-director"},
            {".dxr", "application/x-director"},
            {".der", "application/x-x509-ca-cert"},
            {".dib", "image/bmp"},
            {".dcr", "application/x-director"},
            {".disco", "text/xml"},
            {".exe", "application/octet-stream"},
            {".etx", "text/x-setext"},
            {".evy", "application/envoy"},
            {".eml", "message/rfc822"},
            {".eps", "application/postscript"},
            {".flr", "x-world/x-vrml"},
            {".fif", "application/fractals"},
            {".gtar", "application/x-gtar"},
            {".gif", "image/gif"},
            {".gz", "application/x-gzip"},
            {".hta", "application/hta"},
            {".htc", "text/x-component"},
            {".htt", "text/webviewhtml"},
            {".h", "text/plain"},
            {".hdf", "application/x-hdf"},
            {".hlp", "application/winhlp"},
            {".html", "text/html"},
            {".htm", "text/html"},
            {".hqx", "application/mac-binhex40"},
            {".isp", "application/x-internet-signup"},
            {".iii", "application/x-iphone"},
            {".ief", "image/ief"},
            {".ivf", "video/x-ivf"},
            {".ins", "application/x-internet-signup"},
            {".ico", "image/x-icon"},
            {".jpg", "image/jpeg"},
            {".jfif", "image/pjpeg"},
            {".jpe", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".lsx", "video/x-la-asf"},
            {".latex", "application/x-latex"},
            {".lsf", "video/x-la-asf"},
            {".manifest", "application/x-ms-manifest"},
            {".mhtml", "message/rfc822"},
            {".mny", "application/x-msmoney"},
            {".mht", "message/rfc822"},
            {".mid", "audio/mid"},
            {".mpv2", "video/mpeg"},
            {".man", "application/x-troff-man"},
            {".mvb", "application/x-msmediaview"},
            {".mpeg", "video/mpeg"},
            {".m3u", "audio/x-mpegurl"},
            {".mdb", "application/x-msaccess"},
            {".mpp", "application/vnd.ms-project"},
            {".m1v", "video/mpeg"},
            {".mpa", "video/mpeg"},
            {".me", "application/x-troff-me"},
            {".m13", "application/x-msmediaview"},
            {".movie", "video/x-sgi-movie"},
            {".m14", "application/x-msmediaview"},
            {".mpe", "video/mpeg"},
            {".mp2", "video/mpeg"},
            {".mov", "video/quicktime"},
            {".mp3", "audio/mpeg"},
            {".mpg", "video/mpeg"},
            {".ms", "application/x-troff-ms"},
            {".nc", "application/x-netcdf"},
            {".nws", "message/rfc822"},
            {".oda", "application/oda"},
            {".ods", "application/oleobject"},
            {".pmc", "application/x-perfmon"},
            {".p7r", "application/x-pkcs7-certreqresp"},
            {".p7b", "application/x-pkcs7-certificates"},
            {".p7s", "application/pkcs7-signature"},
            {".pmw", "application/x-perfmon"},
            {".ps", "application/postscript"},
            {".p7c", "application/pkcs7-mime"},
            {".pbm", "image/x-portable-bitmap"},
            {".ppm", "image/x-portable-pixmap"},
            {".pub", "application/x-mspublisher"},
            {".pnm", "image/x-portable-anymap"},
            {".png", "image/png"},
            {".pml", "application/x-perfmon"},
            {".p10", "application/pkcs10"},
            {".pfx", "application/x-pkcs12"},
            {".p12", "application/x-pkcs12"},
            {".pdf", "application/pdf"},
            {".pps", "application/vnd.ms-powerpoint"},
            {".p7m", "application/pkcs7-mime"},
            {".pko", "application/vndms-pkipko"},
            {".ppt", "application/vnd.ms-powerpoint"},
            {".pmr", "application/x-perfmon"},
            {".pma", "application/x-perfmon"},
            {".pot", "application/vnd.ms-powerpoint"},
            {".prf", "application/pics-rules"},
            {".pgm", "image/x-portable-graymap"},
            {".qt", "video/quicktime"},
            {".ra", "audio/x-pn-realaudio"},
            {".rgb", "image/x-rgb"},
            {".ram", "audio/x-pn-realaudio"},
            {".rmi", "audio/mid"},
            {".ras", "image/x-cmu-raster"},
            {".roff", "application/x-troff"},
            {".rtf", "application/rtf"},
            {".rtx", "text/richtext"},
            {".sv4crc", "application/x-sv4crc"},
            {".spc", "application/x-pkcs7-certificates"},
            {".setreg", "application/set-registration-initiation"},
            {".snd", "audio/basic"},
            {".stl", "application/vndms-pkistl"},
            {".setpay", "application/set-payment-initiation"},
            {".stm", "text/html"},
            {".shar", "application/x-shar"},
            {".sh", "application/x-sh"},
            {".sit", "application/x-stuffit"},
            {".spl", "application/futuresplash"},
            {".sct", "text/scriptlet"},
            {".scd", "application/x-msschedule"},
            {".sst", "application/vndms-pkicertstore"},
            {".src", "application/x-wais-source"},
            {".sv4cpio", "application/x-sv4cpio"},
            {".tex", "application/x-tex"},
            {".tgz", "application/x-compressed"},
            {".t", "application/x-troff"},
            {".tar", "application/x-tar"},
            {".tr", "application/x-troff"},
            {".tif", "image/tiff"},
            {".txt", "text/plain"},
            {".texinfo", "application/x-texinfo"},
            {".trm", "application/x-msterminal"},
            {".tiff", "image/tiff"},
            {".tcl", "application/x-tcl"},
            {".texi", "application/x-texinfo"},
            {".tsv", "text/tab-separated-values"},
            {".ustar", "application/x-ustar"},
            {".uls", "text/iuls"},
            {".vcf", "text/x-vcard"},
            {".wps", "application/vnd.ms-works"},
            {".wav", "audio/wav"},
            {".wrz", "x-world/x-vrml"},
            {".wri", "application/x-mswrite"},
            {".wks", "application/vnd.ms-works"},
            {".wmf", "application/x-msmetafile"},
            {".wcm", "application/vnd.ms-works"},
            {".wrl", "x-world/x-vrml"},
            {".wdb", "application/vnd.ms-works"},
            {".wsdl", "text/xml"},
            {".xap", "application/x-silverlight-app"},
            {".xml", "text/xml"},
            {".xlm", "application/vnd.ms-excel"},
            {".xaf", "x-world/x-vrml"},
            {".xla", "application/vnd.ms-excel"},
            {".xls", "application/vnd.ms-excel"},
            {".xof", "x-world/x-vrml"},
            {".xlt", "application/vnd.ms-excel"},
            {".xlc", "application/vnd.ms-excel"},
            {".xsl", "text/xml"},
            {".xbm", "image/x-xbitmap"},
            {".xlw", "application/vnd.ms-excel"},
            {".xpm", "image/x-xpixmap"},
            {".xwd", "image/x-xwindowdump"},
            {".xsd", "text/xml"},
            {".z", "application/x-compress"},
            {".zip", "application/x-zip-compressed"},
        };


    }
}
