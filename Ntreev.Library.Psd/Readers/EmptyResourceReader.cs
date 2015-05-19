using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class EmptyResourceReader : ResourceReaderBase
    {
        public EmptyResourceReader(PsdReader reader, long length)
            : base(reader, length, null)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            value = new Properties();
        }
    }
}
