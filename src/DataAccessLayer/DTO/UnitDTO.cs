using System;

namespace DataAccessLayer.DTO
{
    public class CUnitDto
    {
        public CUnitDto(Guid id, String name, String type, String data, Int32 cost)
        {
            Id = id;
            Name = name;
            Type = type;
            Data = data;
            Cost = cost;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Data { get; set; }
        public Int32 Cost { get; set; }

        public static CUnitDto Create(Guid id, String name, String type, String data, Int32 cost)
        {
            return new CUnitDto(id, name, type, data, cost);
        }
    }
}