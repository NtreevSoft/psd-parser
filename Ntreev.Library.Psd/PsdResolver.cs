using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public abstract class PsdResolver
    {
        public abstract PsdDocument GetDocument(string path);
    }
}
