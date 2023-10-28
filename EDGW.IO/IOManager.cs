using EDGW.Data.Logging;
using EDGW.Data.Registries;
using EDGW.Globalization;
using Newtonsoft.Json.Linq;
using NotSupportedException = EDGW.Globalization.NotSupportedException;

namespace EDGW.IO
{
    public static class IOManager
    {
        static IOManager()
        {
            IOResolvers.Register(new BasicResolver());
        }
        static Logger logger = new Logger("EDGW.IO"); 
        public static void LoadLanguageFiles()
        {
            Languages.AddLanguageFile("zh_cn", new JsonLanguageFile(JObject.Parse(LanRes.zh_cn)));
            logger.Info("Registered language files.");
        }
        public static IRegistry<IIOResolver> IOResolvers { get; } = new PriorityRegistry<IIOResolver>();
        
        public static (List<IFile> files, List<IDirectory> directories) ReadFile(string name, IDirectory parent, IIOProvider provider)
        {
            foreach(IIOResolver res in IOResolvers)
            {
                var file = res.ReadFile(name, parent, provider);
                if (file != null)
                {
                    return file.Value;
                }
            }
            throw new NotSupportedException(TextParsers.EXCEPTIONS.GetText("unresolvable_file", name, parent.FullName));
        }
        public static (List<IFile> files, List<IDirectory> directories) ReadDirectory(string name, IDirectory parent, IIOProvider provider)
        {
            foreach (IIOResolver res in IOResolvers)
            {
                var dir = res.ReadDirectory(name, parent, provider);
                if (dir != null)
                {
                    return dir.Value;
                }
            }
            throw new NotSupportedException(TextParsers.EXCEPTIONS.GetText("unresolvable_directory", name, parent.FullName));
        }
        public static IFile? GetFile(string name, IDirectory parent, IIOProvider provider)
        {
            foreach (IIOResolver res in IOResolvers)
            {
                var file = res.GetFile(name, parent, provider);
                if (file != null)
                {
                    return file;
                }
            }
            return null;
        }
        public static IDirectory? GetDirectory(string name, IIOProvider provider)
        {
            foreach (IIOResolver res in IOResolvers)
            {
                var dir = res.GetDirectory(name, provider);
                if (dir != null)
                {
                    return dir;
                }
            }
            return null;
        }

        public static bool ExistsFile(string name, IIOProvider provider)
        {
            foreach (IIOResolver res in IOResolvers)
            {
                var file = res.ExistsFile(name, provider);
                if (file)
                {
                    return file;
                }
            }
            return false;
        }
        public static bool ExistsDirectory(string name, IIOProvider provider)
        {
            foreach (IIOResolver res in IOResolvers)
            {
                var file = res.ExistsDirectory(name, provider);
                if (file)
                {
                    return file;
                }
            }
            return false;
        }
    }
}