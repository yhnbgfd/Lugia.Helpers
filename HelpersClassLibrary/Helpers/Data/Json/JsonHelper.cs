using Newtonsoft.Json.Linq;

namespace Lugia.Helpers.Data.Json
{
    public class JsonHelper
    {
        public string Read(string json, string key)
        {
            JObject jObj = JObject.Parse(json);
            return Read(jObj, key);
        }

        public string Read(JObject json, string key)
        {
            JToken token = json[key];
            return token.ToString();
        }
    }
}
