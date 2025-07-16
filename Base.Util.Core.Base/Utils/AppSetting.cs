using Microsoft.Extensions.Configuration;

namespace Base.Util.Core.Base.Utils
{
    /// <summary>
    /// AppJson配置读取帮助类
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Json配置项
        /// </summary>
        private static IConfiguration Configuration;
        /// <summary>
        /// 默认配置文件名
        /// </summary>
        private static string CONFIG_FILE = "appsettings.json";
        /// <summary>
        /// 默认配置文件名集合
        /// </summary>
        private static readonly HashSet<string> CONFIG_FILE_Collection = new HashSet<string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">Json配置项</param>
        public AppSetting(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="json">Json配置项</param>
        public AppSetting(string json)
        {
            CONFIG_FILE = json;
            ConfigurationBuild();
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        private static void GetConfiguration()
        {
            try
            {
                if (Configuration == null)
                {
                    ConfigurationBuild();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建Configuration
        /// </summary>
        /// <returns></returns>
        private static void ConfigurationBuild()
        {
            try
            {
                if (!CONFIG_FILE_Collection.Contains(CONFIG_FILE)) CONFIG_FILE_Collection.Add(CONFIG_FILE);
                var build = new ConfigurationBuilder();
                foreach (var file in CONFIG_FILE_Collection)
                {
                    build.SetBasePath(Directory.GetCurrentDirectory());  // 获取当前程序执行目录
                    build.AddJsonFile(file, true, true);
                }
                Configuration = build.Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置默认配置Json文件
        /// </summary>
        /// <param name="file"></param>
        public static void SetDefaultConfigJsonFile(string file)
        {
            try
            {
                if (Configuration == null) CONFIG_FILE = file;
                ConfigurationBuild();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 添加配置Json文件集合
        /// </summary>
        /// <param name="files"></param>
        public static void AddConfigJsonFile(params string[] files)
        {
            try
            {
                foreach (var file in files)
                {
                    if (!CONFIG_FILE_Collection.Contains(file))
                    {
                        CONFIG_FILE_Collection.Add(file);
                    }
                }
                ConfigurationBuild();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="configKey">多个节点可以用英文.号隔开</param>
        /// <returns></returns>
        public static string GetConfiguration(string configKey)
        {
            try
            {
                GetConfiguration();
                if (configKey.Contains("."))
                {
                    IConfigurationSection child = null;
                    foreach (string key in configKey.Split('.'))
                    {
                        if (child == null)
                            child = Configuration.GetSection(key);
                        else
                            child = child.GetSection(key);
                    }
                    return (child == null) ? "" : child.Value;
                }
                else
                {
                    return Configuration.GetSection(configKey).Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public static T GetConfiguration<T>(string configKey)
        {
            T model = Activator.CreateInstance<T>();
            try
            {
                GetConfiguration();
                if (configKey.Contains("."))
                {
                    IConfigurationSection configurationSection = null;
                    string[] array = configKey.Split('.');
                    foreach (string key in array)
                    {
                        configurationSection = ((configurationSection != null) ? configurationSection.GetSection(key) : Configuration.GetSection(key));
                    }

                    configurationSection.Bind(model);
                }
                else
                {
                    Configuration.GetSection(configKey).Bind(model);
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="configKey">多个节点可以用英文.号隔开</param>
        /// <returns></returns>
        public static List<T> GetConfigurations<T>(string configKey)
        {
            List<T> result = new List<T>();
            try
            {
                GetConfiguration();
                if (configKey.Contains("."))
                {
                    IConfigurationSection child = null;
                    foreach (string key in configKey.Split('.'))
                    {
                        if (child == null)
                            child = Configuration.GetSection(key);
                        else
                            child = child.GetSection(key);
                    }
                    child.Bind(result);
                }
                else
                {
                    Configuration.GetSection(configKey).Bind(result);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
