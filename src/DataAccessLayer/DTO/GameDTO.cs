using System;

namespace DataAccessLayer.DTO
{
    public class CGameDto
    {
        private CGameDto(Guid id, String name, Guid mapId, DateTime startedAt, DateTime endedAt, Guid winnerId)
        {
            Id = id;
            Name = name;
            MapId = mapId;
            StartedAt = startedAt;
            EndedAt = endedAt;
            WinnerId = winnerId;
        }

        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid MapId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public Guid WinnerId { get; set; }

        public static CGameDto Create(Guid id, String name, Guid mapId, DateTime startedAt, DateTime endedAt,
            Guid winnerId)
        {
            return new CGameDto(id, name, mapId, startedAt, endedAt, winnerId);
        }
    }
}