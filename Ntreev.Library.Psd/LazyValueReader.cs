using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class LazyValueReader<T> : ValueReader<T>
    {
        protected LazyValueReader(PsdReader reader, object userData)
            : base(reader, true, userData)
        {

        }

        protected LazyValueReader(PsdReader reader, long length, object userData)
            : base(reader, length, userData)
        {
            
        }
    }
}