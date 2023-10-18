using EDGW.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.Data.Registries
{
    public class Registry<T> : IRegistry<T> where T : IIdentified
    {
        protected Dictionary<Identifier, T> Map { get; } = new();
        protected List<T> Values { get; } = new();
        public virtual T Get(Identifier id)
        {
            return Map[id];
        }

        public virtual IEnumerator<T> GetEnumerator() => Values.GetEnumerator();

        public virtual bool IsRegistered(Identifier id)
        {
            return Map.ContainsKey(id);
        }

        public void Register(T value)
        {
            if (IsRegistered(value.Id))
            {
                throw new DuplicateRegistrationException(value.Id);
            }
            Reregister(value);
        }

        public virtual void Reregister(T value)
        {
            if (Map.ContainsKey(value.Id))
            {
                Values.Remove(value);
            }
            Values.Add(value);
            Map[value.Id] = value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
