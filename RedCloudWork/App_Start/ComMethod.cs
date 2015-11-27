using System.Data;
using NPOI.HSSF.UserModel;

namespace RedCloudWork
{
    public class ComMethod
    {
        /// <summary>
        /// 根据execl文件名获取数据表
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByExecl(string fileName)
        {
            var table = new DataTable();
            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var workbook = new HSSFWorkbook(stream);
                var sheet = workbook.GetSheetAt(0);

                var headerRow = sheet.GetRow(0);
                var cellCount = headerRow.LastCellNum;
                var rowCount = sheet.LastRowNum;

                for (var i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                for (var i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    var row = sheet.GetRow(i);
                    var dataRow = table.NewRow();

                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j);
                        }
                    }
                    table.Rows.Add(dataRow);
                }
            }
            return table;
        }
    }
}