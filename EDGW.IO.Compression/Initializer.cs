using EDGW.Data.Logging;
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
        static Logger logger = new("EDGW.IO.Compression");
        public static void LoadLanguageFiles()
        {
            Languages.AddLanguageFile("zh_cn", new JsonLanguageFile(JObject.Parse(LanRes.zh_cn)));
            logger.Info("Registered language files.");
        }
        public static Identifier Id { get; } = "edgw.io.compression";
        public static TextParser EXCEPTIONS { get; } = new(Id + "exceptions");
    }
}
