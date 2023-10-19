namespace EDGW.IO
{
    public class NormalFileStream : Stream
    {
        public NormalFileStream(NormalFile file,FileMode mode,FileAccess access)
        {
            lock (file.locker)
            {
                File = file;
                Stream = file.IOProvider.CreateStream(file.Name, mode, access);
                if (access == FileAccess.Read) UsingType = UsingType.READING;
                else if (access == FileAccess.ReadWrite) UsingType = UsingType.WRITING;
                else if (access == FileAccess.Write) UsingType = UsingType.WRITING;
                else throw new ArgumentException($"Unsupported FileAccess \"{access}\".");
                var usingtype = File.UsingType;
                usingtype.Add(UsingType);
                File.SaveUsingTypes(usingtype);
            }
        }
        public Stream Stream { get; }
        public bool IsClosed { get; private set; } = false;
        object locker = new();
        public UsingType UsingType { get; }
        public NormalFile File { get; }
        void CloseFileUse()
        {
            lock (File.locker)
            {
                var usingtype = File.UsingType;
                usingtype.Remove(UsingType);
                File.SaveUsingTypes(usingtype);
            }
        }
        public override void Close()
        {
            base.Close();
            lock (locker)
            {
                if (!IsClosed)
                {
                    IsClosed = true;
                    Stream.Close();
                    CloseFileUse();
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!IsClosed)
            {
                IsClosed = true;
                CloseFileUse();
            }
            Stream.Dispose();
        }
        ~NormalFileStream()
        {
            if (!IsClosed)
            {
                CloseFileUse();
            }
        }
        public override bool CanRead => Stream.CanRead;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanWrite => Stream.CanWrite;

        public override long Length => Stream.Length;

        public override long Position { get => Stream.Position; set => Stream.Position = value; }

        public override void Flush()
        {
            Stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Stream.Write(buffer, offset, count);
        }
    }
}