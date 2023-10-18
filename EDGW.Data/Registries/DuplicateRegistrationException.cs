using EDGW.Globalization;

namespace EDGW.Data.Registries
{
    public class DuplicateRegistrationException : GlobalizedException
    {
        public DuplicateRegistrationException(string keyname) : base(TextParsers.EXCEPTIONS.GetText("duplicate_registration", keyname))
        {
        }
        public DuplicateRegistrationException(Text text) : base(text)
        {
        }
    }
}
