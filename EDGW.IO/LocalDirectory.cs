using EDGW.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace EDGW.IO
{
    public class LocalDirectory : SimpleDirectory, IIOProvider
    {
        public LocalDirectory(IRoot root, string name, string physicalPath) : base(root)
        {
            PhysicalPath = physicalPath;
            Name = name;
        }

        public LocalDirectory(IDirectory parent, string name,string physicalPath) : base(parent)
        {
            PhysicalPath = physicalPath;
            Name = name;
        }
        public string PhysicalPath { get; }
        public override string Name { get; }

        public override Text FriendlyName => Name;

        public override IIOProvider IOProvider => this;

        public override bool IsReadOnly => false;

        public HashSet<string> ProcessedNames { get; } = new();

        public override bool Exists => Directory.Exists(PhysicalPath);

        public object Locker { get; } = new object();

        bool IIOProvider.IsReadOnly => IsReadOnly;

        HashSet<string> IIOProvider.ProcessedNames => new();

        public override string[] GetPhysicalDirectories()
        {
            if (Directory.Exists(PhysicalPath))
            {
                return Directory.GetDirectories(PhysicalPath).Select(x=>EPath.GetName(x)).ToArray();
            }
            else
            {
                return new string[0];
            }
        }

        public override string[] GetPhysicalFiles()
        {
            if (Directory.Exists(PhysicalPath))
            {
                return Directory.GetFiles(PhysicalPath).Select(x => EPath.GetName(x)).ToArray();
            }
            else
            {
                return new string[0];
            }
        }

        public override IDirectory MakeRoot(IRoot root)
        {
            return new LocalDirectory(root, "", PhysicalPath);
        }

        bool IIOProvider.ExistsDirectory(string name)
        {
            return Directory.Exists(EPath.Combine(PhysicalPath, name));
        }

        bool IIOProvider.ExistsFile(string name)
        {
            return File.Exists(EPath.Combine(PhysicalPath, name));
        }

        IDirectory IIOProvider.GetDirectory(string name)
        {
            return new LocalDirectory(this, name, EPath.Combine(PhysicalPath, name));
        }

        Stream IIOProvider.OpenRead(string name)
        {
            return new FileStream(EPath.Combine(PhysicalPath, name), FileMode.Open, FileAccess.Read);
        }

        Stream IIOProvider.OpenRW(string name)
        {
            return new FileStream(EPath.Combine(PhysicalPath, name), FileMode.Open, FileAccess.ReadWrite);
        }

        Stream IIOProvider.OpenWrite(string name)
        {
            return new FileStream(EPath.Combine(PhysicalPath, name), FileMode.Open, FileAccess.Write);
        }
    }
}