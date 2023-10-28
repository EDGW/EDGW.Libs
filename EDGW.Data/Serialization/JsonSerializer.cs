using Newtonsoft.Json.Linq;

namespace EDGW.Data.Serialization
{
    public static class JsonSerializer
    {
        public static void SetValue<T>(this JToken obj,string key,T value,IJsonCaster<T>? caster = null) where T : notnull
        {
            caster = GetCaster<T>(caster);
            obj[key] = caster.GetJson(value);
        }
        public static T GetValueOrThrow<T>(this JToken obj,string key,string formatType,IJsonCaster<T>? caster = null) where T:notnull
        {
            var v = GetValueOrNull(obj, key, formatType, caster);
            return v ?? throw JsonSerializationException.MISSING_KEY(key, formatType, obj);
        }
        public static dynamic GetCaster(Type tp)
        {
            var interf = tp.GetInterface(typeof(IJsonSerializable<,>).Name);
            if (interf != null && interf.GenericTypeArguments.Length == 2)
            {
                var gena = interf.GenericTypeArguments;
                if (gena[0].GetInterface(typeof(IJsonCaster<>).Name)?.GenericTypeArguments[0] == tp)
                {
                    var t = gena[0];
                    return t.Assembly?.CreateInstance(t.FullName ?? t.Name) ?? throw new Exception($"Internal Unexpected Core Exception:Cannot create cast instance of type {t.FullName ?? t.Name}.");

                }
            }
            {
                Type t = typeof(DefaultCaster<>).MakeGenericType(tp);
                return Activator.CreateInstance(t) ?? new DefaultCaster<object>();
            }
        }
        public static IJsonCaster<T> GetCaster<T>(IJsonCaster<T>? caster) where T : notnull
        {

            if (caster == null)
            {
                var interf = typeof(T).GetInterface(typeof(IJsonSerializable<,>).Name);
                if (interf != null && interf.GenericTypeArguments.Length == 2)
                {
                    var gena = interf.GenericTypeArguments;
                    if (gena[0].GetInterface(typeof(IJsonCaster<>).Name)?.GenericTypeArguments[0] == typeof(T))
                    {
                        var t = gena[0];
                        caster = (IJsonCaster<T>?)t.Assembly?.CreateInstance(t.FullName ?? t.Name) ?? throw new Exception($"Internal Unexpected Core Exception:Cannot create cast instance of type {t.FullName ?? t.Name}.");

                    }
                    else caster = new DefaultCaster<T>();
                }
                else caster = new DefaultCaster<T>();
            }
            return caster;
        }
        public static T? GetValueOrNull<T>(this JToken obj, string key, string formatType, IJsonCaster<T>? caster = null) where T : notnull
        {
            caster = GetCaster<T>(caster);
            var k = obj[key];
            if (k == null) return default(T);
            return caster.GetValue(k);
        }
        public static T? GetValueOrDefault<T>(this JToken obj, string key, string formatType, T? defValue, IJsonCaster<T>? caster = null) where T : notnull
        {
            return GetValueOrNull(obj, key, formatType, caster) ?? defValue;
        }
    }
}
