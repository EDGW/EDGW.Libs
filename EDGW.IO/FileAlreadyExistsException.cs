using EDGW.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.IO
{
    public class FileAlreadyExistsException : GlobalizedException
    {
        public FileAlreadyExistsException(string filename) : base(TextParsers.EXCEPTIONS.GetText("file_already_exists", filename))
        {
        }
    }
}
