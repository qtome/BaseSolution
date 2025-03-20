namespace Base.Util.Common.Models.Common
{
    /// <summary>
    /// 分组类
    /// </summary>
    /// <typeparam name="TModel">主信息</typeparam>
    /// <typeparam name="TGroup">分组信息</typeparam>
    public class BaseGroupModel<TModel, TGroup>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseGroupModel()
        {
            Key = default;
            Value = default;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public BaseGroupModel(TModel key, TGroup value)
        {
            Key = key;
            Value = value;
        }


        /// <summary>
        /// 主信息
        /// </summary>
        public TModel Key { get; set; }

        /// <summary>
        /// 副信息
        /// </summary>
        public TGroup Value { get; set; }

    }
}
