using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class ValueReader<T>
    {
        private readonly PsdReader reader;
        private readonly int readerVersion;
        private readonly long position;
        private readonly long length;
        private readonly object userData;
        private T value;
        private bool isRead;


        //protected ValueReader(PsdReader reader, object userData)
        //    : this(reader, false, userData)
        //{

        //}

        protected ValueReader(PsdReader reader, bool hasLength, object userData)
        {
            if (hasLength == true)
            {
                this.length = this.OnLengthGet(reader);
            }

            this.reader = reader;
            this.readerVersion = reader.Version;
            this.position = reader.Position;
            this.userData = userData;

            if (this.length == 0)
            {
                this.Refresh();
                this.length = reader.Position - this.position;
            }
            else
            {
                //this.Refresh();
            }
            //else
            {
                this.reader.Position = this.position + this.length;
            }
        }

        protected ValueReader(PsdReader reader, long length, object userData)
        {
            this.reader = reader;
            this.length = length;
            this.readerVersion = reader.Version;
            this.position = reader.Position;
            this.userData = userData;

            if (this.length == 0)
            {
                this.Refresh();
                this.length = reader.Position - this.position;
            }
            else
            {
                //this.Refresh();
            }

            {
                this.reader.Position = this.position + this.length;
            }
        }

        public void Refresh()
        {
            this.reader.Position = this.position;
            this.reader.Version = this.readerVersion;
            this.ReadValue(this.reader, this.userData, out this.value);
            if (this.length > 0)
                this.reader.Position = this.position + this.length;
            this.isRead = true;
        }

        public T Value
        {
            get
            {
                if (this.isRead == false)
                {
                    this.Refresh();
                }
                return this.value;
            }
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
            get { return this.position + this.length; }
        }

        protected virtual long OnLengthGet(PsdReader reader)
        {
            return reader.ReadLength();
        }

        protected abstract void ReadValue(PsdReader reader, object userData, out T value);
    }
}