namespace EDGW.Globalization
{
    public class NotSupportedException : GlobalizedException
    {
        public NotSupportedException(Text text) : base(text)
        {
        }
    }
}