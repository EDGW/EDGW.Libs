using Newtonsoft.Json.Linq;
using System.Collections;

namespace EDGW.IO
{
    public class UsingTypeSet : ICollection<UsingType>, IUsingTypeSet
    {
        static UsingTypeSet()
        {
            Random = new Random().Next();
        }
        internal static int Random { get; }
        List<UsingType> UsingTypes { get; } = new();
        int unmodifiable = 0;
        int unreadable = 0;
        int unremovable = 0;
        public bool IsModifiable => unmodifiable == 0;
        public bool IsReadable => unreadable == 0;
        public bool IsRemovable => unremovable == 0;
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => false;

        public void Add(UsingType item)
        {
            if (!item.IsModifiable) unmodifiable++;
            if (!item.IsReadable) unreadable++;
            if (!item.IsRemovable) unremovable++;
        }

        public void Clear()
        {
            unmodifiable = unreadable = unremovable = 0;
        }

        public bool Contains(UsingType item)
        {
            return UsingTypes.Contains(item);
        }

        public void CopyTo(UsingType[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<UsingType> GetEnumerator() => UsingTypes.GetEnumerator();

        public bool Remove(UsingType item)
        {
            if (Contains(item))
            {
                if (!item.IsModifiable) unmodifiable--;
                if (!item.IsReadable) unreadable--;
                if (!item.IsRemovable) unremovable--;
                return true;
            }
            return false;
        }
        const string ARR = "8sajk8";
        const string RAND = "89saj2";
        public JObject ToJson()
        {
            JArray arr = new();
            lock (UsingTypes)
            {
                foreach (var child in this)
                {
                    arr.Add(child);
                }
            }
            JObject obj = new();
            obj[ARR] = arr;
            obj[RAND] = Random;
            return obj;
        }
        public static UsingTypeSet Parse(JObject obj)
        {
            if (obj[RAND]?.ToString() != Random.ToString()) return new();
            UsingTypeSet set = new();
            JArray arr = obj[ARR] as JArray ?? new();
            foreach (JObject aobj in arr)
            {
                set.Add(UsingType.Parse(aobj));
            }
            return set;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}