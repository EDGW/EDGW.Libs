namespace EDGW.Globalization
{
    public class GlobalizedException : Exception
    {
        public GlobalizedException(Text text)
        {
            Text = text;
        }
        public Text Text { get; }
        public override string Message => Text.ToString() ?? "";
    }
}