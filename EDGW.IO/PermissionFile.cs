using EDGW.Globalization;
using Newtonsoft.Json.Linq;

namespace EDGW.IO
{
    public class PermissionFile : IFile
    {
        public PermissionFile(IDirectory parent, IIOProvider iOProvider, string name)
        {
            Parent = parent;
            IOProvider = iOProvider;
            Name = name;
        }

        public IDirectory Parent { get; }
        public IIOProvider IOProvider { get; }
        public string Name { get; }

        public Text FriendlyName => Name;

        public string FullName => EPath.Combine(Parent.FullName, Name);

        public IUsingTypeSet UsingType => new UsingTypeSet();

        public bool Exists => true;
        public UsingTypeSet GetUsingTypes()
        {
            string s = IOProvider.ReadAllText(Name);
            try
            {
                JObject obj = JObject.Parse(s);
                return UsingTypeSet.Parse(obj);
            }
            catch { }
            return new();
        }

        public Stream Open(FileMode mode, FileAccess access)
        {
            throw new NotSupportedException();
        }
    }
}