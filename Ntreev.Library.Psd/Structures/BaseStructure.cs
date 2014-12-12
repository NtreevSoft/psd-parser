using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class BaseStructure : Properties
    {
        public BaseStructure(PSDReader reader)
        {
            List<object> items = new List<object>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string osType = reader.ReadAscii(4);
                object value = StructureReader.Read(osType, reader);
                items.Add(value);
            }
            this.Add("Items", items.ToArray());
        }
    }
}
