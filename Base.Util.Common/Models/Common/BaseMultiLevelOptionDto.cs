using System.Collections.Generic;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 多级复杂选项类
    /// </summary>
    public class BaseMultiLevelOptionDto : BaseOptionItemDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private BaseMultiLevelOptionDto()
        {
            Children = new List<BaseMultiLevelOptionDto>();
        }

        /// <summary>
        /// 下级选项数据
        /// </summary>
        public List<BaseMultiLevelOptionDto> Children { get; set; }


        /// <summary>
        /// 创建单例
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static BaseMultiLevelOptionDto CreateModel(string text, string value, List<BaseMultiLevelOptionDto> children)
        {
            return new BaseMultiLevelOptionDto()
            {
                Text = text,
                Value = value,
                Children = children,
            };
        }
    }
}
