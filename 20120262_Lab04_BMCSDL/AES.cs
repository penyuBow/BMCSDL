using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace _20120262_Lab04_BMCSDL
{
    internal class AES
    {
        private static byte[] iv = Encoding.UTF8.GetBytes("20120262lab04kkd");


        private static byte[] key = new byte[32]
        {
        //    2     0    1      2    0      2    6     2 
            0x32, 0x48, 0x31, 0x32, 0x48, 0x32, 0x36, 0x32,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };
        private static AesCryptoServiceProvider CreateProvider()
        {
            AesCryptoServiceProvider cp = new AesCryptoServiceProvider();
            cp.KeySize = 256;
            cp.BlockSize = 128;
            cp.Key = key;
            cp.Padding = PaddingMode.PKCS7;
            cp.Mode = CipherMode.CBC;
            cp.IV = iv;
            return cp;
        }
        public static byte[] Encrypt(byte[] data)
        {
            byte[] enc;
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("data");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            using (AesCryptoServiceProvider csp = CreateProvider())
            {
                ICryptoTransform encrypter = csp.CreateEncryptor();
                enc = encrypter.TransformFinalBlock(data, 0, data.Length);
                csp.Clear();
            }
            return enc;
        }

        public static byte[] Decrypt(byte[] data)
        {
            byte[] de;
            if (data == null || data.Length <= 0)
                throw new ArgumentNullException("data");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            using (AesCryptoServiceProvider csp = CreateProvider())
            {

                try
                {
                    ICryptoTransform decrypter = csp.CreateDecryptor();
                    de = decrypter.TransformFinalBlock(data, 0, data.Length);
                }
                catch
                {
                    //Decrypt failed
                    de = null;
                }
                csp.Clear();
            }
            return de;
        }
    }
}
