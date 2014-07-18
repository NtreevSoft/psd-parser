using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public class Properties : Dictionary<string, object>, IProperties
    {
        bool IProperties.Contains(string property)
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
                else if (value is Dictionary<string, object> == true)
                {
                    Dictionary<string, object> props = value as Dictionary<string, object>;
                    if (props.ContainsKey(item) == false)
                    {
                        return false;
                    }

                    value = props[item];
                }

            }
            return true;
        }

        object IProperties.this[string property]
        {
            get 
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
                    else if (value is Dictionary<string, object> == true)
                    {
                        Dictionary<string, object> props = value as Dictionary<string, object>;
                        value = props[item];
                    }

                }
                return value;
            }
        }
    }
}
