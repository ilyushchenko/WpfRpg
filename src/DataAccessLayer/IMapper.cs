using System.Data.SqlClient;

namespace DataAccessLayer
{
    public interface IMapper<out T>
    {
        T ReadItem(SqlDataReader reader);
    }
}
