using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Util.Common.BaseClass.Attributes
{
    /// <summary>
    /// 数据来源属性
    /// </summary>
    public class DataSourceAttribute : BaseCustomAttribute
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataSource">数据来源</param>
        public DataSourceAttribute(string dataSource) : base(dataSource)
        {

        }

        /// <summary>
        /// 获取数据来源
        /// </summary>
        /// <returns></returns>
        public string GetDataSource()
        {
            return Name;
        }
    }
}
