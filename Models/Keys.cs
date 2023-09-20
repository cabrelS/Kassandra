using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kassandra.Keys;

public class Keys
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("key")]
    public string Key { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string[] Docs { get; set; } = null!;
}