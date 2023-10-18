using EDGW.Globalization;

namespace EDGW.IO
{
    public class VirtualRoot : IRoot
    {
        public VirtualRoot(string name, Text friendlyName)
        {
            Name = name;
            FriendlyName = friendlyName;
            var v = new VirtualDirectory("", friendlyName);
            v.Root = this;
            Directory = v;
        }

        public string Name { get; }

        public Text FriendlyName { get; }

        public IDirectory Directory { get; }
        public void AddDirectory(IDirectory dir)
        {
            ((dynamic)Directory).AddDirectory(dir);
        }
    }
}