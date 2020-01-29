using System;
using System.Data.SqlClient;
using DataAccessLayer.DTO;

namespace DataAccessLayer.Mappers
{
    internal class CHeroMapper : IMapper<CHeroDto>
    {
        public CHeroDto ReadItem(SqlDataReader reader)
        {
            var id = (Guid) reader["Id"];
            var name = (String) reader["Name"];
            var type = (Int32) reader["Type"];
            var health = (Int32) reader["Health"];
            var movingEnergy = (Int32) reader["MovingEnergy"];
            var description = (String) reader["Description"];
            var data = (String) reader["Data"];
            return CHeroDto.Create(id, name, type, health, movingEnergy, description, data);
        }
    }
}