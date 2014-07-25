using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    public sealed class PSDLayerResource : Properties
    {
        public bool drop;
        public int groupStatus;
        public int id;
        public string name;

        private const string PSD_LADJ_BALANCE = "blnc";
        private const string PSD_LADJ_BLACK_WHITE = "blwh";
        private const string PSD_LADJ_BRIGHTNESS = "brit";
        private const string PSD_LADJ_CURVE = "curv";
        private const string PSD_LADJ_EXPOSURE = "expA";
        private const string PSD_LADJ_GRAD_MAP = "grdm";
        private const string PSD_LADJ_HUE = "hue ";
        private const string PSD_LADJ_HUE2 = "hue2";
        private const string PSD_LADJ_INVERT = "nvrt";
        private const string PSD_LADJ_LEVEL = "levl";
        private const string PSD_LADJ_MIXER = "mixr";
        private const string PSD_LADJ_PHOTO_FILT = "phfl";
        private const string PSD_LADJ_POSTERIZE = "post";
        private const string PSD_LADJ_SELECTIVE = "selc";
        private const string PSD_LADJ_THRESHOLD = "thrs";
        private const string PSD_LFIL_GRADIENT = "GdFl";
        private const string PSD_LFIL_PATTERN = "PtFl";
        private const string PSD_LFIL_SOLID = "SoCo";
        private const string PSD_LFX_BEVEL = "bevl";
        private const string PSD_LFX_COMMON = "cmnS";
        private const string PSD_LFX_DROP_SDW = "dsdw";
        private const string PSD_LFX_FX = "lrFX";
        private const string PSD_LFX_FX2 = "lfx2";
        private const string PSD_LFX_INNER_GLW = "iglw";
        private const string PSD_LFX_INNER_SDW = "isdw";
        private const string PSD_LFX_OUTER_GLW = "oglw";
        private const string PSD_LMSK_VMASK = "vmsk";
        private const string PSD_LOTH_FOREIGN_FX = "ffxi";
        private const string PSD_LOTH_GRADIENT = "grdm";
        private const string PSD_LOTH_LAYER_DATA = "layr";
        private const string PSD_LOTH_META_DATA = "shmd";
        private const string PSD_LOTH_PATT_DATA = "shpa";
        private const string PSD_LOTH_PATTERN = "Patt";
        private const string PSD_LOTH_RESTRICT = "brst";
        private const string PSD_LOTH_SECTION = "lsct";
        private const string PSD_LPAR_ANNOTATE = "Anno";
        private const string PSD_LPRP_BLEND_CLIP = "clbl";
        private const string PSD_LPRP_BLEND_INT = "infx";
        private const string PSD_LPRP_COLOR = "lclr";
        private const string PSD_LPRP_ID = "lyid";
        private const string PSD_LPRP_KNOCKOUT = "knko";
        private const string PSD_LPRP_PROTECT = "lspf";
        private const string PSD_LPRP_REF_POINT = "fxrp";
        private const string PSD_LPRP_SOURCE = "lnsr";
        private const string PSD_LPRP_UNICODE = "luni";
        private const string PSD_LTYP_TYPE = "tySh";
        private const string PSD_LTYP_TYPE2 = "TySh";
        public PSDTypeToolObject typeToolObj = new PSDTypeToolObject();
        //public IProperties typeToolObj2 = new PSDTypeToolObject2();

        public void load(BinaryReader br)
        {
            string str = PSDUtil.readAscii(br, 4);
            string str2 = PSDUtil.readAscii(br, 4);
            
                
            int num = EndianReverser.getInt32(br);
            if (str != "8BIM")
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }

            

            long position = br.BaseStream.Position;
            switch (str2)
            {
                case "levl":
                case "curv":
                case "brit":
                case "blnc":
                case "blwh":
                case "hue ":
                case "hue2":
                case "selc":
                case "mixr":
                case "grdm":
                case "phfl":
                case "expA":
                case "thrs":
                case "nvrt":
                case "post":
                    this.drop = true;
                    break;
                case "lrFX":
                    {
                        short version = EndianReverser.getInt16(br);
                        int count = EndianReverser.getInt16(br);

                        for (int i = 0; i < count; i++)
                        {
                            string _8bim = PSDUtil.readAscii(br, 4);
                            string effectType = PSDUtil.readAscii(br, 4);
                            int size = EndianReverser.getInt32(br);
                            long p = br.BaseStream.Position;

                            switch (effectType)
                            {
                                case "dsdw":
                                    {
                                        //ShadowInfo.Parse(br);
                                    }
                                    break;
                                case "sofi":
                                    {
                                        //this.solidFillInfo = SolidFillInfo.Parse(br);
                                    }
                                    break;
                            }

                            br.BaseStream.Position = p + size;

                        }
                    }
                    break;
                case "SoLd":
                    {
                        int id = EndianReverser.getInt32(br);
                        int ver = EndianReverser.getInt32(br);
                        int descVer = EndianReverser.getInt32(br);
                        //DescriptorStructure desc = new DescriptorStructure(br);
                        this.Add("SoLd", new DescriptorStructure(br));
                    }
                    break;
                case "lfx2":
                    {
                        int n1 = EndianReverser.getInt32(br);
                        int n2 = EndianReverser.getInt32(br);
                        this.Add("LayerEffectInfo", new DescriptorStructure(br));
                    }
                    break;
                case "PlLd":
                    {
                        Properties props = new Properties();
                        //props.Add("Version", EndianReverser.getInt32(br));
                        //props.Add("Version", EndianReverser.getInt32(br));
                        props.Add("Type", EndianReverser.getInt32(br));
                        props.Add("Version", EndianReverser.getInt32(br));
                        props.Add("UniqueID", PSDUtil.readPascalString(br, 1));
                        props.Add("PageNumbers", EndianReverser.getInt32(br));
                        props.Add("Pages", EndianReverser.getInt32(br));
                        props.Add("AntiAlias", EndianReverser.getInt32(br));
                        props.Add("LayerType", EndianReverser.getInt32(br));
                        props.Add("Transformation", EndianReverser.getDouble(br, 8));
                        props.Add("WarpVersion ", EndianReverser.getInt32(br));
                        props.Add("WarpDescriptorVersion ", EndianReverser.getInt32(br));
                        props.Add("WarpDescriptor", new DescriptorStructure(br));

                        this.Add(str2, props);
                    }
                    break;
                case "lnsr":
                    {
                        this.Add(str2, PSDUtil.readAscii(br, 4));
                    }
                    break;
                case "fxrp":
                    {
                        Properties props = new Properties();
                        props.Add("RefernecePoint", EndianReverser.getDouble(br, 2));
                        this.Add(str2, props);
                    }
                    break;

                default:
                    if ((((str2 != "SoCo") && (str2 != "PtFl")) && ((str2 != "GdFl") && (str2 != "lrFX"))) && ((str2 != "lfx2") && (str2 != "tySh")))
                    {
                        if (str2 == "TySh")
                        {
                            this.Add("TypeToolInfo", new PSDTypeToolObject2(br));
                        }
                        else if (str2 == "luni")
                        {
                            Properties props = new Properties();
                            this.name = PSDUtil.readUnicodeString(br);
                            props.Add("Name", this.name);
                            this.Add(str2, props);
                            this.Add("Name", this.name);
                        }
                        else if (str2 == "lyid")
                        {
                            Properties props = new Properties();
                            this.id = EndianReverser.getInt32(br);
                            props.Add("ID", this.id);
                            this.Add(str2, props);
                            this.Add("ID", this.id);
                        }
                        else if (str2 == "lsct")
                        {
                            this.drop = true;
                            this.groupStatus = EndianReverser.getInt32(br);
                            this.Add("GroupStatus", this.groupStatus);
                        }
                    }
                    break;
            }
            br.BaseStream.Position = position;
            Stream baseStream = br.BaseStream;
            baseStream.Position += num;
            if ((num % 2) != 0)
            {
                Stream stream2 = br.BaseStream;
                stream2.Position += 1L;
            }

            if (this.ContainsKey(str2) == false)
            {
                this.Add(str2, "empty");
            }
        }
    }
}

