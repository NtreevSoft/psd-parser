//Released under the MIT License.
//
//Copyright (c) 2015 Ntreev Soft co., Ltd.
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//documentation files (the "Software"), to deal in the Software without restriction, including without limitation the 
//rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
//persons to whom the Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Ntreev.Library.Psd;
using Ntreev.ModernUI.Framework.ViewModels;
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
        private string type;

        public PropertiesItemViewModel(string name, IProperties properties, TreeViewItemViewModel parent)
        {
            this.name = name;
            foreach (var item in properties)
            {
                object value = item.Value;

                if (value is IProperties == true)
                {
                    this.Items.Add(new PropertiesItemViewModel(item.Key, value as IProperties, this));
                }
                else if (value is IEnumerable == true && value is string == false)
                {
                    int index = 0;
                    foreach (var i in value as IEnumerable)
                    {
                        string n = string.Format("{0}[{1}]", item.Key, index);
                        if (i is IProperties == true)
                            this.Items.Add(new PropertiesItemViewModel(n, i as IProperties, this));
                        else
                            this.Items.Add(new PropertiesItemViewModel(n, i, this));
                        index++;
                    }
                }
                else
                {
                    this.Items.Add(new PropertiesItemViewModel(item.Key, value, this));
                }
            }
        }

        public PropertiesItemViewModel(string name, object value, TreeViewItemViewModel parent)
        {
            this.name = name;
            this.value = value;
            this.type = value.GetType().Name;
        }

        public override string DisplayName
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

        public string Type
        {
            get { return this.type; }
        }
    }
}
