using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ntreev.Library.Psd
{
    class Properties : IProperties
    {
        private readonly Dictionary<string, object> props;

        public Properties()
        {
            this.props = new Dictionary<string, object>();
        }

        public Properties(int capacity)
        {
            this.props = new Dictionary<string, object>(capacity);
        }

        public void Add(string key, object value)
        {
            this.props.Add(key, value);
        }

        public bool Contains(string property)
        {
            string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

            object value = this.props;

            foreach (var item in ss)
            {
                if (value is ArrayList == true)
                {
                    ArrayList arrayList = value as ArrayList;
                    int index;
                    if (int.TryParse(item, out index) == false)
                        return false;
                    if (index >= arrayList.Count)
                        return false;
                    value = arrayList[index];
                }
                else if (value is IDictionary<string, object> == true)
                {
                    IDictionary<string, object> props = value as IDictionary<string, object>;
                    if (props.ContainsKey(item) == false)
                    {
                        return false;
                    }

                    value = props[item];
                }

            }
            return true; 
        }

        private object GetProperty(string property)
        {
            string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

            object value = this.props;

            foreach (var item in ss)
            {
                if (value is ArrayList == true)
                {
                    ArrayList arrayList = value as ArrayList;
                    value = arrayList[int.Parse(item)];
                }
                else if (value is IDictionary<string, object> == true)
                {
                    IDictionary<string, object> props = value as IDictionary<string, object>;
                    value = props[item];
                }
                else if (value is IProperties == true)
                {
                    IProperties props = value as IProperties;
                    value = props[item];
                }
            }
            return value;
        }

        public int Count
        {
            get { return this.props.Count; }
        }

        public object this[string property]
        {
            get
            {
                return this.GetProperty(property);
            }
            set
            {
                this.props[property] = value;
            }
        }

        #region IProperties

        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            return this.props.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.props.GetEnumerator();
        }

        #endregion
    }
}
