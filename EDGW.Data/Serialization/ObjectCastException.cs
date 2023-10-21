using EDGW.Globalization;

namespace EDGW.Data.Serialization
{
    public class ObjectCastException : GlobalizedException
    {
        public ObjectCastException(Type from, Type to) : this(from.FullName ?? "<null_type>", to.FullName ?? "<null_type>")
        {
        }
        public ObjectCastException(string from, string to) : base(TextParsers.EXCEPTIONS.GetText("object_cast", from, to))
        {
        }
    }
}
