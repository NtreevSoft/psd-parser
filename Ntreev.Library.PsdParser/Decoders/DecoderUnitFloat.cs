using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdParser.Decoders
{
    class DecoderUnitFloat : Properties
    {
        private const string ANGLE = "#Ang";
        private const string DENSITY = "#Rsl";
        private const string DISTANCE = "#Rlt";
        
        private const string NONE = "#Nne";
        private const string PERCENT = "#Prc";
        private const string PIXELS = "#Pxl";

        public DecoderUnitFloat(PSDReader reader)
        {
            this.Add("Key", reader.ReadAscii(4));
            this.Add("Value", reader.ReadDouble());
        }
    }
}
