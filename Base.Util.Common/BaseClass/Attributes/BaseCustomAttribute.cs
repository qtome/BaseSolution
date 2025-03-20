using System;

namespace Base.Util.Common.BaseClass.Attributes
{
    /// <summary>
    /// 自定义属性基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
      AllowMultiple = false)]
    public class BaseCustomAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        protected virtual string Name { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseCustomAttribute()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseCustomAttribute(string name)
        {
            Name = name;
        }
    }
}
