using EDGW.Data.Registries;
using System.Collections.ObjectModel;

namespace EDGW.Globalization
{
    public class Language : Collection<ILanguageFile>, IIdentified, IPriority
    {
        public Language(string id, string name, Priority priority)
        {
            Id = id;
            Name = name;
            Priority = priority;
        }
        public string Name { get; }
        public Identifier Id { get; }

        public Priority Priority { get; }

        public string? GetString(string key,params string[] parameters)
        {
            var s = GetFormatString(key);
            if (s == null) return null;
            else
            {
                return string.Format(s, parameters);
            }
        }
        public string? GetFormatString(string key)
        {
            foreach(var file in this)
            {
                var val = file.GetFormatString(key);
                if (val != null) return val;
            }
            return null;
        }
    }
}