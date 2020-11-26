using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToResourceDicTool
{
    public class ExcelHelper
    {
        
        #region 公共方法

        /// <summary>
        /// 获取excel中的数据
        /// </summary>
        /// <param name="excelPath"></param>
        public static DataSet GetDataSet(string excelPath)
        {
            DataSet dataSet;

            using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                }
            }

            return dataSet;
        }

        /// <summary>
        /// 通过dataTable生成多语言资源文件字典数据。
        /// </summary>
        /// <param name="table"></param>
        /// <returns>多语言资源文件的字典映射集合。</returns>
        public static Dictionary<string, Dictionary<string,string>> GenerateLangPacksDic(DataTable table)
        {
            Dictionary<string, Dictionary<string, string>> langPacks=new Dictionary<string, Dictionary<string,string>>();


            var colNames = GetTableColNames(table);
            string keyHeaderName = colNames.Where(it => it.ToUpper() == "ID").FirstOrDefault(); //key 
            var cultures = colNames.Where(it => it.ToUpper() != "ID"); // values  这个表中所有多语言的值。

            foreach (var culture in cultures)
            {
                Dictionary<string,string> langPack=new Dictionary<string, string>();

                foreach (DataRow row in table.Rows)
                {
                    langPack.Add(row[keyHeaderName].ToString(), row[culture].ToString());
                }

                langPacks.Add(culture.ToUpper(),langPack);
            }

            return langPacks;
        }

        #endregion

        #region 私有方法

        private static IList<string> GetTableColNames(DataTable table)
        {
            var tableColNames = new List<string>();

            foreach (DataColumn col in table.Columns)
            {
                tableColNames.Add(col.ColumnName);
            }

            return tableColNames;
        }

        #endregion
    }
}
