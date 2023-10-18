using EDGW.Globalization;
using Newtonsoft.Json.Linq;

namespace EDGW.IO
{
    public class UsingType : IEquatable<UsingType?>
    {
        public static UsingType FREE { get; } = new("edgw.io.usingtype.free", "edgw.io.free", true, true, true);
        public static UsingType READING { get; } = new("edgw.io.usingtype.reading", "edgw.io.reading", true, true, false);
        public static UsingType WRITING { get; } = new("edgw.io.usingtype.writing", "edgw.io.writing", false, true, false);
        public static UsingType READONLY { get; } = new("edgw.io.usingtype.readonly", "edgw.io.readonly", false, true, false);
        public static UsingType SYNCHRONIZING { get; } = new("edgw.io.usingtype.synchronizing", "edgw.io.synchronizing", false, false, false);
        public UsingType(Text text, Identifier name, bool isModifiable, bool isReadable, bool isRemovable)
        {
            Text = text;
            Name = name;
            IsModifiable = isModifiable;
            IsReadable = isReadable;
            IsRemovable = isRemovable;
        }

        public Text Text { get; protected set; }
        public Identifier Name { get;protected set; }
        public bool IsModifiable { get; protected set; }
        public bool IsReadable { get; protected set; }
        public bool IsRemovable { get; protected set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UsingType);
        }

        public bool Equals(UsingType? other)
        {
            return other is not null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(UsingType? left, UsingType? right)
        {
            return EqualityComparer<UsingType>.Default.Equals(left, right);
        }

        public static bool operator !=(UsingType? left, UsingType? right)
        {
            return !(left == right);
        }
        public JObject ToJson()
        {
            JObject obj = new();
            obj["text"] = Text.ToJson();
            obj["name"] = Name.ToString();
            obj["im"] = IsModifiable;
            obj["ir"] = IsReadable;
            obj["irm"] = IsRemovable;
            return obj;
        }
        public static UsingType Parse(JObject obj)
        {
            Text txt;
            var text = obj["text"];
            if (text != null)
                txt = Text.Parse((JObject)text);
            else return FREE;
            string? name = obj["name"]?.ToString();
            if (name == null) return FREE;
            bool? im = (bool?)obj["im"];
            if (im == null) return FREE;
            bool? ir = (bool?)obj["ir"];
            if (ir == null) return FREE;
            bool? irm = (bool?)obj["irm"];
            if (irm == null) return FREE;
            return new(txt, name, im.Value, ir.Value, irm.Value);
        }
    }
}