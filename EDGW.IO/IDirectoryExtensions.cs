namespace EDGW.IO
{
    public static class IDirectoryExtensions
    {
        public static string[] ReadLines(this IDirectory dir, string filename)
        {
            using (var stream = dir.GetFile(filename).Open(FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd().Split('\n');
                }
            }
        }
        public static void AppendLine(this IDirectory dir, string filename, string line)
        {
            using (var stream = dir.GetFile(filename).Open(FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                stream.Seek(0, SeekOrigin.End);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }
        }
        public static string ReadAllText(this IDirectory dir,string filename)
        {
            using(var stream = dir.GetFile(filename).Open(FileMode.Open, FileAccess.Read))
            {
                using(var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        public static void WriteAllText(this IDirectory dir, string filename,string text)
        {
            using (var stream = dir.GetFile(filename).Open(FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                }
            }
        }
        public static byte[] ReadAllBytes(this IDirectory dir, string filename)
        {
            using (var stream = dir.GetFile(filename).Open(FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    return reader.ReadBytes((int)stream.Length);
                }
            }
        }
        public static void WriteAllBytes(this IDirectory dir, string filename, byte[] text)
        {
            using (var stream = dir.GetFile(filename).Open(FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Write(text, 0, text.Length);
                stream.Flush();
            }
        }

    }
}