namespace EDGW.Globalization
{
    public class TextParser
    {
        public TextParser(Identifier baseIdentifier)
        {
            BaseIdentifier = baseIdentifier;
        }

        public Identifier BaseIdentifier { get; }
        public Text GetText(string id, params string[] parameters)
        {
            return new Text(BaseIdentifier + id, parameters);
        }
    }
}