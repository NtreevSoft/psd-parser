using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public struct SolidFillInfo
    {
        public int Version { get; set; }

        public int BlendMode { get; set; }

        public string Color { get; set; }

        public bool Opacity { get; set; }

        public bool Enabled { get; set; }

        public string NativeColor { get; set; }

        internal static SolidFillInfo Parse(BinaryReader br)
        {
            SolidFillInfo value = new SolidFillInfo();


            value.Version = EndianReverser.getInt32(br);

            string _8bim = PSDUtil.readAscii(br, 4);
            //value.BlendMode = EndianReverser.getInt32(br);

            var bbbb = br.ReadBytes(10);

            value.Opacity = EndianReverser.getBoolean(br);
            value.Enabled = EndianReverser.getBoolean(br);

            var bbbb1 = br.ReadBytes(10);

            return value;
        }

    }
}
