using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _20120262_Lab04_BMCSDL
{
    internal class MD5
    {
        private static MD5CryptoServiceProvider CreateProvider()
        {
            MD5CryptoServiceProvider cp = new MD5CryptoServiceProvider();
            return cp;
        }
        public static byte[] Hash(byte[] data)
        {
            using (MD5CryptoServiceProvider csp = CreateProvider())
            {
                byte[] hash = csp.ComputeHash(data);
                csp.Clear();
                return hash;
            }
        }
    }
}
