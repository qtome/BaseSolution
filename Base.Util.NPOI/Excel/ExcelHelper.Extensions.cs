using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace Base.Util.NPOI.Excel
{
    /// <summary>
    /// 工作表-行相关
    /// </summary>
    public static partial class ExcelHelper
    {
        /// <summary>
        /// 获取工作表所有行
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static List<IRow> GetRows(this ISheet sheet)
        {
            var rows = new List<IRow>();
            foreach (IRow row in sheet)
            {
                rows.Add(row);
            }
            return rows;
        }

        /// <summary>
        /// 获取所有工作表
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static List<ISheet> GetSheets(this IWorkbook workbook)
        {
            var sheets = new List<ISheet>();
            foreach (var sheet in workbook)
            {
                sheets.Add(sheet);
            }
            return sheets;
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="values">数据</param>
        /// <returns></returns>
        public static void FillRowData(this IRow row, List<string> values)
        {
            if (values.Count == 0) return;
            for (int i = 0; i < values.Count; i++)
            {
                var cell = row.CreateCell(i);
                cell.SetCellValue(values[i]);
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="propertyInfos">属性列</param>
        /// <param name="model">数据</param>
        /// <returns></returns>
        public static void FillRowData<T>(this IRow row, List<PropertyInfo> propertyInfos, T model)
        {
            if (propertyInfos.Count == 0) return;
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                var prop = propertyInfos[i];
                var obj = prop.GetValue(model);
                var value = obj == null ? string.Empty : obj.ToString();
                var cell = row.CreateCell(i);
                var attrubte = Attribute.GetCustomAttribute(prop, typeof(ExcelAttrubte)) as ExcelAttrubte;
                cell.SetCellStyle(attrubte);
                cell.SetCellValue(value);
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="attrubtes">属性列</param>
        /// <returns></returns>
        public static void FillRowData(this IRow row, List<ExcelAttrubte> attrubtes, Color? cellFillColor = null, Color? cellFontColor = null)
        {
            if (attrubtes.Count == 0) return;
            for (int i = 0; i < attrubtes.Count; i++)
            {
                var attrubte = attrubtes[i];
                row.SetColumnStyle(attrubte, i);
                var cell = row.CreateCell(i);
                cell.SetCellStyle(attrubte, cellFillColor, cellFontColor);
                cell.SetCellValue(attrubte.GetColumnName());
            }
        }

        /// <summary>
        /// 设置列样式
        /// </summary>
        /// <param name="row"></param>
        /// <param name="attrubte">属性列</param>
        /// <returns></returns>
        private static void SetColumnStyle(this IRow row, ExcelAttrubte attrubte, int index)
        {
            if (attrubte == null) return;
            if (attrubte.ColumnWidth > 0)
            {
                row.Sheet.SetColumnWidth(index, 256 * attrubte.ColumnWidth + 200);
            }
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="attrubte"></param>
        /// <param name="cellFillColor"></param>
        /// <param name="cellFontColor"></param>
        private static void SetCellStyle(this ICell cell, ExcelAttrubte attrubte, Color? cellFillColor = null, Color? cellFontColor = null)
        {
            if (attrubte == null) return;
            bool hasStyle = false;// 是否有样式变更
            ICellStyle style = cell.Row.Sheet.Workbook.CreateCellStyle();
            if (attrubte.CellFillColor.HasValue || cellFillColor.HasValue)
            {
                hasStyle = true;
                var index = GetColorIndex(cellFillColor ?? attrubte.CellFillColor.Value);
                style.FillForegroundColor = index;
                style.FillPattern = FillPattern.SolidForeground;
            }
            bool hasFontStyle = false;// 字体是否有样式变更
            IFont font = cell.Row.Sheet.Workbook.CreateFont();
            if (attrubte.CellFontColor.HasValue || cellFontColor.HasValue)
            {
                hasStyle = true;
                hasFontStyle = true;
                var index = GetColorIndex(cellFontColor ?? attrubte.CellFontColor.Value);
                font.Color = index;
            }
            if (hasFontStyle) style.SetFont(font);
            if (hasStyle) cell.CellStyle = style;
        }

        /// <summary>
        /// 获取单元格配色Index
        /// </summary>
        /// <param name="SystemColour"></param>
        /// <returns></returns>
        public static short GetColorIndex(System.Drawing.Color SystemColour)
        {
            short s = 0;
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFPalette XlPalette = workbook.GetCustomPalette();
            HSSFColor XlColour = XlPalette.FindColor(SystemColour.R, SystemColour.G, SystemColour.B);
            if (XlColour == null)
            {
                XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                if (XlColour != null)
                {
                    s = XlColour.Indexed;
                }
            }
            else
                s = XlColour.Indexed;
            return s;
        }
    }
}
