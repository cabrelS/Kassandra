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
    
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Short>> Get(string id)
    {
        var _short = await _shortService.GetAsync(id);

        if (_short is null)
        {
            return NotFound();
        }
        return _short;
    }
    
    
}