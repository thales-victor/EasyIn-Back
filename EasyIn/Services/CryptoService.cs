using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EasyIn.Services
{
    public class CryptoService
    {
        private static readonly byte[] Key;
        private static readonly byte[] IV;

        public static string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                var encryptor = tdes.CreateEncryptor(Key, IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(byte[] cipherText)
        {
            string plaintext = null; 
            using (var tdes = new TripleDESCryptoServiceProvider())
            {
                var decryptor = tdes.CreateDecryptor(Key, IV);
                using (var ms = new MemoryStream(cipherText))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
