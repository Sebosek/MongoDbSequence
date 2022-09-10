using MongoDB.Driver;
using MongoDbSequence.Configurations;
using MongoDbSequence.Documents;

namespace MongoDbSequence.Repositories;

public class SequencesRepository
{
    private const string NAME = nameof(Sequence);

    private readonly IMongoDatabase database;

    public SequencesRepository(
        MongoDbConfiguration configuration,
        IMongoClient mongoClient)
    {
        database = mongoClient.GetDatabase(configuration.Database);
    }

    public async Task<long> FetchAndAddAsync(string key, CancellationToken token)
    {
        var collection = database.GetCollection<Sequence>(NAME);
        
        var filter = Builders<Sequence>.Filter.Eq(f => f.Name, key);
        var update = Builders<Sequence>.Update.Inc(f => f.Value, 1);

        var result = await collection.FindOneAndUpdateAsync(filter, update, cancellationToken: token);
        return result.Value;
    }
}