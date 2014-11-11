using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface IPsdLayer : IImageSource
    {
        BlendMode BlendMode { get; }

        IEnumerable<IPsdLayer> Childs { get; }

        bool IsClipping { get; }

        IPsdLayer LinkedLayer { get; }

        string Name { get; }

        IPsdLayer Parent { get; }

        IProperties Properties { get; }

        PsdDocument PSD { get; }

        int Left { get; }

        int Top { get; }

        int Right { get; }

        int Bottom { get; }
    }
}
