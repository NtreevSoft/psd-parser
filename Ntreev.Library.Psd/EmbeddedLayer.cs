using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    class EmbeddedLayer : LinkedLayer
    {
        public EmbeddedLayer(PsdReader reader)
            : base(reader)
        {

        }

        protected override void Validate(string type, int version)
        {
            if (type != "liFE" || version < 5)
                throw new InvalidFormatException();
        }

        public override bool IsEmbedded
        {
            get { return true; }
        }
    }
}
