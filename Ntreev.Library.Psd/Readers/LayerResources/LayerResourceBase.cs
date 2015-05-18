using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerResources
{
    abstract class LayerResourceBase : LazyReadableProperties
    {
        protected LayerResourceBase(PsdReader reader)
            : base(reader)
        {

        }
    }
}
