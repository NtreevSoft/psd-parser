using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.ImageResources
{
    abstract class ImageResourceBase : LazyReadableProperties
    {
        protected ImageResourceBase(PsdReader reader)
            : base(reader)
        {

        }
    }
}
