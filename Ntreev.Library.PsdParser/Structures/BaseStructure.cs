using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Structures
{
    class BaseStructure
    {
        public List<object> items = new List<object>();

        public BaseStructure(PSDReader reader, string key)
        {
            int num = reader.ReadInt32();
            while (num-- > 0)
            {
                string osType = reader.ReadAscii(4);
                StructureFactory.DecodeFunc func = StructureFactory.GetDecoder(osType);
                if (func != null)
                {
                    object item = func(reader, key);
                    if (item != null)
                    {
                        this.items.Add(item);
                    }
                }
            }
        }
    }
}
