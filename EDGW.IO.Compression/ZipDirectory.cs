using EDGW.Globalization;
using NotSupportedException = EDGW.Globalization.NotSupportedException;

namespace EDGW.IO.Compression
{
    public class ZipDirectory : SimpleDirectory ,IIOProvider
    {
        internal ZipDirectory(IDirectory parent, ZipArchiveFile.ZipNode node) : base(parent)
        {
            Node = node;
        }

        internal ZipDirectory(IRoot root, ZipArchiveFile.ZipNode node) : base(root)
        {
            Node = node;
        }
        internal ZipArchiveFile.ZipNode Node { get; }
        public override string Name => Node.Name;

        public override Text FriendlyName => Name;

        public override IIOProvider IOProvider => this;

        public override bool Exists => Node.IsOpening;

        public override bool IsReadOnly => true;

        public override string[] GetPhysicalDirectories()
        {
            return Node.ChildNodes.Where(x => x.Value.IsDirectory).Select(x => x.Key).ToArray();
        }

        public override string[] GetPhysicalFiles()
        {
            return Node.ChildNodes.Where(x=>!x.Value.IsDirectory).Select(x=>x.Key).ToArray();
        }

        public override IDirectory MakeRoot(IRoot root)
        {
            return new ZipDirectory(root, Node);
        }


        Stream IIOProvider.OpenRead(string name)
        {
            if (ExistsFile(name))
            {
                var entry = Node.ChildNodes[name].Entry;
                if (entry != null)
                {
                    return entry.Open();
                }
            }
            throw new FileNotFoundException(EPath.Combine(FullName, name));
        }

        Stream IIOProvider.OpenWrite(string name)
        {
            throw new NotSupportedException(Initializer.EXCEPTIONS.GetText("zip_file_is_readonly"));
        }

        Stream IIOProvider.OpenRW(string name)
        {
            throw new NotSupportedException(Initializer.EXCEPTIONS.GetText("zip_file_is_readonly"));
        }

        Stream IIOProvider.CreateStream(string name, FileMode mode, FileAccess access)
        {
            if (mode == FileMode.Open && access == FileAccess.Read)
            {
                return ((IIOProvider)this).OpenRead(name);
            }
            throw new NotSupportedException(Initializer.EXCEPTIONS.GetText("zip_file_is_readonly"));
        }

        bool IIOProvider.ExistsFile(string name)
        {
            if (Node.ChildNodes.ContainsKey(name))
            {
                return !Node.ChildNodes[name].IsDirectory;
            }
            return false;
        }

        bool IIOProvider.ExistsDirectory(string name)
        {
            if (Node.ChildNodes.ContainsKey(name))
            {
                return Node.ChildNodes[name].IsDirectory;
            }
            return false;
        }

        IDirectory IIOProvider.GetDirectory(string name)
        {
            if (Node.ChildNodes.ContainsKey(name) && Node.ChildNodes[name].IsDirectory)
            {
                return new ZipDirectory(this, Node.ChildNodes[name]);
            }
            throw new DirectoryNotFoundException(EPath.Combine(FullName, name));
        }

        bool IIOProvider.IsReadOnly => true;

        HashSet<string> IIOProvider.ProcessedNames { get; } = new();

        object IIOProvider.Locker { get; } =new object();
    }

}