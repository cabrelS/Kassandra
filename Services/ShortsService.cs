using Kassandra.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kassandra.Services;

public class ShortsService
{
    private readonly IMongoCollection<Short> _shortsCollection;

    public ShortsService(IOptions<KassandraDatabaseSettings> kassandraDatabaseSettings)
    {
        var mongoClient = new MongoClient(kassandraDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(kassandraDatabaseSettings.Value.DatabaseName);

        _shortsCollection = mongoDatabase.GetCollection<Short>(kassandraDatabaseSettings.Value.ShortsCollectionName);
    }

    public async Task<List<Short>> GetAsync() =>
        await _shortsCollection.Find(_ => true).ToListAsync();
    
    public async Task<Short?> GetAsync(string id) =>
        await _shortsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    

}