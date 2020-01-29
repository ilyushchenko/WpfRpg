using System;
using System.Runtime.Serialization;

namespace Core.Models.Heroes
{
    [DataContract]
    public class CThief : CHeroBase
    {
        private CThief(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
            : base(name, maxHealthPoints, maxMovingEnergy, maxWeight)
        {
        }

        public static CThief Create(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
        {
            return new CThief(name, maxHealthPoints, maxMovingEnergy, maxWeight);
        }
    }
}
