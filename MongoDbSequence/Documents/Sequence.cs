using MongoDB.Bson;

namespace MongoDbSequence.Documents;

public class Sequence
{
    public Sequence(string name)
    {
        Name = name;
    }
    
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public string Name { get; init; }

    public long Value { get; set; } = 1;
}