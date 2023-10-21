using EDGW.Globalization;
using Newtonsoft.Json.Linq;

namespace EDGW.Data.Serialization
{
    public class JsonSerializationException : GlobalizedException
    {
        public static JsonSerializationException MISSING_KEY(string keyname, string formatType, JToken obj)
        {
            return new(TextParsers.EXCEPTIONS.GetText("json.missing_key", keyname, formatType, obj.ToString()));
        }
        public JsonSerializationException(Text text) : base(text)
        {
        }
    }
}
