using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser
{
    [Flags]
    public enum LayerFlags
    {
        Transparency = 1,

        Visible = 2,

        Obsolete = 4,

        Unknown0 = 8, // 1 for Photoshop 5.0 and later, tells if bit 4 has useful information;

        Unknown1 = 16, // pixel data irrelevant to appearance of document
    }
}
