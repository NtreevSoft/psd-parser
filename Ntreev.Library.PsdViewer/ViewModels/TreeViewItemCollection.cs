#region License
//Ntreev Photoshop Document Parser for .Net
//
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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Windows.Threading;
using System.Windows;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    public class TreeViewItemCollection<T> : ObservableCollection<T> where T : TreeViewItemViewModel
    {
        public new void Add(T item)
        {
            Application.Current.Dispatcher.Invoke(() => base.Add(item), DispatcherPriority.Background);
        }

        public void Insert(T item)
        {
            IComparable comparer = (IComparable)item;

            if (this.Count == 0)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Add(item);
                }, DispatcherPriority.Background);
            }
            else
            {
                bool last = true;
                for (int i = 0; i < this.Count; i++)
                {
                    int result = comparer.CompareTo(this[i]);
                    if (result < 1)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this.Insert(i, item);
                        }, DispatcherPriority.Background);

                        last = false;
                        break;
                    }
                }
                if (last)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.Add(item);
                    }, DispatcherPriority.Background);
                }

            }
        }

        public new void Clear()
        {
            Application.Current.Dispatcher.Invoke(() => base.Clear(), DispatcherPriority.Background);
        }

        public new void Remove(T item)
        {
            Application.Current.Dispatcher.Invoke(() => base.Remove(item), DispatcherPriority.Background);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in items.Reverse().ToArray())
                {
                    base.Remove(item);
                }
            }, DispatcherPriority.Background);
        }
    }

    public class TreeViewItemCollection : TreeViewItemCollection<TreeViewItemViewModel>
    {
        public void Reposition(TreeViewItemViewModel item)
        {
            IComparable comparer = (IComparable)item;
            int index = this.IndexOf(item);
            int newIndex = this.Count - 1;
            for (int i = 0; i < this.Count; i++)
            {
                int result = comparer.CompareTo(this[i]);
                if (result < 0)
                {
                    newIndex = i;
                    break;
                }
            }

            Application.Current.Dispatcher.Invoke(() => this.Move(index, newIndex));
        }
    }
}