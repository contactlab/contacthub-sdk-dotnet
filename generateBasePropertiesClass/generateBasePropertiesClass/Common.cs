using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


    public static class Common
    {

    public static  string NO_VALUE = "_NoValue";

        public static string makeValidFileName(string name)
        {
            name = name.Replace("-", "Minus");
            Regex illegalInFileName = new Regex(@"[^a-zA-Z0-9]");
            name = illegalInFileName.Replace(name, "");
            return name;
        }
    }
