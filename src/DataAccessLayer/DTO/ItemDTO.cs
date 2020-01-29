using System;

namespace DataAccessLayer.DTO
{
    public class CItemDto
    {
        private CItemDto(Guid id, String name, Int32 cost, Int32 type, String data)
        {
            Id = id;
            Name = name;
            Cost = cost;
            Type = type;
            Data = data;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Cost { get; set; }

        public Int32 Type { get; set; }
        public String Data { get; set; }

        public static CItemDto Create(Guid id, String name, Int32 cost, Int32 type, String data)
        {
            return new CItemDto(id, name, cost, type, data);
        }

        public static CItemDto Create(String name, Int32 cost, Int32 type, String data)
        {
            return new CItemDto(Guid.Empty, name, cost, type, data);
        }
    }
}