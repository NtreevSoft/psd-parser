//Released under the MIT License.
//
//Copyright (c) 2018 Ntreev Soft co., Ltd.
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

using FirstFloor.ModernUI.Windows.Controls;
using MahApps.Metro.Controls;
using Ntreev.ModernUI.Framework.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ntreev.Library.PsdViewer
{
    partial class ShellView : ModernWindow
    {
        public ShellView()
        {
            this.InitializeComponent();
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files.Length == 1 && files.First() is string path && Path.GetExtension(path) == ".psd")
                {
                    e.Effects = DragDropEffects.Copy;
                    e.Handled = true;
                    return;
                }
            }
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                var filename = files.First();
                var command = DragDropUtility.GetDropCommand(this);
                if (command != null && command.CanExecute(filename) == true)
                {
                    command.Execute(filename);
                }
            }
        }
    }
}
