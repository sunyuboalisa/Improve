using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToResourceDicTool
{
    /// <summary>
    /// Language dictionary
    /// </summary>
    class LangDic
    {
        public string LanguageName { get; set; }

        public IEnumerable<KeyValuePair<string,string>> Content { get; set; }
    }
}
