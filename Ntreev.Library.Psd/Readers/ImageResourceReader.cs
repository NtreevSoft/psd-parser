using Ntreev.Library.Psd.Readers;
using Ntreev.Library.Psd.Readers.ImageResource;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers
{
    class ImageResourceReader : ReadableLazyProperties
    {
        private static readonly IDictionary<string, Type> readers = ReaderCollector.Collect(typeof(ImageResourceBase));

        public ImageResourceReader(PsdReader reader)
            : base(reader, true)
        {

        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return reader.ReadInt32();
        }

        protected override IEnumerable<KeyValuePair<string, object>> CreateProperties(PsdReader reader)
        {
            Dictionary<string, object> props = new Dictionary<string, object>();

            long position = reader.Position;

            while ((reader.Position - position) < this.Length)
            {
                reader.ValidateSignature();

                string resourceID = reader.ReadInt16().ToString();
                string name = reader.ReadPascalString(2);

                int resourceSize = reader.ReadInt32();

                if (resourceSize == 0)
                    continue;

                if (readers.ContainsKey(resourceID) == true)
                {
                    Type readerType = readers[resourceID];
      
                    string displayName = ReaderCollector.GetDisplayName(readerType);
                    object instance = TypeDescriptor.CreateInstance(null, readerType, new Type[] { typeof(PsdReader), }, new object[] { reader, });
                    props[displayName] = instance;
                }

                reader.Position += resourceSize;

                if ((resourceSize % 2) != 0)
                {
                    reader.Position += 1L;
                }
            }
            return props;
        }
    }
}
