using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ntreev.Library.Psd.Readers
{
    class LinkedLayerReader : ReadableValue<LinkedLayer[]>
    {
        private readonly long end;
        private readonly Uri baseUri;

        public LinkedLayerReader(PsdReader reader, long end, Uri baseUri)
            : base(reader)
        {
            this.end = end;
            this.baseUri = baseUri;
        }

        //public LinkedLayer[] LinkedLayers
        //{
        //    get
        //    {
        //        if (this.linkedLayers == null)
        //        {
        //            this.reader.Position = this.position;
        //            this.linkedLayers = Read(this.reader, this.end, this.baseUri);
        //        }
        //        return this.linkedLayers;
        //    }
        //}

        //private static LinkedLayer[] Read(PsdReader reader, long end, Uri baseUri)
        //{
        //    List<string> keys = new List<string>(new string[] { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", "extd", });
        //    List<LinkedLayer> linkedLayers = new List<LinkedLayer>();

        //    while (reader.Position < end)
        //    {
        //        string signature = reader.ReadType();
        //        string key = reader.ReadType();
        //        if (signature != "8BIM" && signature != "8B64")
        //            throw new InvalidFormatException();

        //        long ssss = reader.Position;

        //        long l = 0;
        //        long p;

        //        if (keys.Contains(key) == true && reader.Version == 2)
        //        {
        //            l = reader.ReadInt64();
        //            p = reader.Position;
        //        }
        //        else
        //        {
        //            l = reader.ReadInt32();
        //            p = reader.Position;
        //        }

        //        switch (key)
        //        {
        //            case "lnkE":
        //                {
        //                    long e = reader.Position + l;
        //                    while (reader.Position < e)
        //                    {
        //                        linkedLayers.Add(new EmbeddedLayer(reader, baseUri));
        //                    }
        //                }
        //                break;
        //            case "lnkD":
        //            case "lnk2":
        //            case "lnk3":
        //                {
        //                    long e = reader.Position + l;
        //                    while (reader.Position < e)
        //                    {
        //                        linkedLayers.Add(new LinkedLayer(reader, baseUri));
        //                    }
        //                }
        //                break;
        //        }

        //        reader.Position = p + l;
        //        if (reader.Position % 2 != 0)
        //            reader.Position++;

        //        reader.Position += ((reader.Position - p) % 4);
        //    }

        //    return linkedLayers.ToArray();
        //}

        protected override LinkedLayer[] ReadValue(PsdReader reader)
        {
            List<string> keys = new List<string>(new string[] { "LMsk", "Lr16", "Lr32", "Layr", "Mt16", "Mt32", "Mtrn", "Alph", "FMsk", "lnk2", "FEid", "FXid", "PxSD", "lnkE", "extd", });
            List<LinkedLayer> linkedLayers = new List<LinkedLayer>();

            while (reader.Position < this.end)
            {
                string signature = reader.ReadType();
                string key = reader.ReadType();
                if (signature != "8BIM" && signature != "8B64")
                    throw new InvalidFormatException();

                long ssss = reader.Position;

                long l = 0;
                long p;

                if (keys.Contains(key) == true && reader.Version == 2)
                {
                    l = reader.ReadInt64();
                    p = reader.Position;
                }
                else
                {
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

            return linkedLayers.ToArray();
        }
    }
}