using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CPlayersRepository : CRepositoryBase<CPlayerDto, Guid>
    {
        public CPlayersRepository(IMapper<CPlayerDto> mapper) : base(mapper)
        {
        }

        public static CPlayersRepository GeRepository()
        {
            return new CPlayersRepository(new CPersonMapper());
        }

        public override Guid Add(CPlayerDto player)
        {
            return AddItem("dbo.AddPlayer", new Dictionary<String, Object> {{"login", player.Login}});
        }

        public override Boolean Update(CPlayerDto player)
        {
            throw new NotImplementedException();
        }

        public override Boolean Remove(Guid id)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object> {{"@id", id}};
            return Execute(query, parameters);
        }

        protected override String GetRepositoryName()
        {
            return GetType().ToString();
        }
    }
}