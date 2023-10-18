namespace EDGW.IO
{
    public static class IOReaderExtensions
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
    }
}