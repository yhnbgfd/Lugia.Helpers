using System;
using System.Security.Cryptography;
using System.Text;

namespace Lugia.Helpers.Algorithm
{
    /// <summary>
    /// RSA工具类
    /// 出于安全的考虑，RSACryptoServiceProvider中Encrypt、Decrypt方法只支持公钥加密、私钥解密，用途是数据加密
    /// 而私钥加密、公钥解密的使用途径是数字签名，内容验证，对应RSACryptoServiceProvider中的SignData、VerifyData
    /// </summary>
    public class RSAHelper
    {
        /// <summary>
        /// 产生公钥(1)和私钥(0)
        /// </summary>
        /// <param name="dwKeySize">The size of the key to use in bits.</param>
        /// <returns></returns>
        public string[] CreateKey(int dwKeySize = 1024)
        {
            string[] keys = new string[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(dwKeySize);
            keys[0] = rsa.ToXmlString(true);//私钥（私钥里面前半段为公钥）
            keys[1] = rsa.ToXmlString(false);//公钥
            return keys;
        }

        #region 公钥加密、私钥解密
        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">原数据</param>
        /// <returns></returns>
        public string Encrypt(string publicKey, string data)
        {
            return this.Encrypt(publicKey, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">原数据</param>
        /// <returns></returns>
        public string Encrypt(string publicKey, byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] cipherbytes = null;
            try
            {
                cipherbytes = rsa.Encrypt(data, false);
            }
            catch (CryptographicException)
            {
                return "ERROR：不正确的长度";
            }
            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="data">原数据</param>
        /// <returns></returns>
        public string Decrypt(string privateKey, string data)
        {
            byte[] bdata = null;
            try
            {
                bdata = Convert.FromBase64String(data);
            }
            catch
            {
                return "ERROR：加密结果格式错误";
            }
            return this.Decrypt(privateKey, bdata);
        }

        /// <summary>
        /// 私钥解密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Decrypt(string privateKey, byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] cipherbytes = null;
            try
            {
                cipherbytes = rsa.Decrypt(data, false);
            }
            catch (CryptographicException)
            {
                return "ERROR";
            }
            return Encoding.UTF8.GetString(cipherbytes);
        }
        #endregion

        #region 私钥加密、公钥解密
        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="data">原数据</param>
        /// <returns></returns>
        public string SignData(string privateKey, byte[] data)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            return Convert.ToBase64String(rsa.SignData(data, "MD5"));
        }

        /// <summary>
        /// 私钥加密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SignData(string privateKey, string data)
        {
            return SignData(privateKey, Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="data">原数据</param>
        /// <param name="signature">证书数据</param>
        /// <returns></returns>
        public bool VerifyData(string publicKey, byte[] data, byte[] signature)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            return rsa.VerifyData(data, "MD5", signature);
        }

        /// <summary>
        /// 公钥解密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public bool VerifyData(string publicKey, string data, string signature)
        {
            return VerifyData(publicKey, Encoding.UTF8.GetBytes(data), Encoding.UTF8.GetBytes(signature));
        }

        #endregion
    }
}
