using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DecoderFactory.DecodeFunc func = DecoderFactory.GetDecoder(reader.ReadAscii(4));
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
