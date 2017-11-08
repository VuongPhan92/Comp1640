using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSTVN.PMS.Common
{
    public static class MixFunctions
    {
        public static string ConvertURLSEO(string url)
        {
            var tmp = Regex.Replace(url, Constant.REGEX_URLSEO, "-");
            if (tmp.EndsWith("-"))
            {
                tmp = tmp.Substring(0, tmp.LastIndexOf("-"));
            }
            return tmp;
        }
    }
}
