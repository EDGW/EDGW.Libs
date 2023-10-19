namespace EDGW.IO
{
    public static class IOProviderExtensions
    {
        public static string ReadAllText(this IIOProvider reader,string name)
        {   using (var stream = reader.OpenRead(name))
            {
                using(var st = new StreamReader(stream))
                {
                    return st.ReadToEnd();
                }
            }
        }
        public static void WriteAllText(this IIOProvider reader, string name, string text)
        {
            using (var stream = reader.CreateStream(name,FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var st = new StreamWriter(stream))
                {
                    st.Write(text);
                }
            }
        }
    }
}