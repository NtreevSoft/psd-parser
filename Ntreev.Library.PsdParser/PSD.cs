using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSD
    {
        public ColorModeData colorModeInfo;
        public DisplayInfo displayInfo;
        public string fileName;
        public string filePath;
        public FileHeader headerInfo;
        public PSDLayerInfo layerInfo;
        public ResolutionInfo resolutionInfo;

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
                    this.ReadFileHeader(reader);
                    this.ReadColorModeData(reader);
                    this.ReadImageResources(reader);
                    this.ReadLayers(reader);
                }
            }
        }

        private void ReadColorModeData(BinaryReader br)
        {
            this.colorModeInfo = new ColorModeData();
            this.colorModeInfo.load(br);
        }

        private void ReadFileHeader(BinaryReader br)
        {
            this.headerInfo = new FileHeader();
            this.headerInfo.load(br);
            if (this.headerInfo.bpp != 8)
            {
                throw new SystemException("For now, only Support 8 Bit Per Channel");
            }
        }

        private void ReadImageResources(BinaryReader br)
        {
            int size = EndianReverser.getInt32(br);
            long position = br.BaseStream.Position;
            while ((br.BaseStream.Position - position) < size)
            {
                string signature = PSDUtil.readAscii(br, 4);
                if (signature != "8BIM")
                {
                    continue;
                }
                short imageResourceID = EndianReverser.getInt16(br);
                string name = PSDUtil.readPascalString(br, 2);
                int resourceSize = EndianReverser.getInt32(br);
                if (resourceSize > 0)
                {
                    switch (imageResourceID)
                    {
                        case 0x3ed:
                            this.resolutionInfo = new ResolutionInfo(br);
                            break;

                        case 0x3ef:
                            this.displayInfo = new DisplayInfo(br);
                            break;

                        default:
                        {
                            Stream baseStream = br.BaseStream;
                            baseStream.Position += resourceSize;
                            break;
                        }
                    }
                    if ((resourceSize % 2) != 0)
                    {
                        Stream stream2 = br.BaseStream;
                        stream2.Position += 1L;
                    }
                }
            }
        }

        private void ReadLayers(BinaryReader br)
        {
            this.layerInfo = new PSDLayerInfo();
            this.layerInfo.loadHeader(br, this.headerInfo.bpp);
        }
    }
}

