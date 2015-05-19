using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.LayerResources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class LayerResourceReader : LazyProperties
    {
        public LayerResourceReader(PsdReader reader, long length)
            : base(reader, length, null)
        {
            
        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();

            while (reader.Position < this.EndPosition)
            {
                reader.ValidateSignature();
                string resourceID = reader.ReadType();
                long length = reader.ReadInt32();
                length += length % 2;

                ResourceReaderBase resourceReader = ReaderCollector.CreateReader(resourceID, reader, length);
                string resourceName = ReaderCollector.GetDisplayName(resourceID);

                props[resourceName] = resourceReader;
            }

            value = props;
        }
    }
}

