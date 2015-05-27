using Ntreev.Library.Psd.Readers;
using System;
using System.IO;
using System.Text;

namespace Ntreev.Library.Psd
{
    public struct FileHeaderSection
    {
        public int Depth
        {
            get;
            set;
        }

        public int NumberOfChannels
        {
            get;
            set;
        }

        public ColorMode ColorMode
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public static FileHeaderSection FromFile(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (PsdReader reader = new PsdReader(stream, new PathResolver(), new Uri(Path.GetDirectoryName(filename))))
            {
                reader.ReadDocumentHeader();
                return FileHeaderSectionReader.Read(reader);
            }
        }
    }
}
