using EDGW.Globalization;
using Newtonsoft.Json.Linq;
using NotSupportedException = EDGW.Globalization.NotSupportedException;

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
        public void WriteUsingType(IUsingTypeSet usingtype)
        {
            IOProvider.WriteAllText(Name, usingtype.ToJson().ToString());
        }
        public Stream Open(FileMode mode, FileAccess access)
        {
            throw new NotSupportedException(TextParsers.EXCEPTIONS.GetText("permissions_file_is_locked"));
        }
    }
}