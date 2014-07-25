using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser
{
    public enum ColorMode
    {
        Bitmap = 0,
        CMYK = 4,
        DUOTONE = 8,
        GrayScale = 1,
        Indexed = 2,
        LAB = 9,
        MultiChannel = 7,
        RGB = 3
    }
}
