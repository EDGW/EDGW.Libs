using Newtonsoft.Json.Linq;

namespace EDGW.Data.Serialization
{
    public interface IJsonCaster<T>
    {
        public T GetValue(JToken token);
        public JToken GetJson(T value);
    }
}
