using Ntreev.Library.Psd.Readers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd
{
    public class PsdDocument : IPsdLayer, IDisposable
    {
        private ColorModeData colorModeData;
        private FileHeader fileHeader;
        private GridAndGuidesInfo gridAndGuidesInfo;
        private PsdLayer[] layers;
        private ResolutionInfo resolutionInfo;
        private Channel[] channels;
        private LinkedLayer[] linkedLayers;
        private SliceInfo[] slices = new SliceInfo[] { };
        private GlobalLayerMask globalLayerMask;
        private VersionInfo versionInfo;
        private Properties props = new Properties();
        private PsdReader reader;
        private long imagePosition;
        private Uri baseUri;

        public PsdDocument()
        {
            
        }

        internal PsdDocument(Uri baseUri)
        {
            this.baseUri = baseUri;
        }

        public void Read(string filename)
        {
            this.Read(filename, new PathResolver());
        }

        public void Read(string filename, PsdResolver resolver)
        {
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            this.baseUri = new Uri(Path.GetDirectoryName(filename) + Path.AltDirectorySeparatorChar, UriKind.RelativeOrAbsolute);
            if (this.baseUri.IsAbsoluteUri == false)
                this.baseUri = new Uri(new Uri(Directory.GetCurrentDirectory() + Path.AltDirectorySeparatorChar), this.baseUri);
            this.Read(stream, resolver);
        }

        public void Read(Stream stream)
        {
            this.Read(stream, null);
        }

        public void Read(Stream stream, PsdResolver resolver)
        {
            this.reader = new PsdReader(stream, resolver);
            this.fileHeader = new FileHeaderReader(this.reader);
            this.colorModeData = new ColorModeDataReader(this.reader);
            this.ReadImageResources(this.reader);
            this.ReadLayers(this.reader);
            this.imagePosition = this.reader.Position;
        }

        public void Dispose()
        {
            if (this.reader == null)
                return;

            this.reader.Dispose();
            this.reader = null;
            this.OnDisposed(EventArgs.Empty);
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

        public VersionInfo VersionInfo
        {
            get { return this.versionInfo; }
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

        public IProperties Properties
        {
            get { return this.props; }
        }

        public GlobalLayerMask GlobalLayerMask
        {
            get { return this.globalLayerMask; }
        }

        public bool HasImage
        {
            get 
            {
                if (this.versionInfo == null)
                    return false;
                return this.versionInfo.HasCompatibilityImage;
            }
        }

        public event EventHandler Disposed;

        internal Uri BaseUri
        {
            get
            {
                if (this.baseUri == null)
                    return new Uri(Directory.GetCurrentDirectory());
                return this.baseUri;
            }
        }

        protected virtual void OnDisposed(EventArgs e)
        {
            if (this.Disposed != null)
            {
                this.Disposed(this, e);
            }
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
                        case 0x0421:
                            {
                                this.versionInfo = new VersionInfoReader(reader);
                            }
                            break;
                        //case 0x040C:
                        //    {
                        //        int qwerqwer = 0;
                        //        var format = reader.ReadInt32();
                        //        var w = reader.ReadInt32();
                        //        var h = reader.ReadInt32();
                        //        var wb = reader.ReadInt32();
                        //        var ts = reader.ReadInt32();
                        //        var sac = reader.ReadInt32();
                        //        var bpp = reader.ReadInt16();
                        //        var nop = reader.ReadInt16();
                        //    }
                        //    break;
                        //case 0x0409:
                        //    {
                        //        int wqer = 0;
                        //    }
                        //    break;
                        case 0x0408:
                            this.gridAndGuidesInfo = new GridAndGuidesInfo(reader);
                            break;
                        case 0x3ed:
                            this.resolutionInfo = new ResolutionInfo(reader);
                            break;
                        case 0x041a:
                            {
                                var version = reader.ReadInt32();
                                if (version == 6)
                                {
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

                                        //this.slices = slices.ToArray();
                                    }

                                    {
                                        var descriptor = new DescriptorStructure(reader) as IProperties;
                                        this.props.Add(imageResourceID.ToString(), descriptor);

                                        var items = descriptor["slices.Items[0]"] as object[];
                                        List<SliceInfo> slices = new List<SliceInfo>(items.Length);
                                        foreach (var item in items)
                                        {
                                            slices.Add(new SliceInfo(item as IProperties));
                                        }
                                        this.slices = slices.ToArray();
                                    }
                                }
                                else
                                {
                                    var descriptor = new DescriptorStructure(reader) as IProperties;
                                    this.props.Add(imageResourceID.ToString(), descriptor);

                                    var items = descriptor["slices.Items[0]"] as object[];
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

            this.layers = LayerInfo.ReadLayers(reader, this);
            LayerInfo.ComputeBounds(this.layers);
            this.globalLayerMask = new GlobalLayerMask(reader);

            List<string> keys = new List<string>(new string[] { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", "extd", });
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
                                linkedLayers.Add(new EmbeddedLayer(reader, this.BaseUri));
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
                                linkedLayers.Add(new LinkedLayer(reader, this.BaseUri));
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
                    item.LinkedLayer = this.linkedLayers.Where(i => i.ID == item.PlacedID && i.HasDocument).FirstOrDefault();
                }

                this.SetLinkedLayer(item.Childs);
            }
        }

        private void ReadImageData(PsdReader reader)
        {
            CompressionType compressionType = (CompressionType)reader.ReadInt16();

            ChannelType[] types = new ChannelType[] { ChannelType.Red, ChannelType.Green, ChannelType.Blue, ChannelType.Alpha, };
            Channel[] channels = new Channel[this.fileHeader.NumberOfChannels];

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i] = new Channel(types[i], this.Width, this.Height, 0);
                channels[i].LoadHeader(reader, compressionType);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                channels[i].Load(reader, this.fileHeader.Depth, compressionType);
            }

            if (channels.Length == 4)
            {
                for (int i = 0; i < channels[3].Data.Length; i++)
                {
                    float a = channels[3].Data[i] / 255.0f;

                    for (int j = 0; j < 3; j++)
                    {
                        float r = channels[j].Data[i] / 255.0f;
                        float r1 = (((a + r) - 1f) * 1f) / a;
                        channels[j].Data[i] = (byte)(r1 * 255.0f);
                    }
                }
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

        string IPsdLayer.Name
        {
            get { return "Root"; }
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

        IChannel[] IImageSource.Channels
        {
            get 
            {
                if (this.channels == null)
                {
                    this.reader.Position = this.imagePosition;
                    this.ReadImageData(this.reader);
                }
                return this.channels; 
            }
        }

        float IImageSource.Opacity
        {
            get { return 1.0f; }
        }

        #endregion
    }
}

