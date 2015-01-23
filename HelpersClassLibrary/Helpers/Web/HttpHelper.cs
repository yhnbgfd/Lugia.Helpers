using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lugia.Helpers.Web
{
    public class HttpHelper
    {
        public string POST(string url, string parm)
        {
            string ret = string.Empty;
            byte[] byteArray = Encoding.UTF8.GetBytes(parm); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.ContentLength = byteArray.Length;
            using (Stream newStream = webReq.GetRequestStream())
            {
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            }
            using (HttpWebResponse response = (HttpWebResponse)webReq.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    ret = sr.ReadToEnd();
                }
            }
            return ret;
        }

        public string GET(string url)
        {
            string ret = string.Empty;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8))
                {
                    ret = sr.ReadToEnd();
                }
            }
            return ret;
        }

        [Obsolete("Not implemented")]
        public string PUT(string url)
        {
            return string.Empty;
        }

        [Obsolete("Not implemented")]
        public string DELETE(string url)
        {
            return string.Empty;
        }
    }
}
