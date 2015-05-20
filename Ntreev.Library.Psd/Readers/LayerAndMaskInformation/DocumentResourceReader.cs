using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class DocumentResourceReader : LazyProperties
    {
        private static string[] doubleTypeKeys = { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", "extd", };

        public DocumentResourceReader(PsdReader reader, long length)
            : base(reader, length, null)
        {

        }

        protected override void ReadValue(PsdReader reader, object userData, out IProperties value)
        {
            Properties props = new Properties();

            while (reader.Position < this.EndPosition)
            {
                reader.ValidateSignature(true);
                string resourceID = reader.ReadType();
                long length = this.ReadLength(reader, resourceID);

                ResourceReaderBase resourceReader = ReaderCollector.CreateReader(resourceID, reader, length);
                string resourceName = ReaderCollector.GetDisplayName(resourceID);

                props[resourceName] = resourceReader;
            }

            value = props;
        }

        private long ReadLength(PsdReader reader, string resourceID)
        {
            long length = 0;
            if (doubleTypeKeys.Contains(resourceID) && reader.Version == 2)
            {
                length = reader.ReadInt64();
            }
            else
            {
                length = reader.ReadInt32();
            }

            return (length + 3) & (~3);
        }
    }
}