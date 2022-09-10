using MongoDB.Driver;
using MongoDBMigrations;
using MongoDbSequence.Documents;
using MongoDbSequence.Extensions;

using Version = MongoDBMigrations.Version;

namespace MongoDbSequence.Migrations;

public class InitSequences : IMigration
{
    private const string NAME = nameof(Sequence);
    
    public Version Version => new(1, 0, 0);

    public string Name => nameof(InitSequences);
    
    public void Up(IMongoDatabase database)
    {
        var collection = database.GetCollection<Sequence>(NAME);
        var unique = new CreateIndexModel<Sequence>(
            new IndexKeysDefinitionBuilder<Sequence>().Ascending(s => s.Name),
            new CreateIndexOptions { Unique = true });
        var hashed = new CreateIndexModel<Sequence>(
            new IndexKeysDefinitionBuilder<Sequence>().Hashed(s => s.Name));

        collection.Indexes.CreateOne(unique);
        collection.Indexes.CreateOne(hashed);
        collection.InsertMany(new []
        {
            new Sequence(SequencesRepositoryExtensions.INVOICES_KEY),
            new Sequence(SequencesRepositoryExtensions.USERS_KEY),
        });
    }

    public void Down(IMongoDatabase database)
    {
        database.DropCollection(NAME);
    }
}