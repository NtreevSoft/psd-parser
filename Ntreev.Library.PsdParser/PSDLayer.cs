using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSDLayer
    {
        private int groupStatus;
        private byte opacityValue;
        private BlendMode blendMode;
        public IProperties props;

        public void load(BinaryReader br, int bpp)
        {
            PSDRect rect;
            rect.top = EndianReverser.getInt32(br);
            rect.left = EndianReverser.getInt32(br);
            rect.bottom = EndianReverser.getInt32(br);
            rect.right = EndianReverser.getInt32(br);
            this.area = rect;
            int width = this.area.width;
            int height = this.area.height;
            if ((width > 0x3000) || (height > 0x3000))
            {
                throw new SystemException(string.Format("Too big image (width:{0} height{1})", width, height));
            }
            ushort num3 = EndianReverser.getUInt16(br);
            if (num3 > 0x38)
            {
                throw new SystemException(string.Format("Too many channels {0}", num3));
            }
            this.channels = new PSDChannelInfo[num3];
            for (int i = 0; i < num3; i++)
            {
                PSDChannelInfo info = new PSDChannelInfo {
                    width = this.area.width,
                    height = this.area.height
                };
                info.loadHeader(br);
                this.channels[i] = info;
            }
            string str = PSDUtil.readAscii(br, 4);
            if ("8BIM" != str)
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }
            //br.ReadInt32();
            this.blendMode = this.ToBlendMode(PSDUtil.readAscii(br, 4));
            this.opacityValue = br.ReadByte();
            bool clipping = EndianReverser.getBoolean(br);
            br.ReadByte(); // Flags
            br.ReadByte(); // Filter
            long num5 = EndianReverser.getUInt32(br); // Length of the extra data field ( = the total length of the next five fields).
            long position = br.BaseStream.Position;
            uint num7 = EndianReverser.getUInt32(br);
            Stream baseStream = br.BaseStream;
            baseStream.Position += num7;
            num5 -= br.BaseStream.Position - position;
            position = br.BaseStream.Position;
            num7 = EndianReverser.getUInt32(br);
            Stream stream2 = br.BaseStream;
            stream2.Position += num7;
            num5 -= br.BaseStream.Position - position;
            position = br.BaseStream.Position;
            this.name = PSDUtil.readPascalString(br, 4);
            num5 -= br.BaseStream.Position - position;
            PSDLayerResource resource = new PSDLayerResource();
            this.props = resource;
            while (num5 > 7L)
            {
                position = br.BaseStream.Position;
                resource.load(br);
                num5 -= br.BaseStream.Position - position;
            }
            if (num5 > 0L)
            {
                Stream stream3 = br.BaseStream;
                stream3.Position += num5;
            }
            this.id = resource.id;
            if (!string.IsNullOrEmpty(resource.name))
            {
                this.name = resource.name;
            }
            this.drop = resource.drop;
            this.groupStatus = resource.groupStatus;

            if (this.props.Contains("TypeToolInfo.Text.Txt"))
            {
                int qwr = 0;
                //this.text = this.props["TypeToolInfo.Text.Txt"] as string;
            }
            if ((this.area.width == 0) && (this.groupStatus == 0))
            {
                //this.area = resource.typeToolObj2.area;
            }
        }

        public byte[] mergeChannels()
        {
            if (!this.isImageLayer)
            {
                return null;
            }
            int length = this.channels.Length;
            int num2 = this.channels[0].data.Length;
            byte[] buffer = new byte[(this.area.width * this.area.height) * length];
            int num3 = 0;
            for (int i = 0; i < num2; i++)
            {
                switch (length)
                {
                    case 4:
                        buffer[num3++] = this.channels[3].data[i];
                        buffer[num3++] = this.channels[2].data[i];
                        buffer[num3++] = this.channels[1].data[i];
                        buffer[num3++] = this.channels[0].data[i];
                        break;

                    case 3:
                        buffer[num3++] = this.channels[2].data[i];
                        buffer[num3++] = this.channels[1].data[i];
                        buffer[num3++] = this.channels[0].data[i];
                        break;
                }
            }
            return buffer;
        }

        public PSDRect area { get; internal set; }

        public PSDChannelInfo[] channels { get; internal set; }

        public bool drop { get; internal set; }

        public bool groupClosed
        {
            get
            {
                return (this.groupStatus == 2);
            }
        }

        public bool groupEnded
        {
            get
            {
                return (this.groupStatus == 3);
            }
        }

        public bool groupOpened
        {
            get
            {
                return (this.groupStatus == 1);
            }
        }

        public bool groupStarted
        {
            get
            {
                if (!this.groupOpened)
                {
                    return this.groupClosed;
                }
                return true;
            }
        }

        public int id { get; internal set; }

        public bool isImageLayer
        {
            get
            {
                return (((!this.drop && (this.area.width > 0)) && (this.area.height > 0)) && (this.channels.Length > 0));
            }
        }

        public bool isTextLayer
        {
            get
            {
                return this.props.Contains("TypeToolInfo");
            }
        }

        public string name { get; internal set; }

        public float opacity
        {
            get
            {
                return (((float) this.opacityValue) / 255f);
            }
        }

        public int pitch
        {
            get
            {
                return (this.area.width * this.channels.Length);
            }
        }

        public string text
        {
            get
            {
                if (this.isTextLayer == true)
                    return this.props["TypeToolInfo.Text.Txt"] as string;
                return null;
            }
        }

        private BlendMode ToBlendMode(string text)
        {
            switch (text)
            {
                case "pass":
                    return BlendMode.PassThrough;
                case "norm":
                    return BlendMode.Normal;
                case "diss":
                    return BlendMode.Dissolve;
                case "dark":
                    return BlendMode.Darken;
                case "mul":
                    return BlendMode.Multiply;
                case "idiv":
                    return BlendMode.ColorBurn;
                case "lbrn":
                    return BlendMode.LinearBurn;
                case "dkCl":
                    return BlendMode.DarkerColor;
                case "lite":
                    return BlendMode.Lighten;
                case "scrn":
                    return BlendMode.Screen;
                case "div":
                    return BlendMode.ColorDodge;
                case "lddg":
                    return BlendMode.LinearDodge;
                case "lgCl":
                    return BlendMode.LighterColor;
                case "over":
                    return BlendMode.Overlay;
                case "sLit":
                    return BlendMode.SoftLight;
                case "hLit":
                    return BlendMode.HardLight;
                case "vLit":
                    return BlendMode.VividLight;
                case "lLit":
                    return BlendMode.LinearLight;
                case "pLit":
                    return BlendMode.PinLight;
                case "hMix":
                    return BlendMode.HardMix;
                case "diff":
                    return BlendMode.Difference;
                case "smud":
                    return BlendMode.Exclusion;
                case "fsub":
                    return BlendMode.Subtract;
                case "fdiv":
                    return BlendMode.Divide;
                case "hue":
                    return BlendMode.Hue;
                case "sat":
                    return BlendMode.Saturation;
                case "colr":
                    return BlendMode.Color;
                case "lum":
                    return BlendMode.Luminosity;
            }
            return BlendMode.Normal;
        }
    }
}

