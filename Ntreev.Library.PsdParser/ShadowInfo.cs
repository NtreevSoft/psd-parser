using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public struct ShadowInfo
    {
        public int Version { get; set; }

        public int BlueInPixels { get; set; }

        public int Intensity { get; set; }

        public int Angle { get; set; }

        public int Distance { get; set; }

        public string Color { get; set; }

        public string BlendMode { get; set; }

        public bool Enabled { get; set; }



        internal static ShadowInfo Parse(BinaryReader br)
        {
            ShadowInfo value = new ShadowInfo();

            value.Version = EndianReverser.getInt32(br);
            EndianReverser.getInt32(br);
            EndianReverser.getInt32(br);
            EndianReverser.getInt32(br);
            EndianReverser.getInt32(br);
            br.ReadBytes(10);

            string _8bim = PSDUtil.readAscii(br, 4);
            string bm = PSDUtil.readAscii(br, 4);
            //value.BlendMode = EndianReverser.getInt32(br);
            value.Enabled = EndianReverser.getBoolean(br);


            return value;
        }
    }
}
