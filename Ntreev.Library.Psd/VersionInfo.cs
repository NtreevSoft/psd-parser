using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public abstract class VersionInfo
    {
        public abstract int Version
        {
            get;
        }

        public abstract bool HasCompatibilityImage
        {
            get;
        }

        public abstract string WriterName
        {
            get;
        }

        public abstract string ReaderName
        {
            get;
        }

        public abstract int FileVersion
        {
            get;
        }
    }
}
