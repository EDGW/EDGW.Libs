using EDGW.Globalization;

namespace EDGW.IO
{
    public interface IFile
    {
        public IDirectory Parent { get; }
        public string Name { get; }
        public Text FriendlyName { get; }
        public string FullName { get; }
        public IUsingTypeSet UsingType { get; }
        public bool Exists { get; }
        public Stream Open(FileMode mode, FileAccess access);
    }
}