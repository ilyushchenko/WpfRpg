using System;

namespace Interfaces
{
    public interface IHero
    {
        String Name { get; set; }
        Int32 MaxHealthPoints { get; set; }
        Int32 MaxMovingEnergy { get; set; }
        Double MaxWeight { get; }
    }
}