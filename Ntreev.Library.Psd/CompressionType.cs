using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public enum CompressionType
    {
        Raw = 0,

        RLE = 1,

        Zip = 2,

        ZipPrediction = 3,
    }
}
