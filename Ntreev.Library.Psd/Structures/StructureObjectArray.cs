using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureObjectArray : Properties
    {
        public StructureObjectArray(PSDReader reader)
        {
            int version = reader.ReadInt32();
            this.Add("Name", reader.ReadString());
            this.Add("ClassID", reader.ReadKey());

            int count = reader.ReadInt32();

            List<Properties> items = new List<Properties>();

            for (int i = 0; i < count; i++)
            {
                Properties props = new Properties();
                props.Add("Type1", reader.ReadKey());
                props.Add("EnumName", reader.ReadType());


                props.Add("Type2", PSDUtil.ToUnitType(reader.ReadType()));
                int d4 = reader.ReadInt32();
                props.Add("Values", reader.ReadDoubles(d4));

                items.Add(props);
            }
            this.Add("items", items.ToArray());
        }
    }
}
