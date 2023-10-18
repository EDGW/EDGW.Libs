using EDGW.Globalization;

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
        public static string GetUsingTypeFileName(string file) => $"$permission:|{file}|$";

        public Stream Open(FileMode mode, FileAccess access)
        {
            //TODO:FileOpen
            throw new NotImplementedException();
        }

        public IUsingTypeSet UsingType => GetUsingTypes();

        public bool Exists => Parent.ExistsFile(Name);
    }
}