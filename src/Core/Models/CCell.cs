using System;
using System.Runtime.Serialization;
using System.Text;
using Core.Models.Terrains;
using Interfaces;

namespace Core.Models
{
    [DataContract(IsReference = true)]
    public class CCell : ICell
    {
        public CCell(Int32 x, Int32 y)
        {
            Location = new SPoint(x, y);
            Terrain = new CGrass();
        }

        [DataMember]
        public SPoint Location { get; private set; }

        public CCell()
        {
            Location = new SPoint(-1, -1);
        }

        [DataMember]
        public ITerrain Terrain { get; set; }

        [DataMember]
        public IPositionable Unit { get; set; }

        public override String ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Position: {Location}");
            if (Unit != null)
            {
                stringBuilder.AppendLine($"Unit: {Unit}");
            }

            if (Terrain != null)
            {
                stringBuilder.AppendLine($"Terrain: {Terrain}");
            }

            return stringBuilder.ToString();
        }
    }
}