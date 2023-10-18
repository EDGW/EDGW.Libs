using EDGW.Globalization;

namespace EDGW.Data.Registries
{
    public static class IRegistryExtensions
    {
        public static T? GetOrNull<T>(this IRegistry<T> reg, Identifier id) where T : IIdentified
        {
            return reg.IsRegistered(id) ? reg.Get(id) : default(T);
        }
        public static T? GetOrDefault<T>(this IRegistry<T> reg, Identifier id, Func<T> defaultValue) where T : IIdentified
        {
            return reg.IsRegistered(id) ? reg.Get(id) : defaultValue();
        }
        public static T? GetOrDefault<T>(this IRegistry<T> reg, Identifier id, T defaultValue) where T : IIdentified
        {
            return reg.IsRegistered(id) ? reg.Get(id) : defaultValue;
        }
        public static void RegisterIf<T>(this IRegistry<T> reg,T value) where T: IIdentified
        {
            if (!reg.IsRegistered(value.Id))
            {
                reg.Register(value);
            }
        }
        public static bool IsRegistered<T>(this IRegistry<T> reg, T value) where T : IIdentified
        {
            return reg.IsRegistered(value.Id);
        }
    }
}
