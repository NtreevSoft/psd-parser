using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.ImageResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageResourcesSectionReader : LazyProperties
    {
        public ImageResourcesSectionReader(PsdReader reader)
            : base(reader, null)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();

            while(reader.Position < this.EndPosition)
            {
                reader.ValidateSignature();

                string resourceID = reader.ReadInt16().ToString();
                string name = reader.ReadPascalString(2);
                long length = reader.ReadInt32();
                length += (length % 2);

                ResourceReaderBase resourceReader = ReaderCollector.CreateReader(resourceID, reader, length);
                string resourceName = ReaderCollector.GetDisplayName(resourceID);

                props[resourceName] = resourceReader;
            }

            value = props;
        }
    }
}
