using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class LazyReadableProperties : LazyReadableValue<IProperties>, IProperties
    {
        protected LazyReadableProperties(PsdReader reader)
            : this(reader, false)
        {

        }

        protected LazyReadableProperties(PsdReader reader, bool hasLength)
            : base(reader, hasLength)
        {

        }

        protected LazyReadableProperties(PsdReader reader, long length)
            : base(reader, length)
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