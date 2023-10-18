using EDGW.Globalization;

namespace EDGW.IO
{
    public abstract class SimpleDirectory : IDirectory
    {
        public SimpleDirectory(IDirectory parent)
        {
            Parent = parent;
        }
        public SimpleDirectory(IRoot root)
        {
            Root = root;
        }
        public abstract string[] GetPhysicalFiles(); 
        public abstract string[] GetPhysicalDirectories();
        public abstract string Name { get; }

        public abstract Text FriendlyName { get; }

        public virtual string FullName
        {
            get
            {
                if (Parent != null)
                {
                    return EPath.Combine(Parent.FullName, Name);
                }
                else
                {
                    return Name;
                }
            }
        }
        private IRoot? Root { get; set; }
        public IDirectory? Parent { get; set; }
        public abstract IIOProvider IOProvider { get; }

        public abstract bool Exists { get; }

        public abstract bool IsReadOnly { get; }

        public virtual IDirectory[] GetDirectories()
        {
            lock (IOProvider.Locker)
            {
                List<IDirectory> res = new List<IDirectory>();
                foreach (var dir in GetPhysicalDirectories())
                {
                    var get = IOManager.ReadDirectory(dir, this, IOProvider);
                    res.AddRange(get.directories);
                }
                foreach (var dir in GetPhysicalFiles())
                {
                    var get = IOManager.ReadFile(dir, this, IOProvider);
                    res.AddRange(get.directories);
                }
                IOProvider.ProcessedNames.Clear();
                return res.ToArray();
            }
        }

        public virtual IDirectory GetDirectorySub(string name)
        {
            return IOManager.GetDirectory(name, IOProvider) ?? throw new DirectoryNotFoundException(EPath.Combine(FullName, name));
        }

        public virtual IFile GetFileSub(string name)
        {
            return IOManager.GetFile(name, this, IOProvider) ?? throw new FileNotFoundException(EPath.Combine(FullName, name));
        }

        public virtual (IDirectory[] directories, IFile[] getfiles) GetDirectoryAndFiles()
        {
            lock (IOProvider.Locker)
            {
                List<IFile> resf = new List<IFile>();
                List<IDirectory> resd = new List<IDirectory>();
                foreach (var dir in GetPhysicalDirectories())
                {
                    var get = IOManager.ReadDirectory(dir, this, IOProvider);
                    resf.AddRange(get.files);
                    resd.AddRange(get.directories);
                }
                foreach (var dir in GetPhysicalFiles())
                {
                    var get = IOManager.ReadFile(dir, this, IOProvider);
                    resf.AddRange(get.files);
                    resd.AddRange(get.directories);
                }
                IOProvider.ProcessedNames.Clear();
                return (resd.ToArray(), resf.ToArray());
            }
        }

        public virtual IFile[] GetFiles()
        {
            lock (IOProvider.Locker)
            {
                List<IFile> res = new List<IFile>();
                foreach (var dir in GetPhysicalDirectories())
                {
                    var get = IOManager.ReadDirectory(dir, this, IOProvider);
                    res.AddRange(get.files);
                }
                foreach (var dir in GetPhysicalFiles())
                {
                    var get = IOManager.ReadFile(dir, this, IOProvider);
                    res.AddRange(get.files);
                }
                IOProvider.ProcessedNames.Clear();
                return res.ToArray();
            }
        }

        public virtual IRoot GetRoot()
        {
            if (Parent != null) return Parent.GetRoot();
#pragma warning disable CS8603 // 可能返回 null 引用。
            return Root;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }

        public virtual IRoot MakeRoot()
        {
            var root = new SimpleRoot(Name, FriendlyName);
            root.Directory = MakeRoot(root);
            return root;
        }
        public abstract IDirectory MakeRoot(IRoot root);
        public class SimpleRoot : IRoot
        {
            public SimpleRoot(string name, Text friendlyName)
            {
                Name = name;
                FriendlyName = friendlyName;
            }

            public string Name { get; }

            public Text FriendlyName { get; }

            public IDirectory Directory { get; internal set; }
        }
        public virtual IDirectory GetDirectory(string name)
        {
            string[] pths = EPath.SplitFirst(name);
            if (pths.Length == 1) return GetDirectorySub(pths[0]);
            else
            {
                return GetDirectorySub(pths[0]).GetDirectory(pths[1]);
            }
        }

        public virtual IFile GetFile(string name)
        {
            string[] pths = EPath.SplitFirst(name);
            if (pths.Length == 1) return GetFileSub(pths[0]);
            else
            {
                return GetDirectorySub(pths[0]).GetFile(pths[1]);
            }
        }

        public bool ExistsDirectory(string name)
        {
            return IOManager.ExistsDirectory(name, IOProvider);
        }

        public bool ExistsFile(string name)
        {
            return IOManager.ExistsFile(name, IOProvider);
        }
    }
}