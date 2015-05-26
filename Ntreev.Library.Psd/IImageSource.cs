using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface IImageSource
    {
        int Width { get; }

        int Height { get; }

        /// <summary>
        /// the number of bits per channel. Supported values are 1, 8, 16 and 32.
        /// </summary>
        int Depth { get; }

        IChannel[] Channels { get; }

        float Opacity { get; }

        bool HasImage { get; }

        bool HasMask { get; }
    }
}
