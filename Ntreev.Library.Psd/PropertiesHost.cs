using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    abstract class PropertiesHost : IProperties
    {
        private Properties properties;

        protected abstract IDictionary<string, object> CreateProperties();

        protected void Initialize()
        {
            if (this.properties == null)
            {
                this.Create();
            }
        }

        private IProperties Properties
        {
            get
            {
                if (this.properties == null)
                {
                    this.Create();
                }
                return this.properties;
            }
        }

        private void Create()
        {
            var props = this.CreateProperties();
            this.properties = new Properties(props.Count);
            foreach (var item in props)
            {
                this.properties.Add(item.Key, item.Value);
            }
        }

        #region IProperties

        bool IProperties.Contains(string property)
        {
            return this.Properties.Contains(property);
        }

        object IProperties.this[string property]
        {
            get { return this.Properties[property]; }
        }

        int IProperties.Count
        {
            get { return this.Properties.Count; }
        }

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string,object>>.GetEnumerator()
        {
            return this.Properties.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Properties.GetEnumerator();
        }

        #endregion
    }
}
