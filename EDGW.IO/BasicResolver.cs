using EDGW.Data.Registries;
using EDGW.Globalization;

namespace EDGW.IO
{
    public class BasicResolver : IIOResolver
    {
        public Identifier Id => "edgw.io.resolvers.basicresolver";

        public Priority Priority => Priority.INNER_LOWEST;

        public bool ExistsDirectory(string name, IIOProvider reader)
        {
            return reader.ExistsDirectory(name);
        }

        public bool ExistsFile(string name, IIOProvider reader)
        {
            return reader.ExistsFile(name);
        }

        public IDirectory? GetDirectory(string name, IIOProvider reader)
        {
            if (reader.ExistsDirectory(name))
            {
                return reader.GetDirectory(name);
            }
            return null;
        }

        public IFile? GetFile(string name, IDirectory parent,IIOProvider reader)
        {
            if (reader.ExistsFile(name))
            {
                return new NormalFile(reader, parent, name);
            }
            return null;
        }

        public (List<IFile> files, List<IDirectory> directories)? ReadDirectory(string name, IDirectory parent, IIOProvider reader)
        {
            if (!reader.ProcessedNames.Contains(name))
            {
                reader.ProcessedNames.Add(name);
                var dir = GetDirectory(name, reader);
                if (dir != null) return (new(), new() { dir });
                else return (new(), new());
            }
            return (new(), new());
        }

        public (List<IFile> files, List<IDirectory> directories)? ReadFile(string name, IDirectory parent, IIOProvider reader)
        {
            if (!reader.ProcessedNames.Contains(name))
            {
                reader.ProcessedNames.Add(name);
                var file = GetFile(name, parent, reader);
                if (file != null) return (new() { file }, new());
                else return (new(), new());
            }
            return (new(), new());
        }

    }
}