using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.Psd
{
    public abstract class FileHeader
    {
        public abstract int Depth
        {
            get;
        }

        public abstract int NumberOfChannels
        {
            get;
        }

        public abstract ColorMode ColorMode
        {
            get;
        }

        public abstract int Height
        {
            get;
        }

        public abstract int Width
        {
            get;
        }

        public abstract int Version
        {
            get;
        }
    }
}
