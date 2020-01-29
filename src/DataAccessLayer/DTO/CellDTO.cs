using System;

namespace DataAccessLayer.DTO
{
    public class CCellDTO
    {
        private CCellDTO(Guid id, Int32 x, Int32 y, Guid mapId, Guid terrainId)
        {
            Id = id;
            X = x;
            Y = y;
            MapId = mapId;
            TerrainId = terrainId;
        }

        public Guid Id { get; set; }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Guid MapId { get; set; }
        public Guid TerrainId { get; set; }

        public static CCellDTO Create(Guid id, Int32 x, Int32 y, Guid mapId, Guid terrainId)
        {
            return new CCellDTO(id, x, y, mapId, terrainId);
        }
    }
}