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
            ExcelHelper.VerifyFormat(@"C:\Users\Administrator\Downloads\tt.xlsx");
            var cmdArgs = CommandArgs.Parse(args);

            if (cmdArgs==null)
            {
                Debug.WriteLine("参数错误。");
                return;
            }

            ResourceDicHelper
                .GenerateResourceDicByExcel("", "");
        }
    }
}
