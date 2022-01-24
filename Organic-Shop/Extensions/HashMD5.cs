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
        public static string createMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder hashSB = new StringBuilder();
            foreach(byte b in hashByte)
            {
                hashSB.Append(String.Format("{0:x2}", b));
            }
            return hashSB.ToString();
        }

    }
}
