using System.Collections.Generic;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 多级下拉框 统计类
    /// </summary>
    public class BaseMultiLevelCountDto : BaseDropDownListItem
    {
        /// <summary>
        /// 统计值
        /// </summary>
        public virtual decimal Count { get; set; }

        /// <summary>
        /// 下级子数据
        /// </summary>
        public List<BaseMultiLevelCountDto> Children { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseMultiLevelCountDto()
        {
            Children = new List<BaseMultiLevelCountDto>();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static BaseMultiLevelCountDto CreateModel(string value, string text)
        {
            return CreateModel(value, text, 0, new List<BaseMultiLevelCountDto>());
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static BaseMultiLevelCountDto CreateModel(string value, string text, decimal count)
        {
            return CreateModel(value, text, count, new List<BaseMultiLevelCountDto>());
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        /// <param name="count"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static BaseMultiLevelCountDto CreateModel(string value, string text, decimal count, List<BaseMultiLevelCountDto> children)
        {
            return new BaseMultiLevelCountDto()
            {
                Value = value,
                Text = text,
                Count = count,
                Children = children,
            };
        }
    }
}
