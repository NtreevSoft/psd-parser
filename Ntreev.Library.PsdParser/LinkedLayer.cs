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
        private PSD psb;
        private DescriptorStructure descriptor;

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

            string fileType = reader.ReadAscii(4);
            string fileCreator = reader.ReadAscii(4);

            long p = reader.Position - position;

            //long p = reader.Position;
            this.ReadPSB(reader);
            //if ((fileType == "8BPB" && fileCreator == "8BIM") || Path.GetExtension(this.fileName) == ".psb")
            //{
            //    this.psb = ;
            //}
            //else
            //{
            //    this.ReadOther(reader);
            //}

            reader.Position = position + length;
            if (reader.Position % 2 != 0)
                reader.Position++;

            reader.Position += ((reader.Position - position) % 4);
        }

        private void ReadOther(PSDReader reader)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            bool fod = reader.ReadBoolean();
            if (fod == true)
            {
                this.descriptor = new DescriptorStructure(reader);
            }

            byte[] bytes = reader.ReadBytes((int)length);

            //using (MemoryStream ms = new MemoryStream(bytes))
            //{
            //    System.Drawing.Bitmap b = new System.Drawing.Bitmap(ms);
            //    b.Save("d:\\test.png", System.Drawing.Imaging.ImageFormat.Png);
            //}
            
            //CompressionType compressionType = (CompressionType)reader.ReadInt16();
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

        private void ReadPSB(PSDReader reader)
        {
            long length = reader.ReadInt64();
            long position = reader.Position;

            if (length % 4 != 0)
            {
                int weqr = 0;
            }

            bool fod = reader.ReadBoolean();
            if (fod == true)
            {
                this.descriptor = new DescriptorStructure(reader);
            }

            byte[] bytes = reader.ReadBytes((int)length);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                if (this.IsPsd(bytes) == true)
                {
                    PSD psb = new PSD();
                    psb.Read(stream);
                    this.psb = psb;
                }
                else
                {
                    
                }
                //reader.Position = position + length;
            }
        }

        private bool IsPsd(byte[] bytes)
        {
             using (MemoryStream stream = new MemoryStream(bytes))
             using (PSDReader reader = new PSDReader(stream))
             {
                 return reader.ReadAscii(4) == "8BPS";
             }
        }
    }
}
