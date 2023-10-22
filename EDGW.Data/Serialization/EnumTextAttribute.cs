namespace EDGW.Data.Serialization
{
    public class EnumTextAttribute : Attribute
    {
        public EnumTextAttribute(string text)
        {
            Text = text;
        }
        public EnumTextAttribute()
        {
            Text = "";
        }

        public string Text { get; set; }
    }
}
