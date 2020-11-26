using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToResourceDicTool
{
    abstract class CommandArgs
    {
        public abstract string ExcelPath { get; }

        public abstract string LangPacksDir { get; }

        public static CommandArgs Parse(params string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    var excelPath = args[0];
                    var resourceDicPath = args[1];

                    var obj = new AppArgs(excelPath, resourceDicPath);

                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public static CommandArgs Parse(string excelPath,string resourceDicPath)
        {
            var obj = new AppArgs(excelPath, resourceDicPath);
            
            return obj;
        }

    }


    internal class AppArgs : CommandArgs
    {
        public AppArgs(string excelPath,string resourceDicPath)
        {
            _excelPath = excelPath;
            _resourceDicPath = resourceDicPath;
        }

        private string _excelPath;
        public override string ExcelPath => _excelPath;

        private string _resourceDicPath;
        public override string LangPacksDir => _resourceDicPath;
    }
}
