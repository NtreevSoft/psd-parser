using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class InternalDocument : PsdDocument
    {
        protected override void OnDisposed(EventArgs e)
        {
            throw new Exception();
        }
    }
}
