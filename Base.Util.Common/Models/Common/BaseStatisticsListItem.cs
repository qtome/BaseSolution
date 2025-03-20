namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 统计用基础信息类
    /// </summary>
    public class BaseStatisticsListItem : BaseDropDownListItem
    {
        /// <summary>
        /// 统计后台值
        /// </summary>
        public override string Value { get; set; }
        /// <summary>
        /// 统计描述
        /// </summary>
        public override string Text { get; set; }
        /// <summary>
        /// 统计值
        /// </summary>
        public virtual int Count { get; set; }
        /// <summary>
        /// 统计类型
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 创建统计信息类
        /// </summary>
        /// <param name="value">后台值</param>
        /// <param name="text">描述</param>
        /// <param name="count">统计值</param>
        /// <param name="type">类别</param>
        /// <returns></returns>
        protected static BaseStatisticsListItem CreateBaseModel(string value, string text, int count, string type)
        {
            return new BaseStatisticsListItem() { Value = value, Text = text, Count = count, Type = type };
        }
    }
}
