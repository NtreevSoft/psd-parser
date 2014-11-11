using Ntreev.Library.Psd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    class PropertiesItemViewModel : TreeViewItemViewModel
    {
        private readonly string name;
        private object value;

        public PropertiesItemViewModel(string name, IProperties properties, TreeViewItemViewModel parent)
            : base(parent)
        {
            this.name = name;
            foreach (var item in properties)
            {
                object value = item.Value;

                if (value is IProperties == true)
                {
                    this.Children.Add(new PropertiesItemViewModel(item.Key, value as IProperties, this));
                }
                else if (value is IEnumerable == true && value is string == false)
                {
                    int index = 0;
                    foreach (var i in value as IEnumerable)
                    {
                        string n = string.Format("{0}[{1}]", item.Key, index);
                        if (i is IProperties == true)
                            this.Children.Add(new PropertiesItemViewModel(n, i as IProperties, this));
                        else
                            this.Children.Add(new PropertiesItemViewModel(n, i, this));
                        index++;
                    }
                }
                else
                {
                    this.Children.Add(new PropertiesItemViewModel(item.Key, value, this));
                }
            }
        }


        public PropertiesItemViewModel(string name, object value, TreeViewItemViewModel parent)
            : base(parent)
        {
            this.name = name;
            this.value = value;
        }

        public override string Text
        {
            get
            {
                return this.name;
            }
        }

        public object Value
        {
            get { return this.value; }
        }
    }
}
