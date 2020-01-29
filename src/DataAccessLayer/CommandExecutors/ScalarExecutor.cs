using System;
using System.Data.SqlClient;

namespace DataAccessLayer.CommandExecutors
{
    internal class CScalarExecutor : ICommandExecutor
    {
        public Object Result { get; private set; }

        public void Execute(SqlCommand command)
        {
            Object result = command.ExecuteScalar();
            Result = result;
        }
    }
}