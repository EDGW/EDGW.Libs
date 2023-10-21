using Newtonsoft.Json.Linq;

namespace EDGW.Data.Serialization
{
    public class DefaultCaster<T> : IJsonCaster<T> where T:notnull
    {
        public JToken GetJson(T value)
        {
            { if (value is string s) return s; }
            { if (value is int s) return s; }
            { if (value is byte s) return s; }
            { if (value is long s) return s; }
            { if (value is JToken s) return s; }
            { if (value is IJsonSerializable s) return s.ToJson(); }
            throw new ObjectCastException(value.GetType(), typeof(JToken));
        }

        public T GetValue(JToken token)
        {
            if (typeof(T) == typeof(string)) { return (T)(object)token.ToString(); }
            if (typeof(T) == typeof(int)) { return (T)(object)(int)token; }
            if (typeof(T) == typeof(byte)) { return (T)(object)(byte)token; }
            if (typeof(T) == typeof(long)) { return (T)(object)(long)token; }
            if (typeof(T) == typeof(JToken)) { return (T)(object)token; }
            if (typeof(T) == typeof(JArray)) { return (T)(object)token; }
            if (typeof(T) == typeof(JObject)) { return (T)(object)token; }
            if (typeof(T) == typeof(JValue)) { return (T)(object)token; }
            throw new ObjectCastException(typeof(JToken), typeof(T));
        }
    }
}
