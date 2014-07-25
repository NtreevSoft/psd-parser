using Ntreev.Library.PsdParser.Decoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    class DescriptorStructure : Properties
    {
        public DescriptorStructure(PSDReader reader)
        {
            this.Add("Name", reader.ReadUnicodeString2());
            this.Add("ClassID", reader.ReadStringOrKey());

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadStringOrKey().Trim();
                string osType = reader.ReadAscii(4);
                DecoderFactory.DecodeFunc func = DecoderFactory.GetDecoder(osType);
                if (func != null)
                {
                    object value = func(reader, key);
                    this.Add(key, value);    
                }
            }
        }
    }
}
