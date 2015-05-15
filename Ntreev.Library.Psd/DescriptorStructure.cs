using Ntreev.Library.Psd.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd
{
    class DescriptorStructure : Properties
    {
        private readonly int version;

        public DescriptorStructure(PsdReader reader)
            : this(reader, true)
        {

        }

        public DescriptorStructure(PsdReader reader, bool hasVersion)
        {
            if (hasVersion == true)
            {
                this.version = reader.ReadInt32();
            }

            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadKey();
                string osType = reader.ReadType();
                if (key == "EngineData")
                {
                    this.Add(key.Trim(), new StructureEngineData(reader));
                }
                else
                {
                    object value = StructureReader.Read(osType, reader);
                    this.Add(key.Trim(), value);
                }
            }
        }
    }
}
