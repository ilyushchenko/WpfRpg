using System;

namespace DataAccessLayer.DTO
{
    public class CMapDto
    {
        private CMapDto(Guid id, String name, Int32 width, Int32 height)
        {
            Id = id;
            Name = name;
            Width = width;
            Height = height;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Width { get; set; }
        public Int32 Height { get; set; }

        public static CMapDto Create(Guid id, String name, Int32 width, Int32 height)
        {
            return new CMapDto(id, name, width, height);
        }
    }
}