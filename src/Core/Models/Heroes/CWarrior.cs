using System;
using System.Runtime.Serialization;

namespace Core.Models.Heroes
{
    [DataContract]
    public class CWarrior : CHeroBase
    {
        private CWarrior(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
            : base(name, maxHealthPoints, maxMovingEnergy, maxWeight)
        {
        }

        public static CWarrior Create(String name, Int32 maxHealthPoints, Int32 maxMovingEnergy, Double maxWeight)
        {
            return new CWarrior(name, maxHealthPoints, maxMovingEnergy, maxWeight);
        }
    }
}