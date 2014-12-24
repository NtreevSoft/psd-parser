using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd
{
    sealed class GridAndGuidesInfo
    {
        private readonly GridInfo gridInfo = new GridInfo();
        private readonly GuideInfo guidesInfo = new GuideInfo();

        internal GridAndGuidesInfo(PsdReader reader)
        {
            int version = reader.ReadInt32();
            
            if (version != 1)
                throw new InvalidFormatException();


            this.gridInfo.Horizontal = reader.ReadInt32();
            this.gridInfo.Vertical = reader.ReadInt32();

            int guideCount = reader.ReadInt32();

            List<int> hg = new List<int>();
            List<int> vg = new List<int>();

            for (int i = 0; i < guideCount; i++)
            {
                int n = reader.ReadInt32();
                byte t = reader.ReadByte();

                if (t == 0)
                    vg.Add(n);
                else
                    hg.Add(n);
            }

            this.guidesInfo.Horizontals = hg.ToArray();
            this.guidesInfo.Verticals = vg.ToArray();
        }

        public GridInfo GridInfo
        {
            get { return this.gridInfo; }
        }

        public GuideInfo GuidesInfo
        {
            get { return this.guidesInfo; }
        }
    }
}
