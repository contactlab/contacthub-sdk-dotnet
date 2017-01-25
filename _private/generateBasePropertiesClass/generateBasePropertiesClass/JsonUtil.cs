using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generateBasePropertiesClass
{
    public static class JSONUtilities
    {
        /// <summary>
        /// replace external "$ref" with a normal "ref" attribute 
        /// </summary>
        public static string FixReference(string json)
        {
            return json.Replace("$ref", "ref");
        }
    }
}
