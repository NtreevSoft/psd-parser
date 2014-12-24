using Ntreev.Library.Psd.Readers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    public sealed class PsdDocument : IPsdLayer
    {
        private string name;
        private ColorModeData colorModeData;
        private FileHeader fileHeader;
        private GridAndGuidesInfo gridAndGuidesInfo;
        private PsdLayer[] layers;
        private ResolutionInfo resolutionInfo;
        private Channel[] channels;
        private LinkedLayer[] linkedLayers;
        private SliceInfo[] slices = new SliceInfo[] { };
        private GlobalLayerMask globalLayerMask;
        private Properties props = new Properties();

        public PsdDocument()
            : this("Root")
        {
            
        }

        public PsdDocument(string name)
        {
            this.name = name;
        }

        public void Read(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                this.Read(stream, new PathResolver(Path.GetDirectoryName(filename)));
            }
        }

        public void Read(Stream stream)
        {
            this.Read(stream, null);
        }

        public void Read(Stream stream, PsdResolver resolver)
        {
            using (PsdReader reader = new PsdReader(stream, resolver))
            {
                this.fileHeader = new FileHeaderReader(reader);
                this.colorModeData = new ColorModeDataReader(reader);
                this.ReadImageResources(reader);
                this.ReadLayers(reader);
                this.ReadImageData(reader);
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

        public ResolutionInfo ResolutionInfo
        {
            get { return this.resolutionInfo; }
        }

        public GridInfo GridInfo
        {
            get { return this.gridAndGuidesInfo.GridInfo; }
        }

        public GuideInfo GuideInfo
        {
            get { return this.gridAndGuidesInfo.GuidesInfo; }
        }

        public SliceInfo[] Slices
        {
            get { return this.slices; }
        }

        public int Width
        {
            get { return this.fileHeader.Width; }
        }

        public int Height
        {
            get { return this.fileHeader.Height; }
        }

        public int Depth
        {
            get { return this.fileHeader.Depth; }
        }

        public IEnumerable<IPsdLayer> Childs
        {
            get { return this.layers; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public IProperties Properties
        {
            get { return this.props; }
        }

        public GlobalLayerMask GlobalLayerMask
        {
            get { return this.globalLayerMask; }
        }

        private void ReadColorModeData(PsdReader reader)
        {
            this.colorModeData = new ColorModeDataReader(reader);
        }

        private void ReadFileHeader(PsdReader reader)
        {
            this.fileHeader = new FileHeaderReader(reader);
            reader.Version = this.fileHeader.Version;
            if (this.fileHeader.Depth != 8)
            {
                throw new SystemException("For now, only Support 8 Bit Per Channel");
            }
        }

        private void ReadImageResources(PsdReader reader)
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
                        case 0x0408:
                            this.gridAndGuidesInfo = new GridAndGuidesInfo(reader);
                            break;
                        case 0x3ed:
                            this.resolutionInfo = new ResolutionInfo(reader);
                            break;
                        case 0x041a:
                            {
                                long ppp = reader.Position;
                                var version = reader.ReadInt32();
                                if (version == 6)
                                {
                                    var r1 = reader.ReadInt32();
                                    var r2 = reader.ReadInt32();
                                    var r3 = reader.ReadInt32();
                                    var r4 = reader.ReadInt32();
                                    string text = reader.ReadString();
                                    var count = reader.ReadInt32();

                                    List<SliceInfo> slices = new List<SliceInfo>(count);
                                    for (int i = 0; i < count; i++)
                                    {
                                        slices.Add(new SliceInfo(reader));
                                    }

                                    this.slices = slices.ToArray();

                                    int descVer = reader.ReadInt32();
                                    this.props.Add(imageResourceID.ToString(), new DescriptorStructure(reader));
                                }
                                else
                                {
                                    int descVer = reader.ReadInt32();
                                    var v = new DescriptorStructure(reader) as IProperties;
                                    this.props.Add(imageResourceID.ToString(), v);

                                    var items = v["slices.Items[0]"] as object[];
                                    List<SliceInfo> slices = new List<SliceInfo>(items.Length);
                                    foreach (var item in items)
                                    {
                                        slices.Add(new SliceInfo(item as IProperties));
                                    }
                                    this.slices = slices.ToArray();
                                }
                            }
                            break;

                        default:
                            {
                                reader.Position += resourceSize;
                                this.props.Add(imageResourceID.ToString(), null);
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

        private void ReadLayers(PsdReader reader)
        {
            long length = reader.ReadLength();
            long end = reader.Position + length;

            this.layers = LayerInfo.ReadLayers(reader, this, this.fileHeader.Depth);
            LayerInfo.ComputeBounds(this.layers);
            this.globalLayerMask = new GlobalLayerMask(reader);

            List<string> keys = new List<string>(new string[] { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", });
            List<LinkedLayer> linkedLayers = new List<LinkedLayer>();

            while (reader.Position < end)
            {
                string signature = reader.ReadType();
                string key = reader.ReadType();
                if (signature != "8BIM" && signature != "8B64")
                    throw new InvalidFormatException();

                long ssss = reader.Position;

                long l = 0;
                long p;

                if (keys.Contains(key) == true && reader.Version == 2)
                {
                    l = reader.ReadInt64();
                    p = reader.Position;
                }
                else
                {
                    l = reader.ReadInt32();
                    p = reader.Position;
                }

                switch (key)
                {
                    case "lnkE":
                        {
                            long e = reader.Position + l;
                            while (reader.Position < e)
                            {
                                linkedLayers.Add(new EmbeddedLayer(reader));
                            }
                        }
                        break;
                    case "lnkD":
                    case "lnk2":
                    case "lnk3":
                        {
                            long e = reader.Position + l;
                            while (reader.Position < e)
                            {
                                linkedLayers.Add(new LinkedLayer(reader));
                            }
                        }
                        break;
                }

                reader.Position = p + l;
                if (reader.Position % 2 != 0)
                    reader.Position++;

                reader.Position += ((reader.Position - p) % 4);
            }
            this.linkedLayers = linkedLayers.ToArray();

            this.SetLinkedLayer(this.layers);
        }

        private void SetLinkedLayer(IEnumerable<PsdLayer> layers)
        {
            foreach (var item in layers)
            {
                if (item.PlacedID != Guid.Empty)
                {
                    item.LinkedLayer = this.linkedLayers.Where(i => i.ID == item.PlacedID && i.Document != null).FirstOrDefault();
                }

                this.SetLinkedLayer(item.Childs);
            }
        }

        private void ReadImageData(PsdReader reader)
        {
            CompressionType compressionType = (CompressionType)reader.ReadInt16();

            ChannelType[] types = new ChannelType[] { ChannelType.Red, ChannelType.Green, ChannelType.Blue, ChannelType.Alpha, };
            Channel[] channels = new Channel[this.fileHeader.NumberOfChannels];

            for(int i=0; i <channels.Length ; i++)
            {
                channels[i] = new Channel(types[i], this.Width, this.Height);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = new Channel(types[i], this.Width, this.Height);
                channels[i].LoadHeader(reader, compressionType);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i].Load(reader, this.fileHeader.Depth, compressionType);
            }

            this.channels = channels.OrderBy(item => item.Type).ToArray();
        }

        #region IPsdLayer

        IPsdLayer IPsdLayer.Parent
        {
            get { return null; }
        }

        bool IPsdLayer.IsClipping
        {
            get { return false; }
        }

        PsdDocument IPsdLayer.Document
        {
            get { return this; }
        }

        ILinkedLayer IPsdLayer.LinkedLayer
        {
            get { return null; }
        }

        int IPsdLayer.Left
        {
            get { return 0; }
        }

        int IPsdLayer.Top
        {
            get { return 0; }
        }

        int IPsdLayer.Right
        {
            get { return this.Width; }
        }

        int IPsdLayer.Bottom
        {
            get { return this.Height; }
        }

        BlendMode IPsdLayer.BlendMode
        {
            get { return BlendMode.Normal; }
        }

        bool IImageSource.HasImage
        {
            get { return true; }
        }

        IChannel[] IImageSource.Channels
        {
            get { return this.channels; }
        }

        float IImageSource.Opacity
        {
            get { return 1.0f; }
        }

        #endregion
    }
}

