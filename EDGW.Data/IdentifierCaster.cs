using EDGW.Data.Serialization;
using Newtonsoft.Json.Linq;

namespace EDGW.Globalization
{
    public class IdentifierCaster : IJsonCaster<Identifier>
    {
        public JToken GetJson(Identifier value)
        {
            return value.ToString();
        }

        public Identifier GetValue(JToken token)
        {
            return token.ToString();
        }
    }
}