using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ExcelToResourceDicTool
{
    class ResourceDicHelper
    {
        #region 定义命名空间

        public static XNamespace xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        public static XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
        public static XNamespace sys = "clr-namespace:System;assembly=System.Runtime";

        #endregion

        private static string _langPacksDir;

        #region 公共方法

        /// <summary>
        /// 通过excel生成多语言的资源字典xaml文件.
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="langPacksDir"></param>
        public static void GenerateResourceDicByExcel(string excelPath, string langPacksDir)
        {
            _langPacksDir = langPacksDir;

            foreach (DataTable table in ExcelHelper.GetDataSet(excelPath).Tables)
            {
                GenerateResourceDicFiles(table);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成所有多语言资源字典文件
        /// </summary>
        /// <param name="table"></param>
        static void GenerateResourceDicFiles(DataTable table)
        {
            bool res = false;

            var appLangPackDir = System.IO.Path.Combine(_langPacksDir, table.TableName);

            if (!Directory.Exists(appLangPackDir))
            {
                Directory.CreateDirectory(appLangPackDir);
            }

            var landPacks = ExcelHelper.GenerateLangPacksDic(table);

            foreach (var lang in landPacks)
            {
                var name = lang.Key + ".xaml";
                var filePath = Path.Combine(appLangPackDir ,name);

                var resourceDic = lang.Value;

                using (StreamWriter streamWriter = File.CreateText(filePath))
                {
                    XElement root = new XElement(xmlns + "ResourceDictionary");
                    root.SetAttributeValue(XNamespace.Xmlns + "sys", sys);
                    root.SetAttributeValue(XNamespace.Xmlns + "x", x);

                    foreach (var item in resourceDic)
                    {
                        root.Add(CreateLangElement(item.Key, item.Value));
                    }

                    streamWriter.Write(root);
                    streamWriter.Flush();
                }

            }

        }

        /// <summary>
        /// 创建多语言资源字典元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static XElement CreateLangElement(string key, string value)
        {
            XElement element = new XElement(sys + "String", value);
            element.SetAttributeValue(x + "Key", key);

            return element;
        }

        #endregion
    }
}
