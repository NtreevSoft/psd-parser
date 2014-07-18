using System;
using System.Collections.Generic;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public interface IProperties
    {
        bool Contains(string property);

        object this[string property]
        {
            get;
        }

        int Count { get; }
    }
}
