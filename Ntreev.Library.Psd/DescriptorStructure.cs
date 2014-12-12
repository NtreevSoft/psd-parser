using Ntreev.Library.Psd.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.Psd
{
    sealed class DescriptorStructure : Properties
    {
        public DescriptorStructure(PSDReader reader)
        {
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
