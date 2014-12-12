using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public class PathResolver : PsdResolver
    {
        private readonly string directory;

        public PathResolver(string directory)
        {
            this.directory = directory;
        }

        public override PsdDocument GetDocument(string relativePath)
        {
            string filename = System.IO.Path.Combine(this.directory, relativePath);
            PsdDocument document = new PsdDocument(relativePath);
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                document.Read(stream, this);
            }
            
            return document;
        }
    }
}
