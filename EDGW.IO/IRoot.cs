using EDGW.Globalization;

namespace EDGW.IO
{
    public interface IRoot
    {
        public string Name { get; }
        public Text FriendlyName { get; }
        public IDirectory Directory { get; }
    }
}