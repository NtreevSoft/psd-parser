using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface IChannel
    {
        byte[] Data
        {
            get;
        }

        ChannelType Type
        {
            get;
        }
    }
}
