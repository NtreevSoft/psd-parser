using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderProperty : Properties
    {
        public DecoderProperty(PSDReader reader)
        {
            this.Add("Name", reader.ReadUnicodeString2());
            this.Add("ClassID", reader.ReadStringOrKey());
            this.Add("KeyID", reader.ReadStringOrKey());
        }
    }
}
