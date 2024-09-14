namespace NomuBackend.Settings
{
    public interface IMongoDbSettings
    {
        string? Name { get; init; }
        string? Host { get; init; }
        int Port { get; init; }
        string? DatabaseName { get; set; }
        string ConnectionString { get; }
    }
}
