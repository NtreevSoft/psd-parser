using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class EmbeddedLayerReader : ValueReader<EmbeddedLayer>
    {
        public EmbeddedLayerReader(PsdReader reader)
            : base(reader, true, null)
        {
            
        }

        protected override long OnLengthGet(PsdReader reader)
        {
            return (reader.ReadInt64() + 3) & (~3);
        }

        private Uri ReadAboluteUri(PsdReader reader)
        {
            IProperties props = new DescriptorStructure(reader);
            if (props.Contains("fullPath") == true)
            {
                Uri absoluteUri = new Uri(props["fullPath"] as string);
                if (File.Exists(absoluteUri.LocalPath) == true)
                    return absoluteUri;
            }

            if (props.Contains("relPath") == true)
            {
                string relativePath = props["relPath"] as string;
                Uri absoluteUri = reader.Resolver.ResolveUri(reader.Uri, relativePath);
                if (File.Exists(absoluteUri.LocalPath) == true)
                    return absoluteUri;
            }

            if (props.Contains("Nm") == true)
            {
                string name = props["Nm"] as string;
                Uri absoluteUri = reader.Resolver.ResolveUri(reader.Uri, name);
                if (File.Exists(absoluteUri.LocalPath) == true)
                    return absoluteUri;
            }

            if (props.Contains("fullPath") == true)
            {
                return new Uri(props["fullPath"] as string);
            }

            return null;
        }

        protected override void ReadValue(PsdReader reader, object userData, out EmbeddedLayer value)
        {
            reader.ValidateSignature("liFE");

            int version = reader.ReadInt32();
            
            Guid id = new Guid(reader.ReadPascalString(1));
            string name = reader.ReadString();
            string type = reader.ReadType();
            string creator = reader.ReadType();

            long length = reader.ReadInt64();
            IProperties properties = reader.ReadBoolean() == true ? new DescriptorStructure(reader) : null;
            Uri absoluteUri = this.ReadAboluteUri(reader);

            value = new EmbeddedLayer(id, reader.Resolver, absoluteUri);
        }
    }
}
