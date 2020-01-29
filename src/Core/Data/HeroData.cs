using System;
using Interfaces.Enums;

namespace Core.Data
{
    public class CHeroData
    {
        public CHeroData()
        {
        }
        private CHeroData(String name, EHeroTypes type, Int32 healthPoints, Int32 movingEnergy, String description)
        {
            Name = name;
            Type = type;
            HealthPoints = healthPoints;
            MovingEnergy = movingEnergy;
            Description = description;
        }

        public String Name { get; set; }
        public EHeroTypes Type { get; set; }
        public Int32 HealthPoints { get; set; }
        public Int32 MovingEnergy { get; set; }
        public String Description { get; set; }

        public static CHeroData Create(String name, EHeroTypes type,
            Int32 healthPoints, Int32 movingEnergy, String description)
        {
            return new CHeroData(name, type, healthPoints, movingEnergy, description);
        }
    }
}