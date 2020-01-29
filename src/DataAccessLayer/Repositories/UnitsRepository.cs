using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CUnitsRepository : CRepositoryBase<CUnitDto, Guid>
    {
        private CUnitsRepository(IMapper<CUnitDto> mapper) : base(mapper)
        {
        }

        public static CUnitsRepository GetRepository()
        {
            return new CUnitsRepository(new CUnitMapper());
        }

        public override Guid Add(CUnitDto item)
        {
            var parameters = new Dictionary<String, Object>
            {
                {"@name", item.Name},
                {"@type", item.Type},
                {"@data", item.Data},
            };
            return AddItem("AddItem", parameters);
        }

        public override Boolean Update(CUnitDto item)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object>
            {
                {"@id", item.Id},
                {"@name", item.Name},
                {"@type", item.Type},
                {"@data", item.Data},
            };
            return Execute(query, parameters);
        }

        protected override String GetRepositoryName()
        {
            return GetType().ToString();
        }
    }
}