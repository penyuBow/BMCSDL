﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _20120262_Lab04_BMCSDL
{
    internal class SHA1
    {
        private static SHA1CryptoServiceProvider CreateProvider()
        {
            SHA1CryptoServiceProvider cp = new SHA1CryptoServiceProvider();
            return cp;
        }
        public static byte[] Hash(byte[] data)
        {
            using (SHA1CryptoServiceProvider csp = CreateProvider())
            {
                byte[] hash = csp.ComputeHash(data);
                csp.Clear();
                return hash;
            }
        }
    }
}
