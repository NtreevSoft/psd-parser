using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public sealed class GuidesInfo
    {
        public int[] Horizontals
        {
            get;
            internal set;
        }

        public int[] Verticals
        {
            get;
            internal set;
        }
    }
}
