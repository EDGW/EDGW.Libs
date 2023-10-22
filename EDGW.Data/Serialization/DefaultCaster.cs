﻿using Newtonsoft.Json.Linq;
using System.Reflection;

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
            { if (value is bool s) return s; }
            { if (value is JToken s) return s; }
            { if (value is IJsonSerializable s) return s.ToJson(); }
            {
                if(value is Enum enu)
                {
                    if (value.GetType().GetCustomAttribute<EnumTextAttribute>() != null)
                    {
                        int i = 0;
                        foreach(FieldInfo field in value.GetType().GetFields())
                        {
                            if (i++ == 0) continue;
                            var val = field.GetValue(null);
                            if (val?.Equals(value) == true)
                            {
                                var attr = field.GetCustomAttribute<EnumTextAttribute>();
                                if (attr != null)
                                {
                                    return attr.Text;
                                }else return Enum.GetName(typeof(T), value);
                            }
                        }
                    }
                    return Enum.GetName(typeof(T), value);
                }
            }
            throw new ObjectCastException(value.GetType(), typeof(JToken));
        }

        public T GetValue(JToken token)
        {
            try
            {
                if (typeof(T) == typeof(string)) { return (T)(object)token.ToString(); }
                if (typeof(T) == typeof(int)) { return (T)(object)(int)token; }
                if (typeof(T) == typeof(byte)) { return (T)(object)(byte)token; }
                if (typeof(T) == typeof(long)) { return (T)(object)(long)token; }
                if (typeof(T) == typeof(bool)) { return (T)(object)(bool)token; }
                if (typeof(T) == typeof(JToken)) { return (T)(object)token; }
                if (typeof(T) == typeof(JArray)) { return (T)(object)token; }
                if (typeof(T) == typeof(JObject)) { return (T)(object)token; }
                if (typeof(T) == typeof(JValue)) { return (T)(object)token; }
                if (typeof(Enum).IsAssignableFrom(typeof(T)))
                {
                    if (typeof(T).GetCustomAttribute<EnumTextAttribute>() != null)
                    {
                        int i = 0;
                        foreach (FieldInfo field in typeof(T).GetFields())
                        {
                            if (i++ == 0) continue;
                            var val = field.GetValue(null);
                            if (val != null)
                            {
                                var attr = field.GetCustomAttribute<EnumTextAttribute>();
                                if (attr != null && attr.Text == token.ToString())
                                {
                                    return (T)val;
                                }
                            }
                        }
                    }
                    return (T)Enum.Parse(typeof(T), token.ToString());
                }
                throw new ObjectCastException(typeof(JToken), typeof(T));
            }
#if DEBUG
            finally { }
#else
            catch(ArgumentException)
            {
                throw new ObjectCastException(typeof(JToken), typeof(T));
            }
#endif
        }
    }
}
