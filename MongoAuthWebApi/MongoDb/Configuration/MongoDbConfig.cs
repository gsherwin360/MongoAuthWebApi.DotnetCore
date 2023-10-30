namespace MongoAuthWebApi.MongoDb.Configuration;

public class MongoDbConfig
{
    public static readonly string ConfigSection = "MongoDb";

    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }
}
