using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class ChannelsReader : LazyValueReader<Channel[]>
    {
        public ChannelsReader(PsdReader reader, long length, PsdLayer layer)
            : base(reader, length, layer)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out Channel[] value)
        {
            PsdLayer layer = userData as PsdLayer;
            LayerRecords records = layer.Records;

            using (MemoryStream stream = new MemoryStream(reader.ReadBytes((int)this.Length)))
            using (PsdReader r = new PsdReader(stream, reader.Resolver, reader.Uri))
            {
                r.Version = reader.Version;
                this.ReadValue(r, layer.Depth, records.Channels);
            }

            value = records.Channels;
        }

        private void ReadValue(PsdReader reader, int depth, Channel[] channels)
        {
            foreach (var item in channels)
            {
                CompressionType compressionType = reader.ReadCompressionType();
                item.ReadHeader(reader, compressionType);
                item.Read(reader, depth, compressionType);
            }
        }
    }
}
