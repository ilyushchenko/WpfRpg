using System;
using System.Data.SqlClient;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CGameMapper : IMapper<CGameDto>
    {
        public CGameDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var name = (String) reader["Name"];
            var mapId = (Guid) reader["MapId"];
            var startedAt = (DateTime) reader["StartedAt"];
            var endedAt = (DateTime) reader["EndedAt"];
            var winnerId = (Guid) reader["WinnerId"];
            return CGameDto.Create(id, name, mapId, startedAt, endedAt, winnerId);
        }
    }
}