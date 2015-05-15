using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResource
{
    abstract class LayerResourceBase : ReadablePropertiesHost
    {
        protected LayerResourceBase(PsdReader reader)
            : base(reader)
        {

        }
    }
}
