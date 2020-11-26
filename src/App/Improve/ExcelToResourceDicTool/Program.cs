using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToResourceDicTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdArgs = CommandArgs.Parse(args);

            if (cmdArgs==null)
            {
                Console.WriteLine("参数错误。ex: ExcelToResourceDicTool excelPath langPacksDirectory ");
                return;
            }

            ResourceDicHelper
                .GenerateResourceDicByExcel(cmdArgs.ExcelPath, cmdArgs.LangPacksDir);
        }
    }
}
