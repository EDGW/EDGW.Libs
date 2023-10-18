using EDGW.Globalization;

namespace EDGW.Data.Registries
{
    public interface IRegistry<T> : IEnumerable<T> where T:IIdentified
    {
        public void Register(T value);
        public void Reregister(T value);
        public bool IsRegistered(Identifier id);
        public T Get(Identifier id);

    }
}
