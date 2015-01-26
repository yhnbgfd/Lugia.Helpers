using System;
using System.Security.Cryptography;
using System.Text;

namespace Lugia.Helpers.Algorithm.RSA
{
    public class RSAHelper
    {
        public string Encrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);
            byte[] cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }

        public string Decrypt(string privatekey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey);
            byte[] cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);
            return Encoding.UTF8.GetString(cipherbytes);
        }

        /// <summary>
        /// 产生公钥和私钥对
        /// </summary>
        /// <returns>string[] 0:私钥;1:公钥</returns>
        public string[] CreateKey()
        {
            string[] keys = new string[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            keys[0] = rsa.ToXmlString(true);
            keys[1] = rsa.ToXmlString(false);
            return keys;
        }
    }
}
