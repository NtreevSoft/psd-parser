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
                string type = reader.ReadType();
                string resourceID = reader.ReadType();

                long length = type != "8BIM" ? reader.ReadInt64() : reader.ReadInt32();
                length = (length + 3) & (~3);

                ResourceReaderBase resourceReader = ReaderCollector.CreateReader(resourceID, reader, length);
                string resourceName = ReaderCollector.GetDisplayName(resourceID);

                props[resourceName] = resourceReader;
            }

            value = props;
        }
    }
}