using Newtonsoft.Json.Linq;

namespace EDGW.Data.Serialization
{
    public static class JsonSerializer
    {
        public static T GetValueOrThrow<T>(this JToken obj,string key,string formatType,IJsonCaster<T>? caster = null) where T:notnull
        {
            if (caster == null) caster = new DefaultCaster<T>();
            var k = obj[key];
            if (k == null) throw JsonSerializationException.MISSING_KEY(key, formatType, obj);
            return caster.GetValue(k);
        }
    }
}
