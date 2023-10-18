using EDGW.Globalization;

namespace EDGW.IO
{
    public class FileNotFoundException : GlobalizedException
    {
        public FileNotFoundException(string filename) : base(TextParsers.EXCEPTIONS.GetText("file_not_found", filename))
        {
        }
        public FileNotFoundException(Text text) : base(text)
        {
        }
    }
}