using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class BaseStructure : Properties
    {
        public BaseStructure(PsdReader reader)
        {
            List<object> items = new List<object>();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                string type = reader.ReadType();
                object value = StructureReader.Read(type, reader);
                items.Add(value);
            }
            this.Add("Items", items.ToArray());
        }
    }
}
