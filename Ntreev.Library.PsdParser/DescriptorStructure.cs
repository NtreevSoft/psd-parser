using Ntreev.Library.PsdParser.Decoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ntreev.Library.PsdParser
{
    sealed class DescriptorStructure : Properties
    {
        public DescriptorStructure(PSDReader reader)
        {
            this.Add("Name", reader.ReadUnicodeString2());
            this.Add("ClassID", reader.ReadStringOrKey());

            int count = reader.ReadInt32();
            List<string> keys = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string key = reader.ReadStringOrKey().Trim();
                if (i == 10 && key == "warp")
                {
                    int wqer = 0;
                }
                keys.Add(key);

                string osType = reader.ReadAscii(4);
                DecoderFactory.DecodeFunc func = DecoderFactory.GetDecoder(osType);
                if (func != null)
                {
                    object value = func(reader, key);
                    this.Add(key, value);
                }
                else
                {
                    var d1 = reader.ReadInt32();
                    new DecoderClass(reader);
                    var d3 = reader.ReadInt32();

                    for (int o = 0; o < d3; o++)
                    {
                        string kkk = reader.ReadStringOrKey();
                        string ttt = reader.ReadAscii(4);
                        string ttt1 = reader.ReadAscii(4);
                        int d4 = reader.ReadInt32();

                        var ddd = reader.ReadDoubles(d4);
                    }
                    //string lksdj = reader.ReadPascalString(1);
                    //for (int z = 0; z < d1; z++)
                    //{
                    //    new DecoderReference(reader, key);
                    //}

                    //new DecoderEngineData(reader);
                    
                    if(key == "meshPoints")
                    {
                        //new DecoderReference(reader, key);
                        int qwer = 0;
                        //var d1 = reader.ReadInt32();
                        //var ddd = reader.ReadBytes(16);

                        //var d6 = reader.ReadAscii(100);
                        //var offset = reader.ReadInt32();
                        //var len = reader.ReadInt32();
                        //new DescriptorStructure(reader);

                    }
                    //if (reader.Position % 2 !=0)
                    //{
                    //    reader.Position++;
                    //}
                    //int dfd = reader.ReadInt32();
                    //reader.Position += dfd;
                    //if (dfd % 2 != 0)
                    //   reader.Position++;
                    //int qwer = 0;
                }


                
            }
        }
    }
}
