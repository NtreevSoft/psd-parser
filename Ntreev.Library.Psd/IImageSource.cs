using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public interface IImageSource
    {
        int Width { get; }

        int Height { get; }

        int Depth { get; }

        Channel[] Channels { get; }

        float Opacity { get; }

        bool HasImage { get; }
    }
}
