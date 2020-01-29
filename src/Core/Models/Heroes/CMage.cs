using System;
using System.Runtime.Serialization;

namespace Core.Models.Heroes
{
    [DataContract]
    public class CMage : CHeroBase
    {
        private CMage(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
            : base(name, maxHealthPoints, maxMovingEnergy, maxWeight)
        {
        }

        public static CMage Create(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
        {
            return new CMage(name, maxHealthPoints, maxMovingEnergy, maxWeight);
        }
    }
}