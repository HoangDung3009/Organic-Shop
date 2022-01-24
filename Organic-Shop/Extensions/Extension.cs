using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Shop.Extensions
{
    public static class Extension
    {
        public static string FormatPrice(this double price)
        {
            return price.ToString("#,#0" + "$");
        }

        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var substr = str.Split(" ");
                for(int i = 0; i < substr.Length; i++)
                {
                    var s = substr[i];

                    if (s.Length > 0)
                    {
                        substr[i] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                    result = string.Join(" ", substr[i]);
                }
            }
            return result;
        }


    }
}
