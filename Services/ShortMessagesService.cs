using Kassandra.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Kassandra.Services;

public class ShortMessagesService
{
    private readonly ShortsService _shortService;
    
    public ShortMessagesService (ShortsService shortService) 
    {
         _shortService = shortService;
    }

    public async Task<ShortMessage?> GetAny(string _key = "any")
    {
        var _short = await _shortService.GetAsync(_key);
        if (_short is null)
        {
            return null;
        }
        else
        {
            var shortMessages = _short.ShortM;
            if (shortMessages.Count() > 1)
            {
                Random rand = new Random();
                ShortMessage shortMessage = new()
                {
                    Key = string.Join(',',_short.Key),
                    Message = shortMessages.ElementAt(rand.Next(0,(int)shortMessages.Count()))
                };
                return shortMessage;
            }
            else
            {
                ShortMessage shortMessage = new()
                {
                    Key = string.Join(',',_short.Key),
                    Message = shortMessages.ElementAt(0)
                };
                return shortMessage;
            }
        }
    }
}
