using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CGamesRepository : CRepositoryBase<CGameDto, Guid>
    {
        private CGamesRepository(IMapper<CGameDto> mapper) : base(mapper)
        {
        }

        public static CGamesRepository GetRepository()
        {
            return new CGamesRepository(new CGameMapper());
        }

        public override Guid Add(CGameDto game)
        {
            var parameters = new Dictionary<String, Object>
            {
                {"@name", game.Name},
                {"@mapId", game.MapId},
                {"@startedAt", game.StartedAt},
                {"@endedAt", game.EndedAt},
                {"@winnerId", game.WinnerId},
            };
            return AddItem("AddGame", parameters);
        }

        public override Boolean Update(CGameDto game)
        {
            var parameters = new Dictionary<String, Object>
            {
                {"@id", game.Id},
                {"@name", game.Name},
                {"@mapId", game.MapId},
                {"@startedAt", game.StartedAt},
                {"@endedAt", game.EndedAt},
                {"@winnerId", game.WinnerId},
            };
            String query = GetQuery();
            return Execute(query, parameters);
        }

        protected override String GetRepositoryName()
        {
            return GetType().ToString();
        }
    }
}