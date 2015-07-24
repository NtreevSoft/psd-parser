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
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace Ntreev.Library.PsdViewer.ViewModels
{
    public class TreeViewItemViewModel : PropertyChangedBase, INotifyPropertyChanged, IComparable, IDisposable
    {
        private readonly TreeViewItemCollection childrens;
        private TreeViewItemViewModel parent;

        private bool isExpanded;
        private bool isSelected;
        private bool isVisibled = true;

        protected TreeViewItemViewModel(TreeViewItemViewModel parent)
        {
            this.parent = parent;

            this.childrens = new TreeViewItemCollection();
            this.childrens.CollectionChanged += childrens_CollectionChanged;
        }

        public virtual int CompareTo(object obj)
        {
            TreeViewItemViewModel vm = obj as TreeViewItemViewModel;
            return this.Text.CompareTo(vm.Text);
        }

        public virtual void Dispose()
        {

        }

        public int Count
        {
            get
            {
                return this.Children.Count;
            }
        }

        public virtual string Text
        {
            get { return null; }
        }

        public TreeViewItemCollection Children
        {
            get { return this.childrens; }
        }

        public bool HasItems
        {
            get { return this.childrens.Any(); }
        }

        public bool IsExpanded
        {
            get
            {
                //if (this.Children.Count == 0)
                //    return false;
                return this.isExpanded;
            }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    this.NotifyOfPropertyChange(() => this.IsExpanded);
                }

                // Expand all the way up to the root.
                if (this.isExpanded && this.parent != null)
                    this.parent.IsExpanded = true;
            }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    this.NotifyOfPropertyChange(() => this.IsSelected);

                    if (this.isSelected == true)
                        this.AddSelectedItem(this);
                    else
                        this.RemoveSelectedItem(this);
                }
            }
        }

        public bool IsVisibled
        {
            get
            {
                return this.isVisibled;
            }
            set
            {
                if (value != this.isVisibled)
                {
                    this.isVisibled = value;
                    this.NotifyOfPropertyChange(() => this.IsVisibled);
                }
            }
        }

        public TreeViewItemViewModel Parent
        {
            get { return this.parent; }
        }

        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif

            }
        }

        protected virtual void AddSelectedItem(TreeViewItemViewModel item)
        {

        }

        protected virtual void RemoveSelectedItem(TreeViewItemViewModel item)
        {
        }

        private void childrens_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.NotifyOfPropertyChange(() => this.Count);

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        foreach (TreeViewItemViewModel item in e.NewItems)
                        {
                            item.parent = this;
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        foreach (TreeViewItemViewModel item in e.OldItems)
                        {
                            item.parent = null;
                        }
                    }
                    break;
            }
        }
    }
}
