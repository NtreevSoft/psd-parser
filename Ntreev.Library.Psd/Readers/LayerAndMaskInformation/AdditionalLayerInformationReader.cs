using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers.LayerAndMaskInformation
{
    class AdditionalLayerInformationReader : LazyValueReader<AdditionalLayerInformation>
    {
        private static string[] doubleTypeKeys = { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", "extd", };
        private readonly Uri baseUri;

        public AdditionalLayerInformationReader(PsdReader reader, long length, Uri baseUri)
            : base(reader, length, null)
        {
            this.baseUri = baseUri;
        }

        protected override void ReadValue(PsdReader reader, object userData, out AdditionalLayerInformation value)
        {
            value = new AdditionalLayerInformation();

            List<LinkedLayer> linkedLayers = new List<LinkedLayer>();

            long end = this.Position + this.Length;
            while (reader.Position < end)
            {
                //reader.ValidateSignature(true);
                string typess = reader.ReadType();
                string key = reader.ReadType();

                Console.WriteLine(key + " : " + typess);

                long ssss = reader.Position;

                long l = 0;
                long p;

                //if (doubleTypeKeys.Contains(key) == true && reader.Version == 2)
                if(typess != "8BIM")
                {
                    l = reader.ReadInt64();
                    p = reader.Position;
                }
                else
                {
                    //if (typess != "8BIM")
                    //    throw new Exception();
                    l = reader.ReadInt32();
                    p = reader.Position;
                }

                switch (key)
                {
                    case "lnkE":
                        {
                            long e = reader.Position + l;
                            while (reader.Position < e)
                            {
                                linkedLayers.Add(new EmbeddedLayer(reader, this.baseUri));
                            }
                        }
                        break;
                    case "lnkD":
                    case "lnk2":
                    case "lnk3":
                        {
                            long e = reader.Position + l;
                            while (reader.Position < e)
                            {
                                linkedLayers.Add(new LinkedLayer(reader, this.baseUri));
                            }
                        }
                        break;
                }

                reader.Position = p + l;
                if (reader.Position % 2 != 0)
                    reader.Position++;

                reader.Position += ((reader.Position - p) % 4);
            }
            value.LinkedLayers = linkedLayers.ToArray();
        }
    }
}