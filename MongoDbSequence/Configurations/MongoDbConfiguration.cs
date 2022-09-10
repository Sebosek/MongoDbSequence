namespace MongoDbSequence.Configurations;

public class MongoDbConfiguration
{
    public string ConnectionString { get; init; } = "mongodb://root:lok@dockerhost"; // "mongodb://localhost:27017"

    public string Database { get; init; } = "Sequences";
}