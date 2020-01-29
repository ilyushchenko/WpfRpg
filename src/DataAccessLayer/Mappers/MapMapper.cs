using System;
using System.Data.SqlClient;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CMapMapper : IMapper<CMapDto>
    {
        public CMapDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var name = (String) reader["Name"];
            var width = (Int32) reader["Width"];
            var height = (Int32) reader["Height"];
            return CMapDto.Create(id, name, width, height);
        }
    }
}