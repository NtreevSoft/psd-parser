using Ntreev.Library.Psd.Readers.LayerAndMaskInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    [ResourceID("lnk2")]
    class Reader_lnk2 : Reader_lnkD
    {
        public Reader_lnk2(PsdReader reader, long length)
            : base(reader, length)
        {

        }
    }
}
