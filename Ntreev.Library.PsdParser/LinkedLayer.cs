using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public sealed class LinkedLayer
    {
        private readonly Guid id;
        private readonly string fileName;
        private readonly PSD psb;

        internal LinkedLayer(PSDReader reader)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            string type = reader.ReadAscii(4);
            int version = reader.ReadInt32();

            if (type != "liFD" || version != 2)
                throw new Exception("Invalid PSD file");
            this.id = new Guid(reader.ReadPascalString(1));
            this.fileName = reader.ReadUnicodeString2();

            int fileType = reader.ReadInt32();
            int fileCreator = reader.ReadInt32();
            this.psb = this.ReadPSB(reader);

            reader.Position = position + length;
        }

        public PSD PSD
        {
            get { return this.psb; }
        }

        public Guid ID
        {
            get { return this.id; }
        }

        public string FileName
        {
            get { return this.fileName; }
        }

        private PSD ReadPSB(PSDReader reader)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            bool fod = reader.ReadBoolean();
            if (fod == true)
            {
                DescriptorStructure desc = new DescriptorStructure(reader);
            }

            byte[] bytes = reader.ReadBytes((int)length);

            PSD psb = new PSD();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                psb.Read(stream);
            }

            reader.Position = position + length;
            return psb;
        }
    }
}
