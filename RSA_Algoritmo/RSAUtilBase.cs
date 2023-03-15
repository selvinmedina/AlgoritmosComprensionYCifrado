using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Algoritmo
{
    public abstract class RSAUtilBase : IDisposable
    {
        public RSA PrivateRsa;
        public RSA PublicRsa;
        public Encoding DataEncoding;

        static readonly Dictionary<RSAEncryptionPadding, int> PaddingLimitDic = new Dictionary<RSAEncryptionPadding, int>()
        {
            [RSAEncryptionPadding.Pkcs1] = 11,
            [RSAEncryptionPadding.OaepSHA1] = 42,
            [RSAEncryptionPadding.OaepSHA256] = 66,
            [RSAEncryptionPadding.OaepSHA384] = 98,
            [RSAEncryptionPadding.OaepSHA512] = 130,
        };

        /// <summary>
        /// <para>El cifrado RSA no admite datos demasiado grandes. En este caso, se debe usar cifrado simétrico y RSA se usa para cifrar contraseñas cifradas simétricamente.</para>
        /// </summary>
        /// <param name="dataStr">Datos que se deben cifrar</param>
        /// <param name="connChar">Carácter de enlace del resultado cifrado</param>
        /// <param name="padding">Algoritmo de relleno</param>
        /// <returns>Texto encriptado por alumnos de estructura de datos ll</returns>
        public string EncryptBigData(string dataStr, RSAEncryptionPadding padding, char connChar = '$')
        {
            var data = Encoding.UTF8.GetBytes(dataStr);
            var modulusLength = PublicRsa.KeySize / 8;
            var splitLength = modulusLength - PaddingLimitDic[padding];

            var sb = new StringBuilder();

            var splitsNumber = Convert.ToInt32(Math.Ceiling(data.Length * 1.0 / splitLength));

            var pointer = 0;
            for (int i = 0; i < splitsNumber; i++)
            {
                byte[] current = pointer + splitLength < data.Length ? data.Skip(pointer).Take(splitLength).ToArray() : data.Skip(pointer).Take(data.Length - pointer).ToArray();

                sb.Append(Convert.ToBase64String(PublicRsa.Encrypt(current, padding)));
                sb.Append(connChar);
                pointer += splitLength;
            }

            return sb.ToString().TrimEnd(connChar);
        }

        /// <summary>
        /// <para>El cifrado RSA no admite datos demasiado grandes. En este caso, se debe usar cifrado simétrico y RSA se usa para cifrar contraseñas cifradas simétricamente.</para>
        /// </summary>
        /// <param name="connChar">Carácter de enlace del resultado cifrado</param>
        /// <param name="dataStr">Datos que se deben descifrar</param>
        /// <param name="padding">Algoritmo de relleno</param>
        /// <returns>Texto desencriptado por alumnos de estructura de datos ll</returns>
        public string DecryptBigData(string dataStr, RSAEncryptionPadding padding, char connChar = '$')
        {
            if (PrivateRsa == null)
            {
                throw new ArgumentException("private key can not null");
            }

            var data = dataStr.Split(new[] { connChar }, StringSplitOptions.RemoveEmptyEntries);
            var byteList = new List<byte>();

            foreach (var item in data)
            {
                byteList.AddRange(PrivateRsa.Decrypt(Convert.FromBase64String(item), padding));
            }

            return Encoding.UTF8.GetString(byteList.ToArray());
        }

        protected abstract RSAParameters CreateRsapFromPrivateKey(string privateKey);
        protected abstract RSAParameters CreateRsapFromPublicKey(string publicKey);

        public void Dispose()
        {
            PrivateRsa?.Dispose();
            PublicRsa?.Dispose();
        }
    }
}