using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.Data.Serialization
{
    public interface IJsonSerializable
    {
        public JToken ToJson();
    }
    public interface IJsonSerializable<T, T2> : IJsonSerializable where T : IJsonCaster<T2>, new()
    {

    }
}
