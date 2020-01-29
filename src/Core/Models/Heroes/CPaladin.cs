using System;
using System.Runtime.Serialization;

namespace Core.Models.Heroes
{
    [DataContract]
    public class CPaladin : CHeroBase
    {
        private CPaladin(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
            : base(name, maxHealthPoints, maxMovingEnergy, maxWeight)
        {
        }

        public static CPaladin Create(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
        {
            return new CPaladin(name, maxHealthPoints, maxMovingEnergy, maxWeight);
        }
    }
}
