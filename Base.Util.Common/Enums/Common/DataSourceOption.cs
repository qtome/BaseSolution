using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.Util.Common.Enums.Common
{
    /// <summary>
    /// 数据来源枚举
    /// </summary>
    public enum DataSourceOption
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        未知 = 0,

        /// <summary>
        /// 微信小程序提交
        /// </summary>
        [Description("微信小程序提交")]
        微信小程序提交 = 1,
        /// <summary>
        /// PC提交
        /// </summary>
        [Description("PC提交")]
        PC提交 = 2,
        /// <summary>
        /// PC导入
        /// </summary>
        [Description("PC导入")]
        PC导入 = 3,
        /// <summary>
        /// 第三方接入
        /// </summary>
        [Description("第三方接入")]
        第三方接入 = 4,

    }
}
