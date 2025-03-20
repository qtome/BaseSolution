using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace Base.Util.NPOI.Excel
{
    /// <summary>
    /// 工作簿相关
    /// </summary>
    public static partial class ExcelHelper
    {
        /// <summary>
        /// 创建 Excel工作表接口
        /// </summary>
        /// <returns></returns>
        public static IWorkbook CreateIWorkbook()
        {
            try
            {
                //2007以上版本excel
                return new XSSFWorkbook();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建 Excel工作表接口
        /// </summary>
        /// <param name="fileName">文件完整名称</param>
        /// <returns></returns>
        public static IWorkbook CreateIWorkbook(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return null;

            try
            {
                string fileExt = Path.GetExtension(fileName).ToLower();
                if (string.IsNullOrEmpty(fileExt))
                    return null;

                if (fileExt == ".xlsx")
                {   //2007以上版本excel
                    return new XSSFWorkbook();
                }
                if (fileExt == ".xls")
                {   //2007以下版本excel
                    return new HSSFWorkbook();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回Excel工作表接口
        /// </summary>
        /// <param name="fileName">文件完整名称</param>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public static IWorkbook GetIWorkbook(string fileName, Stream stream)
        {
            if (string.IsNullOrWhiteSpace(fileName) || stream == null)
                return null;

            try
            {
                string fileExt = Path.GetExtension(fileName).ToLower();
                if (string.IsNullOrEmpty(fileExt))
                    return null;

                if (fileExt == ".xlsx")
                {   //2007以上版本excel
                    return new XSSFWorkbook(stream);
                }
                if (fileExt == ".xls")
                {   //2007以下版本excel
                    return new HSSFWorkbook(stream);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将数据填充到工作簿工作表中
        /// </summary>
        /// <param name="workbook">工作簿</param>
        /// <param name="list">数据</param>
        /// <param name="sheetName">工作表名</param>
        /// <returns></returns>
        public static void FillData<T>(this IWorkbook workbook, List<T> list, string sheetName = "")
             where T : class, new()
        {
            if (workbook == null) return;
            if (string.IsNullOrEmpty(sheetName)) sheetName = "Sheet1";
            try
            {
                ISheet sheet = workbook.CreateSheet(sheetName);
                sheet.FillData(list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
