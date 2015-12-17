using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using NPOI.HSSF.UserModel;
using RedCloudWork.Domian;

namespace RedCloudWork
{
    public static class ComMethod
    {

        public static MyDbcontext _mycontext = new MyDbcontext();
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


        public static IList<T> GetModelByDataTable<T>(this DataTable dt) where T:class,new()
        {
            var list=new List<T>();
            var keys = typeof (T).GetFields(BindingFlags.Public);

            foreach (DataRow row in dt.Rows)
            {
                var model=new T();
                foreach (DataColumn col in dt.Columns)
                {
                    var propertyInfo = model.GetType().GetProperty(col.ColumnName);
                    if (propertyInfo != null && row[col.ColumnName] != DBNull.Value)
                    {
                        propertyInfo.SetValue(model, row[col.ColumnName],null);
                    }
                }
                list.Add(model);
            }

            return list;
        }


        public static string ComTime(this string str)
        {
            return str.Insert(str.LastIndexOf('/')+1, "20");
        }

        public static int SaveChange(this IList<Bills> billses)
        {
            var myContext=new MyDbcontext();
            foreach (var bill in billses)
            {
                myContext.Bills.Add(bill);
            }
            return myContext.SaveChanges();
        }

        public static IRepository<T> GetRepository<T>() where T : BaseEntity
        {

            return new EfRepository<T>(_mycontext);
        }
    }
}