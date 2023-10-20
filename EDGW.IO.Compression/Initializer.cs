using EDGW.Globalization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDGW.IO.Compression
{
    public static class Initializer
    {
        public static void LoadLanguageFiles()
        {
            Languages.AddLanguageFile("zh_cn", new JsonLanguageFile(JObject.Parse(LanRes.zh_cn)));
        }
        public static Identifier Id { get; } = "edgw.io.compression";
        public static TextParser EXCEPTIONS { get; } = new(Id + "exceptions");
    }
}
