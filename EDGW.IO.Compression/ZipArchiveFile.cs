using System.IO.Compression;

namespace EDGW.IO.Compression
{
    public class ZipArchiveFile
    {
        public ZipArchiveFile(string path) : this(EPath.GetName(path), ZipFile.OpenRead(path))
        {

        }
        public ZipArchiveFile(string name, ZipArchive zip)
        {
            Zip = zip;
            Name = name;
            Root = new ZipNode(null, Name, this, null, true);
            foreach(var entry in Zip.Entries)
            {
                AddEntry(entry);
            }
        }
        private ZipArchive Zip { get; }
        private ZipNode Root { get; }
        public IDirectory CreateRootDirectory(IDirectory parent)
        {
            return new ZipDirectory(parent, Root);
        }
        public IDirectory CreateRootDirectory(IRoot root)
        {
            return new ZipDirectory(root, Root);
        }
        public string Name { get; }
        public bool IsOpening { get; }
        void AddEntry(ZipArchiveEntry entry)
        {
            AddEntry(Root, entry.FullName, entry);
        }
        void AddEntry(ZipNode parent,string name,ZipArchiveEntry entry)
        {
            var paths = EPath.SplitFirst(name);
            if (paths.Length == 1)
            {
                ZipNode nd = new(parent, name, this, entry, false);
                parent.AddNode(nd);
            }
            else
            {
                AddEntry(parent.GetOrCreateDirectory(paths[0]), paths[1], entry);
            }
        }
        internal class ZipNode
        {
            public ZipNode(ZipNode? parent, string name, ZipArchiveFile file,ZipArchiveEntry? entry, bool isDirectory)
            {
                Parent = parent;
                Name = name;
                File = file;
                Entry = entry;
                IsDirectory = isDirectory;
            }
            public ZipArchiveFile File { get; }
            public ZipArchiveEntry? Entry { get; set; }
            public ZipNode? Parent { get; set; }
            public string Name { get; set; }
            public bool IsOpening => File.IsOpening;
            public string FullName => Parent != null ? EPath.Combine(Parent.FullName, Name) : Name;
            public bool IsDirectory { get; set; }
            public void AddNode(ZipNode nd)
            {
                ChildNodes[nd.Name] = nd;
            }
            public ZipNode GetOrCreateDirectory(string name)
            {
                var nd = ChildNodes.GetValueOrDefault(name);
                if (nd == null)
                {
                    ZipNode node = new(this, name, File, null, true);
                    AddNode(node);
                    return node;
                }
                else
                {
                    if (!nd.IsDirectory) throw new FileAlreadyExistsException(Path.Combine(FullName, name));
                    return nd;
                }
            }


            public Dictionary<string,ZipNode> ChildNodes { get; } = new();
        }
    }

}