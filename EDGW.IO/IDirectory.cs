using EDGW.Globalization;

namespace EDGW.IO
{
    public interface IDirectory
    {
        public string Name { get; }
        public Text FriendlyName { get; }
        public string FullName { get; }
        public IDirectory? Parent { get; }
        public IDirectory[] GetDirectories();
        public IFile[] GetFiles();
        public (IDirectory[] directories, IFile[] getfiles) GetDirectoryAndFiles();
        public IDirectory GetDirectory(string name);
        public bool ExistsDirectory(string name);
        public bool ExistsFile(string name);
        public bool Exists { get; }
        public IFile GetFile(string name);
        public IRoot GetRoot();
        public IRoot MakeRoot();
        public bool IsReadOnly { get; }
    }
}