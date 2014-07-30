using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Decoders
{
    class BaseDecoder
    {
        public List<object> items = new List<object>();

        public BaseDecoder(PSDReader reader, string key)
        {
            int num = reader.ReadInt32();
            while (num-- > 0)
            {
                string osType = reader.ReadAscii(4);
                DecoderFactory.DecodeFunc func = DecoderFactory.GetDecoder(osType);
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
