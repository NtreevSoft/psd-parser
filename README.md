===========================================Usage :using (PsdDocument document = PsdDocument.Create(filename)){    foreach (var item in document.Childs)	{        Console.WriteLine("LayerName : " + item.Name);    }}Example
-------

SourceCode:

    using System;
    using Ntreev.Library;
	using System.Collections.Generic;

    namespace Example
    {
        class Program
        {
            static void Main()
            {
                Options options = new Options();
                CommandLineParser parser = new CommandLineParser();

                if (parser.TryParse(Environment.CommandLine, options) == false)
                {
                    parser.PrintUsage();
                    Environment.Exit(1);
                }
                Environment.Exit(0);
            }

            class Options
            {
                public bool Toggle { get; set; }
                [Switch("i")]
                public int Index { get; set; }
                public string Text { get; set; }
				public List<int> Numbers { get; set; }
            }
        }
    }

You can call like this:

    C:\>Example.exe /Toggle /Index 3 /Text "this is text" /Numbers "1,2,3,4,5"
    or
    C:\>Example.exe /i 3License-------Ntreev Photoshop Document Parser for .NetReleased under the MIT License.Copyright (c) 2015 Ntreev Soft co., Ltd.Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.