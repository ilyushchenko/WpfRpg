using System;

namespace DataAccessLayer.DTO
{
    public class CTerrainDto
    {
        private CTerrainDto(Guid id, String name, Int32 penalty)
        {
            Id = id;
            Name = name;
            Penalty = penalty;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public Int32 Penalty { get; set; }

        public static CTerrainDto Create(Guid id, String name, Int32 penalty)
        {
            return new CTerrainDto(id, name, penalty);
        }
    }
}