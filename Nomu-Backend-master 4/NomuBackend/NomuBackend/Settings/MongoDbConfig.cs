namespace NomuBackend.Settings
{
    public class MongoDbConfig : IMongoDbSettings
    {
        public string? Name { get; init; }
        public string? Host { get; init; }
        public int Port { get; init; }
        public string? DatabaseName { get; set; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
