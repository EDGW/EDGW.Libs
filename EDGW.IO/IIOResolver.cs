using EDGW.Data.Registries;

namespace EDGW.IO
{
    public interface IIOResolver : IIdentified , IPriority
    {
        public (List<IFile> files, List<IDirectory> directories)? ReadFile(string name, IDirectory parent, IIOProvider reader);
        public (List<IFile> files, List<IDirectory> directories)? ReadDirectory(string name, IDirectory parent, IIOProvider reader);
        public IFile? GetFile(string name, IDirectory parent, IIOProvider reader);
        public IDirectory? GetDirectory(string name, IIOProvider reader);
        public bool ExistsFile(string name, IIOProvider reader);
        public bool ExistsDirectory(string name, IIOProvider reader);
    }
}