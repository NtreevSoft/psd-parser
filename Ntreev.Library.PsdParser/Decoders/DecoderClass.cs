using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderClass : Properties
    {
        public DecoderClass(PSDReader reader)
        {
            this.Add("Name", reader.ReadUnicodeString2());
            this.Add("ClassID", reader.ReadStringOrKey());
        }
    }
}
