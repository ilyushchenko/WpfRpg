using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CHeroesRepository : CRepositoryBase<CHeroDto, Guid>
    {
        private CHeroesRepository(IMapper<CHeroDto> mapper) : base(mapper)
        {
        }

        public static CHeroesRepository GetRepository()
        {
            return new CHeroesRepository(new CHeroMapper());
        }

        public override Guid Add(CHeroDto item)
        {
            throw new NotImplementedException();
        }

        public override Boolean Update(CHeroDto item)
        {
            throw new NotImplementedException();
        }

        public CHeroDto GetByType(Int32 type)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object> {{"@type", type}};
            return GetItem(query, parameters, new CHeroMapper());
        }

        protected override String GetRepositoryName()
        {
            return GetType().ToString();
        }
    }
}