using Base.Util.NPOI.Excel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Util.NPOI.Excel
{
    /// <summary>
    /// 工作表相关
    /// </summary>
    public partial class ExcelHelper
    {
        /// <summary>
        /// 将数据填充到工作表中
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="list">行数据</param>
        /// <returns></returns>
        public static void FillData<T>(this ISheet sheet, List<T> list)
             where T : class, new()
        {
            if (sheet == null) return;
            try
            {
                var rows = sheet.CreateRow(list.Count + 1);
                var type = typeof(T);
                var props = type.GetProperties().ToList();
                props = props.Where(ww => ww.CustomAttributes.Any(aa => aa.AttributeType == typeof(ExcelAttrubte))).ToList();//找出有Excel属性的字段集合
                var columnHead = Attribute.GetCustomAttribute(type, typeof(ExcelAttrubte)) as ExcelAttrubte;
                var columns = props.Select(ss => (Attribute.GetCustomAttribute(ss, typeof(ExcelAttrubte)) as ExcelAttrubte)).ToList();
                for (int i = 0; i <= list.Count; i++) //数据量 + 列头
                {
                    var row = sheet.CreateRow(i);//创建行
                    if (i == 0)
                    {   // 第一行是列头
                        row.FillRowData(columns, columnHead?.CellFillColor, columnHead?.CellFontColor);
                    }
                    else
                    {   // 第二行开始是数据
                        var values = new List<string>();
                        var model = list[i - 1];
                        row.FillRowData(props, model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
