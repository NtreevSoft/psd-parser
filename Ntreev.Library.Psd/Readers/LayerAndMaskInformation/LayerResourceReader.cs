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
    class LayerResourceReader : LazyReadableProperties
    {
        private static readonly IDictionary<string, Type> readers = ReaderCollector.Collect(typeof(LayerResourceBase));

        public LayerResourceReader(PsdReader reader, long length)
            : base(reader, length)
        {
            
        }

        private void Read(PsdReader reader, Properties properties)
        {
            reader.ValidateSignature();
            string resourceID = reader.ReadAscii(4);

            int length = reader.ReadInt32();

            long position = reader.Position;

            Type readerType = null;
            if (readers.ContainsKey(resourceID) == true)
            {
                readerType = readers[resourceID];
            }

            if (readerType != null)
            {
                properties.Add(resourceID, TypeDescriptor.CreateInstance(null, readerType, new Type[] { typeof(PsdReader), }, new object[] { reader, }));
            }


            if (reader.Position > position + length)
            {
                throw new InvalidFormatException();
            }

            reader.Position = position + length;
        }

        protected override void ReadValue(PsdReader reader, out IProperties value)
        {
            Properties props = new Properties();
            while (reader.Position < this.EndPosition)
            {
                this.Read(reader, props);
            }
            value = props;
        }
    }
}

