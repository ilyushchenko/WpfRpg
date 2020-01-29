using System;
using System.Data.SqlClient;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CUnitMapper : IMapper<CUnitDto>
    {
        public CUnitDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var name = (String) reader["Name"];
            var type = (String) reader["Type"];
            var data = (String) reader["Data"];
            var cost = (Int32) reader["Cost"];
            return CUnitDto.Create(id, name, type, data, cost);
        }
    }
}