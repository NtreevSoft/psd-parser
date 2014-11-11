using System;
using System.Collections.Generic;
using System.Text;

namespace Ntreev.Library.Psd
{
    public interface IProperties : IEnumerable<KeyValuePair<string, object>>
    {
        bool Contains(string property);

        object this[string property]
        {
            get;
        }

        int Count { get; }
    }
}
