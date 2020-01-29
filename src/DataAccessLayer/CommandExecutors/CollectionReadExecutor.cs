using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.CommandExecutors
{
    internal class CCollectionReadExecutor<T> : ICommandExecutor
    {
        private readonly IMapper<T> _mapper;

        public CCollectionReadExecutor(IMapper<T> mapper)
        {
            Result = default;
            _mapper = mapper;
        }

        public IReadOnlyCollection<T> Result { get; private set; }

        public void Execute(SqlCommand command)
        {
            SqlDataReader reader = default;
            var collection = new List<T>();
            try
            {
                reader = command.ExecuteReader();

                while (reader.Read()) collection.Add(_mapper.ReadItem(reader));
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            Result = collection;
        }
    }
}