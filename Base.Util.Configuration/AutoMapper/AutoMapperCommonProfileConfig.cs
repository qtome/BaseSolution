using AutoMapper;
using Base.Util.Configuration.AutoMapper.Profiles;

namespace Base.Util.Configuration.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public class AutoMapperCommonProfileConfig
    {
        /// <summary>
        /// 获取通用映射规则文件
        /// </summary>
        /// <returns></returns>
        internal static List<Profile> GetMapperProfiles()
        {
            var profiles = new List<Profile>();

            profiles.Add(new GenericsProfile());// 泛型模型映射规则
            return profiles;
        }


        /// <summary>
        /// 注册映射规范
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration RegisterMappings()
        {
            #region 生成映射规范配置

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                var profiles = GetMapperProfiles();// 通用映射规则
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            #endregion

            #region 验证映射规则

            configuration.AssertConfigurationIsValid();

            #endregion

            return configuration;
        }
    }
}
