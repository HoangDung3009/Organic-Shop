using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Organic_Shop.Extensions
{
    public static class HashMD5
    {
        public static string CreateMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] HashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder HashSB = new StringBuilder();
            foreach(byte b in HashByte)
            {
                HashSB.Append(String.Format("{0:x2}", b));
            }
            return HashSB.ToString();
        }

    }
}
