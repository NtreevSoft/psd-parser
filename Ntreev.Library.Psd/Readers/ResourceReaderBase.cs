using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    abstract class ResourceReaderBase : LazyProperties
    {
        public ResourceReaderBase(PsdReader reader, long length)
            : base(reader, length, null)
        {

        }
    }
}
