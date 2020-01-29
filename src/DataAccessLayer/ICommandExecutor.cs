using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal interface ICommandExecutor
    {
        void Execute(SqlCommand command);
    }
}