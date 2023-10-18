using EDGW.Globalization;

namespace EDGW.IO
{
    public class DirectoryNotFoundException : GlobalizedException
    {
        public DirectoryNotFoundException(string dirname) : base(TextParsers.EXCEPTIONS.GetText("directory_not_found", dirname))
        {
        }
        public DirectoryNotFoundException(Text text) : base(text)
        {
        }
    }
}