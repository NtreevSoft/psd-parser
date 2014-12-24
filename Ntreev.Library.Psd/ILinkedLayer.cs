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

        bool IsEmbedded
        {
            get;
        }

        Guid ID
        {
            get;
        }
    }
}
