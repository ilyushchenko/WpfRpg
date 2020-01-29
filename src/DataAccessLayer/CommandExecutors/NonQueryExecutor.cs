using System;
using System.Data.SqlClient;

namespace DataAccessLayer.CommandExecutors
{
    internal class CNonQueryExecutor : ICommandExecutor
    {
        public Int32 RowsAffected { get; private set; }

        public void Execute(SqlCommand command)
        {
            RowsAffected = command.ExecuteNonQuery();
        }
    }
}