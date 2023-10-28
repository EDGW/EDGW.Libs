using Newtonsoft.Json.Linq;
using System.Text;

namespace EDGW.Data.Logging
{
    public class BindedTextWriter : TextWriter
    {
        public BindedTextWriter(TextWriter main, params TextWriter[] bindings)
        {
            Main = main;
            Bindings = bindings;
        }

        public TextWriter Main { get; }
        public TextWriter[] Bindings { get; }
        public override Encoding Encoding => Main.Encoding;
        public override void Close()
        {
            Main.Close();
        }
        protected override void Dispose(bool disposing)
        {
            Main.Dispose();
        }
        public async override ValueTask DisposeAsync()
        {
            await Main.DisposeAsync();
        }
        public override void Write(string? value)
        {
            Main.Write(value);
            foreach(var m in Bindings)
            {
                m.Write(value);
            }
        }
        public override void Flush()
        {
            Main.Flush();
            foreach (var m in Bindings)
            {
                m.Flush();
            }

        }
        public async override Task FlushAsync()
        {
            await Main.FlushAsync();
            foreach (var m in Bindings)
            {
                await m.FlushAsync();
            }
        }
    }
}
