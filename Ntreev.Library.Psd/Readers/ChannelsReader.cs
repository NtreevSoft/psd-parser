using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ChannelsReader : ReadableLazyValue<Channel[]>
    {
        public ChannelsReader(PsdReader reader, LayerRecords layerRecords)
            : base(reader)
        {

        }

        protected override Channel[] ReadValue(PsdReader reader)
        {
            return null;
        }
    }
}
