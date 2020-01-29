using System;

namespace DataAccessLayer.DTO
{
    public class CHeroDto
    {
        private CHeroDto(Guid id, String name, Int32 type, Int32 health, Int32 movingEnergy, String description,
            String data)
        {
            Id = id;
            Name = name;
            Type = type;
            Data = data;
            Description = description;
            Health = health;
            MovingEnergy = movingEnergy;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Type { get; set; }
        public Int32 Health { get; set; }
        public Int32 MovingEnergy { get; set; }
        public String Description { get; set; }
        public String Data { get; set; }

        public static CHeroDto Create(Guid id, String name, Int32 type, Int32 health, Int32 movingEnergy,
            String description, String data)
        {
            return new CHeroDto(id, name, type, health, movingEnergy, description, data);
        }
    }
}