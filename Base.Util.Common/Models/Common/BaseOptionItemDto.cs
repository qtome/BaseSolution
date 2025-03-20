namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 选项信息类（用来展示单选复选）
    /// </summary>
    public class BaseOptionItemDto : BaseDropDownListItem
    {
        /// <summary>
        /// 选项框  值
        /// </summary>
        public override string Value { get; set; }
        /// <summary>
        /// 选项框  文本
        /// </summary>
        public override string Text { get; set; }
        /// <summary>
        /// 是否是多选
        /// </summary>
        public bool MultiSelect { get; set; }
        /// <summary>
        /// 是否是必选
        /// </summary>
        public bool Required { get; set; }
    }
}
