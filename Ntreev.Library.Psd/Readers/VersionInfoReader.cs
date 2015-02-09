using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class VersionInfoReader : VersionInfo
    {
        private readonly int version;
        private readonly bool hasCompatibilityImage;
        private readonly string writerName;
        private readonly string readerName;
        private readonly int fileVersion;

        public VersionInfoReader(PsdReader reader)
        {
            this.version = reader.ReadInt32();
            this.hasCompatibilityImage = reader.ReadBoolean();
            this.writerName = reader.ReadString();
            this.readerName = reader.ReadString();
            this.fileVersion = reader.ReadInt32();
        }

        public override int Version
        {
            get { return this.version; }
        }

        public override bool HasCompatibilityImage
        {
            get { return hasCompatibilityImage; }
        }

        public override string WriterName
        {
            get { return this.writerName; }
        }

        public override string ReaderName
        {
            get { return this.readerName; }
        }

        public override int FileVersion
        {
            get { return this.fileVersion; }
        }
    }
}
