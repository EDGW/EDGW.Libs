using EDGW.Data;
using EDGW.Data.Logging;
using EDGW.Data.Registries;
using Newtonsoft.Json.Linq;

namespace EDGW.Globalization
{
    public static class Languages
    {
        static Logger logger = new("Globalization");
        public static Language EnglishUS { get; } = new Language("en_us", "English(US)", Priority.INNER_HIGHEST);
        public static Language ChineseSimplified { get; } = new Language("zh_cn", "简体中文", Priority.HIGHEST);
        static Languages()
        {
            Registry.Register(EnglishUS);
            Registry.Register(ChineseSimplified);

            AddLanguageFile("zh_cn", new JsonLanguageFile(JObject.Parse(LanRes.zh_cn)));
        }
        public static IRegistry<Language> Registry { get; } = new PriorityRegistry<Language>();
        public static string SelectedLanguage { get; } = "zh_cn";
        public static Language GetSelectedLanguage()
        {
            return Registry.GetOrNull(SelectedLanguage) ?? EnglishUS;
        }
        public static void AddLanguageFile(Language lan, ILanguageFile lanfile)
        {
            lan.Add(lanfile);
            logger.Info($"Registered language file {lanfile} for {lan}.");
        }
        public static void AddLanguageFile(string lantype,ILanguageFile lanfile)
        {
            if (!Registry.IsRegistered(lantype))
            {
                Language lan = new Language(lantype, $"(NS)lantype", Priority.LOW);
                Registry.Register(lan);
                logger.Info($"Registered language file {lanfile} for {lantype}.");
            }
            AddLanguageFile(Registry.Get(lantype), lanfile);
        }
        public static string GetString(string key,params string[] parameters)
        {
            var lan = GetSelectedLanguage();
            var val = lan.GetString(key, parameters);
            if (val != null) return val;
            else
            {
                foreach(var language in Registry)
                {
                    val = language.GetString(key, parameters);
                    if (val != null) return val;
                }
            }
            logger.Warn($"missing value of key {key} in {lan}");
            return key;
        }
    }
}