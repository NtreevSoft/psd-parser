namespace Ntreev.Library.PsdParser
{
    using System;
    using System.IO;

    public sealed class PSD
    {
        public PSDColorModeInfo colorModeInfo;
        public PSDDisplayInfo displayInfo;
        public string fileName;
        public string filePath;
        public PSDHeaderInfo headerInfo;
        public PSDLayerInfo layerInfo;
        public PSDResolutionInfo resolutionInfo;

        public void loadData()
        {
            if (this.layerInfo == null)
            {
                throw new SystemException("Load header in the first");
            }
            using (FileStream stream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    this.layerInfo.loadData(reader, this.headerInfo.bpp);
                }
            }
        }

        public void loadHeader(string filePath)
        {
            this.filePath = filePath;
            int num = filePath.LastIndexOfAny(@"/\".ToCharArray());
            if (num >= 0)
            {
                this.fileName = filePath.Substring(num + 1);
            }
            else
            {
                this.fileName = filePath;
            }
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    this.readHeader(reader);
                    this.readColorMode(reader);
                    this.readImageResource(reader);
                    this.readLayers(reader);
                }
            }
        }

        private void readColorMode(BinaryReader br)
        {
            this.colorModeInfo = new PSDColorModeInfo();
            this.colorModeInfo.load(br);
        }

        private void readHeader(BinaryReader br)
        {
            this.headerInfo = new PSDHeaderInfo();
            this.headerInfo.load(br);
            if (this.headerInfo.bpp != 8)
            {
                throw new SystemException("For now, only Support 8 Bit Per Channel");
            }
        }

        private void readImageResource(BinaryReader br)
        {
            int num = EndianReverser.getInt32(br);
            long position = br.BaseStream.Position;
            while ((br.BaseStream.Position - position) < num)
            {
                if (PSDUtil.readAscii(br, 4) != "8BIM")
                {
                    continue;
                }
                short num3 = EndianReverser.getInt16(br);
                PSDUtil.readPascalString(br, 2);
                int num4 = EndianReverser.getInt32(br);
                if (num4 > 0)
                {
                    switch (num3)
                    {
                        case 0x3ed:
                            this.resolutionInfo = new PSDResolutionInfo();
                            this.resolutionInfo.load(br);
                            break;

                        case 0x3ef:
                            this.displayInfo = new PSDDisplayInfo();
                            this.displayInfo.load(br);
                            break;

                        default:
                        {
                            Stream baseStream = br.BaseStream;
                            baseStream.Position += num4;
                            break;
                        }
                    }
                    if ((num4 % 2) != 0)
                    {
                        Stream stream2 = br.BaseStream;
                        stream2.Position += 1L;
                    }
                }
            }
        }

        private void readLayers(BinaryReader br)
        {
            this.layerInfo = new PSDLayerInfo();
            this.layerInfo.loadHeader(br, this.headerInfo.bpp);
        }
    }
}

