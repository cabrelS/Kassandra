using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kassandra.Models;

public class Keys
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("key")]
    public string Key { get; set; } = null!;
    
    [BsonElement("docs")]
    // [BsonRepresentation(BsonType.Array)]
    public string[]? Docs { get; set; }
}