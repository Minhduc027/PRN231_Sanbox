namespace DEMO_ASP_.NET_CORE_Web_API.Data

{
    using StackExchange.Redis;
    public class RedisCacheManager
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public RedisCacheManager(string connectionString, string instanceName)
        {
            var configurationOptions = ConfigurationOptions.Parse(connectionString);
            configurationOptions.ClientName = instanceName;

            _redisConnection = ConnectionMultiplexer.Connect(configurationOptions);
        }

        public IDatabase GetDatabase()
        {
            return _redisConnection.GetDatabase();
        }
    }
}
