using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    class Properties : Dictionary<string, object>, IProperties
    {
        public bool ContainsProperty(string property)
        {
            string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

            object value = this;

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

        public object GetProperty(string property)
        {
            string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

            object value = this;

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

            }
            return value;
        }

        bool IProperties.Contains(string property)
        {
            return this.ContainsProperty(property);
        }

        object IProperties.this[string property]
        {
            get
            {
                return this.GetProperty(property);
            }
        }
    }
}
