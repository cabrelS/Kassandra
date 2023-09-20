using Kassandra.Models;
using Kassandra.Services;
using Microsoft.AspNetCore.Mvc;
namespace Kassandra.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortsController : ControllerBase
{
    private readonly ShortsService _shortService;

    public ShortsController(ShortsService shortsService) =>
        _shortService = shortsService;
    
    // [HttpGet]
    // public async Task<List<Short>> Get() => await _shortService.GetAsync();

    [HttpGet]
    public async Task<ActionResult<Short>> Get()
    {
        var max = await _shortService.CountAsync();
        Random random = new Random();
        int rand = random.Next(0, (int)max - 1);
        var _short = await _shortService.GetAsync(rand);
        
        if (_short is null)
        {
            return NotFound();
        }
        return _short;
    }
    
    [HttpGet("{key}")]
    public async Task<ActionResult<Short>> Get(string key)
    {
        var result = await _shortService.GetAsync(key);
        if (result is null)
        {
            return NotFound();
        }
        int count = result.Count();
        if (count > 1)
        {
            Random rand = new Random();
            int it = rand.Next(0,count);
            return result[it];
        }
        else
        {
            return result[0];
        }
    }
    
    
}