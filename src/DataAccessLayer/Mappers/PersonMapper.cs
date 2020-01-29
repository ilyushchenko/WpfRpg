using System;
using System.Data.SqlClient;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CPersonMapper : IMapper<CPlayerDto>
    {
        public CPlayerDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var login = (String) reader["Login"];

            return CPlayerDto.Create(id, login);
        }
    }
}