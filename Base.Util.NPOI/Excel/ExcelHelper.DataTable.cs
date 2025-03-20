using Base.Util.NPOI.Excel;
using Base.Util.NPOI.Internal;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

namespace Base.Util.NPOI.Excel
{
    /// <summary>
    /// DataTable数据操作
    /// </summary>
    public static partial class ExcelHelper
    {
        /// <summary>
        /// 将Sheet中的数据转换成DataTable格式
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this ISheet sheet)
        {
            if (sheet == null) throw new ArgumentNullException(nameof(sheet));
            try
            {
                DataTable dt = new DataTable(sheet.SheetName);
                List<IRow> rows = sheet.GetRows();
                IRow columnRow = null;
                for (int i = 0; i < rows.Count; i++)
                {
                    var row = rows[i];
                    if (i == 0)
                    {   // 第一行是列头
                        columnRow = row;
                        row.FillDataTableColumns(dt);
                    }
                    else
                    {   // 第二行开始是数据
                        row.FillDataTableRows(columnRow, dt);
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将行数据转换成DataColumn 填充到DataTable
        /// </summary>
        /// <param name="row"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void FillDataTableColumns(this IRow row, DataTable dt)
        {
            for (int i = row.FirstCellNum; i < row.Cells.Count; i++)
            {
                ICell cell = row.GetCell(i);
                if (cell != null)
                {
                    string cellValue = cell.StringCellValue.Trim();
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        DataColumn dataColumn = new DataColumn(cellValue);
                        dt.Columns.Add(dataColumn);
                    }
                }
            }
        }

        /// <summary>
        /// 将行数据转换成DataRow 填充到DataTable
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnRow">列头行</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static void FillDataTableRows(this IRow row, IRow columnRow, DataTable dt)
        {
            DataRow dataRow = dt.NewRow();
            if (columnRow == null)
            {   // 旧版有瑕疵
                // 判断行单元格数据是否比dt的列数多,取值少的
                var cellCount = row.Cells.Count > dt.Columns.Count ? dt.Columns.Count : row.Cells.Count;
                if (cellCount == 0) return;
                for (int i = row.FirstCellNum; i < cellCount; i++)
                {
                    ICell cellData = row.GetCell(i);
                    if (cellData != null)
                    {
                        //判断是否为数字型，必须加这个判断不然下面的日期判断会异常
                        if (cellData.CellType == CellType.Numeric)
                        {
                            //判断是否日期类型
                            if (DateUtil.IsCellDateFormatted(cellData))
                            {
                                dataRow[i] = cellData.DateCellValue;
                            }
                            else
                            {
                                dataRow[i] = cellData.ToString().Trim();
                            }
                        }
                        else
                        {
                            dataRow[i] = cellData.ToString().Trim();
                        }
                    }
                }
            }
            else
            {   // 根据列头的行数
                int index = 0;
                foreach (ICell cell in columnRow.Cells)
                {
                    ICell cellData = row.Cells.FirstOrDefault(ff => ff.Address.Column == cell.Address.Column);
                    if (cellData == null)
                    {
                        dataRow[index] = null;
                    }
                    else
                    {
                        //判断是否为数字型，必须加这个判断不然下面的日期判断会异常
                        if (cellData.CellType == CellType.Numeric)
                        {
                            //判断是否日期类型
                            if (DateUtil.IsCellDateFormatted(cellData))
                            {
                                dataRow[index] = cellData.DateCellValue;
                            }
                            else
                            {
                                dataRow[index] = cellData.ToString().Trim();
                            }
                        }
                        else
                        {
                            dataRow[index] = cellData.ToString().Trim();
                        }
                    }
                    index++;
                }
            }
            dt.Rows.Add(dataRow);
        }

        /// <summary>
        /// 将DataTable中的数据转换成指定类型的List数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt)
             where T : class, new()
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            try
            {
                var list = new List<T>();
                var props = typeof(T).GetProperties();

                foreach (DataRow row in dt.Rows)
                {
                    var model = new T();
                    foreach (var prop in props)
                    {
                        var attr = Attribute.GetCustomAttribute(prop, typeof(ExcelAttrubte)) as ExcelAttrubte;
                        string columnName = attr != null ? attr.GetColumnName() : prop.Name;

                        if (dt.Columns.Contains(columnName))
                        {
                            if (row.IsNull(columnName))
                            {   // 如果数据列为空 跳过
                                continue;
                            }
                            var rowValue = row[columnName].ToString();
                            var propType = InternalHelper.VerifyNullableType(prop.PropertyType);
                            var value = string.IsNullOrEmpty(rowValue) ? null : Convert.ChangeType(rowValue, propType);
                            prop.SetValue(model, value);
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 校验数据同时
        /// 将DataTable中的数据转换成指定类型的List数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static (List<T>, string) ToVerifyList<T>(this DataTable dt)
             where T : class, new()
        {
            if (dt == null) throw new ArgumentNullException(nameof(dt));
            try
            {
                var list = new List<T>();
                var props = typeof(T).GetProperties();
                StringBuilder message = new StringBuilder();
                var excelProps = props.Where(ww => ww.CustomAttributes.Any(aa => aa.AttributeType == typeof(ExcelAttrubte)))
                                      .Select(ss => Attribute.GetCustomAttribute(ss, typeof(ExcelAttrubte)) as ExcelAttrubte)
                                      .ToList();
                foreach (DataColumn item in dt.Columns)
                {
                    var excelProp = excelProps.FirstOrDefault(ff => ff.GetColumnName() == item.ColumnName);
                    if (excelProp == null) continue;
                    excelProps.Remove(excelProp);
                }
                if (excelProps.Any()) message.Append($@"导入模板不匹配,请重新上传！");
                if (!string.IsNullOrEmpty(message.ToString())) return (list, message.ToString());
                int rowNumber = 1;
                foreach (DataRow row in dt.Rows)
                {
                    var model = new T();
                    rowNumber++;
                    foreach (var prop in props)
                    {
                        var attr = Attribute.GetCustomAttribute(prop, typeof(ExcelAttrubte)) as ExcelAttrubte;
                        string columnName = attr != null ? attr.GetColumnName() : prop.Name;

                        if (dt.Columns.Contains(columnName))
                        {
                            if (row.IsNull(columnName))
                            {   // 如果数据列为空 跳过
                                var requireAttr = Attribute.GetCustomAttribute(prop, typeof(RequiredAttribute)) as RequiredAttribute;
                                if (requireAttr != null)
                                {
                                    message.AppendLine($@"第{rowNumber}行{columnName}的值不能为空，请重新整理！");
                                }
                                continue;
                            }
                            var rowValue = row[columnName].ToString();
                            var propType = InternalHelper.VerifyNullableType(prop.PropertyType);
                            try
                            {
                                var value = string.IsNullOrEmpty(rowValue) ? null : Convert.ChangeType(rowValue, propType);
                                prop.SetValue(model, value);
                            }
                            catch (FormatException)
                            {
                                message.AppendLine($@"第{rowNumber}行{columnName}的值{rowValue}格式不正确，请重新整理！");
                            }
                        }
                    }
                    list.Add(model);
                }
                return (list, message.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
