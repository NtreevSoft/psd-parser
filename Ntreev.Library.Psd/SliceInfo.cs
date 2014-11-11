using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    public sealed class SliceInfo
    {
        private readonly string name;
        private readonly int left;
        private readonly int top;
        private readonly int right;
        private readonly int bottom;

        private readonly string url;
        private readonly string target;
        private readonly string message;
        private readonly string altTag;
        private readonly int horzAlign;
        private readonly int vertAlign;
        private readonly byte alpha;
        private readonly byte red;
        private readonly byte green;
        private readonly byte blue;
        
        internal SliceInfo(PSDReader reader)
        {
            int id = reader.ReadInt32();
            int groupID = reader.ReadInt32();
            int origin = reader.ReadInt32();
            if (origin == 1)
            {
                int asso = reader.ReadInt32();
            }
            this.name = reader.ReadUnicodeString2();
            int type = reader.ReadInt32();

            this.left = reader.ReadInt32();
            this.top = reader.ReadInt32();
            this.right = reader.ReadInt32();
            this.bottom = reader.ReadInt32();

            this.url = reader.ReadUnicodeString2();
            this.target = reader.ReadUnicodeString2();
            this.message = reader.ReadUnicodeString2();
            this.altTag  = reader.ReadUnicodeString2();

            bool b = reader.ReadBoolean();

            string cellText = reader.ReadUnicodeString2();

            this.horzAlign = reader.ReadInt32();
            this.vertAlign = reader.ReadInt32();

            this.alpha = reader.ReadByte();
            this.red = reader.ReadByte();
            this.green = reader.ReadByte();
            this.blue = reader.ReadByte();
        }

        public int Left
        {
            get { return this.left; }
        }

        public int Top
        {
            get { return this.top; }
        }

        public int Right
        {
            get { return this.right; }
        }

        public int Bottom
        {
            get { return this.bottom; }
        }

        public int Width
        {
            get { return this.right - this.left; }
        }

        public int Height
        {
            get { return this.bottom - this.top; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Url
        {
            get { return this.url; }
        }

        public string Target
        {
            get { return this.target; }
        }

        public string Message
        {
            get { return this.message; }
        }

        public string AltTag
        {
            get { return this.altTag; }
        }

        public int HorzAlign
        {
            get { return this.horzAlign; }
        }

        public int VertAlign
        {
            get { return this.vertAlign; }
        }

        public byte Alpha
        {
            get { return this.alpha; }
        }

        public byte Red
        {
            get { return this.red; }
        }

        public byte Green
        {
            get { return this.green; }
        }

        public byte Blue
        {
            get { return this.blue; }
        }
    }
}
