using Microsoft.Extensions.Configuration;

namespace Base.Util.Core8.Utils
{
    /// <summary>
    /// 配置读取类
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// 配置信息接口
        /// </summary>
        private static IConfiguration _configuration;

        /// <summary>
        /// 配置文件
        /// </summary>
        private static readonly string fileName = "appsetting.json";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public AppSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 获取配置信息接口
        /// </summary>
        private static void GetConfiguration()
        {
            try
            {
                if (_configuration == null)
                {
                    var builder = new ConfigurationBuilder();
                    string currentDirectory = Directory.GetCurrentDirectory();
                    builder.SetBasePath(currentDirectory);  // 获取当前程序执行目录
                    builder.AddJsonFile(fileName, false, true);
                    _configuration = builder.Build();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置信息（字符串）
        /// </summary>
        /// <param name="key">配置键</param>
        /// <returns></returns>
        public static string GetSection(string key)
        {
            try
            {
                GetConfiguration();

                var array = key.Split('.');
                IConfigurationSection section = null;
                foreach (var item in array)
                {
                    if (section == null)
                    {
                        section = _configuration.GetSection(item);
                    }
                    else
                    {
                        section = section.GetSection(item);
                    }
                }
                return section?.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置信息（对象）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">配置键</param>
        /// <returns></returns>
        public static T GetSection<T>(string key)
        {

            try
            {
                GetConfiguration();
                var array = key.Split('.');
                IConfigurationSection section = null;
                foreach (var item in array)
                {
                    if (section == null)
                    {
                        section = _configuration.GetSection(item);
                    }
                    else
                    {
                        section = section.GetSection(item);
                    }
                }
                if (section == null) return default(T);
                T result = Activator.CreateInstance<T>();
                section.Bind(result);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置信息（集合）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">配置键</param>
        /// <returns></returns>
        public static List<T> GetSections<T>(string key)
        {
            List<T> result = new List<T>();
            try
            {
                GetConfiguration();
                var array = key.Split('.');
                IConfigurationSection section = null;
                foreach (var item in array)
                {
                    if (section == null)
                    {
                        section = _configuration.GetSection(item);
                    }
                    else
                    {
                        section = section.GetSection(item);
                    }
                }
                if (section != null) section.Bind(result);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
