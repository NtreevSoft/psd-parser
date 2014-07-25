using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser
{
    public enum CompressionType
    {
        RawData = 0,
        RLECompressed = 1,
        ZipWithoutPrediction = 2,
        ZipPrediction = 3,
    }
}
