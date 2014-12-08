using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ntreev.Library.Psd.Structures
{
    class StructureUnitFloat : Properties
    {
        public StructureUnitFloat(PSDReader reader)
        {
            string key = reader.ReadKey();
            UnitType unitType = UnitType.None;

            switch (key)
            {
                case "#Ang":
                    unitType = UnitType.Angle;
                    break;
                case "#Rsl":
                    unitType = UnitType.Density;
                    break;
                case "#Rlt":
                    unitType = UnitType.Distance;
                    break;
                case "#Nne":
                    unitType = UnitType.None;
                    break;
                case "#Prc":
                    unitType = UnitType.Percent;
                    break;
                case "#Pxl":
                    unitType = UnitType.Pixels;
                    break;
                case "#Pnt":
                    unitType = UnitType.Points;
                    break;
                case "#Mlm":
                    unitType = UnitType.Millimeters;
                    break;
                default:
                    throw new NotSupportedException();
            }
            this.Add("Type", unitType);
            this.Add("Value", reader.ReadDouble());
        }
    }
}
