using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class LazyProperties : LazyValueReader<IProperties>, IProperties
    {
        protected LazyProperties(PsdReader reader, object userData)
            : base(reader, userData)
        {

        }

        protected LazyProperties(PsdReader reader, long length, object userData)
            : base(reader, length, userData)
        {

        }

        public bool Contains(string property)
        {
            return this.Value.Contains(property);
        }

        public object this[string property]
        {
            get { return this.Value[property]; }
        }

        public int Count
        {
            get { return this.Value.Count; }
        }

        #region IProperties

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return (this.Value as IProperties).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (this.Value as IProperties).GetEnumerator();
        }

        #endregion
    }
}