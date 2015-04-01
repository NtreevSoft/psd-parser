using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface ILinkedLayer
    {
        PsdDocument Document
        {
            get;
        }

        Uri AbsoluteUri
        {
            get;
        }

        bool HasDocument
        {
            get;
        }

        Guid ID
        {
            get;
        }

        string Name
        {
            get;
        }
    }
}
