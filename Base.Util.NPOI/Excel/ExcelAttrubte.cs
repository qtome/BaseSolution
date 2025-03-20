using System;
using System.Drawing;

namespace Base.Util.NPOI.Excel
{
    /// <summary>
    /// Excel相关属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ExcelAttrubte : Attribute
    {
        /// <summary>
        /// 导出字段名
        /// </summary>
        protected string Name { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public ExcelAttrubte(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fillColor"></param>
        public ExcelAttrubte(string name
            , string fillColor)
        {
            Name = name;
            CellFillColor = GetColor(fillColor);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fillColor"></param>
        /// <param name="fontColor"></param>
        public ExcelAttrubte(string name
            , string fillColor
            , string fontColor)
        {
            Name = name;
            CellFillColor = GetColor(fillColor);
            CellFontColor = GetColor(fontColor);
        }

        /// <summary>
        /// 单元格填充颜色
        /// </summary>
        public Color? CellFillColor { get; }
        /// <summary>
        /// 单元格字体颜色
        /// </summary>
        public Color? CellFontColor { get; }

        /// <summary>
        /// 列宽度
        /// </summary>
        public int ColumnWidth { get; set; } = 0;

        /// <summary>
        /// 获取Excel列名
        /// </summary>
        /// <returns></returns>
        public string GetColumnName()
        {
            return Name;
        }

        /// <summary>
        /// 获取Excel列名
        /// </summary>
        /// <returns></returns>
        private Color? GetColor(string color)
        {
            if (string.IsNullOrEmpty(color)) return null;
            var colors = color.Split(",");
            if (colors.Length == 3)
            {
                try
                {
                    return Color.FromArgb(Convert.ToInt32(colors[0]), Convert.ToInt32(colors[1]), Convert.ToInt32(colors[2]));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            if (colors.Length == 4)
            {
                try
                {
                    return Color.FromArgb(Convert.ToInt32(colors[0]), Convert.ToInt32(colors[1]), Convert.ToInt32(colors[2]), Convert.ToInt32(colors[3]));
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
