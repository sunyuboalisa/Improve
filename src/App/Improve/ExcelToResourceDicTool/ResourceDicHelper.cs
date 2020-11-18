using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;

namespace ExcelToResourceDicTool
{
    class ResourceDicHelper
    {
        public static bool GenerateResourceDicByExcel(string excelPath,string resourceDicPath)
        {
            bool res = false;
            var testPath = @"tt.xaml";
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            var s = resourceDictionary.FindName("ResourceDictionary");
            resourceDictionary.Add("rr", 4444);
            
            using (var fs = new StreamWriter(testPath))
            {
                XamlWriter.Save(resourceDictionary, fs);
            }

            return res;
        }
    }
}
