using EDGW.Data.Serialization;
using Newtonsoft.Json.Linq;

namespace EDGW.Globalization
{
    public struct Identifier : IEquatable<Identifier> , IJsonSerializable<IdentifierCaster, Identifier>
    {
        public static Identifier Parse(string str)
        {
            if (str.Contains(":"))
            {
                if (str.EndsWith(":"))
                    return new(str.Substring(0, str.IndexOf(":")), str.Substring(str.IndexOf(":") + 1));
                else return new(str.Substring(0, str.IndexOf(":")), "");
            }
            else
            {
                return new("", str);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Identifier identifier && Equals(identifier);
        }

        public bool Equals(Identifier other)
        {
            return Namespace == other.Namespace &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Namespace, Id);
        }

        public static implicit operator Identifier(string s)
        {
            return Parse(s);
        }
        public static Identifier operator +(Identifier left, string right)
        {
            if (string.IsNullOrWhiteSpace(left.Id)) return new(left.Namespace, right);
            return new(left.Namespace, $"{left.Id}.{right}");
        }

        public static bool operator ==(Identifier left, Identifier right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Identifier left, Identifier right)
        {
            return !(left == right);
        }

        public Identifier(string id) : this("", id)
        {

        }
        public Identifier(string @namespace, string id)
        {
            Namespace = @namespace;
            Id = id;
        }
        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Namespace)) return $"{Namespace}:{Id}";
            return Id;
        }

        public JToken ToJson()
        {
            return this.ToString();
        }

        public string Namespace { get; }
        public string Id { get; }
    }
}