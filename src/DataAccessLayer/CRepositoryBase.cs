using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using DataAccessLayer.CommandExecutors;

namespace DataAccessLayer
{
    public abstract class CRepositoryBase<TDto, TKey> : IRepository<TDto, TKey>
    {
        private readonly String _defaultConnectionString;
        private readonly IMapper<TDto> _mapper;
        private readonly Dictionary<String, String> _queries;

        protected CRepositoryBase(IMapper<TDto> mapper)
        {
            _mapper = mapper;
            _defaultConnectionString = LoadConnectionString();
            _queries = LoadQueries();
        }

        public virtual TDto Get(TKey id)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object> {{"@id", id}};
            return GetItem(query, parameters, _mapper);
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            String query = GetQuery();
            return GetItems(query, null, _mapper);
        }

        public abstract TKey Add(TDto item);

        public abstract Boolean Update(TDto item);

        public virtual Boolean Remove(Guid id)
        {
            String query = GetQuery();
            var parameters = new Dictionary<String, Object>
            {
                {"@id", id}
            };
            return Execute(query, parameters);
        }

        protected abstract String GetRepositoryName();

        protected String GetQuery([CallerMemberName] String queryName = "")
        {
            if (_queries.ContainsKey(queryName)) return _queries[queryName];
            throw new Exception($"Query '{queryName}' not found. Check SQL query source file");
        }

        #region DB Helpers

        protected Boolean Execute(String query, Dictionary<String, Object> parameters)
        {
            using (var command = new SqlCommand(query))
            {
                AddParameters(command, parameters);

                var executor = new CNonQueryExecutor();

                ExecuteCommand(command, executor);

                return executor.RowsAffected > 0;
            }
        }

        protected Guid AddItem(String storedProcedureName, Dictionary<String, Object> parameters)
        {
            using (var command = new SqlCommand(storedProcedureName))
            {
                command.CommandType = CommandType.StoredProcedure;

                AddParameters(command, parameters);

                var executor = new CScalarExecutor();
                ExecuteCommand(command, executor);

                if (!(executor.Result is Guid itemId))
                    throw new SqlTypeException("Element has not been added");

                return itemId;
            }
        }

        protected IEnumerable<T> GetItems<T>(String query, Dictionary<String, Object> parameters, IMapper<T> mapper)
        {
            using (var command = new SqlCommand(query))
            {
                AddParameters(command, parameters);

                var reader = new CCollectionReadExecutor<T>(mapper);

                ExecuteCommand(command, reader);

                return reader.Result;
            }
        }

        protected T GetItem<T>(String query, Dictionary<String, Object> parameters, IMapper<T> mapper)
        {
            using (var command = new SqlCommand(query))
            {
                AddParameters(command, parameters);

                var reader = new CItemReadExecutor<T>(mapper);

                ExecuteCommand(command, reader);

                return reader.Result;
            }
        }

        private void ExecuteCommand(SqlCommand command, ICommandExecutor commandExecutor)
        {
            using (var connection = new SqlConnection(_defaultConnectionString))
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (command)
                    {
                        command.Connection = connection;
                        commandExecutor.Execute(command);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private static void AddParameters(SqlCommand command, Dictionary<String, Object> parameters)
        {
            if (parameters == null) return;
            SqlParameter[] sqlParameters = parameters.Select(p => new SqlParameter(p.Key, p.Value)).ToArray();
            foreach (SqlParameter sqlParameter in sqlParameters) command.Parameters.Add(sqlParameter);
        }

        #endregion

        #region Initializing

        private Dictionary<String, String> LoadQueries()
        {
            String repositoryName = GetRepositoryName();

            String sqlQueriesSourcePath = ConfigurationManager.AppSettings["SqlQueriesSourcePath"];

            if (sqlQueriesSourcePath == null)
                throw new ApplicationException("SQL queries source path is not define in config file");
            if (!File.Exists(sqlQueriesSourcePath))
                throw new ApplicationException("SQL queries source file not found");

            XElement repositories = XDocument.Load(sqlQueriesSourcePath).Element("repositories") ??
                                    throw new ApplicationException("SQL queries source file is damaged");

            XElement repository = repositories.Elements("repository")
                                      .SingleOrDefault(repoNode =>
                                          repoNode.Attribute("name")?.Value == repositoryName) ??
                                  throw new ApplicationException(
                                      $"SQL queries source file does not contain a repository {repositoryName}");

            var queriesDictionary = new Dictionary<String, String>();
            (String Name, String Query)[] queries = repository.Elements("query").Select(query =>
                (Name: query.Attribute("name")?.Value, Query: query.Attribute("value")?.Value)).ToArray();

            if (queries.Any(q => q.Name == null))
                throw new ApplicationException("In the SQL query source file, the repository " +
                                               $"{repositoryName} contains queries without an attribute Name");
            return queries.Aggregate(new Dictionary<String, String>(), FeedQueryDictionary);
        }

        private static String LoadConnectionString()
        {
            ConnectionStringSettings connectionSettings = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionSettings == null)
                throw new ApplicationException("Default connection string not found in current application");

            return connectionSettings.ConnectionString;
        }

        private static Dictionary<String, String> FeedQueryDictionary(
            Dictionary<String, String> queriesDictionary,
            (String Name, String Query) queryTuple)
        {
            queriesDictionary.Add(queryTuple.Name, queryTuple.Query);
            return queriesDictionary;
        }

        #endregion
    }
}