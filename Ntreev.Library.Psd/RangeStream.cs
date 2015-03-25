using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class RangeStream : Stream
    {
        private readonly Stream stream;
        private readonly long position;
        private readonly long length;

        public RangeStream(Stream stream, long position, long length)
        {
            this.stream = stream;
            this.position = position;
            this.length = length;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
            //this.stream.Flush();
        }

        public override long Length
        {
            get { return this.length; }
        }

        public override long Position
        {
            get
            {
                return this.stream.Position - this.position;
            }
            set
            {
                this.stream.Position = this.position + value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Current)
                return this.stream.Seek(offset, origin) - this.position;

            return this.stream.Seek(this.position + offset, origin) - this.position;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
