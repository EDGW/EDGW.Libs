using EDGW.Globalization;
using System.Transactions;

namespace EDGW.IO
{
    public class NormalFile : IFile
    {
        public NormalFile(IIOProvider iOProvider, IDirectory parent, string name)
        {
            IOProvider = iOProvider;
            Parent = parent;
            Name = name;
        }
        internal object locker = new();
        public IIOProvider IOProvider { get; }
        public IDirectory Parent { get; }

        public string Name { get; }

        public Text FriendlyName => Name;

        public string FullName => EPath.Combine(Parent.FullName, Name);
        public UsingTypeSet GetUsingTypes()
        {
            if (Parent.IsReadOnly)
            {
                var set = new UsingTypeSet
                {
                    IO.UsingType.READONLY
                };
                return set;
            }
            var nme = GetUsingTypeFileName(Name);
            if (!Parent.ExistsFile(nme))
            {
                return new();
            }
            else
            {
                if(Parent.GetFile(nme) is PermissionFile perm)
                {
                    return perm.GetUsingTypes();
                }
                else
                {
                    return new();
                }
            }
        }
        public void SaveUsingTypes(IUsingTypeSet usingtypeset)
        {
            if (!IOProvider.IsReadOnly)
            {
                var nme = GetUsingTypeFileName(Name);
                if (!Parent.ExistsFile(nme))
                {
                    IOProvider.WriteAllText(nme, new UsingTypeSet().ToJson().ToString());
                }
                if (Parent.GetFile(nme) is PermissionFile perm)
                {
                    perm.WriteUsingType(usingtypeset);
                }
            }
        }
        public static string GetUsingTypeFileName(string file) => $"$permission:|{file}|$";

        public Stream Open(FileMode mode, FileAccess access)
        {
            return new NormalFileStream(this, mode, access);
        }

        public IUsingTypeSet UsingType => GetUsingTypes();

        public bool Exists => Parent.ExistsFile(Name);
    }
}