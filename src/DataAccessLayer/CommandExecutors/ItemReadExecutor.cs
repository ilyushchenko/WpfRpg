using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.CommandExecutors
{
    internal class CItemReadExecutor<T> : ICommandExecutor
    {
        private readonly IMapper<T> _mapper;

        public CItemReadExecutor(IMapper<T> mapper)
        {
            Result = default;
            _mapper = mapper;
        }

        public T Result { get; private set; }

        public void Execute(SqlCommand command)
        {
            SqlDataReader reader = default;
            try
            {
                reader = command.ExecuteReader();
                if (reader.Read())
                    Result = _mapper.ReadItem(reader);
                else
                    throw new DataException("Item not found in database");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
        }
    }
}