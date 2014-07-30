using System;
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    sealed class TypeToolInfo : Properties
    {
        internal TypeToolInfo(PSDReader reader)
        {
            this.Add("Version", reader.ReadInt16());
            this.Add("Transforms", reader.ReadDoubles(6));
            this.Add("TextVersion", reader.ReadInt16());
            this.Add("TextDescVersion", reader.ReadInt32());
            this.Add("Text", new DescriptorStructure(reader));
            this.Add("WarpVersion", reader.ReadInt16());
            this.Add("WarpDescVersion", reader.ReadInt32());
            this.Add("Warp", new DescriptorStructure(reader));
            this.Add("Bounds", reader.ReadDoubles(2));
        }

        //public PSDRect area
        //{
        //    get
        //    {
        //        PSDRect rect;
        //        double[] transforms = this["Transforms"] as double[];
        //        return new PSDRect { left = rect.right = (int)transforms[4], top = rect.bottom = (int)transforms[5] };
        //    }
        //}
    }
}

