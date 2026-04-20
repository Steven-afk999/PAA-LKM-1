using Npgsql;
using System.Data;

namespace PAA_LKM_1.Data
{
    public class ConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
    }
}