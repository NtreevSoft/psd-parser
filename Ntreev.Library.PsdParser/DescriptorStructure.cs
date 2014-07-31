using Ntreev.Library.PsdParser.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    sealed class DescriptorStructure : Properties
    {
        public DescriptorStructure(PSDReader reader)
        {
            this.Add("Name", reader.ReadUnicodeString2());
            this.Add("ClassID", reader.ReadStringOrKey());

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadStringOrKey();
                string osType = reader.ReadAscii(4);
                StructureFactory.DecodeFunc func = StructureFactory.GetDecoder(osType);
                if (func != null)
                {
                    object value = func(reader, key);
                    this.Add(key.Trim(), value);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}
