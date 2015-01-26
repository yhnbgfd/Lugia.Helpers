using System;
using System.Security.Cryptography;
using System.Text;

namespace Lugia.Helpers.Algorithm.RSA
{
    /// <summary>
    /// RSA工具类
    /// </summary>
    public class RSAHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Encrypt(string publicKey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Decrypt(string privateKey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
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
