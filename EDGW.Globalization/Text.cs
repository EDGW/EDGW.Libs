using Newtonsoft.Json.Linq;

namespace EDGW.Globalization
{
    public class Text
    {
        public static implicit operator Text(Identifier s)
        {
            return new(s);
        }
        public static implicit operator Text(string s)
        {
            return new(s);
        }
        public Text(Identifier id,params string[] parameters)
        {
            Id = id;
            Parameters.AddRange(parameters);
        }
        public List<string> Parameters { get; } = new();
        public Identifier Id { get; }
        public JObject ToJson()
        {
            var json = new JObject();
            json["id"] = Id.ToString();
            JArray arr = new JArray();
            foreach(var param in Parameters)
            {
                arr.Add(param);
            }
            json["params"] = arr;
            return json;
        }
        public static Text Parse(JObject obj)
        {
            return new Text(obj["id"]?.ToString() ?? "", obj["params"]?.Select((x) => x.ToString()).ToArray() ?? new string[0]);
        }
    }
}