using System;
using System.Xml.Linq;
using Core.Models.Units;
using Interfaces;

namespace Core.FactoryMethods.Units
{
    public class CWallFactoryMethod : CUnitFactoryMethod
    {
        private CWallFactoryMethod()
        {
        }

        public override IPositionable CreateUnit(XElement wallData)
        {
            if (wallData == null) throw new ArgumentNullException(nameof(wallData));

            String orientationData = wallData.Element("orientation")?.Value;

            Enum.TryParse<EWallOrientation>(orientationData, true, out EWallOrientation orientation);

            return CWall.Create(orientation);
        }

        public static CWallFactoryMethod GetFactory()
        {
            return new CWallFactoryMethod();
        }
    }
}