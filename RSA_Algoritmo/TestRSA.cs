using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Algoritmo
{
    public static class TestRSA
    {
        public static void Run()
        {
            Console.WriteLine("Key Convert:");
            var keyList = RsaKeyGenerator.Pkcs1Key(512, false);
            var privateKey = keyList[0];
            var publicKey = keyList[1];

            var bigDataRsa = new RsaPkcs1Util(Encoding.UTF8, publicKey, privateKey, 2048);
            var data = File.ReadAllText("frase.txt"); ;
            var str = bigDataRsa.EncryptBigData(data, RSAEncryptionPadding.Pkcs1);
            Console.WriteLine("Big Data Encrypt:");
            Console.WriteLine(str);
            Console.WriteLine("Big Data Decrypt:");
            Console.WriteLine(string.Join("", bigDataRsa.DecryptBigData(str, RSAEncryptionPadding.Pkcs1)));

            Console.ReadKey();
        }
    }
}
