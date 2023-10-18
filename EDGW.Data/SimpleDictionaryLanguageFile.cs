namespace EDGW.Globalization
{
    public abstract class SimpleDictionaryLanguageFile : ILanguageFile
    {
        protected Dictionary<string, string> Dictionary { get; } = new();

        public string? GetFormatString(string key)
        {
            if(Dictionary.ContainsKey(key))return Dictionary[key];
            return null;
        }
    }
}