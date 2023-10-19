using System.IO.Compression;

namespace EDGW.IO.Compression
{
    public class ZipArchiveFile
    {
        public ZipArchiveFile(ZipArchive zip)
        {
            Zip = zip;
            Root = new ZipNode(null, "", null, true);
            foreach(var entry in Zip.Entries)
            {
                AddEntry(entry);
            }
        }
        private ZipArchive Zip { get; }
        private ZipNode Root { get; }
        void AddEntry(ZipArchiveEntry entry)
        {
            AddEntry(Root, entry.FullName, entry);
        }
        void AddEntry(ZipNode parent,string name,ZipArchiveEntry entry)
        {
            var paths = EPath.SplitFirst(name);
            if (paths.Length == 1)
            {
                ZipNode nd = new(parent, name, entry, false);
                parent.AddNode(nd);
            }
            else
            {
                AddEntry(parent.GetOrCreateDirectory(paths[0]), paths[1], entry);
            }
        }
        internal class ZipNode
        {
            public ZipNode(ZipNode? parent, string name, ZipArchiveEntry? entry, bool isDirectory)
            {
                Parent = parent;
                Name = name;
                Entry = entry;
                IsDirectory = isDirectory;
            }
            public ZipArchiveEntry? Entry { get; set; }
            public ZipNode? Parent { get; set; }
            public string Name { get; set; }
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
                    ZipNode node = new(this, name, null, true);
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