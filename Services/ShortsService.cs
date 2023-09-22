using Kassandra.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kassandra.Services;

public class ShortsService
{
    private readonly IMongoCollection<Short> _shortsCollection;
    private readonly KeysService _keyService;

    public ShortsService(IOptions<KassandraDatabaseSettings> kassandraDatabaseSettings, KeysService keysService)
    {
        var mongoClient = new MongoClient(kassandraDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(kassandraDatabaseSettings.Value.DatabaseName);

        _shortsCollection = mongoDatabase.GetCollection<Short>(kassandraDatabaseSettings.Value.ShortsCollectionName);

        _keyService = keysService; 
    }

    public async Task<List<Short>> GetAsync() => await _shortsCollection.Find(_ => true).ToListAsync();
    public async Task<Short> GetAsync(string id) => await _shortsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    public async Task<Short> GetAnyAsync()
    {
        var filter = Builders<Short>.Filter.Empty;
        var _short = new Short();
        var key = await _keyService.GetAnyAsync();
        if (key is null) { return null!;}
        else{
            string[]? shortsId = key.Docs;
            if (shortsId?.Count() > 1)
            {
                Random rand = new Random();
                _short = GetAsync(shortsId?.ElementAt(rand.Next(0, shortsId?.Count() ?? 2)) ?? "6507585a2cadab045f83ecab").Result;
            }
            else
            {
                _short = GetAsync(shortsId?.ElementAt(0) ?? "6507585a2cadab045f83ecab").Result;
            }
            return _short;
        }
    }
}