using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderAlias : Properties
    {
        public DecoderAlias(PSDReader reader)
        {
            int length = reader.ReadInt32();
            this.Add("Alias", reader.ReadAscii(length));
        }
    }
}
