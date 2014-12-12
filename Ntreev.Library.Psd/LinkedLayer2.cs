using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class LinkedLayer2 : LinkedLayer
    {
        public LinkedLayer2(PSDReader reader)
            : base(reader)
        {

        }

        protected override void Validate(string type, int version)
        {
            if (type != "liFE" || version < 5)
                throw new Exception("Invalid PSD file");
        }

        public override bool IsEmbedded
        {
            get { return true; }
        }
    }
}
