using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;

namespace StartMenu.Model
{
    internal class Database
    {
       private ConfigurationLib.Configuration _config;
        private SqliteConnection _connection;

        public Database(ConfigurationLib.Configuration config)
        {
            _config = config;
            _connection = new SqliteConnection(_config.GetValueOrThrow<string>("sqlite-connection"));
        }
    }
}
