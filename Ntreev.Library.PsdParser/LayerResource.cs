using System;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    sealed class LayerResource : Properties
    {
        public bool drop;
        //public int groupStatus;
        //public int id;

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
        //public PSDTypeToolObject typeToolObj = new PSDTypeToolObject();
        //public IProperties typeToolObj2 = new PSDTypeToolObject2();

        internal void Load(PSDReader reader, int index)
        {
            string str = reader.ReadAscii(4);
            string str2 = reader.ReadAscii(4);

            int num = reader.ReadInt32();
            if (str != "8BIM")
            {
                throw new SystemException(string.Format("Wrong signature {0}", str));
            }

            long position = reader.Position;
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
                        short version = reader.ReadInt16();
                        int count = reader.ReadInt16();

                        for (int i = 0; i < count; i++)
                        {
                            string _8bim = reader.ReadAscii(4);
                            string effectType = reader.ReadAscii(4);
                            int size = reader.ReadInt32();
                            long p = reader.Position;

                            switch (effectType)
                            {
                                case "dsdw":
                                    {
                                        //ShadowInfo.Parse(reader);
                                    }
                                    break;
                                case "sofi":
                                    {
                                        //this.solidFillInfo = SolidFillInfo.Parse(reader);
                                    }
                                    break;
                            }

                            reader.Position = p + size;

                        }
                    }
                    break;
                case "SoLd":
                    {
                        Properties props = new Properties();
                        string id = reader.ReadAscii(4);
                        if (id != "soLD")
                            throw new Exception();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("DescriptorVersion", reader.ReadInt32());
                        props.Add("Descriptor", new DescriptorStructure(reader));
                        this.Add(str2, props);
                    }
                    break;
                case "lfx2":
                    {
                        Properties props = new Properties();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("DescriptorVersion", reader.ReadInt32());
                        props.Add("Descriptor", new DescriptorStructure(reader));
                        this.Add(str2, props);
                    }
                    break;
                case "PlLd":
                    {
                        Properties props = new Properties();
                        string id = reader.ReadAscii(4);
                        if (id != "plcL")
                            throw new Exception();
                        props.Add("Version", reader.ReadInt32());
                        props.Add("UniqueID", reader.ReadPascalString(1));
                        props.Add("PageNumbers", reader.ReadInt32());
                        props.Add("Pages", reader.ReadInt32());
                        props.Add("AntiAlias", reader.ReadInt32());
                        props.Add("LayerType", reader.ReadInt32());
                        props.Add("Transformation", reader.ReadDoubles(8));
                        props.Add("WarpVersion ", reader.ReadInt32());
                        props.Add("WarpDescriptorVersion ", reader.ReadInt32());
                        props.Add("WarpDescriptor", new DescriptorStructure(reader));

                        this.Add(str2, props);
                    }
                    break;
                case "lnsr":
                    {
                        this.Add(str2, reader.ReadAscii(4));
                    }
                    break;
                case "fxrp":
                    {
                        Properties props = new Properties();
                        props.Add("RefernecePoint", reader.ReadDoubles(2));
                        this.Add(str2, props);
                    }
                    break;
                case "TySh":
                    {
                        this.Add(str2, new TypeToolInfo(reader));
                    }
                    break;
                case "lyid":
                    {
                        Properties props = new Properties();
                        props.Add("ID", reader.ReadInt32());
                        this.Add(str2, props);
                    }
                    break;
                case "luni":
                    {
                        Properties props = new Properties();
                        props.Add("Name", reader.ReadUnicodeString());
                        this.Add(str2, props);
                    }
                    break;
                case "lsct":
                    {
                        Properties props = new Properties();
                        props.Add("SectionType", (SectionType)reader.ReadInt32());
                        this.Add(str2, props);
                        this.drop = true;
                    }
                    break;

                default:
                    if ((((str2 != "SoCo") && (str2 != "PtFl")) && ((str2 != "GdFl") && (str2 != "lrFX"))) && ((str2 != "lfx2") && (str2 != "tySh")))
                    {

                    }
                    break;
            }

            if (reader.Position > position + num)
            {
                throw new Exception();
            }

            reader.Position = position + num;

            if (this.ContainsKey(str2) == false)
            {
                this.Add(str2, "empty");
            }
        }
    }
}

