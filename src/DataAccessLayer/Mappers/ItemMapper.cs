using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CItemMapper : IMapper<CItemDto>
    {
        public CItemDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var name = (String) reader["Name"];
            var cost = (Int32) reader["Cost"];
            var type = (Int32) reader["Type"];
            var data = (String) reader["Data"];
            return CItemDto.Create(id, name, cost, type, data);
        }
    }
}