using System;
using System.Collections.Generic;
using DataAccessLayer.DTO;
using DataAccessLayer.Mappers;

namespace DataAccessLayer.Repositories
{
    public class CItemsRepository : CRepositoryBase<CItemDto, Guid>
    {
        private CItemsRepository(CItemMapper mapper) : base(mapper)
        {
        }

        public static CItemsRepository GetRepository()
        {
            return new CItemsRepository(new CItemMapper());
        }

        public override Guid Add(CItemDto item)
        {
            var parameters = new Dictionary<String, Object>
            {
                {"@name", item.Name},
                {"@cost", item.Cost},
                {"@type", item.Type},
                {"@data", item.Data},
            };
            return AddItem("AddItem", parameters);
        }

        public override Boolean Update(CItemDto item)
        {
            var parameters = new Dictionary<String, Object>
            {
                {"@id", item.Id},
                {"@name", item.Name},
                {"@cost", item.Cost},
                {"@type", item.Type},
                {"@data", item.Data},
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