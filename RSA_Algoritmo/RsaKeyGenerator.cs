using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;

namespace RSA_Algoritmo
{
    /// <summary>
    ///  Rsa Key Generator
    /// Author:Zhiqiang Li
    /// </summary>
    public class RsaKeyGenerator
    {
        /// <summary>
        /// Generate RSA key in Pkcs1 format. Result: Index 0 is the private key and index 1 is the public key
        /// </summary>
        /// <param name="keySize">Key Size.Unit: bits</param>
        /// <param name="format">Whether the format is true If it is standard pem file format</param>
        /// <returns></returns>
        public static List<string> Pkcs1Key(int keySize, bool format)
        {
            List<string> res = new List<string>();

            IAsymmetricCipherKeyPairGenerator kpGen = GeneratorUtilities.GetKeyPairGenerator("RSA");
            kpGen.Init(new KeyGenerationParameters(new SecureRandom(), keySize));
            var keyPair = kpGen.GenerateKeyPair();

            StringWriter sw = new StringWriter();
            PemWriter pWrt = new PemWriter(sw);
            pWrt.WriteObject(keyPair.Private);
            pWrt.Writer.Close();
            var privateKey = sw.ToString();

            if (!format)
            {
                privateKey = privateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace(Environment.NewLine, "");
            }

            res.Add(privateKey);

            StringWriter swpub = new StringWriter();
            PemWriter pWrtpub = new PemWriter(swpub);
            pWrtpub.WriteObject(keyPair.Public);
            pWrtpub.Writer.Close();
            string publicKey = swpub.ToString();
            if (!format)
            {
                publicKey = publicKey.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace(Environment.NewLine, "");
            }

            res.Add(publicKey);

            return res;
        }
    }
}