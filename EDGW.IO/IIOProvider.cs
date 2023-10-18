namespace EDGW.IO
{
    public interface IIOProvider
    {
        public Stream OpenRead(string name);
        public Stream OpenWrite(string name);
        public Stream OpenRW(string name);
        public bool IsReadOnly { get; }
        public bool ExistsFile(string name);
        public bool ExistsDirectory(string name);
        public IDirectory GetDirectory(string name);
        public HashSet<string> ProcessedNames { get; }
        public object Locker { get; }
    }
}