using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureObjectArray : Properties
    {
        public StructureObjectArray(PSDReader reader, string key)
        {
            var d1 = reader.ReadInt32();
            new StructureClass(reader);
            var d3 = reader.ReadInt32();

            for (int o = 0; o < d3; o++)
            {
                string kkk = reader.ReadStringOrKey();
                string ttt = reader.ReadAscii(4);
                string ttt1 = reader.ReadAscii(4);
                int d4 = reader.ReadInt32();

                var ddd = reader.ReadDoubles(d4);
            }
        }
    }
}
