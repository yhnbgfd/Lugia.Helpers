using Newtonsoft.Json.Linq;

namespace Lugia.Helpers.Data
{
    /// <summary>
    /// 便捷读取Json字符串
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 从string格式的json字符串读取特定的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Read(string json, string key)
        {
            JObject jObj = JObject.Parse(json);
            return Read(jObj, key);
        }

        /// <summary>
        /// 从JObject格式的json字符串读取特定的值
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Read(JObject json, string key)
        {
            JToken token = json[key];
            return token.ToString();
        }
    }
}
