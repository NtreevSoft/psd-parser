using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.Psd
{
    public class FileHeader
    {
        public int Depth
        {
            get;
            internal set;
        }

        public int NumberOfChannels
        {
            get;
            internal set;
        }

        public ColorMode ColorMode
        {
            get;
            internal set;
        }

        public int Height
        {
            get;
            internal set;
        }

        public int Width
        {
            get;
            internal set;
        }
    }
}
