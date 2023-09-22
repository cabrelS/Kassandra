using System.Linq.Expressions;
using Kassandra.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kassandra.Services;

public class KeysService
{
    private readonly IMongoCollection<Keys> _keysCollection;

    public KeysService(IOptions<KassandraDatabaseSettings> kassandraDatabaseSettings)
    {
        var mongoClient = new MongoClient(kassandraDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(kassandraDatabaseSettings.Value.DatabaseName);
        _keysCollection = mongoDatabase.GetCollection<Keys>(kassandraDatabaseSettings.Value.KeyCollectionName);
    }

    public async Task<List<Keys>> GetAsync() => await _keysCollection.Find(_ => true).ToListAsync();

    public async Task<Keys?> GetAsync(string id) => await _keysCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<Keys> GetAnyAsync()
    {
        var filter = Builders<Keys>.Filter.Empty;
        var count = await _keysCollection.Find(filter).CountDocumentsAsync();
        var docs = await _keysCollection.Find(filter).ToListAsync();
        Random rand = new Random();
        if (count > 0) {
            var result = docs.ElementAt(rand.Next(0,(int)count));
            return result;
        }
        else{
            return null!;
        }
        
    }

}
