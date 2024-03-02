using Npgsql;
using System.Data;

namespace DiplomaAPI.Helpers
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
