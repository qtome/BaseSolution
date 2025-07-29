using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Util.Configuration.AutoMapper
{
    /// <summary>
    /// AutoMapper映射规则通用类
    /// </summary>
    public class MapperCommonCustomConfiguration
        : MapperConfiguration
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configure"></param>
        public MapperCommonCustomConfiguration(Action<IMapperConfigurationExpression> configure)
            : this(Build(configure))
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configurationExpression"></param>
        private MapperCommonCustomConfiguration(MapperConfigurationExpression configurationExpression)
            : base(configurationExpression)
        {
        }

        /// <summary>
        /// 构建表达关系
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        private static MapperConfigurationExpression Build(Action<IMapperConfigurationExpression> configure)
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                var profiles = AutoMapperCommonProfileConfig.GetMapperProfiles();
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
                configure(cfg);
            };
            MapperConfigurationExpression mapperConfigurationExpression = new MapperConfigurationExpression();
            action(mapperConfigurationExpression);
            return mapperConfigurationExpression;
        }
    }
}
