using Newtonsoft.Json.Linq;

namespace EDGW.IO
{
    public interface IUsingTypeSet
    {
        bool IsModifiable { get; }
        bool IsReadable { get; }
        bool IsReadOnly { get; }
        bool IsRemovable { get; }

        void Add(UsingType item);
        void Clear();
        IEnumerator<UsingType> GetEnumerator();
        bool Remove(UsingType item);
        JObject ToJson();
    }
}