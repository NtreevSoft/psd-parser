using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderEnumerate : Properties
    {
        public DecoderEnumerate(PSDReader reader)
        {
            this.Add("Type", reader.ReadStringOrKey());
            this.Add("EnumName", reader.ReadStringOrKey());
        }
    }
}
