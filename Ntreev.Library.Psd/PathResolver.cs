using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    public class PathResolver : PsdResolver
    {
        public override PsdDocument GetDocument(Uri absoluteUri)
        {
            string filename = absoluteUri.LocalPath;
            if (File.Exists(filename) == false)
                throw new FileNotFoundException(string.Format("{0} 파일을 찾을 수 없습니다.", filename), filename);

            PsdDocument document = new PsdDocument();
            document.Read(filename);

            return document;
        }
    }
}
