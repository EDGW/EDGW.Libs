using EDGW.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.IO
{
    internal static class TextParsers
    {
        public static Identifier BaseIdentifier { get; } = "edgw.io";
        public static TextParser EXCEPTIONS { get; } = new TextParser(BaseIdentifier + "exceptions");
    }
}
