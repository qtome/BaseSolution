using System.Collections.Generic;

namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 多级下拉框 选项类
    /// </summary>
    public class BaseMultDropDownListItemDto<T> : BaseDropDownListItem
        where T : class, new()
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        private BaseMultDropDownListItemDto()
        {
            Children = new List<T>();
        }

        /// <summary>
        /// 下级子数据
        /// </summary>
        public List<T> Children { get; set; }


        /// <summary>
        /// 创建单例
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        public static BaseMultDropDownListItemDto<T> CreateModel(string text, string value, List<T> children)
        {
            return new BaseMultDropDownListItemDto<T>()
            {
                Text = text,
                Value = value,
                Children = children,
            };
        }
    }

}
