using System.IO;

namespace EDGW.IO
{
    public static class EPath
    {
        public static string Format(string path)
        {
            return path.Replace("\\", "/").Replace("//", "/");
        }
        public static string Combine(string patha,string pathb)
        {
            patha = Format(patha);
            pathb = Format(pathb);
            if (patha.EndsWith("/")) patha = patha.Substring(0, patha.Length - 1);
            if (pathb.StartsWith("/")) pathb = pathb.Substring(1);
            return $"{patha}/{pathb}";
        }
        public static string Combine(params string[] paths)
        {
            if (paths.Length == 0) throw new ArgumentException($"size of paths cannot be null.");
            string? p = null;
            foreach(string path in paths)
            {
                if (p == null) p = path;
                else
                {
                    p = Combine(p, path);
                }
            }
#pragma warning disable CS8603 // 可能返回 null 引用。
            return p;
#pragma warning restore CS8603 // 可能返回 null 引用。
        }
        public static string[] SplitFirst(string path)
        {
            path = Format(path);
            if (!path.Contains("/")) return new string[1] { path };
            else
            {
                int index = path.IndexOf("/");
                if (index + 1 <= path.Length)
                {
                    return new string[2] { path.Substring(0, index), path.Substring(index + 1) };
                }
                else
                {
                    return new string[1] { path.Substring(0, index) };
                }
            }
        }

        public static string GetName(string path)
        {
            path = Format(path);
            if (!path.Contains("/")) return path;
            else
            {
                int index = path.LastIndexOf("/");
                if (index + 1 <= path.Length)
                {
                    return path.Substring(index + 1);
                }
                else
                {
                    return path;
                }
            }
        }
    }
}