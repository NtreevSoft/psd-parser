using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class LazyReadableValue<T> : ReadableValue<T>
    {
        protected LazyReadableValue(PsdReader reader)
            : this(reader, false)
        {

        }

        protected LazyReadableValue(PsdReader reader, bool hasLength)
            : base(reader, hasLength, true)
        {

        }

        protected LazyReadableValue(PsdReader reader, long length)
            : base(reader, length)
        {

        }
    }
}