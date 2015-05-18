using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ReadableValue<T>
    {
        private readonly PsdReader reader;
        private readonly int readerVersion;
        private readonly long position;
        private readonly long length;
        private T value;
        private bool isRead;

        protected ReadableValue(PsdReader reader)
            : this(reader, false)
        {

        }

        protected ReadableValue(PsdReader reader, bool hasLength)
            : this(reader, hasLength, false)
        {

        }

        protected ReadableValue(PsdReader reader, bool hasLength, bool isLazy)
        {
            if (hasLength == true)
            {
                this.length = this.OnLengthGet(reader);
            }

            this.reader = reader;
            this.readerVersion = reader.Version;
            this.position = reader.Position;

            if (this.length == 0 && isLazy == false)
            {
                this.Read();
            }
            else
            {
                this.reader.Position += this.length;
            }
        }

        protected ReadableValue(PsdReader reader, long length)
        {
            this.reader = reader;
            this.length = length;
            this.readerVersion = reader.Version;
            this.position = reader.Position;

            if (this.length == 0)
            {
                this.Read();
            }
            else
            {
                this.reader.Position += this.length;
            }
        }

        public T Value
        {
            get
            {
                if (this.isRead == false)
                {
                    this.Read();
                }
                return this.value;
            }
        }

        public bool HasLength
        {
            get { return this.length >= 0; }
        }

        public long Length
        {
            get { return this.length; }
        }

        public long Position
        {
            get { return this.position; }
        }

        public long EndPosition
        {
            get
            {
                if (this.HasLength == false)
                    throw new NotSupportedException();
                return this.position + this.length;
            }
        }

        protected virtual long OnLengthGet(PsdReader reader)
        {
            return reader.ReadLength();
        }

        protected abstract void ReadValue(PsdReader reader, out T value);

        private void Read()
        {
            if (this.isRead == true)
                throw new Exception();

            this.reader.Position = this.position;
            this.reader.Version = this.readerVersion;
            this.ReadValue(this.reader, out this.value);
            this.isRead = true;
        }
    }
}