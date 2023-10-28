using Newtonsoft.Json.Linq;

namespace EDGW.Globalization
{
    public class JsonLanguageFile : SimpleDictionaryLanguageFile
    {
        public JsonLanguageFile(JObject json)
        {
            LoadJson(json, "");
        }
        public override string ToString()
        {
            return $"Json({Count} Keys)";
        }
        public int Count { get; private set; }
        public void LoadJson(JObject json,Identifier prefix)
        {
            foreach(var pp in json)
            {
                if(pp.Value is JObject obj)
                {
                    LoadJson(obj, prefix + pp.Key);
                }
                else
                {
                    string? val = pp.Value?.ToString();
                    if (val != null)
                    {
                        Dictionary[(prefix + pp.Key).ToString()] = val;
                        Count++;
                    }
                }
            }
        }
    }
}