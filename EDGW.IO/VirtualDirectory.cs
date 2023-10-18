using EDGW.Globalization;

namespace EDGW.IO
{
    public class VirtualDirectory : IDirectory
    {
        public string Name { get; }

        public Text FriendlyName { get; }

        public string FullName => "";

        public IDirectory? Parent => null;

        public bool Exists => true;

        public bool IsReadOnly => true;
        Dictionary<string, IDirectory> dirs = new();

        public VirtualDirectory(string name, Text friendlyName)
        {
            Name = name;
            FriendlyName = friendlyName;
        }
        public void AddDirectory(IDirectory directory)
        {
            dirs.Add(directory.Name, directory);
        }
        public bool ExistsDirectory(string name)
        {
            return dirs.ContainsKey(name);
        }

        public bool ExistsFile(string name)
        {
            return false;
        }

        public IDirectory[] GetDirectories()
        {
            return dirs.Values.ToArray();
        }

        public IDirectory GetDirectory(string name)
        {
            string[] parts = EPath.SplitFirst(name);
            if (parts.Length == 1)
            {
                if (dirs.ContainsKey(name))
                {
                    return dirs[name];
                }
                else
                {
                    throw new DirectoryNotFoundException(name);
                }
            }
            else
            {
                return GetDirectory(parts[0]).GetDirectory(parts[1]);
            }
        }

        public (IDirectory[] directories, IFile[] getfiles) GetDirectoryAndFiles()
        {
            return (GetDirectories(), new IFile[0]);
        }

        public IFile GetFile(string name)
        {
            string[] parts = EPath.SplitFirst(name);
            if (parts.Length == 1)
            {
                throw new FileNotFoundException(name);
            }
            else
            {
                return GetDirectory(parts[0]).GetFile(parts[1]);
            }
        }

        public IFile[] GetFiles()
        {
            return new IFile[0];
        }
        internal IRoot Root { get; set; }
        public IRoot GetRoot() => Root;

        public IRoot MakeRoot() => Root;
    }
}