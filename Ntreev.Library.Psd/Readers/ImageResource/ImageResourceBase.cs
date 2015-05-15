using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.ImageResource
{
    abstract class ImageResourceBase : ReadablePropertiesHost
    {
        protected ImageResourceBase(PsdReader reader)
            : base(reader)
        {

        }
    }
}
