using System.Collections.Generic;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 多级下拉框 选项类
    /// </summary>
    public class BaseMultiLevelDropDownDto : BaseDropDownListItem
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseMultiLevelDropDownDto()
        {
            Children = new List<BaseMultiLevelDropDownDto>();
        }

        /// <summary>
        /// 下级子数据
        /// </summary>
        public virtual List<BaseMultiLevelDropDownDto> Children { get; set; }

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static BaseMultiLevelDropDownDto CreateModel(string text, string value)
        {
            return CreateModel(text, value, new List<BaseMultiLevelDropDownDto>());
        }

        /// <summary>
        /// 创建单例
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static BaseMultiLevelDropDownDto CreateModel(string text, string value, List<BaseMultiLevelDropDownDto> children)
        {
            return new BaseMultiLevelDropDownDto()
            {
                Text = text,
                Value = value,
                Children = children,
            };
        }
    }
}
