namespace EDGW.Data.Registries
{
    public class PriorityRegistry<T>:Registry<T> where T : IIdentified, IPriority
    {
        
        public override IEnumerator<T> GetEnumerator()
        {
            Values.Sort((T l, T r) =>
            {
                return - l.Priority.CompareTo(r.Priority);
            });
            return base.GetEnumerator();
        }
    }
}
