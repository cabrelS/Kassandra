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
    public async Task<Short> GetAsync(string _key = "any")
    {
        var key = await _keyService.GetAsync(_key);
        if (key is null) { return null!;}
        else{
            var ids = key.Docs;
            Short _short;
            if (ids?.Count() > 1)
            {
                Random rand = new Random();
                _short = GetShortAsync(ids?.ElementAt(rand.Next(0, ids?.Count() ?? 2)) ?? "6507585a2cadab045f83ecab").Result;
            }
            else
            {
                _short = GetShortAsync(ids?.ElementAt(0) ?? "6507585a2cadab045f83ecab").Result;
            }
            return _short;
        }
    }
    public async Task<Short> GetShortAsync(string id) => await _shortsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}