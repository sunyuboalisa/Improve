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
    public static class ExcelHelper
    {
        public static void GetValues(DataSet dataset, string sheetName)
        {
            foreach (DataRow row in dataset.Tables[sheetName].Rows)
            {
                foreach (var value in row.ItemArray)
                {
                    Console.WriteLine("{0}, {1}", value, value.GetType());
                }
            }
        }


        private static IList<string> GetTableColNames(DataTable table)
        {
            var tableColNames = new List<string>();

            foreach (DataColumn col in table.Columns)
            {
                tableColNames.Add(col.ColumnName);
            }

            return tableColNames;
        }

        static Dictionary<string, Dictionary<string,string>> GenerateLangPacksDic(DataTable table)
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


        public static Dictionary<string, Dictionary<string, string>> GetLangPackDic(string filePath)
        {
            Dictionary<string, Dictionary<string, string>> langPacks = new Dictionary<string, Dictionary<string, string>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    //
                    // 只支持第一页sheet
                    //
                    foreach (DataTable table in result.Tables)
                    {
                        langPacks=GenerateLangPacksDic(table);
                        break;
                    }

                }
            }

            return langPacks;
        }


    }
}
