using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    static class StructureReader
    {
        public static object Read(string ostype, PSDReader reader)
        {
            switch (ostype)
            {
                case "obj ":
                    return new StructureReference(reader);

                case "Objc":
                    return new DescriptorStructure(reader);

                case "VlLs":
                    return new StructureList(reader);

                case "doub":
                    return reader.ReadDouble();

                case "UntF":
                    return new StructureUnitFloat(reader);

                case "TEXT":
                    return reader.ReadString();

                case "enum":
                    return new StructureEnumerate(reader);

                case "long":
                    return reader.ReadInt32();

                case "bool":
                    return reader.ReadBoolean();

                case "GlbO":
                    return new DescriptorStructure(reader);

                case "type":
                    return new StructureClass(reader);

                case "GlbC":
                    return new StructureClass(reader);

                case "alis":
                    return new StructureAlias(reader);

                case "tdta":
                    return new StructureUnknownOSType("Cannot read RawData");

                case "prop":
                    return new StructureProperty(reader);

                case "Clss":
                    return new StructureClass(reader);

                case "Enmr":
                    return new StructureEnumerate(reader);

                case "rele":
                    return new StructureOffset(reader);

                case "Idnt":
                    return new StructureUnknownOSType("Cannot read Identifier");

                case "indx":
                    return new StructureUnknownOSType("Cannot read Index");

                case "name":
                    return new StructureUnknownOSType("Cannot read Name");

                case "ObAr":
                    return new StructureObjectArray(reader);

            }
            throw new NotSupportedException(ostype);
        }
    }
}
