using System;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSD
    {
        private ColorModeData colorModeData;
        private DisplayInfo displayInfo;
        public string fileName;
        public string filePath;
        private FileHeader fileHeader;
        public PSDLayerInfo layerInfo;
        private ResolutionInfo resolutionInfo;

        public void loadData()
        {
            if (this.layerInfo == null)
            {
                throw new SystemException("Load header in the first");
            }
            using (FileStream stream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (PSDReader reader = new PSDReader(stream))
                {
                    this.layerInfo.loadData(reader, this.fileHeader.BPP);
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
                using (PSDReader reader = new PSDReader(stream))
                {
                    this.ReadFileHeader(reader);
                    this.ReadColorModeData(reader);
                    this.ReadImageResources(reader);
                    this.ReadLayers(reader);
                }
            }
        }

        public FileHeader FileHeader
        {
            get { return this.fileHeader; }
        }

        public ColorModeData ColorModeData
        {
            get { return this.colorModeData; }
        }

        public DisplayInfo DisplayInfo
        {
            get { return this.displayInfo; }
        }

        public ResolutionInfo ResolutionInfo
        {
            get { return this.resolutionInfo; }
        }

        private void ReadColorModeData(PSDReader reader)
        {
            this.colorModeData = new ColorModeData(reader);
        }

        private void ReadFileHeader(PSDReader reader)
        {
            this.fileHeader = new FileHeader(reader);
            if (this.fileHeader.BPP != 8)
            {
                throw new SystemException("For now, only Support 8 Bit Per Channel");
            }
        }

        private void ReadImageResources(PSDReader reader)
        {
            int size = reader.ReadInt32();
            long position = reader.Position;
            while ((reader.Position - position) < size)
            {
                string signature = reader.ReadAscii(4);
                if (signature != "8BIM")
                {
                    continue;
                }
                short imageResourceID = reader.ReadInt16();
                string name = reader.ReadPascalString(2);
                int resourceSize = reader.ReadInt32();
                if (resourceSize > 0)
                {
                    switch (imageResourceID)
                    {
                        case 0x3ed:
                            this.resolutionInfo = new ResolutionInfo(reader);
                            break;

                        case 0x3ef:
                            this.displayInfo = new DisplayInfo(reader);
                            break;

                        default:
                        {
                            reader.Position += resourceSize;
                            break;
                        }
                    }
                    if ((resourceSize % 2) != 0)
                    {
                        reader.Position += 1L;
                    }
                }
            }
        }

        private void ReadLayers(PSDReader reader)
        {
            this.layerInfo = new PSDLayerInfo();
            this.layerInfo.loadHeader(reader, this.fileHeader.BPP);
        }
    }
}

