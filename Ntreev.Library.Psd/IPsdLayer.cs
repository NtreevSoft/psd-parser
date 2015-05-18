using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface IPsdLayer : IImageSource
    {
        BlendMode BlendMode { get; }

        IPsdLayer[] Childs { get; }

        bool IsClipping { get; }

        ILinkedLayer LinkedLayer { get; }

        string Name { get; }

        IPsdLayer Parent { get; }

        IProperties Resources { get; }

        PsdDocument Document { get; }

        int Left { get; }

        int Top { get; }

        int Right { get; }

        int Bottom { get; }
    }
}
