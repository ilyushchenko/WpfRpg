using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CMapsRepository : CRepositoryBase<CMapDto, Guid>
    {
        private CMapsRepository(IMapper<CMapDto> mapper) : base(mapper)
        {
        }

        public static CMapsRepository GetRepository()
        {
            return new CMapsRepository(new CMapMapper());
        }

        public override Guid Add(CMapDto map)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object>
            {
                {"@name", map.Name},
                {"@width", map.Width},
                {"@height", map.Height},
            };
            return AddItem("AddMap", parameters);
        }

        public override Boolean Update(CMapDto map)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object>
            {
                {"@id", map.Id},
                {"@name", map.Name},
                {"@width", map.Width},
                {"@height", map.Height},
            };
            return Execute(query, parameters);
        }

        protected override String GetRepositoryName()
        {
            return GetType().ToString();
        }
    }
}