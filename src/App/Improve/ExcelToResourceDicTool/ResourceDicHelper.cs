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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ExcelToResourceDicTool
{
    static class ResourceDicHelper
    {
     #region 定义命名空间

        public static XNamespace xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        public static XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";
        public static XNamespace mc = "http://schemas.openxmlformats.org/markup-compatibility/2006";
        public static XNamespace d = "http://schemas.microsoft.com/expression/blend/2008";
        public static XNamespace local = "clr-namespace:GenesysInfo.MainApp;assembly=GenesysInfo.MainApp";
        public static XNamespace dic = "clr-namespace:System.Collections.Specialized;assembly=System";
        public static XNamespace sys = "clr-namespace:System;assembly=System.Runtime";
        public static XNamespace bll = "clr-namespace:GenesysInfo.MainApp.Helper;assembly=GenesysInfo.MainApp";
        public static XNamespace c = "clr-namespace:GenesysInfo.MainApp.CommandCenter;assembly=GenesysInfo.MainApp";
        public static XNamespace ctr = "clr-namespace:GenesysInfo.Windows.Controls;assembly=GenesysInfo.ComponentModel";
        public static XNamespace service = "clr-namespace:GenesysInfo.Data.Service;assembly=GenesysInfo.Data.Service";
        public static XNamespace data = "clr-namespace:GenesysInfo.Data;assembly=GenesysInfo.Data";

        public static void SetNamespace(this XElement xElement,string prefix, XNamespace xNamespace)
       {
           xElement.SetAttributeValue(XNamespace.Xmlns + prefix, xNamespace);
      
       }
    #endregion
    public static bool GenerateResourceDicByExcel(string excelPath,string resourceDicPath)
        {
            bool res = false;

            var langPacks = ExcelHelper.GetLangPackDic(excelPath);
            foreach (var langPack in langPacks)
            {
                
                using (StreamWriter streamWriter = File.CreateText(resourceDicPath))
                {
                    //xmlns: sys = "clr-namespace:System;assembly=System.Runtime"
                    //xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    //xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"


                    XElement root = new XElement("ResourceDictionary");
                    
                    root.SetNamespace("sys", sys);
                    //root.SetNamespace("", xmlns);
                    root.SetNamespace("x", x);
                    //root.SetAttributeValue(XName.Get("xmlns"), "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

                    foreach (var item in langPack.Value)
                    {
                        root.Add(CreateLangElement(item.Key,item.Value));
                    }

                    streamWriter.Write(root);
                    

                }
            }
            

            return res;
        }

        private static XElement CreateLangElement(string key,string value)
        {
            XElement element = new XElement(XName.Get("String",sys.NamespaceName), value);
            element.SetAttributeValue(XName.Get("key", x.NamespaceName), key);
            return element;
        }
    }

    [XmlRoot(Namespace = "", IsNullable = false,ElementName ="ResourceDictionary")]
    public class ResourceDic
    {
        public List<MultiLangElement> Resources { get; set; }
    }

    [XmlRoot("sys:String")]
    public class MultiLangElement
    {
        [XmlAttribute("key")]
        public string Value { get; set; }
    }
}
